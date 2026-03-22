using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ── Aspire Service Defaults ───────────────────────────────
builder.AddServiceDefaults();

// ── OpenAPI ───────────────────────────────────────────────
builder.Services.AddOpenApi();

// ── Redis Distributed Cache ───────────────────────────────
builder.AddRedisDistributedCache("redis");

var app = builder.Build();

// ── Middleware ────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// ── Health checks ─────────────────────────────────────────
app.MapDefaultEndpoints();

app.Run();