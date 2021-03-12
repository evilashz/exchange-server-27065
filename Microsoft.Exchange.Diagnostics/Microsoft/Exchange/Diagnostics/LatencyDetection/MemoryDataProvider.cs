using System;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000179 RID: 377
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MemoryDataProvider : PerformanceDataProvider
	{
		// Token: 0x06000AB5 RID: 2741 RVA: 0x000279FA File Offset: 0x00025BFA
		private MemoryDataProvider() : base("KBytesAllocated", false)
		{
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00027A08 File Offset: 0x00025C08
		internal static MemoryDataProvider Instance
		{
			get
			{
				return MemoryDataProvider.singletonInstance;
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00027A0F File Offset: 0x00025C0F
		public override PerformanceData TakeSnapshot(bool begin)
		{
			base.Latency = TimeSpan.Zero;
			base.RequestCount = (uint)(GC.GetTotalMemory(false) >> 10);
			return base.TakeSnapshot(begin);
		}

		// Token: 0x0400074D RID: 1869
		private const int DivByOneKShiftBits = 10;

		// Token: 0x0400074E RID: 1870
		private static readonly MemoryDataProvider singletonInstance = new MemoryDataProvider();
	}
}
