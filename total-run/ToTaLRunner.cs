namespace Mattodev.ToTaL
{
	public class ToTaLRunner
	{
		public static void exec(ref ToTaLIOStream stream)
		{
			int increment = 1;
			for (int i = 0; i < stream.src.Count; i += increment)
			{
				switch (stream.src[i])
				{
					case 0x10:
						stream.regs['a'] = stream.src[i + 1];
						increment = 2;
						break;
					case 0x11:
						stream.regs['b'] = stream.src[i + 1];
						increment = 2;
						break;
					case 0x12:
						stream.regs['c'] = stream.src[i + 1];
						increment = 2;
						break;
					case 0x13:
						stream.regs['x'] = stream.src[i + 1];
						increment = 2;
						break;
					case 0x14:
						stream.regs['y'] = stream.src[i + 1];
						increment = 2;
						break;
					case 0x15:
						stream.regs['z'] = stream.src[i + 1];
						increment = 2;
						break;
				}
			}
		}
	}

	public class ToTaLIOStream
	{
		public List<byte> buffer = new();
		public List<byte> src = new();
		public List<byte> mem = new(0x1000000);
		public Dictionary<char, byte> regs = new()
		{
			{ 'a', 0x00 },
			{ 'b', 0x00 },
			{ 'c', 0x00 },
			{ 'x', 0x00 },
			{ 'y', 0x00 },
			{ 'z', 0x00 },
		};
		public ToTaLIOStream()
		{
			for (int i = 0; i < mem.Count; i++) mem[i] = 0;
		}
		public void append(byte b) => buffer.Add(b);
		public static int joinBytes(int byte1, int byte2)
			=> (byte1) | (byte2 << 8);
		public static int joinBytes(int byte1, int byte2, int byte3)
			=> (byte1) | (byte2 << 8) | (byte3 << 16);
		public static int joinBytes(int byte1, int byte2, int byte3, int byte4)
			=> (byte1) | (byte2 << 8) | (byte3 << 16) | (byte4 << 24);
	}
}