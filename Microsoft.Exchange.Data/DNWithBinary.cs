using System;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000138 RID: 312
	[Serializable]
	public class DNWithBinary
	{
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x000215C4 File Offset: 0x0001F7C4
		// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x000215CC File Offset: 0x0001F7CC
		public string DistinguishedName
		{
			get
			{
				return this.dn;
			}
			set
			{
				this.dn = value;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x000215D5 File Offset: 0x0001F7D5
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x000215DD File Offset: 0x0001F7DD
		public byte[] Binary
		{
			get
			{
				return this.binary;
			}
			set
			{
				this.binary = value;
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000215E6 File Offset: 0x0001F7E6
		private DNWithBinary()
		{
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000215EE File Offset: 0x0001F7EE
		public DNWithBinary(string dn, byte[] binary)
		{
			this.dn = dn;
			this.binary = binary;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00021604 File Offset: 0x0001F804
		public static DNWithBinary Parse(string expression)
		{
			DNWithBinary result = null;
			if (!DNWithBinary.TryParse(expression, out result))
			{
				throw new FormatException(DataStrings.DNWithBinaryFormatError(expression));
			}
			return result;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00021630 File Offset: 0x0001F830
		public static bool TryParse(string expression, out DNWithBinary dnWithBinary)
		{
			dnWithBinary = null;
			int num = -1;
			int i = num + 1;
			num = expression.IndexOf(':', i);
			if (num != 1 || expression[0] != 'B')
			{
				return false;
			}
			i = num + 1;
			num = expression.IndexOf(':', i);
			if (num == -1)
			{
				return false;
			}
			int num2 = 0;
			while (i < num)
			{
				char c = expression[i];
				if (c < '0' || c > '9')
				{
					return false;
				}
				num2 = 10 * num2 + (int)(c - '0');
				i++;
			}
			if ((num2 & 1) != 0)
			{
				return false;
			}
			i = num + 1;
			num = expression.IndexOf(':', i);
			if (num == -1 || num2 != num - i)
			{
				return false;
			}
			byte[] array = new byte[num2 / 2];
			int num3 = 0;
			while (i + 1 < num)
			{
				byte b = 0;
				char c2 = expression[i];
				if (c2 >= '0' && c2 <= '9')
				{
					b |= (byte)(c2 - '0');
				}
				else if (c2 >= 'A' && c2 <= 'F')
				{
					b |= (byte)(c2 - 'A' + '\n');
				}
				else
				{
					if (c2 < 'a' || c2 > 'f')
					{
						return false;
					}
					b |= (byte)(c2 - 'a' + '\n');
				}
				b = (byte)(b << 4);
				c2 = expression[i + 1];
				if (c2 >= '0' && c2 <= '9')
				{
					b |= (byte)(c2 - '0');
				}
				else if (c2 >= 'A' && c2 <= 'F')
				{
					b |= (byte)(c2 - 'A' + '\n');
				}
				else
				{
					if (c2 < 'a' || c2 > 'f')
					{
						return false;
					}
					b |= (byte)(c2 - 'a' + '\n');
				}
				array[num3] = b;
				num3++;
				i += 2;
			}
			i = num + 1;
			if (string.CompareOrdinal(expression, i, "<GUID=", 0, "<GUID=".Length) == 0)
			{
				int num4 = expression.IndexOf(';', i);
				if (num4 < 0)
				{
					return false;
				}
				i = num4 + 1;
				if (string.CompareOrdinal(expression, i, "<SID=", 0, "<SID=".Length) == 0)
				{
					num4 = expression.IndexOf(';', i);
					if (num4 < 0)
					{
						return false;
					}
					i = num4 + 1;
				}
			}
			string text = expression.Substring(i);
			dnWithBinary = new DNWithBinary(text, array);
			return true;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00021830 File Offset: 0x0001FA30
		public override string ToString()
		{
			string arg = HexConverter.ByteArrayToHexString(this.binary);
			return string.Format("B:{0}:{1}:{2}", this.binary.Length * 2, arg, this.dn);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002186C File Offset: 0x0001FA6C
		public static bool operator ==(DNWithBinary value1, DNWithBinary value2)
		{
			if (value1 == value2)
			{
				return true;
			}
			if (value1 == null || value2 == null)
			{
				return false;
			}
			if (!value1.dn.Equals(value2.dn, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (value1.binary.Length != value2.binary.Length)
			{
				return false;
			}
			for (int i = 0; i < value1.binary.Length; i++)
			{
				if (value1.binary[i] != value2.binary[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000218D9 File Offset: 0x0001FAD9
		public static bool operator !=(DNWithBinary value1, DNWithBinary value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000218E5 File Offset: 0x0001FAE5
		public override int GetHashCode()
		{
			return this.dn.GetHashCode() ^ this.binary.GetHashCode();
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00021900 File Offset: 0x0001FB00
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			DNWithBinary dnwithBinary = obj as DNWithBinary;
			return !(dnwithBinary == null) && this.Equals(dnwithBinary);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002192B File Offset: 0x0001FB2B
		public bool Equals(DNWithBinary other)
		{
			return this == other;
		}

		// Token: 0x0400068E RID: 1678
		private const string GuidDNPrefix = "<GUID=";

		// Token: 0x0400068F RID: 1679
		private const string SidDNPrefix = "<SID=";

		// Token: 0x04000690 RID: 1680
		private string dn;

		// Token: 0x04000691 RID: 1681
		private byte[] binary;
	}
}
