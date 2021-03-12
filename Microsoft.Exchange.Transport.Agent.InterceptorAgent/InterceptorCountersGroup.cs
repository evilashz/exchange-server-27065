using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x0200001A RID: 26
	internal class InterceptorCountersGroup
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00006614 File Offset: 0x00004814
		internal InterceptorCountersGroup(InterceptorAgentRuleBehavior actions)
		{
			foreach (object obj in Enum.GetValues(typeof(InterceptorAgentRuleBehavior)))
			{
				InterceptorAgentRuleBehavior interceptorAgentRuleBehavior = (InterceptorAgentRuleBehavior)obj;
				if (interceptorAgentRuleBehavior != InterceptorAgentRuleBehavior.NoOp && actions.HasFlag(interceptorAgentRuleBehavior) && InterceptorCountersGroup.ActionToPerfCounterNameMap.ContainsKey(interceptorAgentRuleBehavior))
				{
					this.AddCounterOfName(1073741824 | (int)interceptorAgentRuleBehavior, InterceptorCountersGroup.ActionToPerfCounterNameMap[interceptorAgentRuleBehavior]);
				}
			}
			InterceptorCountersGroup.TrackInstance(this);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000066C4 File Offset: 0x000048C4
		internal InterceptorCountersGroup(InterceptorAgentEvent events)
		{
			foreach (object obj in Enum.GetValues(typeof(InterceptorAgentEvent)))
			{
				InterceptorAgentEvent interceptorAgentEvent = (InterceptorAgentEvent)obj;
				if (interceptorAgentEvent != InterceptorAgentEvent.Invalid && events.HasFlag(interceptorAgentEvent))
				{
					this.AddCounterOfName(268435456 | (int)interceptorAgentEvent, string.Format("{0} {1} Messages", "Evaluated", interceptorAgentEvent));
					this.AddCounterOfName(536870912 | (int)interceptorAgentEvent, string.Format("{0} {1} Messages", "Matched", interceptorAgentEvent));
				}
			}
			InterceptorCountersGroup.TrackInstance(this);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006790 File Offset: 0x00004990
		internal void StopTracking()
		{
			lock (InterceptorCountersGroup.InstancesSyncObject)
			{
				if (InterceptorCountersGroup.Instances.Contains(this))
				{
					InterceptorCountersGroup.Instances.Remove(this);
					if (InterceptorCountersGroup.Instances.Count == 0)
					{
						InterceptorCountersGroup.averageUpdateTimer.Pause();
					}
				}
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000067FC File Offset: 0x000049FC
		internal void Increment(InterceptorAgentRuleBehavior action)
		{
			this.IncrementCounterOfHash(1073741824 | (int)action);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000680B File Offset: 0x00004A0B
		internal void Increment(InterceptorAgentEvent evt, bool matched)
		{
			this.IncrementCounterOfHash((matched ? 536870912 : 268435456) | (int)evt);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006824 File Offset: 0x00004A24
		internal void UpdateAverage()
		{
			foreach (KeyValuePair<int, ICountAndRatePairCounter> keyValuePair in this.ruleCounters)
			{
				keyValuePair.Value.UpdateAverage();
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000687C File Offset: 0x00004A7C
		internal void GetDiagnosticInfo(XElement parent)
		{
			foreach (KeyValuePair<int, ICountAndRatePairCounter> keyValuePair in this.ruleCounters)
			{
				keyValuePair.Value.GetDiagnosticInfo(parent);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000068D8 File Offset: 0x00004AD8
		private static void TrackInstance(InterceptorCountersGroup interceptorCountersGroup)
		{
			lock (InterceptorCountersGroup.InstancesSyncObject)
			{
				InterceptorCountersGroup.Instances.Add(interceptorCountersGroup);
				if (InterceptorCountersGroup.averageUpdateTimer == null)
				{
					InterceptorCountersGroup.averageUpdateTimer = new GuardedTimer(new TimerCallback(InterceptorCountersGroup.AverageUpdater), null, 0, 5000);
				}
				else
				{
					InterceptorCountersGroup.averageUpdateTimer.Continue();
				}
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000694C File Offset: 0x00004B4C
		private static void AverageUpdater(object state)
		{
			bool flag = false;
			try
			{
				flag = Monitor.TryEnter(InterceptorCountersGroup.InstancesSyncObject);
				if (flag)
				{
					foreach (KeyValuePair<int, ICountAndRatePairCounter> keyValuePair in InterceptorCountersGroup.TotalCounters)
					{
						keyValuePair.Value.UpdateAverage();
					}
					foreach (InterceptorCountersGroup interceptorCountersGroup in InterceptorCountersGroup.Instances)
					{
						interceptorCountersGroup.UpdateAverage();
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(InterceptorCountersGroup.InstancesSyncObject);
				}
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006A14 File Offset: 0x00004C14
		private void AddCounterOfName(int hashKey, string counterName)
		{
			ICountAndRatePairCounter countAndRatePairCounter = null;
			if (Util.PerfCountersTotalInstance != null && !InterceptorCountersGroup.TotalCounters.TryGetValue(hashKey, out countAndRatePairCounter))
			{
				ExPerformanceCounter counterOfName = Util.PerfCountersTotalInstance.GetCounterOfName(counterName);
				ExPerformanceCounter counterOfName2 = Util.PerfCountersTotalInstance.GetCounterOfName(counterName + " Rate");
				countAndRatePairCounter = new CountAndRatePairWindowsCounter(counterOfName, counterOfName2, InterceptorCountersGroup.TrackingLength, InterceptorCountersGroup.RateDuration, null);
				lock (InterceptorCountersGroup.InstancesSyncObject)
				{
					InterceptorCountersGroup.TotalCounters.Add(hashKey, countAndRatePairCounter);
				}
			}
			string averageCounterName = counterName + " Rate";
			this.ruleCounters.Add(hashKey, new CountAndRatePairMemoryCounter(counterName, averageCounterName, InterceptorCountersGroup.TrackingLength, InterceptorCountersGroup.RateDuration, countAndRatePairCounter));
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006AD4 File Offset: 0x00004CD4
		private void IncrementCounterOfHash(int hashKey)
		{
			ICountAndRatePairCounter countAndRatePairCounter;
			if (this.ruleCounters.TryGetValue(hashKey, out countAndRatePairCounter))
			{
				countAndRatePairCounter.AddValue(1L);
			}
		}

		// Token: 0x040000AD RID: 173
		private const int InterceptorRateCountersUpdatePeriod = 5000;

		// Token: 0x040000AE RID: 174
		private const int EvaluatedHashMask = 268435456;

		// Token: 0x040000AF RID: 175
		private const int MatchedHashMask = 536870912;

		// Token: 0x040000B0 RID: 176
		private const int ActionHashMask = 1073741824;

		// Token: 0x040000B1 RID: 177
		private static readonly TimeSpan TrackingLength = TimeSpan.FromMinutes(30.0);

		// Token: 0x040000B2 RID: 178
		private static readonly TimeSpan RateDuration = TimeSpan.FromMinutes(1.0);

		// Token: 0x040000B3 RID: 179
		private static readonly Dictionary<InterceptorAgentRuleBehavior, string> ActionToPerfCounterNameMap = new Dictionary<InterceptorAgentRuleBehavior, string>
		{
			{
				InterceptorAgentRuleBehavior.Archive,
				"Messages Archived"
			},
			{
				InterceptorAgentRuleBehavior.ArchiveHeaders,
				"Message Headers Archived"
			},
			{
				InterceptorAgentRuleBehavior.Delay,
				"Messages Delayed"
			},
			{
				InterceptorAgentRuleBehavior.PermanentReject,
				"Messages Permanently Rejected"
			},
			{
				InterceptorAgentRuleBehavior.TransientReject,
				"Messages Transiently Rejected"
			},
			{
				InterceptorAgentRuleBehavior.Drop,
				"Messages Dropped"
			},
			{
				InterceptorAgentRuleBehavior.Defer,
				"Messages Deferred"
			}
		};

		// Token: 0x040000B4 RID: 180
		private static readonly Dictionary<int, ICountAndRatePairCounter> TotalCounters = new Dictionary<int, ICountAndRatePairCounter>();

		// Token: 0x040000B5 RID: 181
		private static readonly object InstancesSyncObject = new object();

		// Token: 0x040000B6 RID: 182
		private static readonly List<InterceptorCountersGroup> Instances = new List<InterceptorCountersGroup>();

		// Token: 0x040000B7 RID: 183
		private static GuardedTimer averageUpdateTimer;

		// Token: 0x040000B8 RID: 184
		private readonly Dictionary<int, ICountAndRatePairCounter> ruleCounters = new Dictionary<int, ICountAndRatePairCounter>();
	}
}
