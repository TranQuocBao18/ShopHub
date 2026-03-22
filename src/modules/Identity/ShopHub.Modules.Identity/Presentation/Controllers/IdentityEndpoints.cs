using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopHub.Modules.Identity.Application.Commands.Login;
using ShopHub.Modules.Identity.Application.Commands.Logout;
using ShopHub.Modules.Identity.Application.Commands.RefreshToken;
using ShopHub.Modules.Identity.Application.Commands.Register;
using ShopHub.Modules.Identity.Domain.Exceptions;
using ShopHub.Modules.Identity.Infrastructure.Services;

namespace ShopHub.Modules.Identity.Presentation.Controllers;

public static class IdentityEndpoints
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/identity")
            .WithTags("Identity");

        group.MapPost("/register", async (
            [FromBody] RegisterRequest request,
            ISender sender,
            HttpContext httpContext,
            CancellationToken ct) =>
        {
            try
            {
                var ip = IpAddressHelper.GetClientIpAddress(httpContext);
                var command = new RegisterCommand(request.Email, request.Password, request.FullName, ip);
                var result = await sender.Send(command, ct);
                return Results.Ok(result.Value);
            }
            catch (UserAlreadyExistsException ex)
            {
                return Results.Conflict(new { ex.Code, ex.Message });
            }
        })
        .WithName("Register")
        .WithSummary("Register a new user")
        .AllowAnonymous();

        group.MapPost("/login", async (
            [FromBody] LoginRequest request,
            ISender sender,
            HttpContext httpContext,
            CancellationToken ct) =>
        {
            try
            {
                var ip = IpAddressHelper.GetClientIpAddress(httpContext);
                var command = new LoginCommand(request.Email, request.Password, ip);
                var result = await sender.Send(command, ct);
                return Results.Ok(result.Value);
            }
            catch (InvalidCredentialsException ex)
            {
                return Results.Unauthorized();
            }
        })
        .WithName("Login")
        .WithSummary("Login with email and password")
        .AllowAnonymous();

        group.MapPost("/refresh-token", async (
            [FromBody] RefreshTokenCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            try
            {
                var result = await sender.Send(command, ct);
                return Results.Ok(result.Value);
            }
            catch (InvalidCredentialsException)
            {
                return Results.Unauthorized();
            }
        })
        .WithName("RefreshToken")
        .WithSummary("Refresh access token")
        .AllowAnonymous();

        group.MapPost("/logout", async (
            [FromBody] LogoutCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            try
            {
                await sender.Send(command, ct);
                return Results.NoContent();
            }
            catch (InvalidCredentialsException)
            {
                return Results.Unauthorized();
            }
        })
        .WithName("Logout")
        .WithSummary("Revoke refresh token")
        .RequireAuthorization();

        return app;
    }
}

public sealed record RegisterRequest(string FullName, string Email, string Password);
public sealed record LoginRequest(string Email, string Password);
public sealed record RefreshTokenRequest(string Token);