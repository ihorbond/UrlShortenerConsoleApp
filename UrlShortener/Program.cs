// See https://aka.ms/new-console-template for more information
using CommandLine;

using UrlShortener.Data;
using UrlShortener.DTOs;
using UrlShortener.Enums;
using UrlShortener.Models;
using shortid;
using shortid.Configuration;
using UrlShortener;

Parser.Default.ParseArguments<CliOptions>(args)
       .WithParsed(o =>
       {
           Console.WriteLine("List of commands");

           Console.WriteLine($"Type '{Enum.GetName(Commands.Get)}' to get the long version of shortened url");
           Console.WriteLine($"Type '{Enum.GetName(Commands.Create)}' to enter a long url to be shortened");
           Console.WriteLine($"Type '{Enum.GetName(Commands.Delete)}' to enter a short url to be removed");
           Console.WriteLine($"Type '{Enum.GetName(Commands.Stats)}' to enter a short url and see # of clicks");
           Console.WriteLine($"Type '{Enum.GetName(Commands.Custom)}' to enter a custom ID for a short version");
           Console.WriteLine($"Type '{Enum.GetName(Commands.Exit)}' to exit the application");

           Console.WriteLine("\n");
       });

const string PREFIX = "bit.ly";
Commands parsedCommand;

AppDbContext ctx = new();

do
{
    Console.WriteLine("Enter command");
    string command = Console.ReadLine() ?? "";

    bool success = Enum.TryParse(value: command, ignoreCase: true, out parsedCommand);
    if(!success)
    {
        Console.WriteLine("Invalid Command");
    } else
    {
        switch(parsedCommand)
        {
            case Commands.Get:
                {
                    Console.WriteLine("Enter short URL");
                    string? url = Console.ReadLine()?.FormatAsShortUrl();

                    var result = ctx.Urls.FirstOrDefault(x => x.ShortUrl == url);

                    if (result is null)
                    {
                        Console.WriteLine("Match not found.");
                    }
                    else
                    {
                        result.TimesAccessed += 1;
                        ctx.Urls.Update(result);
                        ctx.SaveChanges();

                        Console.WriteLine(result.LongUrl);
                    }

                    break;
                }
            case Commands.Create:
                {
                    Console.WriteLine("Enter URL to shorten");
                    string? url = Console.ReadLine();

                    var options = new GenerationOptions(useNumbers: true, useSpecialCharacters: false, length: 8);
                    string id = ShortId.Generate(options);
                    string shortUrl = $"{PREFIX}/{id}";

                    ctx.Urls.Add(new Url
                    {
                        LongUrl = url,
                        ShortUrl = shortUrl,
                        TimesAccessed = 0
                    });

                    ctx.SaveChanges();

                    Console.WriteLine($"Success! Your url is: {shortUrl} \n");

                    break;
                }
            case Commands.Delete:
                {
                    Console.WriteLine("Enter short URL to delete:");
                    string? url = Console.ReadLine()?.FormatAsShortUrl();

                    var result = ctx.Urls.FirstOrDefault(x => x.ShortUrl == url);

                    if(result is null)
                    {
                        Console.WriteLine("Match not found.");
                    } 
                    else
                    {
                        ctx.Urls.Remove(result);
                        ctx.SaveChanges();
                        Console.WriteLine("Successfully deleted!");
                    }
                    break;
                }
            case Commands.Stats:
                {
                    Console.WriteLine("Enter short url to see stats");
                    string? url = Console.ReadLine()?.FormatAsShortUrl();

                    var result = ctx.Urls.FirstOrDefault(x => x.ShortUrl == url);
                    if (result is null)
                    {
                        Console.WriteLine("Match not found.");
                    }
                    else
                    {
                        Console.WriteLine($"{url} has been accessed {result.TimesAccessed} times.");
                    }


                    break;
                }
            case Commands.Custom:
                {
                    Console.WriteLine("Enter URL to shorten");
                    string? url = Console.ReadLine();

                    Console.WriteLine("Enter custom identifier");
                    string? id = Console.ReadLine();

                    string shortUrl = $"{PREFIX}/{id}";

                    var result = ctx.Urls.FirstOrDefault(x => x.ShortUrl == shortUrl);
                    if(result is not null)
                    {
                        Console.WriteLine($"Provided ID '{id}' is already in use. Please use a different ID.");
                    }
                    else
                    {
                        ctx.Urls.Add(new Url
                        {
                            LongUrl = url,
                            ShortUrl = shortUrl,
                            TimesAccessed = 0
                        });

                        ctx.SaveChanges();

                        Console.WriteLine($"Success! Your url: {shortUrl} \n");
                    }

                    break;
                }
            case Commands.Exit:
                {
                    Console.WriteLine("Good bye");
                    break;
                }
            default:
                {
                    Console.WriteLine("Invalid command");
                    break;
                }
        }
    }

} while (parsedCommand != Commands.Exit);