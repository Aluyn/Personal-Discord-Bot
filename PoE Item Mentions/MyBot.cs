using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PoE_Item_Mentions
{
    class MyBot
    {
        DiscordClient discord;

        public MyBot()
        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            /*
            discord.UsingCommands(x =>
            {
                x.PrefixChar = '!';
            });

            var commands = discord.GetService<CommandService>();

            commands.CreateCommand("test")
                .Parameter("P1", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage($"test command {e.GetArg("P1").Replace(" ", "_")}");
                });

            commands.CreateCommand("i")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("http://pathofexile.gamepedia.com/" + "something");
                });

            commands.CreateCommand("[")
                .Parameter("UniqueName", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage($"http://pathofexile.gamepedia.com/?search={e.GetArg("UniqueName").Replace(" ", "_")}");
                });

            //discord.MessageReceived += async (s, e) =>
            //{
            //    if (!e.Message.IsAuthor && e.Message.RawText.Contains("[]"))
            //        await e.Channel.SendMessage("[][][]");
            //};

            // discord.MessageReceived += async (s, e) =>
            // {
            //     if(!e.Message.IsAuthor && e.Message.RawText.Contains("["))
            //         await e.Channel.SendMessage(Regex.Match(e.Message.RawText, @"\(([^\)]*)\)").Value);
            // };
            */

            discord.MessageReceived += async (s, e) =>
            {
                if (!e.Message.IsAuthor && e.Message.RawText.Contains("["))
                {
                    string input = e.Message.RawText;
                    string pattern = @"(?<=\[)(.*?)(?=\])";
                    Match output = Regex.Match(input, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    //await e.Channel.SendMessage(output.ToString());
                    while (output.Success)
                    {
                        await e.Channel.SendMessage($"http://pathofexile.gamepedia.com/?search=" + output.ToString().Replace(" ", "_"));
                        output = output.NextMatch();
                    }

                }
            };

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("Mjg2NjY1NjgwNDU0MTU2Mjg5.C5kBzA.YFO8PmgdeVRn4TzaMClN3cOADKo", TokenType.Bot);
            });


        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
