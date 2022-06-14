using Microsoft.Extensions.CommandLineUtils;
using VacancyAggregator.Console.ConsoleCommands;
using System;

namespace VacancyAggregator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication(throwOnUnexpectedArg: false);

            app.HelpOption("-?|-h |--help");
            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 1;
            });

            DbMigrateCommand.Register(app);
            GetVacancyFiltersCommand.Register(app);
            CreateVacancyFilterCommand.Register(app);
            CreateDataSourceCommand.Register(app);
            GetVacanciesFromDataSourceCommand.Register(app);

            try
            {
                Environment.ExitCode = app.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                System.Console.WriteLine(ex.Message);
                app.ShowHelp();
                Environment.ExitCode = -1;
            }
            catch(Exception ex)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.ToString());
                System.Console.ForegroundColor = ConsoleColor.White;
                Environment.ExitCode = -1;
            }

            Environment.Exit(Environment.ExitCode);
        }
    }
}
