using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000357 RID: 855
	public class ThroughputStatistics : Statistics<double, double, double>
	{
		// Token: 0x06001D80 RID: 7552 RVA: 0x00081F34 File Offset: 0x00080134
		[return: Dynamic]
		public static dynamic Calculate(IEnumerable<double> samples)
		{
			IEnumerable<double> source = samples.DefaultIfEmpty<double>();
			ThroughputStatistics throughputStatistics = new ThroughputStatistics();
			throughputStatistics.Average = source.Average((double t) => t);
			throughputStatistics.Maximum = source.Max((double t) => t);
			throughputStatistics.Minimum = source.Min((double t) => t);
			throughputStatistics.Sum = source.Sum((double t) => t);
			throughputStatistics.SampleCount = source.Count<double>();
			return throughputStatistics;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x00082000 File Offset: 0x00080200
		public override string ToString()
		{
			string format = "Avg: {0,-15:F} Max: {1,-15:F} Min: {2,-15:F}";
			return string.Format(format, base.Average, base.Maximum, base.Minimum);
		}
	}
}
