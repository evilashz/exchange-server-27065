using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020001F8 RID: 504
	internal class CpuTracker : DisposeTrackableBase
	{
		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003CEE1 File Offset: 0x0003B0E1
		private CpuTracker(string instance)
		{
			this.instance = instance;
			this.currentThread = ProcessThreadHelper.Current;
			this.startCpuTime = this.currentThread.TotalProcessorTime;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0003CF18 File Offset: 0x0003B118
		public TimeSpan TotalProcessorTime
		{
			get
			{
				if (this.totalProcessorTime == null)
				{
					throw new InvalidOperationException(DiagnosticsResources.ExcInvalidOpPropertyBeforeEnd);
				}
				return this.totalProcessorTime.Value;
			}
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0003CF42 File Offset: 0x0003B142
		public static CpuTracker StartCpuTracking()
		{
			return new CpuTracker(string.Empty);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0003CF4E File Offset: 0x0003B14E
		public static CpuTracker StartCpuTracking(string instance)
		{
			return new CpuTracker(instance);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0003CF56 File Offset: 0x0003B156
		public void End()
		{
			this.Dispose();
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0003CF5E File Offset: 0x0003B15E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CpuTracker>(this);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0003CF68 File Offset: 0x0003B168
		protected override void InternalDispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				this.totalProcessorTime = new TimeSpan?(this.currentThread.TotalProcessorTime - this.startCpuTime);
				ActivityContext.AddOperation(ActivityOperationType.CustomCpu, this.instance, (float)this.TotalProcessorTime.TotalMilliseconds, 1);
			}
		}

		// Token: 0x04000A97 RID: 2711
		private readonly string instance;

		// Token: 0x04000A98 RID: 2712
		private readonly TimeSpan startCpuTime;

		// Token: 0x04000A99 RID: 2713
		private readonly ProcessThread currentThread;

		// Token: 0x04000A9A RID: 2714
		private TimeSpan? totalProcessorTime = null;
	}
}
