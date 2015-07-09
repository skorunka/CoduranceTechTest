namespace UI.Console
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using StructureMap;
	using StructureMap.Graph;

	using UI.Console.Code;
	using UI.Console.Code.Commands;

	public class Program
	{
		private static void Main(string[] args)
		{
			using (var container = ConfigureDependencies())
			{
				var app = container.GetInstance<IApp>();

				while (true)
				{
					var input = Console.ReadLine();
					if (string.IsNullOrWhiteSpace(input))
					{
						break;
					}

					//// Output the command resuls
					var result = app.ProcessInput(input);
					foreach (var text in result)
					{
						Console.WriteLine(text);
					}
				}
			}
		}

		private static IContainer ConfigureDependencies()
		{
			return new Container(x =>
				   {
					   x.Scan(s =>
					   {
						   s.TheCallingAssembly();
						   s.AddAllTypesOf<ICommand>();
						   s.WithDefaultConventions();
					   });
					   x.For<ICollection<ICommand>>().Use(c => c.GetAllInstances<ICommand>().ToList());
					   x.For<IInputParser>().Use<StringInputParser>();
				   });
		}
	}
}
