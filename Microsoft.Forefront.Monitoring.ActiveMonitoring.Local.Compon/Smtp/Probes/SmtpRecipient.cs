using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000259 RID: 601
	public class SmtpRecipient
	{
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x0003B834 File Offset: 0x00039A34
		// (set) Token: 0x0600143A RID: 5178 RVA: 0x0003B83C File Offset: 0x00039A3C
		public string Username
		{
			get
			{
				return this.username;
			}
			internal set
			{
				this.username = value;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x0003B845 File Offset: 0x00039A45
		// (set) Token: 0x0600143C RID: 5180 RVA: 0x0003B84D File Offset: 0x00039A4D
		public SmtpExpectedResponse ExpectedResponse
		{
			get
			{
				return this.expectedResponse;
			}
			internal set
			{
				this.expectedResponse = value;
			}
		}

		// Token: 0x040009BE RID: 2494
		private string username;

		// Token: 0x040009BF RID: 2495
		private SmtpExpectedResponse expectedResponse;
	}
}
