using System;
using System.Text;

namespace Microsoft.Exchange.Data.Globalization
{
	// Token: 0x0200011C RID: 284
	[Serializable]
	internal class RemapEncoding : Encoding
	{
		// Token: 0x06000B4F RID: 2895 RVA: 0x00068A98 File Offset: 0x00066C98
		public RemapEncoding(int codePage) : base(codePage)
		{
			if (codePage == 28591)
			{
				this.encodingEncoding = Encoding.GetEncoding(28591);
				this.decodingEncoding = Encoding.GetEncoding(1252);
				return;
			}
			if (codePage == 28599)
			{
				this.encodingEncoding = Encoding.GetEncoding(28599);
				this.decodingEncoding = Encoding.GetEncoding(1254);
				return;
			}
			throw new ArgumentException();
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00068B03 File Offset: 0x00066D03
		public override int CodePage
		{
			get
			{
				return this.encodingEncoding.CodePage;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00068B10 File Offset: 0x00066D10
		public override string BodyName
		{
			get
			{
				return this.encodingEncoding.BodyName;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00068B1D File Offset: 0x00066D1D
		public override string EncodingName
		{
			get
			{
				return this.encodingEncoding.EncodingName;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00068B2A File Offset: 0x00066D2A
		public override string HeaderName
		{
			get
			{
				return this.encodingEncoding.HeaderName;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00068B37 File Offset: 0x00066D37
		public override string WebName
		{
			get
			{
				return this.encodingEncoding.WebName;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00068B44 File Offset: 0x00066D44
		public override int WindowsCodePage
		{
			get
			{
				return this.encodingEncoding.WindowsCodePage;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00068B51 File Offset: 0x00066D51
		public override bool IsBrowserDisplay
		{
			get
			{
				return this.encodingEncoding.IsBrowserDisplay;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00068B5E File Offset: 0x00066D5E
		public override bool IsBrowserSave
		{
			get
			{
				return this.encodingEncoding.IsBrowserSave;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00068B6B File Offset: 0x00066D6B
		public override bool IsMailNewsDisplay
		{
			get
			{
				return this.encodingEncoding.IsMailNewsDisplay;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00068B78 File Offset: 0x00066D78
		public override bool IsMailNewsSave
		{
			get
			{
				return this.encodingEncoding.IsMailNewsSave;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00068B85 File Offset: 0x00066D85
		public override bool IsSingleByte
		{
			get
			{
				return this.encodingEncoding.IsSingleByte;
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00068B92 File Offset: 0x00066D92
		public override byte[] GetPreamble()
		{
			return this.encodingEncoding.GetPreamble();
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00068B9F File Offset: 0x00066D9F
		public override int GetMaxByteCount(int charCount)
		{
			return this.encodingEncoding.GetMaxByteCount(charCount);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00068BAD File Offset: 0x00066DAD
		public override int GetMaxCharCount(int byteCount)
		{
			return this.decodingEncoding.GetMaxCharCount(byteCount);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00068BBB File Offset: 0x00066DBB
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return this.encodingEncoding.GetByteCount(chars, index, count);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00068BCB File Offset: 0x00066DCB
		public override int GetByteCount(string s)
		{
			return this.encodingEncoding.GetByteCount(s);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00068BD9 File Offset: 0x00066DD9
		public unsafe override int GetByteCount(char* chars, int count)
		{
			return this.encodingEncoding.GetByteCount(chars, count);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00068BE8 File Offset: 0x00066DE8
		public override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return this.encodingEncoding.GetBytes(s, charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00068BFC File Offset: 0x00066DFC
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return this.encodingEncoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00068C10 File Offset: 0x00066E10
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			return this.encodingEncoding.GetBytes(chars, charCount, bytes, byteCount);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00068C22 File Offset: 0x00066E22
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.decodingEncoding.GetCharCount(bytes, index, count);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00068C32 File Offset: 0x00066E32
		public unsafe override int GetCharCount(byte* bytes, int count)
		{
			return this.decodingEncoding.GetCharCount(bytes, count);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00068C41 File Offset: 0x00066E41
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.decodingEncoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00068C55 File Offset: 0x00066E55
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			return this.decodingEncoding.GetChars(bytes, byteCount, chars, charCount);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00068C67 File Offset: 0x00066E67
		public override string GetString(byte[] bytes, int index, int count)
		{
			return this.decodingEncoding.GetString(bytes, index, count);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00068C77 File Offset: 0x00066E77
		public override Decoder GetDecoder()
		{
			return this.decodingEncoding.GetDecoder();
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00068C84 File Offset: 0x00066E84
		public override Encoder GetEncoder()
		{
			return this.encodingEncoding.GetEncoder();
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00068C94 File Offset: 0x00066E94
		public override object Clone()
		{
			return (Encoding)base.MemberwiseClone();
		}

		// Token: 0x04000E4B RID: 3659
		private Encoding encodingEncoding;

		// Token: 0x04000E4C RID: 3660
		private Encoding decodingEncoding;
	}
}
