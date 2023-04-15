using System.Text;

namespace Mattodev.ToTaL
{
	public class ToTaLAssembler
	{
		public static List<byte> assemble(List<string> lns)
		{
			List<byte> bc = new();
			foreach (string l in lns)
			{
				string[] ln = l.Replace("\t", "").Split(' ');

				if (ln.Length > 0)
				{
					string[] ebic = ln.Length > 1 ? string.Join(' ', ln[1..]).Split(',') : Array.Empty<string>();
					#region first byte
					bc.Add(ln[0].ToLower() switch
					{
						"sta" => 0x10,
						"stb" => 0x11,
						"stc" => 0x12,
						"stx" => 0x13,
						"sty" => 0x14,
						"stz" => 0x15,
						"mov" => 0x1f,
						"lda" => 0x20,
						"ldb" => 0x21,
						"ldc" => 0x22,
						"ldx" => 0x23,
						"ldy" => 0x24,
						"ldz" => 0x25,
						"cpy" => 0x2f,
						"srd" => 0x30,
						"swt" => 0x31,
						"srm" => 0x32,
						"swm" => 0x33,
						"spt" => 0x34,
						"spi" => 0x35,
						"spd" => 0x36,
						"saa" => 0x40,
						"sab" => 0x41,
						"sac" => 0x42,
						"sax" => 0x43,
						"say" => 0x44,
						"saz" => 0x45,
						"laa" => 0x46,
						"lab" => 0x47,
						"lac" => 0x48,
						"lax" => 0x49,
						"lay" => 0x4a,
						"laz" => 0x4b,
						"inc" => 0x50,
						"dec" => 0x51,
						"add" => 0x52,
						"sub" => 0x53,
						"mlt" => 0x54,
						"div" => 0x55,
						_ => 0x00
					});
					#endregion
					switch (ln[0].ToLower())
					{
						case "sta":
						case "stb":
						case "stc":
						case "stx":
						case "sty":
						case "stz":
							bc.Add(Convert.ToByte(ebic[0], 16));
							break;
						case "mov":
							int addr = Convert.ToInt32(ebic[0], 16);
							bc.AddRange(new List<byte>()
							{
								(byte)(addr & 0xFF),
								(byte)((addr >> 8) & 0xFF),
								(byte)((addr >> 16) & 0xFF),
								Convert.ToByte(ebic[1], 16),
							});
							break;
						case "lda":
						case "ldb":
						case "ldc":
						case "ldx":
						case "ldy":
						case "ldz":
						case "srd":
							addr = Convert.ToInt32(ebic[0], 16);
							bc.AddRange(new List<byte>()
							{
								(byte)(addr & 0xFF),
								(byte)((addr >> 8) & 0xFF),
								(byte)((addr >> 16) & 0xFF),
							});
							break;
						case "cpy":
							addr = Convert.ToInt32(ebic[0], 16);
							int addr2 = Convert.ToInt32(ebic[1], 16);
							bc.AddRange(new List<byte>()
							{
								(byte)(addr & 0xFF),
								(byte)((addr >> 8) & 0xFF),
								(byte)((addr >> 16) & 0xFF),
								(byte)(addr2 & 0xFF),
								(byte)((addr2 >> 8) & 0xFF),
								(byte)((addr2 >> 16) & 0xFF),
							});
							break;
						case "swt":
							bc.Add(Convert.ToByte(ebic[0], 16));
							break;
						case "swm":
							bc.AddRange(Encoding.Unicode.GetBytes(string.Join(',', ebic[0..])));
							bc.Add(0x00);
							break;
						case "spt":
							addr = Convert.ToInt16(ebic[0], 16);
							bc.AddRange(new List<byte>()
							{
								(byte)(addr & 0xFF),
								(byte)((addr >> 8) & 0xFF),
							});
							break;
						case "srm":
						case "add":
						case "sub":
						case "mlt":
						case "div":
							bc.Add(Convert.ToByte(ebic[0], 16));
							break;
					}
				}
			}
			return bc;
		}
	}
}