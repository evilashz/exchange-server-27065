using System;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000007 RID: 7
	internal class DisabledRequestLogger : RequestLogger
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000236A File Offset: 0x0000056A
		public override LatencyTracker LatencyTracker
		{
			get
			{
				return DisabledRequestLogger.disabledLatencyTracker;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002371 File Offset: 0x00000571
		public override void LogField(LogKey key, object value)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002373 File Offset: 0x00000573
		public override void AppendGenericInfo(string key, object value)
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002375 File Offset: 0x00000575
		public override void AppendErrorInfo(string key, object value)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002377 File Offset: 0x00000577
		public override void LogExceptionDetails(string key, Exception ex)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002379 File Offset: 0x00000579
		public override void Flush()
		{
		}

		// Token: 0x0400000F RID: 15
		private static LatencyTracker disabledLatencyTracker = new DisabledLatencyTracker();
	}
}
