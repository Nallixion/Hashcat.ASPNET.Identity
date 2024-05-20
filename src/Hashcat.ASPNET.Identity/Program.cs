﻿// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;

using Hashcat.ASPNET.Identity;
using NetDevPack.Utilities;
using AspNetIdentityHashInfo = Hashcat.ASPNET.Identity.AspNetIdentityHashInfo;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.Hosting.CommandLine;

[Command(Name = "di", Description = "Dependency Injection sample project")]
class Program {
    static Task<int> Main(string[] args)
        => new HostBuilder()
        .RunCommandLineApplicationAsync<Program>(args);

    [Option("-P|--PORT", Description = "your desired PORT")]
    public int Port { get; } = 8080;

    private IHostEnvironment _env;

    public Program(IHostEnvironment env) {
        _env = env;
    }

    private void OnExecute() {
        var text = new WenceyWang.FIGlet.AsciiArt("ASP2hashcat");
        Console.WriteLine(text);

        var hashDemoV3 = "AQAAAAEAACcQAAAAEG7xx8smhzcYFaAhPSRj1rgxfAoqKBv4WM/4R+Z0SvFxtxuMkfgBS28p1MQzvV0OeQ==";
        ProcessHash(hashDemoV3);

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }

    private static void ProcessHash(string hashDemoV3) {
        var hashDemoBase64Decoded = hashDemoV3.FromBase64();
        var hex = BitConverter.ToString(hashDemoBase64Decoded).Replace("-", "").ToLower();

        Console.WriteLine($"Demo Hash: {hex}");

        var asphash = new AspNetIdentityHashInfo(hashDemoV3);
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(asphash.ShaType);
        Console.ResetColor();
        Console.Write(":");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write(asphash.IterCount);
        Console.ResetColor();
        Console.Write(":");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.Write(asphash.Salt);
        Console.ResetColor();
        Console.Write(":");
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine(asphash.SubKey);
        Console.ResetColor();

        //hashDemoV3
        //sha256:10000:bvHHyyaHNxgVoCE9JGPWuA==:MXwKKigb+FjP+EfmdErxcbcbjJH4AUtvKdTEM71dDnk=

        //hashDemoV2
        //sha1: 1000:p + Lo3nM95k + NIGjObsG3xjM =:M6jP1qYKAFONHGzEyG9sub//Q88Y8UjsIDUCzRImGnY=
    }
}