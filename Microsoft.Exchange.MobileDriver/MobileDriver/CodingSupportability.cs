using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000017 RID: 23
	internal class CodingSupportability
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00003D10 File Offset: 0x00001F10
		public CodingSupportability(CodingScheme codingScheme, int radixPerPart, int radixPerSegment)
		{
			if (codingScheme == CodingScheme.Neutral)
			{
				throw new ArgumentOutOfRangeException("codingScheme");
			}
			if (0 >= radixPerPart)
			{
				throw new ArgumentOutOfRangeException("radixPerPart");
			}
			if (0 >= radixPerSegment)
			{
				throw new ArgumentOutOfRangeException("radixPerSegment");
			}
			this.CodingScheme = codingScheme;
			this.RadixPerSegment = radixPerSegment;
			this.RadixPerPart = radixPerPart;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003D64 File Offset: 0x00001F64
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003D6C File Offset: 0x00001F6C
		public CodingScheme CodingScheme { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003D75 File Offset: 0x00001F75
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003D7D File Offset: 0x00001F7D
		public int RadixPerSegment { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003D86 File Offset: 0x00001F86
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003D8E File Offset: 0x00001F8E
		public int RadixPerPart { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003D97 File Offset: 0x00001F97
		public CodingSchemeInfo CodingSchemeInfo
		{
			get
			{
				if (this.codingSchemeInfo == null)
				{
					this.codingSchemeInfo = CodingSchemeInfo.GetCodingSchemeInfo(this.CodingScheme);
				}
				return this.codingSchemeInfo;
			}
		}

		// Token: 0x04000030 RID: 48
		private CodingSchemeInfo codingSchemeInfo;
	}
}
