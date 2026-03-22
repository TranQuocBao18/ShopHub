using Scalar.AspNetCore;
using ShopHub.Modules.Identity;

var builder = WebApplication.CreateBuilder(args);

// ── Aspire Service Defaults ───────────────────────────────
builder.AddServiceDefaults();

// ── OpenAPI ───────────────────────────────────────────────
builder.Services.AddOpenApi();

// ── Redis Distributed Cache ───────────────────────────────
builder.AddRedisDistributedCache("redis");

// ── Modules ───────────────────────────────────────────────
builder.Services.AddIdentityModule(builder.Configuration);

var app = builder.Build();

// ── Middleware ────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// ── Health checks ─────────────────────────────────────────
app.MapDefaultEndpoints();

// ── Modules ───────────────────────────────────────────────
app.UseIdentityModule();

app.Run();