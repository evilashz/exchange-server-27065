using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000048 RID: 72
	public class LargeValue
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x0000CD74 File Offset: 0x0000AF74
		public LargeValue(long actualLength, byte[] truncatedValue)
		{
			this.ActualLength = actualLength;
			this.TruncatedValue = truncatedValue;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000CD8A File Offset: 0x0000AF8A
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public long ActualLength { get; private set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000CD9B File Offset: 0x0000AF9B
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x0000CDA3 File Offset: 0x0000AFA3
		public byte[] TruncatedValue { get; private set; }
	}
}
