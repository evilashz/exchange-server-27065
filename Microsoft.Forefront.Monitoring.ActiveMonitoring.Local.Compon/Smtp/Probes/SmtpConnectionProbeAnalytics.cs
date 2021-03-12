using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x0200024A RID: 586
	public class SmtpConnectionProbeAnalytics
	{
		// Token: 0x060013B9 RID: 5049 RVA: 0x0003A739 File Offset: 0x00038939
		public SmtpConnectionProbeAnalytics()
		{
			this.latencyContributions = new List<SmtpConnectionProbeLatency>();
			this.needsToBeRecalculated = true;
			this.oneStandardDeviation = new List<SmtpConnectionProbeLatency>();
			this.twoOrMoreStandardDeviation = new List<SmtpConnectionProbeLatency>();
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x0003A769 File Offset: 0x00038969
		// (set) Token: 0x060013BB RID: 5051 RVA: 0x0003A777 File Offset: 0x00038977
		public long Mean
		{
			get
			{
				this.RunAnalysis();
				return this.mean;
			}
			private set
			{
				this.mean = value;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0003A780 File Offset: 0x00038980
		// (set) Token: 0x060013BD RID: 5053 RVA: 0x0003A78E File Offset: 0x0003898E
		public long StandardDeviation
		{
			get
			{
				this.RunAnalysis();
				return this.standardDeviation;
			}
			private set
			{
				this.standardDeviation = value;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0003A797 File Offset: 0x00038997
		// (set) Token: 0x060013BF RID: 5055 RVA: 0x0003A7A5 File Offset: 0x000389A5
		internal List<SmtpConnectionProbeLatency> OneStandardDeviation
		{
			get
			{
				this.RunAnalysis();
				return this.oneStandardDeviation;
			}
			private set
			{
				this.oneStandardDeviation = value;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0003A7AE File Offset: 0x000389AE
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x0003A7BC File Offset: 0x000389BC
		internal List<SmtpConnectionProbeLatency> TwoOrMoreStandardDeviation
		{
			get
			{
				this.RunAnalysis();
				return this.twoOrMoreStandardDeviation;
			}
			private set
			{
				this.twoOrMoreStandardDeviation = value;
			}
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0003A7CD File Offset: 0x000389CD
		internal static long GetMean(List<SmtpConnectionProbeLatency> latencies)
		{
			return (long)latencies.Average((SmtpConnectionProbeLatency l) => l.Latency);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0003A810 File Offset: 0x00038A10
		internal static long GetStandardDeviation(long mean, List<SmtpConnectionProbeLatency> latencies)
		{
			return (long)Math.Sqrt(latencies.Average((SmtpConnectionProbeLatency l) => l.Latency - mean ^ 2L));
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0003A888 File Offset: 0x00038A88
		internal static List<SmtpConnectionProbeLatency> GetLatenciesOneStandardDeviationAboveMean(long mean, long standardDeviation, List<SmtpConnectionProbeLatency> latencies)
		{
			return new List<SmtpConnectionProbeLatency>(from latency in latencies
			where latency.Latency >= mean + standardDeviation && latency.Latency < mean + standardDeviation + standardDeviation
			orderby latency.Latency descending
			select latency);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0003A914 File Offset: 0x00038B14
		internal static List<SmtpConnectionProbeLatency> GetLatenciesTwoOrMoreStandardDeviationAboveMean(long mean, long standardDeviation, List<SmtpConnectionProbeLatency> latencies)
		{
			return new List<SmtpConnectionProbeLatency>(from latency in latencies
			where latency.Latency >= mean + standardDeviation + standardDeviation
			orderby latency.Latency descending
			select latency);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0003A96E File Offset: 0x00038B6E
		internal void AddLatency(SmtpConnectionProbeLatency latency)
		{
			this.latencyContributions.Add(latency);
			this.needsToBeRecalculated = true;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0003A9BD File Offset: 0x00038BBD
		internal SmtpConnectionProbeLatency GetHighestLatencyValue()
		{
			return (from latency in this.latencyContributions
			where latency.Latency == this.latencyContributions.Max((SmtpConnectionProbeLatency l) => l.Latency)
			select latency).FirstOrDefault<SmtpConnectionProbeLatency>();
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0003A9DC File Offset: 0x00038BDC
		internal virtual void RunAnalysis()
		{
			if (this.needsToBeRecalculated && this.latencyContributions.Count > 0)
			{
				this.mean = SmtpConnectionProbeAnalytics.GetMean(this.latencyContributions);
				this.standardDeviation = SmtpConnectionProbeAnalytics.GetStandardDeviation(this.mean, this.latencyContributions);
				this.oneStandardDeviation = SmtpConnectionProbeAnalytics.GetLatenciesOneStandardDeviationAboveMean(this.mean, this.standardDeviation, this.latencyContributions);
				this.twoOrMoreStandardDeviation = SmtpConnectionProbeAnalytics.GetLatenciesTwoOrMoreStandardDeviationAboveMean(this.mean, this.standardDeviation, this.latencyContributions);
			}
			this.needsToBeRecalculated = false;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0003AAB8 File Offset: 0x00038CB8
		internal virtual string GenerateLatencyAnalysis()
		{
			StringBuilder sb = new StringBuilder();
			if (this.OneStandardDeviation.Count == 0 && this.TwoOrMoreStandardDeviation.Count == 0)
			{
				sb.AppendFormat("No latencies were above the standard deviation. Mean: {0} Standard Deviation: {1}", this.Mean, this.StandardDeviation);
			}
			else
			{
				sb.AppendFormat("{0} latencies were above the standard deviation. Mean: {1} Standard Deviation: {2}. ", this.OneStandardDeviation.Count + this.TwoOrMoreStandardDeviation.Count, this.Mean, this.StandardDeviation);
				this.TwoOrMoreStandardDeviation.ForEach(delegate(SmtpConnectionProbeLatency latency)
				{
					sb.AppendFormat("{0} ({1}, Two) ", latency.Reason, latency.Latency);
				});
				this.OneStandardDeviation.ForEach(delegate(SmtpConnectionProbeLatency latency)
				{
					sb.AppendFormat("{0} ({1}, One) ", latency.Reason, latency.Latency);
				});
			}
			return sb.ToString();
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0003ABA3 File Offset: 0x00038DA3
		internal string LatencySummary()
		{
			return string.Join<SmtpConnectionProbeLatency>(";", this.latencyContributions);
		}

		// Token: 0x04000936 RID: 2358
		private List<SmtpConnectionProbeLatency> latencyContributions;

		// Token: 0x04000937 RID: 2359
		private bool needsToBeRecalculated;

		// Token: 0x04000938 RID: 2360
		private List<SmtpConnectionProbeLatency> oneStandardDeviation;

		// Token: 0x04000939 RID: 2361
		private List<SmtpConnectionProbeLatency> twoOrMoreStandardDeviation;

		// Token: 0x0400093A RID: 2362
		private long mean;

		// Token: 0x0400093B RID: 2363
		private long standardDeviation;
	}
}
