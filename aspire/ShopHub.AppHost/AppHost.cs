var builder = DistributedApplication.CreateBuilder(args);

// ── Infrastructure ────────────────────────────────────────
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume("shophub-postgres-data")
    .WithHostPort(5432);

var db = postgres.AddDatabase("shophub");

var redis = builder.AddRedis("redis")
    .WithRedisInsight()
    .WithDataVolume("shophub-redis-data");

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin()
    .WithDataVolume("shophub-rabbitmq-data");

// ── Migration ─────────────────────────────────────────────
var migration = builder.AddProject<Projects.ShopHub_Migration>("migration")
    .WithReference(db)
    .WaitFor(db);

// ── API ───────────────────────────────────────────────────
builder.AddProject<Projects.ShopHub_Api>("api")
    .WithReference(db)
    .WithReference(redis)
    .WithReference(rabbitmq)
    .WaitForCompletion(migration);

builder.Build().Run();