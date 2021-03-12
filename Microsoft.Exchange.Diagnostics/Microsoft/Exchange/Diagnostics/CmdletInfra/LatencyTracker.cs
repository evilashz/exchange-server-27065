using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x02000105 RID: 261
	internal class LatencyTracker
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
		internal LatencyTracker(string name, Func<IActivityScope> activityScopeGetter)
		{
			this.Name = name;
			this.activityScopeGetter = activityScopeGetter;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0001E200 File Offset: 0x0001C400
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x0001E208 File Offset: 0x0001C408
		internal string Name { get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0001E211 File Offset: 0x0001C411
		internal bool IsRunning
		{
			get
			{
				return this.stopWatch.IsRunning;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001E21E File Offset: 0x0001C41E
		internal long ElapsedMilliseconds
		{
			get
			{
				return this.stopWatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001E22B File Offset: 0x0001C42B
		internal void Start()
		{
			this.stopWatch.Start();
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001E238 File Offset: 0x0001C438
		internal long Stop()
		{
			this.stopWatch.Stop();
			return this.stopWatch.ElapsedMilliseconds;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001E250 File Offset: 0x0001C450
		internal bool StartInternalTracking(string funcName)
		{
			return this.StartInternalTracking(funcName, funcName, false);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001E25C File Offset: 0x0001C45C
		internal bool StartInternalTracking(string groupName, string funcName, bool logDetailsAlways)
		{
			LatencyTracker.LatencyInfo currentLatencyInfo = this.GetCurrentLatencyInfo();
			currentLatencyInfo.LogDetailsAlways = logDetailsAlways;
			currentLatencyInfo.FuncName = funcName;
			currentLatencyInfo.GroupName = groupName;
			string key = LatencyTracker.LatencyInfo.GetKey(groupName, funcName);
			if (this.internalTrackingDic.ContainsKey(key))
			{
				return false;
			}
			if (!this.groupTrackingDic.ContainsKey(groupName))
			{
				this.groupTrackingDic.Add(groupName, funcName);
			}
			this.internalTrackingDic.Add(key, currentLatencyInfo);
			return true;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001E2C9 File Offset: 0x0001C4C9
		internal void EndInternalTracking(string funcName)
		{
			this.EndInternalTracking(funcName, funcName);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001E2D4 File Offset: 0x0001C4D4
		internal void EndInternalTracking(string groupName, string funcName)
		{
			string key = LatencyTracker.LatencyInfo.GetKey(groupName, funcName);
			if (!this.internalTrackingDic.ContainsKey(key))
			{
				return;
			}
			LatencyTracker.LatencyInfo latencyInfo = this.internalTrackingDic[key];
			this.internalTrackingDic.Remove(key);
			LatencyTracker.LatencyInfo latencyInfo2 = this.GetCurrentLatencyInfo() - latencyInfo;
			long elapsedTime = latencyInfo2.ElapsedTime;
			if (this.latencyBreakDowns.ContainsKey(groupName))
			{
				long value = 2L;
				string key2 = groupName + ".C";
				if (this.latencyBreakDowns.ContainsKey(key2))
				{
					value = this.latencyBreakDowns[key2] + 1L;
				}
				this.latencyBreakDowns[key2] = value;
			}
			long num = elapsedTime;
			string strA;
			if (this.groupTrackingDic.TryGetValue(groupName, out strA) && string.Compare(strA, funcName, true) == 0)
			{
				if (this.latencyBreakDowns.ContainsKey(groupName))
				{
					num = this.latencyBreakDowns[groupName] + num;
				}
				this.latencyBreakDowns[groupName] = num;
				this.groupTrackingDic.Remove(groupName);
			}
			string funcNameForDetailedLatencyLogging = this.GetFuncNameForDetailedLatencyLogging(funcName, latencyInfo);
			if (latencyInfo.LogDetailsAlways || elapsedTime >= (long)LoggerSettings.ThresholdToLogActivityLatency)
			{
				if (!string.Equals(funcNameForDetailedLatencyLogging, groupName))
				{
					this.latencyBreakDowns[funcNameForDetailedLatencyLogging] = elapsedTime;
				}
				if (latencyInfo2.ADLatency.Count > 0L)
				{
					this.latencyBreakDowns.Add(funcNameForDetailedLatencyLogging + ".ADC", latencyInfo2.ADLatency.Count);
					this.latencyBreakDowns.Add(funcNameForDetailedLatencyLogging + ".AD", (long)latencyInfo2.ADLatency.TotalMilliseconds);
				}
				if (latencyInfo2.RpcLatency.Count > 0L)
				{
					this.latencyBreakDowns.Add(funcNameForDetailedLatencyLogging + ".RpcC", latencyInfo2.RpcLatency.Count);
					this.latencyBreakDowns.Add(funcNameForDetailedLatencyLogging + ".Rpc", (long)latencyInfo2.RpcLatency.TotalMilliseconds);
				}
				if (latencyInfo2.ADObjToExchObjLatency.Count > 0L)
				{
					this.latencyBreakDowns.Add(funcNameForDetailedLatencyLogging + ".ATEC", latencyInfo2.ADObjToExchObjLatency.Count);
					this.latencyBreakDowns.Add(funcNameForDetailedLatencyLogging + ".ATE", (long)latencyInfo2.ADObjToExchObjLatency.TotalMilliseconds);
				}
			}
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001E50C File Offset: 0x0001C70C
		internal void PushLatencyDetailsToLog(Dictionary<string, Enum> knownFuncNameToLogMetadataDic, Action<Enum, double> updateLatencyToLogger, Action<string, string> defaultLatencyLogger)
		{
			if (this.latencyBreakDowns.Count != 0)
			{
				foreach (KeyValuePair<string, long> keyValuePair in this.latencyBreakDowns)
				{
					string key = keyValuePair.Key;
					long value = keyValuePair.Value;
					Enum arg;
					if (knownFuncNameToLogMetadataDic != null && updateLatencyToLogger != null && knownFuncNameToLogMetadataDic.TryGetValue(key, out arg))
					{
						updateLatencyToLogger(arg, (double)value);
					}
					else
					{
						defaultLatencyLogger(key, value.ToString());
					}
				}
				return;
			}
			if (defaultLatencyLogger != null)
			{
				defaultLatencyLogger("LatencyMissed", "latencyBreakDowns.Count is Zero");
				return;
			}
			if (updateLatencyToLogger != null)
			{
				updateLatencyToLogger(RpsCommonMetadata.GenericLatency, 0.0);
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001E5D0 File Offset: 0x0001C7D0
		private string GetFuncNameForDetailedLatencyLogging(string funcName, LatencyTracker.LatencyInfo latencyInfoAtStart)
		{
			string text = funcName;
			if (latencyInfoAtStart.GroupName != null && !string.Equals(funcName, latencyInfoAtStart.GroupName))
			{
				text = latencyInfoAtStart.GroupName + "." + text;
			}
			int num;
			if (this.funcNameTrackingDic.TryGetValue(text, out num))
			{
				num++;
			}
			else
			{
				num = 1;
			}
			this.funcNameTrackingDic[text] = num;
			if (num > 1)
			{
				text = text + "$" + num;
			}
			if (!string.Equals(text, funcName) && this.latencyBreakDowns.ContainsKey(text))
			{
				text = Guid.NewGuid().ToString();
			}
			return text;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001E674 File Offset: 0x0001C874
		private LatencyTracker.LatencyInfo GetCurrentLatencyInfo()
		{
			AggregatedOperationStatistics adlatency = new AggregatedOperationStatistics
			{
				Type = AggregatedOperationType.ADCalls,
				Count = 0L,
				TotalMilliseconds = 0.0
			};
			AggregatedOperationStatistics rpcLatency = new AggregatedOperationStatistics
			{
				Type = AggregatedOperationType.StoreRPCs,
				Count = 0L,
				TotalMilliseconds = 0.0
			};
			AggregatedOperationStatistics adobjToExchObjLatency = new AggregatedOperationStatistics
			{
				Type = AggregatedOperationType.ADObjToExchObjLatency,
				Count = 0L,
				TotalMilliseconds = 0.0
			};
			if (this.activityScopeGetter != null)
			{
				IActivityScope activityScope = this.activityScopeGetter();
				if (activityScope != null)
				{
					adlatency = activityScope.TakeStatisticsSnapshot(AggregatedOperationType.ADCalls);
					rpcLatency = activityScope.TakeStatisticsSnapshot(AggregatedOperationType.StoreRPCs);
					adobjToExchObjLatency = activityScope.TakeStatisticsSnapshot(AggregatedOperationType.ADObjToExchObjLatency);
				}
			}
			return new LatencyTracker.LatencyInfo
			{
				ElapsedTime = this.stopWatch.ElapsedMilliseconds,
				ADLatency = adlatency,
				RpcLatency = rpcLatency,
				ADObjToExchObjLatency = adobjToExchObjLatency
			};
		}

		// Token: 0x040004BE RID: 1214
		internal const string LatencyMissed = "LatencyMissed";

		// Token: 0x040004BF RID: 1215
		private readonly Stopwatch stopWatch = new Stopwatch();

		// Token: 0x040004C0 RID: 1216
		private readonly Dictionary<string, long> latencyBreakDowns = new Dictionary<string, long>();

		// Token: 0x040004C1 RID: 1217
		private readonly Dictionary<string, LatencyTracker.LatencyInfo> internalTrackingDic = new Dictionary<string, LatencyTracker.LatencyInfo>();

		// Token: 0x040004C2 RID: 1218
		private readonly Dictionary<string, string> groupTrackingDic = new Dictionary<string, string>();

		// Token: 0x040004C3 RID: 1219
		private readonly Dictionary<string, int> funcNameTrackingDic = new Dictionary<string, int>();

		// Token: 0x040004C4 RID: 1220
		private readonly Func<IActivityScope> activityScopeGetter;

		// Token: 0x02000106 RID: 262
		private struct LatencyInfo
		{
			// Token: 0x17000140 RID: 320
			// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001E76C File Offset: 0x0001C96C
			// (set) Token: 0x0600079F RID: 1951 RVA: 0x0001E774 File Offset: 0x0001C974
			public bool LogDetailsAlways { get; set; }

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0001E77D File Offset: 0x0001C97D
			// (set) Token: 0x060007A1 RID: 1953 RVA: 0x0001E785 File Offset: 0x0001C985
			public string GroupName { get; set; }

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0001E78E File Offset: 0x0001C98E
			// (set) Token: 0x060007A3 RID: 1955 RVA: 0x0001E796 File Offset: 0x0001C996
			public string FuncName { get; set; }

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0001E79F File Offset: 0x0001C99F
			// (set) Token: 0x060007A5 RID: 1957 RVA: 0x0001E7A7 File Offset: 0x0001C9A7
			public long ElapsedTime { get; set; }

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0001E7B0 File Offset: 0x0001C9B0
			// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0001E7B8 File Offset: 0x0001C9B8
			public AggregatedOperationStatistics ADLatency { get; set; }

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0001E7C1 File Offset: 0x0001C9C1
			// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0001E7C9 File Offset: 0x0001C9C9
			public AggregatedOperationStatistics RpcLatency { get; set; }

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x060007AA RID: 1962 RVA: 0x0001E7D2 File Offset: 0x0001C9D2
			// (set) Token: 0x060007AB RID: 1963 RVA: 0x0001E7DA File Offset: 0x0001C9DA
			public AggregatedOperationStatistics ADObjToExchObjLatency { get; set; }

			// Token: 0x060007AC RID: 1964 RVA: 0x0001E7E3 File Offset: 0x0001C9E3
			public static string GetKey(string groupName, string funcName)
			{
				return groupName + "." + funcName;
			}

			// Token: 0x060007AD RID: 1965 RVA: 0x0001E7F4 File Offset: 0x0001C9F4
			public static LatencyTracker.LatencyInfo operator -(LatencyTracker.LatencyInfo s1, LatencyTracker.LatencyInfo s2)
			{
				return new LatencyTracker.LatencyInfo
				{
					ElapsedTime = s1.ElapsedTime - s2.ElapsedTime,
					ADLatency = s1.ADLatency - s2.ADLatency,
					RpcLatency = s1.RpcLatency - s2.RpcLatency,
					ADObjToExchObjLatency = s1.ADObjToExchObjLatency - s2.ADObjToExchObjLatency
				};
			}
		}
	}
}
