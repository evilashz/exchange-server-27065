using System;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Globalization.Iso2022Jp;

namespace Microsoft.Exchange.Data.Globalization
{
	// Token: 0x0200012A RID: 298
	internal class Iso2022JpEncoding : Encoding
	{
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0006A74D File Offset: 0x0006894D
		internal Iso2022DecodingMode KillSwitch
		{
			get
			{
				return Iso2022JpEncoding.InternalReadKillSwitch();
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0006A754 File Offset: 0x00068954
		public Iso2022JpEncoding(int codePage) : base(codePage)
		{
			if (codePage == 50220)
			{
				this.defaultEncoding = Encoding.GetEncoding(50220);
				return;
			}
			if (codePage == 50221)
			{
				this.defaultEncoding = Encoding.GetEncoding(50221);
				return;
			}
			if (codePage == 50222)
			{
				this.defaultEncoding = Encoding.GetEncoding(50222);
				return;
			}
			string paramName = string.Format("Iso2022JpEncoding does not support codepage {0}", codePage);
			throw new ArgumentException("codePage", paramName);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0006A7D0 File Offset: 0x000689D0
		internal static Iso2022DecodingMode InternalReadKillSwitch()
		{
			switch (RegistryConfigManager.Iso2022JpEncodingOverride)
			{
			default:
				return Iso2022DecodingMode.Default;
			case 1:
				return Iso2022DecodingMode.Override;
			case 2:
				return Iso2022DecodingMode.Throw;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0006A7FC File Offset: 0x000689FC
		public override int CodePage
		{
			get
			{
				return this.defaultEncoding.CodePage;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0006A809 File Offset: 0x00068A09
		public override string BodyName
		{
			get
			{
				return this.defaultEncoding.BodyName;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0006A816 File Offset: 0x00068A16
		public override string EncodingName
		{
			get
			{
				return this.defaultEncoding.EncodingName;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0006A823 File Offset: 0x00068A23
		public override string HeaderName
		{
			get
			{
				return this.defaultEncoding.HeaderName;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0006A830 File Offset: 0x00068A30
		public override string WebName
		{
			get
			{
				return this.defaultEncoding.WebName;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0006A83D File Offset: 0x00068A3D
		public override int WindowsCodePage
		{
			get
			{
				return this.defaultEncoding.WindowsCodePage;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0006A84A File Offset: 0x00068A4A
		public override bool IsBrowserDisplay
		{
			get
			{
				return this.defaultEncoding.IsBrowserDisplay;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0006A857 File Offset: 0x00068A57
		public override bool IsBrowserSave
		{
			get
			{
				return this.defaultEncoding.IsBrowserSave;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0006A864 File Offset: 0x00068A64
		public override bool IsMailNewsDisplay
		{
			get
			{
				return this.defaultEncoding.IsMailNewsDisplay;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0006A871 File Offset: 0x00068A71
		public override bool IsMailNewsSave
		{
			get
			{
				return this.defaultEncoding.IsMailNewsSave;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0006A87E File Offset: 0x00068A7E
		public override bool IsSingleByte
		{
			get
			{
				return this.defaultEncoding.IsSingleByte;
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0006A88B File Offset: 0x00068A8B
		public override byte[] GetPreamble()
		{
			return this.defaultEncoding.GetPreamble();
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0006A898 File Offset: 0x00068A98
		public override int GetMaxByteCount(int charCount)
		{
			return this.defaultEncoding.GetMaxByteCount(charCount);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0006A8A8 File Offset: 0x00068AA8
		public override int GetMaxCharCount(int byteCount)
		{
			switch (this.KillSwitch)
			{
			case Iso2022DecodingMode.Default:
				return this.defaultEncoding.GetMaxCharCount(byteCount);
			case Iso2022DecodingMode.Override:
				return this.defaultEncoding.GetMaxCharCount(byteCount);
			case Iso2022DecodingMode.Throw:
				throw new NotImplementedException();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0006A8F5 File Offset: 0x00068AF5
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return this.defaultEncoding.GetByteCount(chars, index, count);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0006A905 File Offset: 0x00068B05
		public override int GetByteCount(string s)
		{
			return this.defaultEncoding.GetByteCount(s);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0006A913 File Offset: 0x00068B13
		public unsafe override int GetByteCount(char* chars, int count)
		{
			return this.defaultEncoding.GetByteCount(chars, count);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0006A922 File Offset: 0x00068B22
		public override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return this.defaultEncoding.GetBytes(s, charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0006A936 File Offset: 0x00068B36
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return this.defaultEncoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0006A94A File Offset: 0x00068B4A
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			return this.defaultEncoding.GetBytes(chars, charCount, bytes, byteCount);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0006A95C File Offset: 0x00068B5C
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			switch (this.KillSwitch)
			{
			case Iso2022DecodingMode.Default:
				return this.defaultEncoding.GetCharCount(bytes, index, count);
			case Iso2022DecodingMode.Override:
			{
				Decoder decoder = this.GetDecoder();
				return decoder.GetCharCount(bytes, index, count);
			}
			case Iso2022DecodingMode.Throw:
				throw new NotImplementedException();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0006A9B0 File Offset: 0x00068BB0
		public unsafe override int GetCharCount(byte* bytes, int count)
		{
			switch (this.KillSwitch)
			{
			case Iso2022DecodingMode.Default:
				return this.defaultEncoding.GetCharCount(bytes, count);
			case Iso2022DecodingMode.Override:
				throw new NotImplementedException();
			case Iso2022DecodingMode.Throw:
				throw new NotImplementedException();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0006A9F8 File Offset: 0x00068BF8
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			switch (this.KillSwitch)
			{
			case Iso2022DecodingMode.Default:
				return this.defaultEncoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
			case Iso2022DecodingMode.Override:
			{
				Decoder decoder = this.GetDecoder();
				return decoder.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
			}
			case Iso2022DecodingMode.Throw:
				throw new NotImplementedException();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0006AA54 File Offset: 0x00068C54
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			switch (this.KillSwitch)
			{
			case Iso2022DecodingMode.Default:
				return this.defaultEncoding.GetChars(bytes, byteCount, chars, charCount);
			case Iso2022DecodingMode.Override:
				throw new NotImplementedException();
			case Iso2022DecodingMode.Throw:
				throw new NotImplementedException();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0006AAA0 File Offset: 0x00068CA0
		public override string GetString(byte[] bytes, int index, int count)
		{
			switch (this.KillSwitch)
			{
			case Iso2022DecodingMode.Default:
				return this.defaultEncoding.GetString(bytes, index, count);
			case Iso2022DecodingMode.Override:
			{
				Decoder decoder = this.GetDecoder();
				char[] array = new char[this.GetMaxCharCount(count)];
				int chars = decoder.GetChars(bytes, index, count, array, 0);
				return new string(array, 0, chars);
			}
			case Iso2022DecodingMode.Throw:
				throw new NotImplementedException();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0006AB0C File Offset: 0x00068D0C
		public override Decoder GetDecoder()
		{
			switch (this.KillSwitch)
			{
			case Iso2022DecodingMode.Default:
			case Iso2022DecodingMode.Override:
				return new Iso2022JpDecoder(this);
			case Iso2022DecodingMode.Throw:
				throw new NotImplementedException();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0006AB46 File Offset: 0x00068D46
		public override Encoder GetEncoder()
		{
			return this.defaultEncoding.GetEncoder();
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0006AB54 File Offset: 0x00068D54
		public override object Clone()
		{
			return (Iso2022JpEncoding)base.MemberwiseClone();
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0006AB6E File Offset: 0x00068D6E
		internal Encoding DefaultEncoding
		{
			get
			{
				return this.defaultEncoding;
			}
		}

		// Token: 0x04000E8A RID: 3722
		private Encoding defaultEncoding;
	}
}
