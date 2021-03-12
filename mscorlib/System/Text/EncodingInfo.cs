using System;

namespace System.Text
{
	// Token: 0x02000A47 RID: 2631
	[Serializable]
	public sealed class EncodingInfo
	{
		// Token: 0x0600675D RID: 26461 RVA: 0x0015BFD2 File Offset: 0x0015A1D2
		internal EncodingInfo(int codePage, string name, string displayName)
		{
			this.iCodePage = codePage;
			this.strEncodingName = name;
			this.strDisplayName = displayName;
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x0600675E RID: 26462 RVA: 0x0015BFEF File Offset: 0x0015A1EF
		public int CodePage
		{
			get
			{
				return this.iCodePage;
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x0600675F RID: 26463 RVA: 0x0015BFF7 File Offset: 0x0015A1F7
		public string Name
		{
			get
			{
				return this.strEncodingName;
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06006760 RID: 26464 RVA: 0x0015BFFF File Offset: 0x0015A1FF
		public string DisplayName
		{
			get
			{
				return this.strDisplayName;
			}
		}

		// Token: 0x06006761 RID: 26465 RVA: 0x0015C007 File Offset: 0x0015A207
		public Encoding GetEncoding()
		{
			return Encoding.GetEncoding(this.iCodePage);
		}

		// Token: 0x06006762 RID: 26466 RVA: 0x0015C014 File Offset: 0x0015A214
		public override bool Equals(object value)
		{
			EncodingInfo encodingInfo = value as EncodingInfo;
			return encodingInfo != null && this.CodePage == encodingInfo.CodePage;
		}

		// Token: 0x06006763 RID: 26467 RVA: 0x0015C03B File Offset: 0x0015A23B
		public override int GetHashCode()
		{
			return this.CodePage;
		}

		// Token: 0x04002DFA RID: 11770
		private int iCodePage;

		// Token: 0x04002DFB RID: 11771
		private string strEncodingName;

		// Token: 0x04002DFC RID: 11772
		private string strDisplayName;
	}
}
