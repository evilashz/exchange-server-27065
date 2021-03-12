using System;

namespace Microsoft.Exchange.Diagnostics.Performance
{
	// Token: 0x02000101 RID: 257
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPerformanceDataLogger
	{
		// Token: 0x0600077C RID: 1916
		void Log(string marker, string counter, TimeSpan dataPoint);

		// Token: 0x0600077D RID: 1917
		void Log(string marker, string counter, uint dataPoint);

		// Token: 0x0600077E RID: 1918
		void Log(string marker, string counter, string dataPoint);
	}
}
