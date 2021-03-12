using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x0200002B RID: 43
	internal class ExecutionTimeInfo : IExecutionInfo
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00008220 File Offset: 0x00006420
		public TimeSpan CallDuration
		{
			get
			{
				return this.callDuration;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008228 File Offset: 0x00006428
		public void OnStart()
		{
			this.stopwatch.Restart();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008235 File Offset: 0x00006435
		public void OnException(Exception ex)
		{
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008237 File Offset: 0x00006437
		public void OnFinish()
		{
			this.stopwatch.Stop();
			this.callDuration = this.stopwatch.Elapsed;
		}

		// Token: 0x040000D2 RID: 210
		private Stopwatch stopwatch = new Stopwatch();

		// Token: 0x040000D3 RID: 211
		private TimeSpan callDuration = TimeSpan.Zero;
	}
}
