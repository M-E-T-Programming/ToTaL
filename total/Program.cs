namespace Mattodev.ToTaL.MainProgram
{
	internal class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("No file specified to execute!");
				return;
			}
			ToTaLIOStream stream = new()
			{
				src = File.ReadAllBytes(args[0]).ToList()
			};
			ToTaLRunner.exec(ref stream);

			Console.Write("Display stdio buffer (Y/N)?");
			var displayBuffer = Console.ReadKey();
			Console.WriteLine();

			if (displayBuffer.Key == ConsoleKey.Y)
			{
				stream.cursor = 0;
				byte bufferByte;
				while ((bufferByte = stream.bufferGet()) != 0x00 && stream.cursor < stream.buffer.Count)
				{
					Console.Write((char)bufferByte);
					stream.cursor++;
				}
			}
		}
	}
}