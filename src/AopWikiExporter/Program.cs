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
                    Optional = false,
                    ValueOptional = false,
                    Example = "Host=localhost; Database=aop_xml; User=root; Password=password"
                };
                var outputFileArgument = new ValueArgument<string>(
                    'o',
                    "outputFile",
                    "XML output path. Using .gz or .zip extension will produce a compressed output. If omitted, will write to stdout.")
                {
                    ValueOptional = false
                };
                var excludeUnreferencedElementsArgument = new SwitchArgument(
                    'e',
                    "exclude-unreferenced-elements",
                    "Exclude unreferenced elements from output (to reduce file size)",
                    false);
                var targetAopId = new ValueArgument<int>(
                    'a',
                    "aop-id",
                    $"Export aop having passed id. Implies {excludeUnreferencedElementsArgument.LongName}")
                {
                    Optional = true,
                    ValueOptional = false
                };
                var targetKeyEventId = new ValueArgument<int>(
                    'k',
                    "key-event-id",
                    $"Export key event having passed id. Implies {excludeUnreferencedElementsArgument.LongName}.")
                {
                    Optional = true,
                    ValueOptional = false
                };
                var help = new SwitchArgument('h', "help", "Show this help.", false);

                parser.Arguments.Add(connectionStringArgument);
                parser.Arguments.Add(outputFileArgument);
                parser.Arguments.Add(excludeUnreferencedElementsArgument);
                parser.Arguments.Add(targetAopId);
                parser.Arguments.Add(targetKeyEventId);
                parser.Arguments.Add(help);

                parser.ParseCommandLine(args);

                if (!parser.ParsingSucceeded)
                {
                    return -1;
                }

                if (help.Value)
                {
                    parser.ShowUsage();
                    return -1;
                }

                TargetType? targetType = null;
                int? targetId = null;
                if (targetAopId.Value != 0 && targetKeyEventId.Value != 0)
                {
                    throw new CommandLineException(
                        $"You cannot specify both {targetAopId.LongName} and {targetKeyEventId.LongName}");
                }

                if (targetAopId.Value != 0)
                {
                    targetType = TargetType.Aop;
                    targetId = targetAopId.Value;
                }
                else if (targetKeyEventId.Value != 0)
                {
                    targetType = TargetType.KeyEvent;
                    targetId = targetKeyEventId.Value;
                }

                using (var output = GetOutputStream(outputFileArgument.Value))
                {
                    var exporter = new XmlExport(
                        connectionStringArgument.Value,
                        excludeUnreferencedElementsArgument.Value,
                        targetId,
                        targetType);
                    exporter.WriteToOutput(output);
                }

                return 0;
            }
            catch (CommandLineException ex)
            {
                Console.Error.WriteLine($"Command line error; run with --help to see help: {ex.Message}");
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

            var fileStream = File.Open(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.Read);
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
