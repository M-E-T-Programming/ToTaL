namespace Mattodev.ToTaL.MainProgram
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("The ToTaL VM, version 1.0.0");
			Console.ResetColor();
			if (args.Length == 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("No file specified to execute!");
				Console.ResetColor();
				return;
			}
			bool bufferToStdout = args.Contains("-bs") || args.Contains("--bufferToStdout");
			bool assemble = args.Contains("-a") || args.Contains("--asm");
			bool assemblyOutput = args.Contains("-ao") || args.Contains("--asmOutput");
			var bcode = File.ReadAllBytes(args[0]).ToList();
			var scode = File.ReadAllLines(args[0]).ToList();

			if (!assemblyOutput)
			{
				ToTaLIOStream stream = !assemble ? new() { src = bcode } : new() { src = ToTaLAssembler.assemble(scode) };
				ToTaLRunner.exec(ref stream, bufferToStdout);

				if (!bufferToStdout)
				{
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
			else
			{
				List<byte> code = ToTaLAssembler.assemble(scode);
				string hexedCode = "";
				code.ForEach(b => hexedCode += byteToHex(b) + " ");
				Console.WriteLine(hexedCode);
			}
		}
		public static string byteToHex(byte b)
		{
			return Convert.ToString(b, 16).PadLeft(2, '0').ToUpper();
		}
	}
}