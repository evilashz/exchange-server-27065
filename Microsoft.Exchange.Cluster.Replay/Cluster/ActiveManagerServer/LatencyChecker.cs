using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200004C RID: 76
	internal class LatencyChecker
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00012464 File Offset: 0x00010664
		private static List<AmServerName> DagServers
		{
			get
			{
				AmLastKnownGoodConfig lastKnownGoodConfig = AmSystemManager.Instance.LastKnownGoodConfig;
				List<AmServerName> result;
				if (lastKnownGoodConfig != null && lastKnownGoodConfig.Members != null)
				{
					result = lastKnownGoodConfig.Members.ToList<AmServerName>();
				}
				else
				{
					result = new List<AmServerName>();
				}
				return result;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0001249E File Offset: 0x0001069E
		private static Dictionary<AmServerName, IEnumerable<IADDatabase>> DatabaseMap
		{
			get
			{
				return Dependencies.MonitoringADConfigProvider.GetRecentConfig(false).DatabaseMap;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000339 RID: 825 RVA: 0x000124B0 File Offset: 0x000106B0
		// (set) Token: 0x0600033A RID: 826 RVA: 0x000124B7 File Offset: 0x000106B7
		internal static bool EnableClusterKill { get; set; }

		// Token: 0x0600033B RID: 827 RVA: 0x000124C0 File Offset: 0x000106C0
		internal static void WmiKillClussvc(AmServerName nodeName, ExDateTime apiInitiatedTime)
		{
			ObjectQuery query = new ObjectQuery("SELECT * From Win32_Process Where Name='clussvc.exe'");
			string arg = "root\\cimv2";
			ManagementScope managementScope = new ManagementScope(string.Format("\\\\{0}\\{1}", nodeName.NetbiosName, arg));
			managementScope.Connect();
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(managementScope, query))
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					string text = (string)managementObject["CreationDate"];
					ExDateTime exDateTime = ExDateTime.Parse(string.Format("{0}/{1}/{2} {3}:{4}:{5}", new object[]
					{
						text.Substring(0, 4),
						text.Substring(4, 2),
						text.Substring(6, 2),
						text.Substring(8, 2),
						text.Substring(10, 2),
						text.Substring(12, 2)
					}));
					if (exDateTime <= apiInitiatedTime)
					{
						managementObject.InvokeMethod("Terminate", new object[]
						{
							0
						});
					}
					else
					{
						ReplayCrimsonEvents.GenericMessage.Log<string>(string.Format("Clussvc start time is greater than the requested time (ApiTime:{0} ProcessTime:{1}", apiInitiatedTime, exDateTime));
					}
				}
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00012718 File Offset: 0x00010918
		internal static Dictionary<string, AmNodeState> QueryClusterNodeStatus(TimeSpan timeout, bool logEvent = true)
		{
			Dictionary<string, AmNodeState> states = new Dictionary<string, AmNodeState>(StringComparer.OrdinalIgnoreCase);
			Task task = new Task(delegate()
			{
				try
				{
					using (AmCluster amCluster = AmCluster.Open())
					{
						if (amCluster != null && amCluster.Nodes != null && amCluster.Nodes.Count<IAmClusterNode>() > 0)
						{
							foreach (IAmClusterNode amClusterNode in amCluster.Nodes)
							{
								states[amClusterNode.Name.NetbiosName] = amClusterNode.GetState(false);
							}
						}
					}
				}
				catch (Exception ex)
				{
					ExTraceGlobals.LatencyCheckerTracer.TraceDebug<string>(0L, "Exception caught in QueryClusterNodeStatus, ex={0}", ex.ToString());
				}
			});
			if (logEvent)
			{
				ReplayCrimsonEvents.HungNodeClusterInfoGatherStart.Log<string>(AmServerName.LocalComputerName.NetbiosName);
			}
			task.Start();
			task.Wait(timeout);
			return states;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00012778 File Offset: 0x00010978
		internal static LatencyChecker.ClusDbHungInfo GatherHungNodesInformation(LatencyChecker.LatencyContext latencyContext)
		{
			LatencyChecker.ClusDbHungInfo clusDbHungInfo = new LatencyChecker.ClusDbHungInfo();
			TimeSpan timeSpan = ExDateTime.Now - latencyContext.StartTime;
			clusDbHungInfo.ApiName = latencyContext.ApiName;
			clusDbHungInfo.ApiHungStartTime = latencyContext.StartTime;
			ReplayCrimsonEvents.ClusApiOperationAppearsToBeHung.Log<string, ExDateTime, TimeSpan, string, TimeSpan>(latencyContext.ApiName, latencyContext.StartTime, timeSpan, latencyContext.HintStr, latencyContext.MaxAllowedLatency);
			clusDbHungInfo.HungNodeApiException = null;
			try
			{
				ReplayCrimsonEvents.AttemptingToGetHungNodes.Log<string, ExDateTime, LatencyChecker.LatencyContext>(latencyContext.ApiName, latencyContext.StartTime, latencyContext);
				HungNodesInfo nodesHungInClusDbUpdate = HungNodesInfo.GetNodesHungInClusDbUpdate();
				if (nodesHungInClusDbUpdate != null)
				{
					ReplayCrimsonEvents.HungNodeDetectionCompleted.Log<int, AmServerName, HungNodesInfo>(nodesHungInClusDbUpdate.CurrentGumId, nodesHungInClusDbUpdate.CurrentLockOwnerName, nodesHungInClusDbUpdate);
					clusDbHungInfo.CurrentGumId = nodesHungInClusDbUpdate.CurrentGumId;
					clusDbHungInfo.CurrentLockOwnerName = nodesHungInClusDbUpdate.CurrentLockOwnerName;
					clusDbHungInfo.HungNodes = nodesHungInClusDbUpdate.NodeMap.Values.ToArray<AmServerName>();
				}
			}
			catch (HungDetectionGumIdChangedException ex)
			{
				clusDbHungInfo.HungNodeApiException = ex;
				ReplayCrimsonEvents.HungActionSkippedSinceGumIdChanged.Log<int, int, string, long>(ex.LocalGumId, ex.RemoteGumId, ex.LockOwnerName, ex.HungNodesMask);
			}
			catch (OpenClusterTimedoutException ex2)
			{
				clusDbHungInfo.HungNodeApiException = ex2;
				clusDbHungInfo.HungNodes = new AmServerName[]
				{
					new AmServerName(ex2.ServerName)
				};
				ReplayCrimsonEvents.OpenClusterCallHung.Log<string, string, string>(ex2.ServerName, ex2.Message, ex2.Context);
			}
			catch (ClusterException ex3)
			{
				clusDbHungInfo.HungNodeApiException = ex3;
				ReplayCrimsonEvents.HungNodeDetectionFailed.Log<string, string>(ex3.Message, ex3.ToString());
			}
			List<AmServerName> dagServers = LatencyChecker.DagServers;
			ReplayCrimsonEvents.HungNodeRpcScanStart.Log<string>(LatencyChecker.ConvertAmServerNamesToString(dagServers));
			AmMultiNodeCopyStatusFetcher amMultiNodeCopyStatusFetcher = new AmMultiNodeCopyStatusFetcher(dagServers, LatencyChecker.DatabaseMap, RpcGetDatabaseCopyStatusFlags2.None, null, false, 60000);
			amMultiNodeCopyStatusFetcher.GetStatus();
			List<AmServerName> list = new List<AmServerName>();
			List<Exception> list2 = new List<Exception>();
			clusDbHungInfo.ClusterNodesStatus = LatencyChecker.QueryClusterNodeStatus(TimeSpan.FromSeconds(30.0), true);
			foreach (AmServerName amServerName in LatencyChecker.DagServers)
			{
				Exception possibleExceptionForServer = amMultiNodeCopyStatusFetcher.GetPossibleExceptionForServer(amServerName);
				if (possibleExceptionForServer != null)
				{
					if (possibleExceptionForServer is ReplayServiceDownException)
					{
						list.Add(amServerName);
					}
					list2.Add(possibleExceptionForServer);
				}
			}
			clusDbHungInfo.RpcFailedNodes = list.ToArray();
			clusDbHungInfo.RpcExceptions = list2.ToArray();
			ReplayCrimsonEvents.HungNodeInformationLog.Log<string>(clusDbHungInfo.ToString());
			return clusDbHungInfo;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000129FC File Offset: 0x00010BFC
		internal static LatencyChecker.ClusDbHungAction AnalyzeAndSuggestActionForClusDbHang(LatencyChecker.ClusDbHungInfo hungInfo)
		{
			LatencyChecker.ClusDbHungAction clusDbHungAction = new LatencyChecker.ClusDbHungAction();
			clusDbHungAction.HungInfo = hungInfo;
			clusDbHungAction.TakeAction = false;
			clusDbHungAction.TargetNodes = hungInfo.HungNodes;
			clusDbHungAction.Reason = "If you see this message, something is wrong...";
			if (hungInfo.HungNodeApiException != null)
			{
				if (hungInfo.HungNodeApiException is HungDetectionGumIdChangedException)
				{
					clusDbHungAction.TakeAction = false;
					clusDbHungAction.Reason = "GumId changed.";
				}
				else if (hungInfo.HungNodeApiException is OpenClusterTimedoutException)
				{
					clusDbHungAction.TakeAction = true;
					OpenClusterTimedoutException ex = (OpenClusterTimedoutException)hungInfo.HungNodeApiException;
					clusDbHungAction.Reason = string.Format("OpenCluster timed-out for {0}", ex.ServerName);
				}
				else if (hungInfo.HungNodeApiException is ClusterException)
				{
					clusDbHungAction.TakeAction = false;
					clusDbHungAction.Reason = "ClusterException was caught.";
				}
			}
			else
			{
				clusDbHungAction.TakeAction = true;
				clusDbHungAction.Reason = "Hung node detected without any Exceptions caught.";
			}
			if (clusDbHungAction.TargetNodes == null || clusDbHungAction.TargetNodes.Length < 1)
			{
				clusDbHungAction.TakeAction = false;
				clusDbHungAction.Reason = "No hung node detected, and Rpc timeout did not catch anything.";
				if (hungInfo.RpcFailedNodes != null && hungInfo.RpcFailedNodes.Length > 0)
				{
					AmServerName amServerName = null;
					foreach (AmServerName amServerName2 in hungInfo.RpcFailedNodes)
					{
						AmNodeState amNodeState = AmNodeState.Unknown;
						if (hungInfo.ClusterNodesStatus.TryGetValue(amServerName2.NetbiosName, out amNodeState) && amNodeState != AmNodeState.Unknown && amNodeState != AmNodeState.Down)
						{
							amServerName = amServerName2;
							break;
						}
					}
					if (amServerName != null)
					{
						clusDbHungAction.TakeAction = true;
						clusDbHungAction.TargetNodes = new AmServerName[]
						{
							amServerName
						};
						clusDbHungAction.Reason = string.Format("Hung nodes detected via Rpc timeout. Node '{0}' chosen for action. Original list={1}", amServerName.NetbiosName, LatencyChecker.ConvertAmServerNamesToString(hungInfo.RpcFailedNodes));
					}
					else
					{
						clusDbHungAction.TakeAction = false;
						clusDbHungAction.TargetNodes = null;
						clusDbHungAction.Reason = string.Format("No nodes in Rpc non-responsive list are UP according to cluster. Skipping reboot. Original list={0}", LatencyChecker.ConvertAmServerNamesToString(hungInfo.RpcFailedNodes));
					}
				}
				if (!clusDbHungAction.TakeAction && !AmServerName.IsNullOrEmpty(hungInfo.CurrentLockOwnerName))
				{
					clusDbHungAction.TakeAction = true;
					clusDbHungAction.TargetNodes = new AmServerName[]
					{
						hungInfo.CurrentLockOwnerName
					};
					clusDbHungAction.Reason = string.Format("Could not find any hung nodes, so taking restart/reboot action for the lock owner '{0}'", hungInfo.CurrentLockOwnerName.NetbiosName);
				}
			}
			ReplayCrimsonEvents.HungNodeAnalysisResult.Log<string>(clusDbHungAction.ToString());
			return clusDbHungAction;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00012C20 File Offset: 0x00010E20
		internal static void ActOnClusDbHang(LatencyChecker.ClusDbHungAction action)
		{
			if (action != null && action.TakeAction && action.HungInfo != null)
			{
				ReplayCrimsonEvents.HungNodeRecoveryActionStart.Log<string>(LatencyChecker.ConvertAmServerNamesToString(action.TargetNodes));
				bool flag;
				if (RegistryParameters.IsKillClusterServiceOnClusApiHang)
				{
					flag = true;
					if (action.TargetNodes != null && action.TargetNodes.Length > 0)
					{
						AmServerName amServerName = action.TargetNodes[0];
						RpcKillServiceImpl.Reply reply = RpcKillServiceImpl.SendKillRequest(amServerName.Fqdn, "Clussvc", action.HungInfo.ApiHungStartTime.LocalTime, false, RegistryParameters.RpcKillServiceTimeoutInMSec);
						flag = (reply != null && reply.IsSucceeded && reply.IsSucceeded);
					}
				}
				else
				{
					flag = false;
					ReplayCrimsonEvents.SkippedSendingClussvcKillRequest.LogPeriodic(action.HungInfo.ApiName, TimeSpan.FromMinutes(15.0));
				}
				if (!flag)
				{
					string text = LatencyChecker.ConvertAmServerNamesToString(action.TargetNodes);
					ReplayCrimsonEvents.HungNodeRebootRequested.Log<string>(text);
					LatencyChecker.TriggerNodeRestart(action.HungInfo.CurrentGumId.ToString(), (action.HungInfo.CurrentLockOwnerName != null) ? action.HungInfo.CurrentLockOwnerName.NetbiosName : "NULL", text, action.HungInfo, action);
					return;
				}
			}
			else if (action == null || action.HungInfo == null)
			{
				ReplayCrimsonEvents.GenericMessage.Log<string>("ActOnClusDbHang: Action is null or action.HungInfo is null");
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00012D68 File Offset: 0x00010F68
		internal static void ReportClusApiHangLongLatency(object context)
		{
			LatencyChecker.LatencyContext latencyContext = (LatencyChecker.LatencyContext)context;
			TimeSpan timeSpan = ExDateTime.Now - latencyContext.StartTime;
			ReplayCrimsonEvents.ClusApiOperationAppearsToBeHungAlert.Log<string, ExDateTime, TimeSpan, string, TimeSpan>(latencyContext.ApiName, latencyContext.StartTime, timeSpan, latencyContext.HintStr, latencyContext.MaxAllowedLatency);
			ReplayEventLogConstants.Tuple_ClusterApiHungAlert.LogEvent(null, new object[]
			{
				latencyContext.ApiName,
				timeSpan.ToString()
			});
			LatencyChecker.RaiseRedEvent();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00012DE4 File Offset: 0x00010FE4
		internal static void OnClusApiHang(object context)
		{
			LatencyChecker.LatencyContext latencyContext = (LatencyChecker.LatencyContext)context;
			LatencyChecker.ClusDbHungInfo clusDbHungInfo = LatencyChecker.GatherHungNodesInformation(latencyContext);
			LatencyChecker.LastKnownHungInfo = clusDbHungInfo;
			LatencyChecker.ClusDbHungAction action = LatencyChecker.AnalyzeAndSuggestActionForClusDbHang(clusDbHungInfo);
			LatencyChecker.ActOnClusDbHang(action);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00012E14 File Offset: 0x00011014
		internal static int MeasureClusApi(string apiName, string hintStr, Func<int> func)
		{
			TimeSpan timeSpan;
			return LatencyChecker.Measure(apiName, hintStr, TimeSpan.FromSeconds((double)RegistryParameters.ClusApiLatencyAllowedInSec), TimeSpan.MaxValue, null, func, out timeSpan);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00012E3C File Offset: 0x0001103C
		internal static int MeasureClusApiAndKillIfExceeds(string apiName, string hintStr, Func<int> func)
		{
			TimerCallback latencyCallback = null;
			if (LatencyChecker.EnableClusterKill)
			{
				latencyCallback = new TimerCallback(LatencyChecker.OnClusApiHang);
			}
			TimeSpan currentLatency;
			int result = LatencyChecker.Measure(apiName, hintStr, TimeSpan.FromSeconds((double)RegistryParameters.ClusApiLatencyAllowedInSec), TimeSpan.FromSeconds((double)RegistryParameters.ClusApiHangActionLatencyAllowedInSec), latencyCallback, func, out currentLatency);
			LatencyChecker.RaiseGreenEventIfNeeded(currentLatency);
			return result;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00012EA0 File Offset: 0x000110A0
		internal static TimeSpan Measure(string apiName, string hintStr, TimeSpan maxAllowedLatency, Action action)
		{
			TimeSpan result;
			LatencyChecker.Measure(apiName, hintStr, maxAllowedLatency, TimeSpan.MaxValue, null, delegate()
			{
				action();
				return 0;
			}, out result);
			return result;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00012ED8 File Offset: 0x000110D8
		internal static int Measure(string apiName, string hintStr, TimeSpan maxAllowedLatency, TimeSpan maxAllowedLatencyForTimer, TimerCallback latencyCallback, Func<int> func, out TimeSpan elapsed)
		{
			ExDateTime now = ExDateTime.Now;
			int num = 0;
			bool flag = true;
			Timer timer = null;
			Timer timer2 = null;
			try
			{
				if (latencyCallback != null && maxAllowedLatencyForTimer.TotalSeconds > 0.0)
				{
					LatencyChecker.LatencyContext latencyContext = new LatencyChecker.LatencyContext(now, apiName, hintStr, maxAllowedLatencyForTimer);
					timer = new Timer(latencyCallback, latencyContext, -1, -1);
					latencyContext.Timer = timer;
					timer.Change(maxAllowedLatencyForTimer, TimeSpan.FromMilliseconds(-1.0));
					timer2 = new Timer(new TimerCallback(LatencyChecker.ReportClusApiHangLongLatency), latencyContext, -1, -1);
					TimeSpan dueTime = TimeSpan.FromSeconds((double)RegistryParameters.ClusApiHangReportLongLatencyDurationInSec);
					timer2.Change(dueTime, TimeSpan.FromMilliseconds(-1.0));
				}
				if (RegistryParameters.IsApiLatencyTestEnabled)
				{
					LatencyChecker.DelayApiIfRequired(apiName);
					num = RegistryParameters.GetApiSimulatedErrorCode(apiName);
					if (num == 0)
					{
						num = func();
					}
					else
					{
						NativeMethods.SetLastError(num);
					}
				}
				else
				{
					num = func();
				}
				flag = false;
			}
			finally
			{
				if (flag)
				{
					num = -1;
				}
				if (timer != null)
				{
					timer.Change(-1, -1);
					timer.Dispose();
				}
				if (timer2 != null)
				{
					timer2.Change(-1, -1);
					timer2.Dispose();
				}
				elapsed = ExDateTime.Now - now;
				ExTraceGlobals.LatencyCheckerTracer.TraceDebug(0L, "Api={0}, StartTime={1}, Elapsed={2}, Hint={3}, IsUnhandled={4}, RetCode={5}, MaxLatency={6}", new object[]
				{
					apiName,
					now,
					elapsed,
					hintStr,
					flag,
					num,
					maxAllowedLatency
				});
				if (elapsed > maxAllowedLatency || (num != 0 && RegistryParameters.GetIsLogApiLatencyFailure()))
				{
					ReplayCrimsonEvents.OperationTookVeryLongTimeToComplete.Log<string, ExDateTime, TimeSpan, string, bool, int, TimeSpan>(apiName, now, elapsed, hintStr, flag, num, maxAllowedLatency);
				}
			}
			return num;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00013094 File Offset: 0x00011294
		private static void DelayApiIfRequired(string apiName)
		{
			if (RegistryParameters.IsApiLatencyTestEnabled)
			{
				int num = 0;
				while (!AmSystemManager.Instance.IsShutdown)
				{
					int apiLatencyInSec = RegistryParameters.GetApiLatencyInSec(apiName);
					if (num >= apiLatencyInSec)
					{
						break;
					}
					Thread.Sleep(1000);
					num++;
				}
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000130D4 File Offset: 0x000112D4
		private static void TriggerNodeRestart(string currentGumId, string currentLockOwnerName, string hungNodeCsv, LatencyChecker.ClusDbHungInfo hungInfo, LatencyChecker.ClusDbHungAction hungAction)
		{
			EventNotificationItem eventNotificationItem = new EventNotificationItem("MSExchangeRepl", "Cluster", "ClusterNodeRestart", string.Format("Cluster Hung detected. GumId={0}, LockOwner={1}, HungNodes={2}, HungInfo={3}, Decision={4}", new object[]
			{
				currentGumId,
				currentLockOwnerName,
				hungNodeCsv,
				hungInfo.ToString(),
				hungAction.ToString()
			}), hungNodeCsv, ResultSeverityLevel.Critical);
			eventNotificationItem.Publish(false);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00013130 File Offset: 0x00011330
		private static void RaiseRedEvent()
		{
			string arg = string.Empty;
			string arg2 = string.Empty;
			if (LatencyChecker.LastKnownHungInfo != null)
			{
				arg = LatencyChecker.ConvertAmServerNamesToString(LatencyChecker.LastKnownHungInfo.HungNodes);
				arg2 = LatencyChecker.ConvertAmServerNamesToString(LatencyChecker.LastKnownHungInfo.RpcFailedNodes);
			}
			new EventNotificationItem("MSExchangeRepl", "Cluster", "ClusterHung", string.Format("ClusDb write timed out. HungNodeInfo={0}", (LatencyChecker.LastKnownHungInfo == null) ? "NULL" : LatencyChecker.LastKnownHungInfo.ToString()), string.Format("HungNodeApi={0}", arg), ResultSeverityLevel.Critical)
			{
				StateAttribute2 = string.Format("RpcHungNode={0}", arg2)
			}.Publish(false);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000131CC File Offset: 0x000113CC
		private static void RaiseGreenEventIfNeeded(TimeSpan currentLatency)
		{
			if (currentLatency.TotalMilliseconds < 15000.0 && ExDateTime.Now - LatencyChecker.lastGreenEventRaisedTime > TimeSpan.FromMinutes(5.0))
			{
				EventNotificationItem eventNotificationItem = new EventNotificationItem("MSExchangeRepl", "Cluster", "ClusterHung", "ClusDb write completed normally", ((int)currentLatency.TotalMilliseconds).ToString(), ResultSeverityLevel.Informational);
				eventNotificationItem.Publish(false);
				eventNotificationItem = new EventNotificationItem("MSExchangeRepl", "Cluster", "ClusterNodeRestart", "ClusDb write completed normally", ((int)currentLatency.TotalMilliseconds).ToString(), ResultSeverityLevel.Informational);
				eventNotificationItem.Publish(false);
				eventNotificationItem = new EventNotificationItem("MSExchangeRepl", "Cluster", "HammerDown", "ClusDb write completed normally", ((int)currentLatency.TotalMilliseconds).ToString(), ResultSeverityLevel.Informational);
				eventNotificationItem.Publish(false);
				eventNotificationItem = new EventNotificationItem("ExCapacity", "NodeEvicted", "RepeatedlyOffendingNode", "ClusDb write completed normally", ((int)currentLatency.TotalMilliseconds).ToString(), ResultSeverityLevel.Informational);
				eventNotificationItem.Publish(false);
				LatencyChecker.lastGreenEventRaisedTime = ExDateTime.Now;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000132F0 File Offset: 0x000114F0
		private static string ConvertAmServerNamesToString(IEnumerable<AmServerName> servers)
		{
			if (servers != null && servers.Count<AmServerName>() > 0)
			{
				return string.Join(",", from s in servers
				select s.NetbiosName);
			}
			return string.Empty;
		}

		// Token: 0x0400016C RID: 364
		private const string NotificationItemServiceName = "MSExchangeRepl";

		// Token: 0x0400016D RID: 365
		private const string NotificationItemClusterComponentName = "Cluster";

		// Token: 0x0400016E RID: 366
		private const string NotificationItemClusNodeRestartTag = "ClusterNodeRestart";

		// Token: 0x0400016F RID: 367
		private const string NotificationItemClusDbHungTag = "ClusterHung";

		// Token: 0x04000170 RID: 368
		private const string NotificationItemClusHammerDownTag = "HammerDown";

		// Token: 0x04000171 RID: 369
		private const string NotificationItemCapacityServiceName = "ExCapacity";

		// Token: 0x04000172 RID: 370
		private const string NotificationItemCapacityComponentName = "NodeEvicted";

		// Token: 0x04000173 RID: 371
		private const string NotificationItemCapacityTagName = "RepeatedlyOffendingNode";

		// Token: 0x04000174 RID: 372
		private const string NotificationItemClusDbWriteSuccessMessage = "ClusDb write completed normally";

		// Token: 0x04000175 RID: 373
		private const string NotificationItemClusDbWriteFailureMessage = "Cluster Hung detected. GumId={0}, LockOwner={1}, HungNodes={2}, HungInfo={3}, Decision={4}";

		// Token: 0x04000176 RID: 374
		private const string NotificationItemClusDbWriteTimeoutFailureMessage = "ClusDb write timed out. HungNodeInfo={0}";

		// Token: 0x04000177 RID: 375
		private static readonly TimeSpan AdCacheTimeout = TimeSpan.FromMinutes(15.0);

		// Token: 0x04000178 RID: 376
		private static ExDateTime lastGreenEventRaisedTime = ExDateTime.MinValue;

		// Token: 0x04000179 RID: 377
		private static LatencyChecker.ClusDbHungInfo LastKnownHungInfo = null;

		// Token: 0x0400017A RID: 378
		private static ExDateTime adCacheLastUpdateTime = ExDateTime.MinValue;

		// Token: 0x0200004D RID: 77
		internal class LatencyContext
		{
			// Token: 0x170000BF RID: 191
			// (get) Token: 0x0600034E RID: 846 RVA: 0x00013373 File Offset: 0x00011573
			// (set) Token: 0x0600034F RID: 847 RVA: 0x0001337B File Offset: 0x0001157B
			internal ExDateTime StartTime { get; set; }

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000350 RID: 848 RVA: 0x00013384 File Offset: 0x00011584
			// (set) Token: 0x06000351 RID: 849 RVA: 0x0001338C File Offset: 0x0001158C
			internal string ApiName { get; set; }

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000352 RID: 850 RVA: 0x00013395 File Offset: 0x00011595
			// (set) Token: 0x06000353 RID: 851 RVA: 0x0001339D File Offset: 0x0001159D
			internal string HintStr { get; set; }

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000354 RID: 852 RVA: 0x000133A6 File Offset: 0x000115A6
			// (set) Token: 0x06000355 RID: 853 RVA: 0x000133AE File Offset: 0x000115AE
			internal TimeSpan MaxAllowedLatency { get; set; }

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000356 RID: 854 RVA: 0x000133B7 File Offset: 0x000115B7
			// (set) Token: 0x06000357 RID: 855 RVA: 0x000133BF File Offset: 0x000115BF
			internal Timer Timer { get; set; }

			// Token: 0x06000358 RID: 856 RVA: 0x000133C8 File Offset: 0x000115C8
			internal LatencyContext(ExDateTime startTime, string apiName, string hintStr, TimeSpan maxAllowedLatency)
			{
				this.StartTime = startTime;
				this.ApiName = apiName;
				this.HintStr = hintStr;
				this.MaxAllowedLatency = maxAllowedLatency;
				this.Timer = null;
			}

			// Token: 0x06000359 RID: 857 RVA: 0x000133F4 File Offset: 0x000115F4
			public override string ToString()
			{
				return string.Format("StartTime: '{0}' ApiName: '{1}' HintStr: '{2}' MaxAllowedLatency: '{3}'", new object[]
				{
					this.StartTime.ToString("o"),
					this.ApiName,
					this.HintStr,
					this.MaxAllowedLatency
				});
			}
		}

		// Token: 0x0200004E RID: 78
		internal class ClusDbHungInfo
		{
			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x0600035A RID: 858 RVA: 0x00013449 File Offset: 0x00011649
			// (set) Token: 0x0600035B RID: 859 RVA: 0x00013451 File Offset: 0x00011651
			internal int CurrentGumId { get; set; }

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x0600035C RID: 860 RVA: 0x0001345A File Offset: 0x0001165A
			// (set) Token: 0x0600035D RID: 861 RVA: 0x00013462 File Offset: 0x00011662
			internal AmServerName CurrentLockOwnerName { get; set; }

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x0600035E RID: 862 RVA: 0x0001346B File Offset: 0x0001166B
			// (set) Token: 0x0600035F RID: 863 RVA: 0x00013473 File Offset: 0x00011673
			internal AmServerName[] HungNodes { get; set; }

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000360 RID: 864 RVA: 0x0001347C File Offset: 0x0001167C
			// (set) Token: 0x06000361 RID: 865 RVA: 0x00013484 File Offset: 0x00011684
			internal Exception HungNodeApiException { get; set; }

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000362 RID: 866 RVA: 0x0001348D File Offset: 0x0001168D
			// (set) Token: 0x06000363 RID: 867 RVA: 0x00013495 File Offset: 0x00011695
			internal Dictionary<string, AmNodeState> ClusterNodesStatus { get; set; }

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x06000364 RID: 868 RVA: 0x0001349E File Offset: 0x0001169E
			// (set) Token: 0x06000365 RID: 869 RVA: 0x000134A6 File Offset: 0x000116A6
			internal AmServerName[] RpcFailedNodes { get; set; }

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x06000366 RID: 870 RVA: 0x000134AF File Offset: 0x000116AF
			// (set) Token: 0x06000367 RID: 871 RVA: 0x000134B7 File Offset: 0x000116B7
			internal Exception[] RpcExceptions { get; set; }

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06000368 RID: 872 RVA: 0x000134C0 File Offset: 0x000116C0
			// (set) Token: 0x06000369 RID: 873 RVA: 0x000134C8 File Offset: 0x000116C8
			internal ExDateTime ApiHungStartTime { get; set; }

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x0600036A RID: 874 RVA: 0x000134D1 File Offset: 0x000116D1
			// (set) Token: 0x0600036B RID: 875 RVA: 0x000134D9 File Offset: 0x000116D9
			internal string ApiName { get; set; }

			// Token: 0x0600036C RID: 876 RVA: 0x0001350C File Offset: 0x0001170C
			public override string ToString()
			{
				string format = "CurrentGumId: '{0}' LockOwner: '{1}' ApiName: '{2}' ApiHungStartTime: '{3}' HungNodes: '{4}' RpcFailedNodes: '{5}' ClusterStatus: '{6}' HungNodeApiEx: '{7}' RpcExs: '{8}'";
				object[] array = new object[9];
				array[0] = this.CurrentGumId;
				array[1] = ((this.CurrentLockOwnerName == null) ? "NULL" : this.CurrentLockOwnerName.NetbiosName);
				array[2] = (string.IsNullOrEmpty(this.ApiName) ? "NULL" : this.ApiName);
				array[3] = this.ApiHungStartTime.ToString("o");
				array[4] = LatencyChecker.ConvertAmServerNamesToString(this.HungNodes);
				array[5] = LatencyChecker.ConvertAmServerNamesToString(this.RpcFailedNodes);
				object[] array2 = array;
				int num = 6;
				string text;
				if (this.ClusterNodesStatus != null && this.ClusterNodesStatus.Count >= 1)
				{
					text = string.Join(",", this.ClusterNodesStatus.Select((KeyValuePair<string, AmNodeState> pair, int sel) => string.Format("{0}={1}", pair.Key, pair.Value)));
				}
				else
				{
					text = "NULL";
				}
				array2[num] = text;
				array[7] = ((this.HungNodeApiException == null) ? "NULL" : this.HungNodeApiException.Message);
				object[] array3 = array;
				int num2 = 8;
				string text2;
				if (this.RpcExceptions != null && this.RpcExceptions.Length >= 1)
				{
					text2 = string.Join(",", from e in this.RpcExceptions
					select e.Message);
				}
				else
				{
					text2 = "NULL";
				}
				array3[num2] = text2;
				return string.Format(format, array);
			}
		}

		// Token: 0x0200004F RID: 79
		internal class ClusDbHungAction
		{
			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06000370 RID: 880 RVA: 0x0001366C File Offset: 0x0001186C
			// (set) Token: 0x06000371 RID: 881 RVA: 0x00013674 File Offset: 0x00011874
			internal bool TakeAction { get; set; }

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000372 RID: 882 RVA: 0x0001367D File Offset: 0x0001187D
			// (set) Token: 0x06000373 RID: 883 RVA: 0x00013685 File Offset: 0x00011885
			internal AmServerName[] TargetNodes { get; set; }

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000374 RID: 884 RVA: 0x0001368E File Offset: 0x0001188E
			// (set) Token: 0x06000375 RID: 885 RVA: 0x00013696 File Offset: 0x00011896
			internal string Reason { get; set; }

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000376 RID: 886 RVA: 0x0001369F File Offset: 0x0001189F
			// (set) Token: 0x06000377 RID: 887 RVA: 0x000136A7 File Offset: 0x000118A7
			internal LatencyChecker.ClusDbHungInfo HungInfo { get; set; }

			// Token: 0x06000378 RID: 888 RVA: 0x000136B0 File Offset: 0x000118B0
			public override string ToString()
			{
				return string.Format("TakeAction: '{0}' TargetNodes: '{1}' Reason: '{2}'", this.TakeAction, LatencyChecker.ConvertAmServerNamesToString(this.TargetNodes), string.IsNullOrEmpty(this.Reason) ? "NULL" : this.Reason);
			}
		}
	}
}
