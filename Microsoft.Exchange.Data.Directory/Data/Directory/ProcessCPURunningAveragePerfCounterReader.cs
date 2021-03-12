using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009C5 RID: 2501
	internal class ProcessCPURunningAveragePerfCounterReader : BaseRunningAveragePerfCounterReader
	{
		// Token: 0x06007409 RID: 29705 RVA: 0x0017EEEC File Offset: 0x0017D0EC
		public ProcessCPURunningAveragePerfCounterReader() : base(10, 500U)
		{
			this.currentProcess = NativeMethods.GetCurrentProcess();
			if (CPUUsage.GetCurrentCPU(this.currentProcess, ref this.lastCPU))
			{
				this.lastTime = DateTime.UtcNow;
				return;
			}
			this.lastCPU = 0L;
			this.lastTime = DateTime.MinValue;
		}

		// Token: 0x0600740A RID: 29706 RVA: 0x0017EF43 File Offset: 0x0017D143
		protected override bool AcquireCounter()
		{
			return true;
		}

		// Token: 0x0600740B RID: 29707 RVA: 0x0017EF48 File Offset: 0x0017D148
		protected override float ReadCounter()
		{
			float result;
			CPUUsage.CalculateCPUUsagePercentage(this.currentProcess, ref this.lastTime, ref this.lastCPU, out result);
			return result;
		}

		// Token: 0x04004AED RID: 19181
		private DateTime lastTime;

		// Token: 0x04004AEE RID: 19182
		private long lastCPU;

		// Token: 0x04004AEF RID: 19183
		private IntPtr currentProcess;
	}
}
