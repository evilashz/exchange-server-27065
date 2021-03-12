using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000090 RID: 144
	internal class AmThrottledActionTracker<TData> where TData : class, IActionData, new()
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001B14C File Offset: 0x0001934C
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0001B154 File Offset: 0x00019354
		internal string ActionName { get; private set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0001B15D File Offset: 0x0001935D
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0001B165 File Offset: 0x00019365
		internal int MaxHistorySize { get; set; }

		// Token: 0x0600055D RID: 1373 RVA: 0x0001B16E File Offset: 0x0001936E
		internal AmThrottledActionTracker(string actionName, int maxHistorySize = 1)
		{
			this.ActionName = actionName;
			this.MaxHistorySize = maxHistorySize;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001B19C File Offset: 0x0001939C
		internal static string ConstructRegKeyName(string serverName)
		{
			return string.Format("ExchangeActiveManager\\SystemState\\{0}\\Throttling", serverName);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001B1F8 File Offset: 0x000193F8
		internal void InitializeFromClusdb()
		{
			Exception ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				lock (this.locker)
				{
					this.InitializeFromClusdbInternal();
				}
			});
			if (ex != null)
			{
				ReplayCrimsonEvents.FailoverOnReplDownClusdbInitializeFailed.Log<string, int, string>(this.ActionName, this.MaxHistorySize, (ex != null) ? ex.Message : "<null>");
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001B240 File Offset: 0x00019440
		private void InitializeFromClusdbInternal()
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (config.IsPAM)
			{
				AmDagConfig dagConfig = config.DagConfig;
				using (IClusterDB clusterDB = ClusterDB.Open())
				{
					foreach (AmServerName amServerName in dagConfig.MemberServers)
					{
						string keyName = AmThrottledActionTracker<TData>.ConstructRegKeyName(amServerName.NetbiosName);
						string[] value = clusterDB.GetValue<string[]>(keyName, this.ActionName, null);
						if (value != null && value.Length > 0)
						{
							LinkedList<TData> linkedList = new LinkedList<TData>();
							foreach (string text in value)
							{
								int num = text.IndexOf('=');
								if (num != -1)
								{
									string s = text.Substring(0, num);
									string dataStr = null;
									if (num < text.Length - 1)
									{
										dataStr = text.Substring(num + 1);
									}
									ExDateTime actionTime = ExDateTime.Parse(s);
									TData value2 = Activator.CreateInstance<TData>();
									value2.Initialize(actionTime, dataStr);
									linkedList.AddFirst(value2);
								}
							}
							this.actionHistory[amServerName] = linkedList;
						}
					}
				}
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001B37C File Offset: 0x0001957C
		internal AmThrottledActionTracker<TData>.ThrottlingShapshot GetThrottlingSnapshot(AmServerName server, TimeSpan minDurationBetweenActionsPerNode, TimeSpan maxCheckDurationPerNode, int maxAllowedActionsPerNode, TimeSpan minDurationBetweenActionsAcrossDag, TimeSpan maxCheckDurationAcrossDag, int maxAllowedActionsAcrossDag)
		{
			AmThrottledActionTracker<TData>.ThrottlingShapshot throttlingShapshot = new AmThrottledActionTracker<TData>.ThrottlingShapshot(server, minDurationBetweenActionsPerNode, maxCheckDurationPerNode, maxAllowedActionsPerNode, minDurationBetweenActionsAcrossDag, maxCheckDurationAcrossDag, maxAllowedActionsAcrossDag);
			ExDateTime now = ExDateTime.Now;
			throttlingShapshot.CalculationBaseTime = now;
			lock (this.locker)
			{
				throttlingShapshot.MostRecentActionDataForNode = this.GetMostRecentActionData(server);
				throttlingShapshot.MostRecentActionDataAcrossDag = this.GetMostRecentActionDataAcrossDag();
				throttlingShapshot.ActionsCountPerNode = this.GetActionsCountForTimeSpan(server, maxCheckDurationPerNode, now);
				throttlingShapshot.ActionsCountAcrossDag = this.GetActionsForTimeSpanAcrossDag(maxCheckDurationAcrossDag, now);
			}
			throttlingShapshot.IsActionCalledTooSoonPerNode = false;
			if (minDurationBetweenActionsPerNode > TimeSpan.Zero)
			{
				throttlingShapshot.IsActionCalledTooSoonPerNode = this.IsActionCalledTooSoon(throttlingShapshot.MostRecentActionDataForNode, minDurationBetweenActionsPerNode, now);
			}
			throttlingShapshot.IsActionCalledTooSoonAcrossDag = false;
			if (minDurationBetweenActionsAcrossDag > TimeSpan.Zero)
			{
				throttlingShapshot.IsActionCalledTooSoonAcrossDag = this.IsActionCalledTooSoon(throttlingShapshot.MostRecentActionDataAcrossDag, minDurationBetweenActionsAcrossDag, now);
			}
			throttlingShapshot.IsMaxActionsPerNodeExceeded = false;
			if (maxAllowedActionsPerNode > 0 && throttlingShapshot.ActionsCountPerNode >= maxAllowedActionsPerNode)
			{
				throttlingShapshot.IsMaxActionsPerNodeExceeded = true;
			}
			throttlingShapshot.IsMaxActionsAcrossDagExceeded = false;
			if (maxAllowedActionsAcrossDag > 0 && throttlingShapshot.ActionsCountAcrossDag >= maxAllowedActionsAcrossDag)
			{
				throttlingShapshot.IsMaxActionsAcrossDagExceeded = true;
			}
			return throttlingShapshot;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001B498 File Offset: 0x00019698
		internal bool IsActionCalledTooSoon(TData actionData, TimeSpan duration, ExDateTime now)
		{
			bool result = false;
			ExDateTime t = now - duration;
			if (actionData != null && actionData.Time != ExDateTime.MinValue && actionData.Time > t)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001B4E8 File Offset: 0x000196E8
		internal ExDateTime GetMostRecentActionTime(AmServerName node)
		{
			ExDateTime result = ExDateTime.MinValue;
			TData mostRecentActionData = this.GetMostRecentActionData(node);
			if (mostRecentActionData != null)
			{
				result = mostRecentActionData.Time;
			}
			return result;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001B51C File Offset: 0x0001971C
		internal TData GetMostRecentActionData(AmServerName node)
		{
			TData result = default(TData);
			lock (this.locker)
			{
				LinkedList<TData> linkedList;
				if (this.actionHistory.TryGetValue(node, out linkedList) && linkedList != null && linkedList.Count > 0)
				{
					result = linkedList.First<TData>();
				}
			}
			return result;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001B584 File Offset: 0x00019784
		internal int GetActionsCountForTimeSpan(AmServerName node, TimeSpan timeSpan, ExDateTime now)
		{
			int num = 0;
			ExDateTime t = now - timeSpan;
			lock (this.locker)
			{
				LinkedList<TData> linkedList;
				if (this.actionHistory.TryGetValue(node, out linkedList) && linkedList != null)
				{
					foreach (TData tdata in linkedList)
					{
						if (tdata != null && tdata.Time >= t)
						{
							num++;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001B638 File Offset: 0x00019838
		internal ExDateTime GetMostRecentActionTimeAcrossDag()
		{
			ExDateTime result = ExDateTime.MinValue;
			TData mostRecentActionDataAcrossDag = this.GetMostRecentActionDataAcrossDag();
			if (mostRecentActionDataAcrossDag != null)
			{
				result = mostRecentActionDataAcrossDag.Time;
			}
			return result;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001B66C File Offset: 0x0001986C
		internal TData GetMostRecentActionDataAcrossDag()
		{
			TData tdata = default(TData);
			lock (this.locker)
			{
				foreach (KeyValuePair<AmServerName, LinkedList<TData>> keyValuePair in this.actionHistory)
				{
					LinkedList<TData> value = keyValuePair.Value;
					if (value != null)
					{
						TData tdata2 = value.First<TData>();
						if (tdata2 != null && (tdata == null || tdata2.Time > tdata.Time))
						{
							tdata = tdata2;
						}
					}
				}
			}
			return tdata;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001B734 File Offset: 0x00019934
		internal int GetActionsForTimeSpanAcrossDag(TimeSpan timeSpan, ExDateTime now)
		{
			int num = 0;
			ExDateTime t = now - timeSpan;
			lock (this.locker)
			{
				foreach (KeyValuePair<AmServerName, LinkedList<TData>> keyValuePair in this.actionHistory)
				{
					LinkedList<TData> value = keyValuePair.Value;
					if (value != null)
					{
						int num2 = 0;
						foreach (TData tdata in value)
						{
							if (tdata != null && tdata.Time >= t)
							{
								num2++;
							}
						}
						num += num2;
					}
				}
			}
			return num;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001B828 File Offset: 0x00019A28
		internal void AddEntry(AmServerName node, TData actionData)
		{
			lock (this.locker)
			{
				LinkedList<TData> linkedList;
				if (!this.actionHistory.TryGetValue(node, out linkedList))
				{
					linkedList = new LinkedList<TData>();
					this.actionHistory.Add(node, linkedList);
				}
				linkedList.AddFirst(actionData);
				if (linkedList.Count > this.MaxHistorySize)
				{
					linkedList.RemoveLast();
				}
				this.Persist(node);
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001B8A8 File Offset: 0x00019AA8
		internal virtual void Cleanup()
		{
			lock (this.locker)
			{
				this.actionHistory.Clear();
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		internal static void RemoveEntryFromClusdb(string serverName, string actionName)
		{
			using (IClusterDB clusterDB = ClusterDB.Open())
			{
				string keyName = AmThrottledActionTracker<TData>.ConstructRegKeyName(serverName);
				clusterDB.DeleteValue(keyName, actionName);
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001B930 File Offset: 0x00019B30
		private void Persist(AmServerName node)
		{
			LinkedList<TData> linkedList;
			if (this.actionHistory.TryGetValue(node, out linkedList) && linkedList != null && linkedList.Count > 0)
			{
				using (IClusterDB clusterDB = ClusterDB.Open())
				{
					List<string> list = new List<string>();
					foreach (TData tdata in linkedList)
					{
						string arg = tdata.Time.UniversalTime.ToString("o");
						string dataStr = tdata.DataStr;
						string item = string.Format("{0}{1}{2}", arg, '=', dataStr);
						list.Add(item);
					}
					string[] propetyValue = list.ToArray();
					string keyName = AmThrottledActionTracker<TData>.ConstructRegKeyName(node.NetbiosName);
					clusterDB.SetValue<string[]>(keyName, this.ActionName, propetyValue);
				}
			}
		}

		// Token: 0x04000235 RID: 565
		private const string ThrottledActionsKeyNameFormat = "ExchangeActiveManager\\SystemState\\{0}\\Throttling";

		// Token: 0x04000236 RID: 566
		private const char SeparatorChar = '=';

		// Token: 0x04000237 RID: 567
		private readonly object locker = new object();

		// Token: 0x04000238 RID: 568
		private readonly Dictionary<AmServerName, LinkedList<TData>> actionHistory = new Dictionary<AmServerName, LinkedList<TData>>();

		// Token: 0x02000091 RID: 145
		internal class ThrottlingShapshot
		{
			// Token: 0x1700011C RID: 284
			// (get) Token: 0x0600056E RID: 1390 RVA: 0x0001BA3C File Offset: 0x00019C3C
			// (set) Token: 0x0600056F RID: 1391 RVA: 0x0001BA44 File Offset: 0x00019C44
			internal AmServerName Server { get; set; }

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001BA4D File Offset: 0x00019C4D
			// (set) Token: 0x06000571 RID: 1393 RVA: 0x0001BA55 File Offset: 0x00019C55
			internal TimeSpan MinDurationBetweenActionsPerNode { get; private set; }

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001BA5E File Offset: 0x00019C5E
			// (set) Token: 0x06000573 RID: 1395 RVA: 0x0001BA66 File Offset: 0x00019C66
			internal int MaxAllowedActionsPerNode { get; private set; }

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x06000574 RID: 1396 RVA: 0x0001BA6F File Offset: 0x00019C6F
			// (set) Token: 0x06000575 RID: 1397 RVA: 0x0001BA77 File Offset: 0x00019C77
			internal TimeSpan MaxCheckDurationPerNode { get; private set; }

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x06000576 RID: 1398 RVA: 0x0001BA80 File Offset: 0x00019C80
			// (set) Token: 0x06000577 RID: 1399 RVA: 0x0001BA88 File Offset: 0x00019C88
			internal TimeSpan MinDurationBetweenActionsAcrossDag { get; private set; }

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001BA91 File Offset: 0x00019C91
			// (set) Token: 0x06000579 RID: 1401 RVA: 0x0001BA99 File Offset: 0x00019C99
			internal int MaxAllowedActionsAcrossDag { get; private set; }

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001BAA2 File Offset: 0x00019CA2
			// (set) Token: 0x0600057B RID: 1403 RVA: 0x0001BAAA File Offset: 0x00019CAA
			internal TimeSpan MaxCheckDurationAcrossDag { get; private set; }

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x0600057C RID: 1404 RVA: 0x0001BAB3 File Offset: 0x00019CB3
			// (set) Token: 0x0600057D RID: 1405 RVA: 0x0001BABB File Offset: 0x00019CBB
			internal TData MostRecentActionDataForNode { get; set; }

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x0600057E RID: 1406 RVA: 0x0001BAC4 File Offset: 0x00019CC4
			// (set) Token: 0x0600057F RID: 1407 RVA: 0x0001BACC File Offset: 0x00019CCC
			internal TData MostRecentActionDataAcrossDag { get; set; }

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x06000580 RID: 1408 RVA: 0x0001BAD5 File Offset: 0x00019CD5
			// (set) Token: 0x06000581 RID: 1409 RVA: 0x0001BADD File Offset: 0x00019CDD
			internal int ActionsCountPerNode { get; set; }

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x06000582 RID: 1410 RVA: 0x0001BAE6 File Offset: 0x00019CE6
			// (set) Token: 0x06000583 RID: 1411 RVA: 0x0001BAEE File Offset: 0x00019CEE
			internal int ActionsCountAcrossDag { get; set; }

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001BAF7 File Offset: 0x00019CF7
			// (set) Token: 0x06000585 RID: 1413 RVA: 0x0001BAFF File Offset: 0x00019CFF
			internal ExDateTime CalculationBaseTime { get; set; }

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001BB08 File Offset: 0x00019D08
			// (set) Token: 0x06000587 RID: 1415 RVA: 0x0001BB10 File Offset: 0x00019D10
			internal bool IsActionCalledTooSoonPerNode { get; set; }

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001BB19 File Offset: 0x00019D19
			// (set) Token: 0x06000589 RID: 1417 RVA: 0x0001BB21 File Offset: 0x00019D21
			internal bool IsActionCalledTooSoonAcrossDag { get; set; }

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x0600058A RID: 1418 RVA: 0x0001BB2A File Offset: 0x00019D2A
			// (set) Token: 0x0600058B RID: 1419 RVA: 0x0001BB32 File Offset: 0x00019D32
			internal bool IsMaxActionsPerNodeExceeded { get; set; }

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x0600058C RID: 1420 RVA: 0x0001BB3B File Offset: 0x00019D3B
			// (set) Token: 0x0600058D RID: 1421 RVA: 0x0001BB43 File Offset: 0x00019D43
			internal bool IsMaxActionsAcrossDagExceeded { get; set; }

			// Token: 0x0600058E RID: 1422 RVA: 0x0001BB4C File Offset: 0x00019D4C
			internal ThrottlingShapshot(AmServerName server, TimeSpan minDurationBetweenActionsPerNode, TimeSpan maxCheckDurationPerNode, int maxAllowedActionsPerNode, TimeSpan minDurationBetweenActionsAcrossDag, TimeSpan maxCheckDurationAcrossDag, int maxAllowedActionsAcrossDag)
			{
				this.Server = server;
				this.MinDurationBetweenActionsPerNode = minDurationBetweenActionsPerNode;
				this.MaxCheckDurationPerNode = maxCheckDurationPerNode;
				this.MaxAllowedActionsPerNode = maxAllowedActionsPerNode;
				this.MinDurationBetweenActionsAcrossDag = minDurationBetweenActionsAcrossDag;
				this.MaxCheckDurationAcrossDag = maxCheckDurationAcrossDag;
				this.MaxAllowedActionsAcrossDag = maxAllowedActionsAcrossDag;
			}

			// Token: 0x0600058F RID: 1423 RVA: 0x0001BB8C File Offset: 0x00019D8C
			internal void LogResults(ReplayCrimsonEvent crimsonEvent, TimeSpan suppressDuration)
			{
				if (suppressDuration != TimeSpan.Zero)
				{
					crimsonEvent.LogPeriodicGeneric(this.Server, suppressDuration, new object[]
					{
						this.Server.NetbiosName,
						this.IsActionCalledTooSoonPerNode,
						this.IsActionCalledTooSoonAcrossDag,
						this.IsMaxActionsPerNodeExceeded,
						this.IsMaxActionsAcrossDagExceeded,
						this.GetStringOrNull(this.MostRecentActionDataForNode),
						this.GetStringOrNull(this.MostRecentActionDataAcrossDag),
						this.ActionsCountPerNode,
						this.ActionsCountAcrossDag,
						this.MinDurationBetweenActionsPerNode,
						this.MaxCheckDurationPerNode,
						this.MaxAllowedActionsPerNode,
						this.MinDurationBetweenActionsAcrossDag,
						this.MaxCheckDurationAcrossDag,
						this.MaxAllowedActionsAcrossDag
					});
					return;
				}
				crimsonEvent.LogGeneric(new object[]
				{
					this.Server.NetbiosName,
					this.GetStringOrNull(this.MostRecentActionDataForNode),
					this.GetStringOrNull(this.MostRecentActionDataAcrossDag),
					this.ActionsCountPerNode,
					this.ActionsCountAcrossDag,
					this.MinDurationBetweenActionsPerNode,
					this.MaxCheckDurationPerNode,
					this.MaxAllowedActionsPerNode,
					this.MinDurationBetweenActionsAcrossDag,
					this.MaxCheckDurationAcrossDag,
					this.MaxAllowedActionsAcrossDag
				});
			}

			// Token: 0x06000590 RID: 1424 RVA: 0x0001BD5C File Offset: 0x00019F5C
			private string GetStringOrNull(object obj)
			{
				if (obj != null)
				{
					return obj.ToString();
				}
				return "<null>";
			}
		}
	}
}
