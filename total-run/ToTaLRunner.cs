namespace Mattodev.ToTaL
{
	public class ToTaLRunner
	{
		public static void exec(ref ToTaLIOStream stream, bool bufferToStdout = false)
		{
			int increment = 1;
			for (int i = 0; i < stream.src.Count; i += increment)
			{
				//Console.WriteLine($"{Convert.ToString(stream.src[i], 16)} OK ({Convert.ToString(i, 16)})");
				switch (stream.src[i])
				{
					case 0x10: // sta
						stream.regs['a'] = stream.src[i + 1];
						increment = 2;
						break;
					case 0x11: // stb
						stream.regs['b'] = stream.src[i + 1];
						increment = 2;
						break;
					case 0x12: // stc
						stream.regs['c'] = stream.src[i + 1];
						increment = 2;
						break;
					case 0x13: // stx
						stream.regs['x'] = stream.src[i + 1];
						increment = 2;
						break;
					case 0x14: // sty
						stream.regs['y'] = stream.src[i + 1];
						increment = 2;
						break;
					case 0x15: // stz
						stream.regs['z'] = stream.src[i + 1];
						increment = 2;
						break;
					case 0x1f: // mov
						stream.mem[ToTaLIOStream.joinBytes(stream.src[i + 1], stream.src[i + 2], stream.src[i + 3])] = stream.src[i + 4];
						increment = 5;
						break;

					case 0x20: // lda
						stream.mem[ToTaLIOStream.joinBytes(stream.src[i + 1], stream.src[i + 2], stream.src[i + 3])] = stream.regs['a'];
						increment = 4;
						break;
					case 0x21: // ldb
						stream.mem[ToTaLIOStream.joinBytes(stream.src[i + 1], stream.src[i + 2], stream.src[i + 3])] = stream.regs['b'];
						increment = 4;
						break;
					case 0x22: // ldc
						stream.mem[ToTaLIOStream.joinBytes(stream.src[i + 1], stream.src[i + 2], stream.src[i + 3])] = stream.regs['c'];
						increment = 4;
						break;
					case 0x23: // ldx
						stream.mem[ToTaLIOStream.joinBytes(stream.src[i + 1], stream.src[i + 2], stream.src[i + 3])] = stream.regs['x'];
						increment = 4;
						break;
					case 0x24: // ldy
						stream.mem[ToTaLIOStream.joinBytes(stream.src[i + 1], stream.src[i + 2], stream.src[i + 3])] = stream.regs['y'];
						increment = 4;
						break;
					case 0x25: // ldz
						stream.mem[ToTaLIOStream.joinBytes(stream.src[i + 1], stream.src[i + 2], stream.src[i + 3])] = stream.regs['z'];
						increment = 4;
						break;

					case 0x30: // srd
						stream.mem[ToTaLIOStream.joinBytes(stream.src[i + 1], stream.src[i + 2], stream.src[i + 3])] = stream.bufferGet();
						increment = 4;
						break;
					case 0x31: // swt
						if (!bufferToStdout) stream.bufferSet(stream.src[i + 1]);
						else Console.Write((char)stream.src[i + 1]);
						increment = 2;
						break;
					case 0x32: // srm
						increment = stream.src[i + 4] + 4;
						int thing = ToTaLIOStream.joinBytes(stream.src[i + 1], stream.src[i + 2], stream.src[i + 3]);
						for (int j = 0; j < stream.src[i + 4]; j++)
						{
							stream.mem[thing + j] = stream.bufferGet();
							stream.cursor++;
						}
						break;
					case 0x33: // swm
						int inx = 1;
						byte bufferbyte;
						while (i + inx < stream.src.Count && (bufferbyte = stream.src[i + inx]) != 0x00)
						{
							if (!bufferToStdout)
							{
								stream.bufferSet(bufferbyte);
								stream.cursor++;
							}
							else Console.Write((char)bufferbyte);
							inx++;
						}
						increment = ++inx; // inx is gonna get discarded anyway, all hail code simplifaction!
						break;
					case 0x34: // spt
						stream.cursor = (ushort)ToTaLIOStream.joinBytes(stream.src[i + 1], stream.src[i + 2]);
						increment = 3;
						break;
					case 0x35: // spi
						stream.cursor++;
						increment = 1;
						break;
					case 0x36: // spd
						stream.cursor--;
						increment = 1;
						break;

					case 0x40: // saa
						stream.mainReg = 'a';
						increment = 1;
						break;
					case 0x41: // sab
						stream.mainReg = 'b';
						increment = 1;
						break;
					case 0x42: // sac
						stream.mainReg = 'c';
						increment = 1;
						break;
					case 0x43: // sax
						stream.mainReg = 'x';
						increment = 1;
						break;
					case 0x44: // say
						stream.mainReg = 'y';
						increment = 1;
						break;
					case 0x45: // saz
						stream.mainReg = 'z';
						increment = 1;
						break;
					case 0x46: // laa
						stream.regs['a'] = stream.regs[stream.mainReg];
						increment = 1;
						break;
					case 0x47: // lab
						stream.regs['b'] = stream.regs[stream.mainReg];
						increment = 1;
						break;
					case 0x48: // lac
						stream.regs['c'] = stream.regs[stream.mainReg];
						increment = 1;
						break;
					case 0x49: // lax
						stream.regs['x'] = stream.regs[stream.mainReg];
						increment = 1;
						break;
					case 0x4a: // lay
						stream.regs['y'] = stream.regs[stream.mainReg];
						increment = 1;
						break;
					case 0x4b: // laz
						stream.regs['z'] = stream.regs[stream.mainReg];
						increment = 1;
						break;

					case 0x50: // inc
						stream.regs[stream.mainReg]++;
						increment = 1;
						break;
					case 0x51: // dec
						stream.regs[stream.mainReg]--;
						increment = 1;
						break;
					case 0x52: // add
						stream.regs[stream.mainReg] += stream.src[i + 1];
						increment = 2;
						break;
					case 0x53: // sub
						stream.regs[stream.mainReg] -= stream.src[i + 1];
						increment = 2;
						break;
					case 0x54: // mlt
						stream.regs[stream.mainReg] *= stream.src[i + 1];
						increment = 2;
						break;
					case 0x55: // div
						stream.regs[stream.mainReg] /= stream.src[i + 1];
						increment = 2;
						break;
				}
			}
		}
	}

	public class ToTaLIOStream
	{
		public List<byte> buffer = new(0x10000);
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
		public char mainReg = 'a';
		public ushort cursor = 0;
		public ToTaLIOStream()
		{
			for (int i = 0; i < mem.Capacity; i++) mem.Add(0);
			for (int i = 0; i < buffer.Capacity; i++) buffer.Add(0);
		}
		public void append(byte b) => buffer.Add(b);

		public static int joinBytes(int byte1, int byte2)
			=> (byte1) | (byte2 << 8);
		public static int joinBytes(int byte1, int byte2, int byte3)
			=> (byte1) | (byte2 << 8) | (byte3 << 16);
		public static int joinBytes(int byte1, int byte2, int byte3, int byte4)
			=> (byte1) | (byte2 << 8) | (byte3 << 16) | (byte4 << 24);

		public byte bufferGet()
		{
			try
			{
				return buffer[cursor];
			}
			catch (IndexOutOfRangeException)
			{
				return 0;
			}
		}
		public void bufferSet(byte b)
		{
			try
			{
				buffer[cursor] = b;
			}
			catch (IndexOutOfRangeException) { } // not the best way to do it but eh
		}
	}
}