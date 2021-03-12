using System;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Utility;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics
{
	// Token: 0x02000018 RID: 24
	internal class ExceptionHandlingOptions
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00004C90 File Offset: 0x00002E90
		public ExceptionHandlingOptions() : this(ExceptionHandlingOptions.minTimeSpan)
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004CA0 File Offset: 0x00002EA0
		public ExceptionHandlingOptions(TimeSpan operationDuration)
		{
			if (operationDuration < ExceptionHandlingOptions.minTimeSpan)
			{
				throw new ArgumentException("operationDuration");
			}
			this.OperationDuration = operationDuration;
			this.MaxRetries = ExceptionHandlingOptions.maxRetries;
			this.IsRetryEnabled = true;
			this.IsTimeoutEnabled = true;
			this.ClientId = "ExceptionHandler";
			this.Operation = "ExceptionHandling";
			this.CorrelationId = null;
			this.OperationTimeout = Helper.GetTimeSpanPercentage(this.OperationDuration, ExceptionHandlingOptions.timeoutPercentage);
			this.RetrySchedule = new TimeSpan[ExceptionHandlingOptions.retrySchedulePercentages.Length];
			for (int i = 0; i < ExceptionHandlingOptions.retrySchedulePercentages.Length; i++)
			{
				this.RetrySchedule[i] = Helper.GetTimeSpanPercentage(this.OperationDuration, ExceptionHandlingOptions.retrySchedulePercentages[i]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004D65 File Offset: 0x00002F65
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00004D6D File Offset: 0x00002F6D
		public TimeSpan[] RetrySchedule { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004D76 File Offset: 0x00002F76
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00004D7E File Offset: 0x00002F7E
		public TimeSpan OperationDuration { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004D87 File Offset: 0x00002F87
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00004D8F File Offset: 0x00002F8F
		public TimeSpan OperationTimeout { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004D98 File Offset: 0x00002F98
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00004DA0 File Offset: 0x00002FA0
		public int MaxRetries { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004DA9 File Offset: 0x00002FA9
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00004DB1 File Offset: 0x00002FB1
		public string ClientId { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004DBA File Offset: 0x00002FBA
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00004DC2 File Offset: 0x00002FC2
		public string CorrelationId { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004DCB File Offset: 0x00002FCB
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00004DD3 File Offset: 0x00002FD3
		public string Operation { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004DDC File Offset: 0x00002FDC
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public bool IsTimeoutEnabled { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004DED File Offset: 0x00002FED
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00004DF5 File Offset: 0x00002FF5
		public bool IsRetryEnabled { get; set; }

		// Token: 0x04000044 RID: 68
		private static double[] retrySchedulePercentages = new double[]
		{
			2.0,
			4.0,
			8.0,
			20.0
		};

		// Token: 0x04000045 RID: 69
		private static double timeoutPercentage = 130.0;

		// Token: 0x04000046 RID: 70
		private static int maxRetries = 50;

		// Token: 0x04000047 RID: 71
		private static TimeSpan minTimeSpan = TimeSpan.FromMilliseconds(1.0);
	}
}
