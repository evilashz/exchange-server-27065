using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200007A RID: 122
	public class TraceTagInfo
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00009134 File Offset: 0x00007334
		public string PrettyName
		{
			get
			{
				return this.prettyName;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000913C File Offset: 0x0000733C
		public int NumericValue
		{
			get
			{
				return this.numericValue;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00009144 File Offset: 0x00007344
		public TraceTagInfo(int numericValue, string prettyName)
		{
			this.numericValue = numericValue;
			this.prettyName = prettyName;
		}

		// Token: 0x04000280 RID: 640
		private string prettyName;

		// Token: 0x04000281 RID: 641
		private int numericValue;
	}
}
