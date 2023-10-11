﻿using MagicOnion;
using MagicOnion.Server;
using MagicOnionApp.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace MagicOnionApp.Server
{

 

    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.ConfigureKestrel(options =>
            {
                // WORKAROUND: Accept HTTP/2 only to allow insecure HTTP/2 connections during development.
                options.ConfigureEndpointDefaults(endpointOptions =>
                {
                    endpointOptions.Protocols = HttpProtocols.Http2;
                });
            });
            builder.Services.AddGrpc();  // MagicOnion depends on ASP.NET Core gRPC service.
            builder.Services.AddMagicOnion();

            var app = builder.Build();
            app.MapMagicOnionService();

            app.Run();
        }
    }
}