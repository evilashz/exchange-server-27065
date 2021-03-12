using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000140 RID: 320
	internal static class GuidFactory
	{
		// Token: 0x06000DAE RID: 3502 RVA: 0x0003E570 File Offset: 0x0003C770
		internal static Guid Parse(string guidString, int offset)
		{
			if (guidString == null)
			{
				throw new ArgumentNullException("guidString");
			}
			if (guidString.Length < offset + 36)
			{
				throw new ArgumentException("Too close to the end to point to a valid Guid string.", "offset");
			}
			foreach (int num in GuidFactory.dashPositions)
			{
				int num2 = offset + num;
				if (guidString[num2] != '-')
				{
					string message = string.Format("Expected dash at index {0}.", num2);
					throw new FormatException(message);
				}
			}
			int a = (int)GuidFactory.Read32(guidString, offset);
			short b = (short)GuidFactory.Read16(guidString, offset + 9);
			short c = (short)GuidFactory.Read16(guidString, offset + 14);
			byte d = GuidFactory.Read8(guidString, offset + 19);
			byte e = GuidFactory.Read8(guidString, offset + 21);
			byte f = GuidFactory.Read8(guidString, offset + 24);
			byte g = GuidFactory.Read8(guidString, offset + 26);
			byte h = GuidFactory.Read8(guidString, offset + 28);
			byte i2 = GuidFactory.Read8(guidString, offset + 30);
			byte j = GuidFactory.Read8(guidString, offset + 32);
			byte k = GuidFactory.Read8(guidString, offset + 34);
			return new Guid(a, b, c, d, e, f, g, h, i2, j, k);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003E68C File Offset: 0x0003C88C
		private static byte Read4(string data, int offset)
		{
			uint num = (uint)data[offset];
			byte b = byte.MaxValue;
			if (num < 104U)
			{
				b = GuidFactory.vals[(int)((UIntPtr)num)];
			}
			if (b == 255)
			{
				string message = string.Format("Expected a hexadecimal digit at index {0}.", offset);
				throw new FormatException(message);
			}
			return b;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0003E6D8 File Offset: 0x0003C8D8
		private static byte Read8(string data, int offset)
		{
			byte b = GuidFactory.Read4(data, offset);
			b = (byte)(b << 4);
			return b | GuidFactory.Read4(data, offset + 1);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0003E700 File Offset: 0x0003C900
		private static ushort Read16(string data, int offset)
		{
			ushort num = (ushort)GuidFactory.Read8(data, offset);
			num = (ushort)(num << 8);
			return num | (ushort)GuidFactory.Read8(data, offset + 2);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0003E728 File Offset: 0x0003C928
		private static uint Read32(string data, int offset)
		{
			uint num = (uint)GuidFactory.Read16(data, offset);
			num <<= 16;
			return num | (uint)GuidFactory.Read16(data, offset + 4);
		}

		// Token: 0x040006FF RID: 1791
		private static readonly byte[] vals = new byte[]
		{
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			10,
			11,
			12,
			13,
			14,
			15,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			10,
			11,
			12,
			13,
			14,
			15,
			byte.MaxValue
		};

		// Token: 0x04000700 RID: 1792
		private static readonly int[] dashPositions = new int[]
		{
			8,
			13,
			18,
			23
		};
	}
}
