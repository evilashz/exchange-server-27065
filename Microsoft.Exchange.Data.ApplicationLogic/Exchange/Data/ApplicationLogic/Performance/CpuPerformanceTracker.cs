using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.ApplicationLogic.Performance
{
	// Token: 0x0200018E RID: 398
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct CpuPerformanceTracker : IDisposable
	{
		// Token: 0x06000F43 RID: 3907 RVA: 0x0003D746 File Offset: 0x0003B946
		public CpuPerformanceTracker(string marker, IPerformanceDataLogger logger)
		{
			if (string.IsNullOrEmpty(marker))
			{
				throw new ArgumentNullException("marker");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this.marker = marker;
			this.logger = logger;
			this.beginThreadTimes = ThreadTimes.GetFromCurrentThread();
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0003D784 File Offset: 0x0003B984
		public void Dispose()
		{
			ThreadTimes fromCurrentThread = ThreadTimes.GetFromCurrentThread();
			this.logger.Log(this.marker, "CpuTime", fromCurrentThread.Kernel - this.beginThreadTimes.Kernel + fromCurrentThread.User - this.beginThreadTimes.User);
		}

		// Token: 0x04000810 RID: 2064
		public const string CpuTime = "CpuTime";

		// Token: 0x04000811 RID: 2065
		private readonly string marker;

		// Token: 0x04000812 RID: 2066
		private readonly IPerformanceDataLogger logger;

		// Token: 0x04000813 RID: 2067
		private readonly ThreadTimes beginThreadTimes;
	}
}
