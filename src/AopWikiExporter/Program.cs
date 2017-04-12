using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using CommandLineParser.Arguments;
using CommandLineParser.Exceptions;

namespace AopWikiExporter
{
    class Program
    {
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        static int Main(string[] args)
        {
            try
            {
                var parser = new CommandLineParser.CommandLineParser();
                var connectionStringArgument = new ValueArgument<string>('c', "connectionString", "MySQL connection string")
                {
                    Optional = false
                };
                var outputFileArgument = new ValueArgument<string>(
                    'o',
                    "outputFile",
                    "Write XML to file path. If missing, will write to stdout");
                var showUsageArgument = new SwitchArgument('h', "help", "Shows this usage message", false);
                parser.Arguments.Add(connectionStringArgument);
                parser.Arguments.Add(outputFileArgument);
                parser.Arguments.Add(showUsageArgument);

                parser.ParseCommandLine(args);

                if (!parser.ParsingSucceeded)
                {
                    return -1;
                }

                using (var output = GetOutputStream(outputFileArgument.Value))
                {
                    var exporter = new XmlExport(connectionStringArgument.Value);
                    exporter.WriteToOutput(output);
                }

                return 0;
            }
            catch (CommandLineException ex)
            {
                Console.Error.WriteLine($"Command line error; run with /? to see help: {ex.Message}");
                return -1;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unhandled exception:{Environment.NewLine}{ex}");
                return -2;
            }
        }

        static Stream GetOutputStream(string outputFilePath)
        {
            if (string.IsNullOrWhiteSpace(outputFilePath))
            {
                return Console.OpenStandardOutput();
            }

            var fileStream = File.OpenWrite(outputFilePath);
            var fileExtension = Path.GetExtension(outputFilePath);

            if (fileExtension == ".gz")
            {
                return new GZipStream(fileStream, CompressionMode.Compress, false);
            }

            if (fileExtension == ".zip")
            {
                var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create, false);
                return zipArchive
                    .CreateEntry(
                        Path.ChangeExtension(Path.GetFileName(outputFilePath), ".xml"),
                        CompressionLevel.Optimal)
                    .Open();
            }

            return fileStream;
        }
    }
}
