using System;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000312 RID: 786
	public class CrossValidationResult
	{
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x00075131 File Offset: 0x00073331
		public double SuccessRate
		{
			get
			{
				return this.successRate;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x00075139 File Offset: 0x00073339
		public double BlankRate
		{
			get
			{
				return this.blankRate;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x00075141 File Offset: 0x00073341
		public double FailureRate
		{
			get
			{
				return this.failureRate;
			}
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00075149 File Offset: 0x00073349
		public CrossValidationResult(double success, double blank, double failure)
		{
			this.successRate = success;
			this.blankRate = blank;
			this.failureRate = failure;
		}

		// Token: 0x04000B80 RID: 2944
		private readonly double successRate;

		// Token: 0x04000B81 RID: 2945
		private readonly double blankRate;

		// Token: 0x04000B82 RID: 2946
		private readonly double failureRate;
	}
}
