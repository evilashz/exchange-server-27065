using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003FF RID: 1023
	public class ClientStatistics
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x0009EDC4 File Offset: 0x0009CFC4
		// (set) Token: 0x06001D01 RID: 7425 RVA: 0x0009EDCC File Offset: 0x0009CFCC
		public string MessageId { get; set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x0009EDD5 File Offset: 0x0009CFD5
		// (set) Token: 0x06001D03 RID: 7427 RVA: 0x0009EDDD File Offset: 0x0009CFDD
		public DateTime RequestTime { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x0009EDE6 File Offset: 0x0009CFE6
		// (set) Token: 0x06001D05 RID: 7429 RVA: 0x0009EDEE File Offset: 0x0009CFEE
		public int ResponseTime { get; set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x0009EDF7 File Offset: 0x0009CFF7
		// (set) Token: 0x06001D07 RID: 7431 RVA: 0x0009EDFF File Offset: 0x0009CFFF
		public int ResponseSize { get; set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x0009EE08 File Offset: 0x0009D008
		// (set) Token: 0x06001D09 RID: 7433 RVA: 0x0009EE10 File Offset: 0x0009D010
		public int HttpResponseCode { get; set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0009EE19 File Offset: 0x0009D019
		// (set) Token: 0x06001D0B RID: 7435 RVA: 0x0009EE21 File Offset: 0x0009D021
		public int[] ErrorCode { get; set; }

		// Token: 0x06001D0C RID: 7436 RVA: 0x0009EE2A File Offset: 0x0009D02A
		public ClientStatistics()
		{
			this.MessageId = null;
			this.RequestTime = DateTime.MinValue;
			this.ResponseSize = -1;
			this.ResponseTime = -1;
			this.HttpResponseCode = -1;
			this.ErrorCode = null;
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0009EE60 File Offset: 0x0009D060
		public bool IsValid()
		{
			return !string.IsNullOrEmpty(this.MessageId) && this.RequestTime != DateTime.MinValue && this.ResponseTime > 0 && this.HttpResponseCode > 0;
		}
	}
}
