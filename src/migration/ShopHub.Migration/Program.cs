using DbUp;
using DbUp.Engine;
using Microsoft.Extensions.Configuration;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("ShopHub Database Migration Tool");

    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

    var configuration = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false)
        .AddJsonFile($"appsettings.{env}.json", optional: true)
        .AddEnvironmentVariables()
        .Build();

    var connectionString = configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("ConnectionString 'DefaultConnection' is not configured.");

    var runSeedData = bool.TryParse(configuration["Migration:RunSeedData"], out var seed) && seed;

    Log.Information("Environment : {Env}", env);
    Log.Information("Seed Data   : {RunSeed}", runSeedData);
    Log.Information("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

    Log.Information("Connecting to PostgreSQL...");
    EnsureDatabase.For.PostgresqlDatabase(connectionString);
    Log.Information("Database ready.");
    Log.Information("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

    // ── Schema Migration ──────────────────────────────────
    Log.Information("Running schema migrations...");

    var schemaScripts = LoadScripts(isSchema: true);
    Log.Information("Scripts to execute ({Count}):", schemaScripts.Count);
    foreach (var s in schemaScripts)
        Log.Information("→ {Name}", s.Name);

    var migrationResult = RunScripts(connectionString, "migration_history", schemaScripts);
    if (!migrationResult.Successful)
    {
        Log.Error("Schema migration FAILED!");
        Log.Error("Script : {Script}", migrationResult.ErrorScript?.Name);
        Log.Error("Error  : {Error}", migrationResult.Error?.Message);
        return 1;
    }

    Log.Information("Schema migration completed successfully!");
    Log.Information("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

    // ── Seed Data ─────────────────────────────────────────
    if (runSeedData)
    {
        Log.Information("Running seed data...");

        var seedScripts = LoadScripts(isSchema: false);
        var seedResult = RunScripts(connectionString, "seed_history", seedScripts);

        if (!seedResult.Successful)
        {
            Log.Error("Seed data FAILED!");
            Log.Error("Script : {Script}", seedResult.ErrorScript?.Name);
            Log.Error("Error  : {Error}", seedResult.Error?.Message);
            return 2;
        }

        Log.Information("Seed data completed successfully!");
        Log.Information("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
    }

    Log.Information("All migrations completed successfully!");
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unexpected error during migration");
    return 99;
}
finally
{
    Log.CloseAndFlush();
}

static List<SqlScript> LoadScripts(bool isSchema)
{
    var assembly = typeof(Program).Assembly;
    var folderToken = isSchema ? ".Scripts." : ".SeedData.";

    return assembly
        .GetManifestResourceNames()
        .Where(name =>
            name.EndsWith(".sql", StringComparison.OrdinalIgnoreCase) &&
            name.Contains(folderToken, StringComparison.Ordinal) &&
            name.StartsWith("ShopHub.Migration.Scripts.", StringComparison.Ordinal))
        .OrderBy(name => name)
        .Select(name =>
        {
            using var stream = assembly.GetManifestResourceStream(name)!;
            using var reader = new StreamReader(stream);
            return new SqlScript(name, reader.ReadToEnd());
        })
        .ToList();
}

static DatabaseUpgradeResult RunScripts(
    string connectionString,
    string journalTable,
    List<SqlScript> scripts)
{
    if (scripts.Count == 0)
    {
        Log.Information("No scripts found.");
        return new DatabaseUpgradeResult([], true, null, null);
    }

    var upgrader = DeployChanges.To
        .PostgresqlDatabase(connectionString)
        .WithScripts(scripts)
        .WithTransactionPerScript()
        .WithVariablesDisabled()
        .LogToConsole()
        .JournalToPostgresqlTable("public", journalTable)
        .Build();

    var scriptsToRun = upgrader.GetScriptsToExecute();
    if (scriptsToRun.Count == 0)
    {
        Log.Information("No new scripts to run (all up to date).");
        return new DatabaseUpgradeResult([], true, null, null);
    }

    return upgrader.PerformUpgrade();
}