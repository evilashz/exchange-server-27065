using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000012 RID: 18
	public class PopImapServiceHealth
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000537D File Offset: 0x0000357D
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00005385 File Offset: 0x00003585
		public string ServerName { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000538E File Offset: 0x0000358E
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00005396 File Offset: 0x00003596
		public long NumberOfRequests { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000539F File Offset: 0x0000359F
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000053A7 File Offset: 0x000035A7
		public int NumberOfErroredRequests { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000053B0 File Offset: 0x000035B0
		// (set) Token: 0x06000128 RID: 296 RVA: 0x000053B8 File Offset: 0x000035B8
		public List<ErrorDetail> ErrorDetails
		{
			get
			{
				return this.errorDetails;
			}
			set
			{
				this.errorDetails = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000053C1 File Offset: 0x000035C1
		// (set) Token: 0x0600012A RID: 298 RVA: 0x000053C9 File Offset: 0x000035C9
		public double OKResponseRatio { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000053D2 File Offset: 0x000035D2
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000053DA File Offset: 0x000035DA
		public double AverageRequestTime { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000053E3 File Offset: 0x000035E3
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000053EB File Offset: 0x000035EB
		public double AverageRpcLatency { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000053F4 File Offset: 0x000035F4
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000053FC File Offset: 0x000035FC
		public double AverageLdapLatency { get; set; }

		// Token: 0x04000087 RID: 135
		private List<ErrorDetail> errorDetails = new List<ErrorDetail>();
	}
}
