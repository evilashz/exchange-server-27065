using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Globalization
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	public class InvalidCharsetException : ExchangeDataException
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x000667E0 File Offset: 0x000649E0
		public InvalidCharsetException(int codePage) : base(GlobalizationStrings.InvalidCodePage(codePage))
		{
			this.codePage = codePage;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000667F5 File Offset: 0x000649F5
		public InvalidCharsetException(string charsetName) : base(GlobalizationStrings.InvalidCharset((charsetName == null) ? "<null>" : charsetName))
		{
			this.charsetName = charsetName;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00066814 File Offset: 0x00064A14
		public InvalidCharsetException(int codePage, string message) : base(message)
		{
			this.codePage = codePage;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00066824 File Offset: 0x00064A24
		public InvalidCharsetException(string charsetName, string message) : base(message)
		{
			this.charsetName = charsetName;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00066834 File Offset: 0x00064A34
		internal InvalidCharsetException(string charsetName, int codePage, string message) : base(message)
		{
			this.codePage = codePage;
			this.charsetName = charsetName;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0006684B File Offset: 0x00064A4B
		public InvalidCharsetException(int codePage, string message, Exception innerException) : base(message, innerException)
		{
			this.codePage = codePage;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0006685C File Offset: 0x00064A5C
		public InvalidCharsetException(string charsetName, string message, Exception innerException) : base(message, innerException)
		{
			this.charsetName = charsetName;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0006686D File Offset: 0x00064A6D
		protected InvalidCharsetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.codePage = info.GetInt32("codePage");
			this.charsetName = info.GetString("charsetName");
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00066899 File Offset: 0x00064A99
		public int CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x000668A1 File Offset: 0x00064AA1
		public string CharsetName
		{
			get
			{
				return this.charsetName;
			}
		}

		// Token: 0x04000DF2 RID: 3570
		private int codePage;

		// Token: 0x04000DF3 RID: 3571
		private string charsetName;
	}
}
