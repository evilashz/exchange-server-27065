using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime.Encoders;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000075 RID: 117
	internal struct MimeString
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x000199C1 File Offset: 0x00017BC1
		public MimeString(byte[] data)
		{
			this = new MimeString(data, 0, data.Length);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000199CE File Offset: 0x00017BCE
		public MimeString(byte[] data, int offset, int count)
		{
			if ((long)data.Length > 268435455L)
			{
				throw new MimeException(Strings.ValueTooLong);
			}
			this.data = data;
			this.offset = offset;
			this.count = (uint)(count | -268435456);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00019A02 File Offset: 0x00017C02
		public MimeString(byte[] data, int offset, int count, uint mask)
		{
			if ((long)data.Length > 268435455L)
			{
				throw new MimeException(Strings.ValueTooLong);
			}
			this.data = data;
			this.offset = offset;
			this.count = (uint)(count | (int)mask);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00019A34 File Offset: 0x00017C34
		public MimeString(string data)
		{
			if ((long)data.Length > 268435455L)
			{
				throw new MimeException(Strings.ValueTooLong);
			}
			this.data = ByteString.StringToBytes(data, true);
			this.offset = 0;
			this.count = (uint)(this.data.Length | -268435456);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00019A83 File Offset: 0x00017C83
		internal MimeString(MimeString original, int offset, int count)
		{
			this.data = original.data;
			this.offset = offset;
			this.count = (uint)(count | -268435456);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00019AA6 File Offset: 0x00017CA6
		internal MimeString(MimeString original, int offset, int count, uint mask)
		{
			this.data = original.data;
			this.offset = offset;
			this.count = (uint)(count | (int)mask);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00019AC6 File Offset: 0x00017CC6
		internal MimeString(byte[] data, int offset, uint countAndMask)
		{
			this.data = data;
			this.offset = offset;
			this.count = countAndMask;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00019ADD File Offset: 0x00017CDD
		public int Length
		{
			get
			{
				return (int)(this.count & 268435455U);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00019AEB File Offset: 0x00017CEB
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00019AF3 File Offset: 0x00017CF3
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00019AFB File Offset: 0x00017CFB
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x00019B09 File Offset: 0x00017D09
		public uint Mask
		{
			get
			{
				return this.count & 4026531840U;
			}
			set
			{
				this.count = ((this.count & 268435455U) | value);
			}
		}

		// Token: 0x1700015D RID: 349
		public byte this[int index]
		{
			get
			{
				return this.data[this.offset + index];
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00019B30 File Offset: 0x00017D30
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00019B38 File Offset: 0x00017D38
		internal uint LengthAndMask
		{
			get
			{
				return this.count;
			}
			set
			{
				this.count = value;
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00019B44 File Offset: 0x00017D44
		public static bool IsPureASCII(string str)
		{
			bool result = true;
			if (!string.IsNullOrEmpty(str))
			{
				foreach (char c in str)
				{
					if (c >= '\u0080')
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00019B84 File Offset: 0x00017D84
		public static bool IsPureASCII(byte[] bytes)
		{
			bool result = true;
			if (bytes != null)
			{
				foreach (byte b in bytes)
				{
					if (b >= 128)
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00019BB8 File Offset: 0x00017DB8
		public static bool IsPureASCII(MimeString str)
		{
			bool result = true;
			for (int i = 0; i < str.Length; i++)
			{
				byte b = str[i];
				if (b >= 128)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00019BF0 File Offset: 0x00017DF0
		public static bool IsPureASCII(MimeStringList str)
		{
			bool result = true;
			for (int i = 0; i < str.Count; i++)
			{
				MimeString str2 = str[i];
				if (!MimeString.IsPureASCII(str2))
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00019C28 File Offset: 0x00017E28
		public static MimeString CopyData(byte[] data, int offset, int count)
		{
			byte[] newData = new byte[count];
			ByteEncoder.BlockCopy(data, offset, newData, 0, count);
			return new MimeString(newData, 0, count);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00019C4E File Offset: 0x00017E4E
		public override string ToString()
		{
			if (this.data != null)
			{
				return ByteString.BytesToString(this.data, this.offset, this.Length, true);
			}
			return string.Empty;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00019C76 File Offset: 0x00017E76
		public void TrimRight(int count)
		{
			this.count -= (uint)count;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00019C88 File Offset: 0x00017E88
		public byte[] GetSz()
		{
			if (this.data == null || (this.offset == 0 && this.Length == this.data.Length))
			{
				return this.data;
			}
			byte[] array = new byte[this.Length];
			this.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00019CD4 File Offset: 0x00017ED4
		public unsafe MimeString CopyData()
		{
			if (this.data == null)
			{
				return default(MimeString);
			}
			byte[] array = new byte[this.Length];
			fixed (byte* ptr = this.data, ptr2 = array)
			{
				byte* ptr3 = ptr + this.offset;
				byte* ptr4 = ptr + this.offset + this.Length;
				byte* ptr5 = ptr2;
				while (ptr3 != ptr4)
				{
					*(ptr5++) = *(ptr3++);
				}
			}
			return new MimeString(array, 0, array.Length, this.Mask);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00019D86 File Offset: 0x00017F86
		public int CopyTo(byte[] destination, int destinationIndex)
		{
			Buffer.BlockCopy(this.data, this.offset, destination, destinationIndex, this.Length);
			return this.Length;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00019DA7 File Offset: 0x00017FA7
		public void WriteTo(Stream stream)
		{
			stream.Write(this.data, this.offset, this.Length);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00019DC1 File Offset: 0x00017FC1
		internal uint ComputeCrcI()
		{
			return ByteString.ComputeCrcI(this.data, this.offset, this.Length);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00019DDA File Offset: 0x00017FDA
		internal bool CompareEqI(string str2)
		{
			return ByteString.EqualsI(str2, this.Data, this.Offset, this.Length, true);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00019DF5 File Offset: 0x00017FF5
		internal bool CompareEqI(byte[] str2)
		{
			return ByteString.EqualsI(this.Data, this.Offset, this.Length, str2, 0, str2.Length, true);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00019E14 File Offset: 0x00018014
		internal bool CompareEqI(MimeString str2)
		{
			return ByteString.EqualsI(this.Data, this.Offset, this.Length, str2.Data, str2.Offset, str2.Length, true);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00019E44 File Offset: 0x00018044
		internal bool HasPrefixEq(byte[] prefix, int start, int count)
		{
			if (count > this.Length)
			{
				return false;
			}
			int num = -1;
			while (++num < count)
			{
				if (this[num] != prefix[start + num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00019E7B File Offset: 0x0001807B
		internal bool HasPrefixEqI(byte[] prefix, int start, int count)
		{
			return count <= this.Length && ByteString.EqualsI(this.Data, this.Offset, count, prefix, start, count, true);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00019E9E File Offset: 0x0001809E
		internal byte[] GetData(out int offset, out int count)
		{
			offset = this.offset;
			count = this.Length;
			return this.data;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00019EB8 File Offset: 0x000180B8
		internal bool MergeIfAdjacent(MimeString refString)
		{
			if (this.data == refString.data && this.offset + this.Length == refString.offset && this.Mask == refString.Mask)
			{
				this.count += (uint)refString.Length;
				return true;
			}
			return false;
		}

		// Token: 0x0400034B RID: 843
		internal const string HdrReceived = "Received";

		// Token: 0x0400034C RID: 844
		internal const string HdrContentType = "Content-Type";

		// Token: 0x0400034D RID: 845
		internal const string HdrContentDisposition = "Content-Disposition";

		// Token: 0x0400034E RID: 846
		internal const string HrdDKIMSignature = "DKIM-Signature";

		// Token: 0x0400034F RID: 847
		internal const string HdrXConvertedToMime = "X-ConvertedToMime";

		// Token: 0x04000350 RID: 848
		internal const byte CARRIAGERETURN = 13;

		// Token: 0x04000351 RID: 849
		internal const byte LINEFEED = 10;

		// Token: 0x04000352 RID: 850
		internal const uint LINEFEEDMASK = 168430090U;

		// Token: 0x04000353 RID: 851
		internal const uint LengthMask = 268435455U;

		// Token: 0x04000354 RID: 852
		internal const uint MaskAny = 4026531840U;

		// Token: 0x04000355 RID: 853
		internal const bool AllowUTF8Value = true;

		// Token: 0x04000356 RID: 854
		internal static readonly byte[] EmptyByteArray = new byte[0];

		// Token: 0x04000357 RID: 855
		public static readonly MimeString Empty = new MimeString(MimeString.EmptyByteArray);

		// Token: 0x04000358 RID: 856
		internal static readonly byte[] CrLf = ByteString.StringToBytes("\r\n", true);

		// Token: 0x04000359 RID: 857
		internal static readonly byte[] TwoDashes = ByteString.StringToBytes("--", true);

		// Token: 0x0400035A RID: 858
		internal static readonly byte[] TwoDashesCRLF = ByteString.StringToBytes("--\r\n", true);

		// Token: 0x0400035B RID: 859
		internal static readonly byte[] CRLF2Dashes = ByteString.StringToBytes("\r\n--", true);

		// Token: 0x0400035C RID: 860
		internal static readonly byte[] XParameterNamePrefix = ByteString.StringToBytes("x-", true);

		// Token: 0x0400035D RID: 861
		internal static readonly byte[] Colon = ByteString.StringToBytes(":", true);

		// Token: 0x0400035E RID: 862
		internal static readonly byte[] Comma = ByteString.StringToBytes(",", true);

		// Token: 0x0400035F RID: 863
		internal static readonly byte[] Semicolon = ByteString.StringToBytes(";", true);

		// Token: 0x04000360 RID: 864
		internal static readonly byte[] Space = ByteString.StringToBytes(" ", true);

		// Token: 0x04000361 RID: 865
		internal static readonly byte[] LessThan = ByteString.StringToBytes("<", true);

		// Token: 0x04000362 RID: 866
		internal static readonly byte[] GreaterThan = ByteString.StringToBytes(">", true);

		// Token: 0x04000363 RID: 867
		internal static readonly byte[] DoubleQuote = ByteString.StringToBytes("\"", true);

		// Token: 0x04000364 RID: 868
		internal static readonly byte[] Tabulation = ByteString.StringToBytes("\t", true);

		// Token: 0x04000365 RID: 869
		internal static readonly byte[] Backslash = ByteString.StringToBytes("\\", true);

		// Token: 0x04000366 RID: 870
		internal static readonly byte[] Asterisk = ByteString.StringToBytes("*", true);

		// Token: 0x04000367 RID: 871
		internal static readonly byte[] EqualTo = ByteString.StringToBytes("=", true);

		// Token: 0x04000368 RID: 872
		internal static readonly byte[] CommentInvalidDate = ByteString.StringToBytes("(invalid)", true);

		// Token: 0x04000369 RID: 873
		internal static readonly byte[] Base64 = ByteString.StringToBytes("base64", true);

		// Token: 0x0400036A RID: 874
		internal static readonly byte[] QuotedPrintable = ByteString.StringToBytes("quoted-printable", true);

		// Token: 0x0400036B RID: 875
		internal static readonly byte[] XUuencode = ByteString.StringToBytes("x-uuencode", true);

		// Token: 0x0400036C RID: 876
		internal static readonly byte[] Uue = ByteString.StringToBytes("x-uue", true);

		// Token: 0x0400036D RID: 877
		internal static readonly byte[] Uuencode = ByteString.StringToBytes("uuencode", true);

		// Token: 0x0400036E RID: 878
		internal static readonly byte[] MacBinhex = ByteString.StringToBytes("mac-binhex40", true);

		// Token: 0x0400036F RID: 879
		internal static readonly byte[] Binary = ByteString.StringToBytes("binary", true);

		// Token: 0x04000370 RID: 880
		internal static readonly byte[] Encoding8Bit = ByteString.StringToBytes("8bit", true);

		// Token: 0x04000371 RID: 881
		internal static readonly byte[] Encoding7Bit = ByteString.StringToBytes("7bit", true);

		// Token: 0x04000372 RID: 882
		internal static readonly byte[] Version1 = ByteString.StringToBytes("1.0", true);

		// Token: 0x04000373 RID: 883
		internal static readonly byte[] MimeVersion = ByteString.StringToBytes("MIME-Version: 1.0\r\n", true);

		// Token: 0x04000374 RID: 884
		internal static readonly byte[] TextPlain = ByteString.StringToBytes("text/plain", true);

		// Token: 0x04000375 RID: 885
		internal static readonly byte[] ConvertedToMimeUU = new byte[]
		{
			49
		};

		// Token: 0x04000376 RID: 886
		private byte[] data;

		// Token: 0x04000377 RID: 887
		private int offset;

		// Token: 0x04000378 RID: 888
		private uint count;
	}
}
