using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Management.Search;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Responders;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Search.Core;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search
{
	// Token: 0x02000486 RID: 1158
	internal static class SearchMonitoringHelper
	{
		// Token: 0x06001D14 RID: 7444 RVA: 0x000ABA10 File Offset: 0x000A9C10
		static SearchMonitoringHelper()
		{
			MonitoringLogConfiguration configuration = new MonitoringLogConfiguration(ExchangeComponent.Search.Name);
			SearchMonitoringHelper.monitoringLogger = new MonitoringLogger(configuration);
			configuration = new MonitoringLogConfiguration(ExchangeComponent.Search.Name, "CopyStatusChange");
			SearchMonitoringHelper.copyStatusLogger = new MonitoringLogger(configuration);
			configuration = new MonitoringLogConfiguration(ExchangeComponent.Search.Name, "RecoveryActions");
			SearchMonitoringHelper.recoveryActionsLogger = new MonitoringLogger(configuration);
			configuration = new MonitoringLogConfiguration(ExchangeComponent.Search.Name, "NodeStateChange");
			SearchMonitoringHelper.nodeStateLogger = new MonitoringLogger(configuration);
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x000ABB43 File Offset: 0x000A9D43
		internal static DiagnosticInfo DiagnosticInfo
		{
			get
			{
				return SearchMonitoringHelper.diagnosticInfoCache;
			}
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x000ABB6C File Offset: 0x000A9D6C
		internal static ProbeResult GetLastProbeResult(ProbeWorkItem probe, IProbeWorkBroker broker, CancellationToken cancellationToken)
		{
			ProbeResult lastProbeResult = null;
			if (broker != null)
			{
				IOrderedEnumerable<ProbeResult> query = from r in broker.GetProbeResults(probe.Definition, probe.Result.ExecutionStartTime.AddSeconds((double)(-5 * probe.Definition.RecurrenceIntervalSeconds)))
				orderby r.ExecutionStartTime descending
				select r;
				Task<int> task = broker.AsDataAccessQuery<ProbeResult>(query).ExecuteAsync(delegate(ProbeResult r)
				{
					if (lastProbeResult == null)
					{
						lastProbeResult = r;
					}
				}, cancellationToken, SearchMonitoringHelper.traceContext);
				task.Wait(cancellationToken);
				return lastProbeResult;
			}
			if (ExEnvironment.IsTest)
			{
				return null;
			}
			throw new ArgumentNullException("broker");
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x000ABC1C File Offset: 0x000A9E1C
		internal static CopyStatusClientCachedEntry GetCachedLocalDatabaseCopyStatus(string databaseName)
		{
			if (string.IsNullOrWhiteSpace(databaseName))
			{
				throw new ArgumentException("databaseName");
			}
			Guid databaseGuidFromName = SearchMonitoringHelper.GetDatabaseGuidFromName(databaseName);
			CopyStatusClientCachedEntry result;
			try
			{
				CopyStatusClientCachedEntry dbCopyStatusOnLocalServer = CachedDbStatusReader.Instance.GetDbCopyStatusOnLocalServer(databaseGuidFromName);
				if (dbCopyStatusOnLocalServer == null)
				{
					SearchMonitoringHelper.LogInfo("GetDbCopyStatusOnLocalServer() for database '{0}' returns null.", new object[]
					{
						databaseGuidFromName
					});
				}
				result = dbCopyStatusOnLocalServer;
			}
			catch (Exception ex)
			{
				SearchMonitoringHelper.LogInfo("Exception caught calling GetDbCopyStatusOnLocalServer() for database '{0}': '{1}'.", new object[]
				{
					databaseGuidFromName,
					ex.Message
				});
				result = null;
			}
			return result;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x000ABCB4 File Offset: 0x000A9EB4
		internal static List<CopyStatusClientCachedEntry> GetCachedDatabaseCopyStatus(string databaseName)
		{
			if (string.IsNullOrWhiteSpace(databaseName))
			{
				throw new ArgumentException("databaseName");
			}
			Guid databaseGuidFromName = SearchMonitoringHelper.GetDatabaseGuidFromName(databaseName);
			List<CopyStatusClientCachedEntry> result;
			try
			{
				List<CopyStatusClientCachedEntry> allCopyStatusesForDatabase = CachedDbStatusReader.Instance.GetAllCopyStatusesForDatabase(databaseGuidFromName);
				result = allCopyStatusesForDatabase;
			}
			catch (Exception ex)
			{
				SearchMonitoringHelper.LogInfo("Exception caught calling GetAllCopyStatusesForDatabase() for database '{0}': '{1}'.", new object[]
				{
					databaseGuidFromName,
					ex.Message
				});
				result = null;
			}
			return result;
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000ABD2C File Offset: 0x000A9F2C
		internal static IndexStatus GetCachedLocalDatabaseIndexStatus(Guid databaseGuid, bool throwOnIndexStatusException = true)
		{
			if (SearchMonitoringHelper.indexStatusCache == null)
			{
				lock (SearchMonitoringHelper.indexStatusCacheTimeoutTimes)
				{
					if (SearchMonitoringHelper.indexStatusCache == null)
					{
						SearchMonitoringHelper.indexStatusCache = new Dictionary<Guid, IndexStatus>();
					}
				}
			}
			lock (SearchMonitoringHelper.indexStatusCache)
			{
				if (SearchMonitoringHelper.indexStatusCache.ContainsKey(databaseGuid) && SearchMonitoringHelper.indexStatusCacheTimeoutTimes[databaseGuid] > DateTime.UtcNow)
				{
					return SearchMonitoringHelper.indexStatusCache[databaseGuid];
				}
			}
			IndexStatus indexStatus = null;
			try
			{
				indexStatus = IndexStatusStore.Instance.GetIndexStatus(databaseGuid);
			}
			catch (IndexStatusException)
			{
				if (throwOnIndexStatusException)
				{
					throw;
				}
			}
			if (indexStatus != null)
			{
				lock (SearchMonitoringHelper.indexStatusCache)
				{
					SearchMonitoringHelper.indexStatusCacheTimeoutTimes[databaseGuid] = DateTime.UtcNow + SearchMonitoringHelper.IndexStatusCacheTimeout;
					SearchMonitoringHelper.indexStatusCache[databaseGuid] = indexStatus;
				}
			}
			return indexStatus;
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x000ABE58 File Offset: 0x000AA058
		internal static IndexStatus GetCachedLocalDatabaseIndexStatus(string databaseName, bool throwOnIndexStatusException = true)
		{
			if (string.IsNullOrWhiteSpace(databaseName))
			{
				throw new ArgumentException("databaseName");
			}
			Guid mailboxDatabaseGuid = SearchMonitoringHelper.GetDatabaseInfo(databaseName).MailboxDatabaseGuid;
			return SearchMonitoringHelper.GetCachedLocalDatabaseIndexStatus(mailboxDatabaseGuid, throwOnIndexStatusException);
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x000ABE8C File Offset: 0x000AA08C
		internal static MailboxDatabaseInfo GetDatabaseInfo(string databaseName)
		{
			if (string.IsNullOrWhiteSpace(databaseName))
			{
				throw new ArgumentException("databaseName");
			}
			lock (SearchMonitoringHelper.databaseInfoDict)
			{
				if (SearchMonitoringHelper.databaseInfoDict.ContainsKey(databaseName))
				{
					return SearchMonitoringHelper.databaseInfoDict[databaseName];
				}
			}
			ICollection<MailboxDatabaseInfo> mailboxDatabaseInfoCollectionForBackend = LocalEndpointManager.Instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
			MailboxDatabaseInfo result;
			lock (SearchMonitoringHelper.databaseInfoDict)
			{
				if (!SearchMonitoringHelper.databaseInfoDict.ContainsKey(databaseName))
				{
					SearchMonitoringHelper.databaseInfoDict.Clear();
					foreach (MailboxDatabaseInfo mailboxDatabaseInfo in mailboxDatabaseInfoCollectionForBackend)
					{
						SearchMonitoringHelper.databaseInfoDict.Add(mailboxDatabaseInfo.MailboxDatabaseName, mailboxDatabaseInfo);
					}
				}
				if (SearchMonitoringHelper.databaseInfoDict.ContainsKey(databaseName))
				{
					result = SearchMonitoringHelper.databaseInfoDict[databaseName];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x000ABFB4 File Offset: 0x000AA1B4
		internal static Guid GetDatabaseGuidFromName(string databaseName)
		{
			if (string.IsNullOrWhiteSpace(databaseName))
			{
				throw new ArgumentException("databaseName");
			}
			MailboxDatabaseInfo databaseInfo = SearchMonitoringHelper.GetDatabaseInfo(databaseName);
			if (databaseInfo == null)
			{
				throw new ArgumentException("databaseName");
			}
			return databaseInfo.MailboxDatabaseGuid;
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000ABFF0 File Offset: 0x000AA1F0
		internal static bool IsCatalogDisabled(string databaseName)
		{
			if (string.IsNullOrWhiteSpace(databaseName))
			{
				throw new ArgumentException("databaseName");
			}
			IndexStatus cachedLocalDatabaseIndexStatus = SearchMonitoringHelper.GetCachedLocalDatabaseIndexStatus(databaseName, true);
			return cachedLocalDatabaseIndexStatus.IndexingState == ContentIndexStatusType.Disabled;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x000AC024 File Offset: 0x000AA224
		internal static bool IsCatalogSeeding(string databaseName)
		{
			if (string.IsNullOrWhiteSpace(databaseName))
			{
				throw new ArgumentException("databaseName");
			}
			IndexStatus cachedLocalDatabaseIndexStatus = SearchMonitoringHelper.GetCachedLocalDatabaseIndexStatus(databaseName, true);
			return cachedLocalDatabaseIndexStatus.IndexingState == ContentIndexStatusType.Seeding;
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x000AC058 File Offset: 0x000AA258
		internal static bool IsDatabaseActive(string databaseName)
		{
			if (string.IsNullOrWhiteSpace(databaseName))
			{
				throw new ArgumentException("databaseName");
			}
			Guid mailboxDatabaseGuid = SearchMonitoringHelper.GetDatabaseInfo(databaseName).MailboxDatabaseGuid;
			return SearchMonitoringHelper.IsDatabaseActive(mailboxDatabaseGuid);
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x000AC08A File Offset: 0x000AA28A
		internal static bool IsDatabaseActive(Guid databaseGuid)
		{
			return DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(databaseGuid);
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x000AC098 File Offset: 0x000AA298
		internal static Dictionary<string, int> GetNodeProcessIds()
		{
			NoderunnerResourceHelper noderunnerResourceHelper = new NoderunnerResourceHelper();
			return noderunnerResourceHelper.ProcessDictionary;
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x000AC0B4 File Offset: 0x000AA2B4
		internal static Process[] GetProcessesForNodeRunner(string nodeRunnerInstanceName)
		{
			Dictionary<string, int> nodeProcessIds = SearchMonitoringHelper.GetNodeProcessIds();
			foreach (string text in nodeProcessIds.Keys)
			{
				if (nodeRunnerInstanceName.EndsWith(text, StringComparison.OrdinalIgnoreCase))
				{
					return new Process[]
					{
						Process.GetProcessById(nodeProcessIds[text])
					};
				}
			}
			return new Process[0];
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x000AC138 File Offset: 0x000AA338
		internal static void CleanUpOrphanedWerProcesses()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			string queryString = string.Format("SELECT ProcessId, ParentProcessId, CommandLine from Win32_Process WHERE Name LIKE \"WerFault.exe\" OR Name LIKE \"WerMgr.exe\"", new object[0]);
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(queryString))
			{
				using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						try
						{
							if (managementObject["CommandLine"] != null && managementObject["ProcessId"] != null && managementObject["ParentProcessId"] != null)
							{
								string text = managementObject["CommandLine"].ToString();
								int num = int.Parse(managementObject["ProcessId"].ToString());
								if (text.IndexOf("WerFault.exe", StringComparison.OrdinalIgnoreCase) >= 0)
								{
									Match match = Regex.Match(text, "-p ([0-9]+)", RegexOptions.IgnoreCase);
									if (match.Success)
									{
										SearchMonitoringHelper.LogInfo("CleanUpOrphanedWerProcesses: WerFault.exe process found with ID {0}, command line '{1}'.", new object[]
										{
											num,
											text
										});
										int value = int.Parse(match.Groups[1].Value);
										dictionary.Add(num, value);
									}
								}
								else if (text.IndexOf("WerMgr.exe", StringComparison.OrdinalIgnoreCase) >= 0)
								{
									int num2 = int.Parse(managementObject["ParentProcessId"].ToString());
									SearchMonitoringHelper.LogInfo("CleanUpOrphanedWerProcesses: WerMgr.exe process found with ID {0}, parent processID {1}.", new object[]
									{
										num,
										num2
									});
									dictionary.Add(num, num2);
								}
							}
						}
						finally
						{
							managementObject.Dispose();
						}
					}
				}
			}
			foreach (int num3 in dictionary.Keys)
			{
				int processId = dictionary[num3];
				using (Process processByIdBestEffort = ProcessHelper.GetProcessByIdBestEffort(processId))
				{
					if (processByIdBestEffort == null)
					{
						using (Process processByIdBestEffort2 = ProcessHelper.GetProcessByIdBestEffort(num3))
						{
							if (processByIdBestEffort2 != null)
							{
								SearchMonitoringHelper.LogInfo("CleanUpOrphanedWerProcesses: Orphaned WER process found with ID {0}. Killing.", new object[]
								{
									num3
								});
								ProcessHelper.KillProcess(processByIdBestEffort2, false, "SearchMonitoringHelper.CleanUpOrphanedWerProcesses()");
							}
						}
					}
				}
			}
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x000AC43C File Offset: 0x000AA63C
		internal static DateTime? GetRecentGracefulDegradationExecutionTime()
		{
			SearchMonitoringHelper.RefreshDiagnosticInfo();
			DateTime? recentGracefulDegradationExecutionTime;
			lock (SearchMonitoringHelper.diagnosticInfoCacheLock)
			{
				if (SearchMonitoringHelper.diagnosticInfoCache != null)
				{
					recentGracefulDegradationExecutionTime = SearchMonitoringHelper.diagnosticInfoCache.RecentGracefulDegradationExecutionTime;
				}
				else
				{
					if (SearchMonitoringHelper.getDiagnosticInfoException != null)
					{
						throw SearchMonitoringHelper.getDiagnosticInfoException;
					}
					throw new InvalidOperationException();
				}
			}
			return recentGracefulDegradationExecutionTime;
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x000AC4A0 File Offset: 0x000AA6A0
		internal static DiagnosticInfo.FeedingControllerDiagnosticInfo GetCachedFeedingControllerDiagnosticInfo(string databaseName)
		{
			if (string.IsNullOrWhiteSpace(databaseName))
			{
				throw new ArgumentException("databaseName");
			}
			SearchMonitoringHelper.RefreshDiagnosticInfo();
			DiagnosticInfo.FeedingControllerDiagnosticInfo feedingControllerDiagnosticInfo;
			lock (SearchMonitoringHelper.diagnosticInfoCacheLock)
			{
				if (SearchMonitoringHelper.diagnosticInfoCache != null)
				{
					feedingControllerDiagnosticInfo = SearchMonitoringHelper.diagnosticInfoCache.GetFeedingControllerDiagnosticInfo(SearchMonitoringHelper.GetDatabaseGuidFromName(databaseName));
				}
				else
				{
					if (SearchMonitoringHelper.getDiagnosticInfoException != null)
					{
						throw SearchMonitoringHelper.getDiagnosticInfoException;
					}
					throw new InvalidOperationException();
				}
			}
			return feedingControllerDiagnosticInfo;
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x000AC520 File Offset: 0x000AA720
		internal static long GetPerformanceCounterValue(string categoryName, string counterName, string instanceName)
		{
			string text = string.Join("\\", new string[]
			{
				categoryName,
				counterName,
				instanceName
			});
			PerformanceCounter performanceCounter = null;
			lock (SearchMonitoringHelper.perfCounters)
			{
				SearchMonitoringHelper.perfCounters.TryGetValue(text, out performanceCounter);
			}
			if (performanceCounter != null)
			{
				try
				{
					return performanceCounter.RawValue;
				}
				catch (Exception ex)
				{
					SearchMonitoringHelper.LogInfo("Fail to reuse the PerformanceCounter instance '{0}'. Exception: '{1}'.", new object[]
					{
						text,
						ex
					});
					try
					{
						performanceCounter.Dispose();
					}
					catch
					{
					}
				}
			}
			performanceCounter = new PerformanceCounter(categoryName, counterName, instanceName, true);
			lock (SearchMonitoringHelper.perfCounters)
			{
				SearchMonitoringHelper.perfCounters[text] = performanceCounter;
			}
			return performanceCounter.RawValue;
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x000AC62C File Offset: 0x000AA82C
		internal static List<EventRecord> GetEvents(string logName, int eventId, string providerName, int timePeriodSeconds, int maxCount, Func<EventRecord, bool> condition = null)
		{
			TimeSpan timeout = TimeSpan.FromSeconds(30.0);
			string query = string.Format("*[System[(EventID={0})] and System[Provider[@Name=\"{1}\"] and TimeCreated[timediff(@SystemTime) <= {2}]]]", eventId, providerName, timePeriodSeconds * 1000);
			List<EventRecord> list = new List<EventRecord>();
			using (EventLogReader eventLogReader = new EventLogReader(new EventLogQuery(logName, PathType.LogName, query)
			{
				ReverseDirection = true
			}))
			{
				EventRecord eventRecord = eventLogReader.ReadEvent(timeout);
				while (eventRecord != null)
				{
					if (condition != null && !condition(eventRecord))
					{
						eventRecord.Dispose();
					}
					else
					{
						list.Add(eventRecord);
						if (list.Count >= maxCount)
						{
							return list;
						}
						eventRecord = eventLogReader.ReadEvent(timeout);
					}
				}
			}
			return list;
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x000AC6F0 File Offset: 0x000AA8F0
		internal static void SetNotificationServiceClass(ProbeResult result, NotificationServiceClass notificationServiceClass)
		{
			result.StateAttribute22 = notificationServiceClass.ToString();
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x000AC758 File Offset: 0x000AA958
		private static void RefreshDiagnosticInfo()
		{
			lock (SearchMonitoringHelper.diagnosticInfoCacheLock)
			{
				if (DateTime.UtcNow > SearchMonitoringHelper.diagnosticInfoCacheTimeoutTime)
				{
					DiagnosticInfo diagnosticInformation = null;
					Exception ex = null;
					Action delegateGetDiagnosticInfo = delegate()
					{
						try
						{
							diagnosticInformation = new DiagnosticInfo(Environment.MachineName);
						}
						catch (Exception ex)
						{
							ex = ex;
						}
					};
					IAsyncResult asyncResult = delegateGetDiagnosticInfo.BeginInvoke(delegate(IAsyncResult r)
					{
						delegateGetDiagnosticInfo.EndInvoke(r);
					}, null);
					if (!asyncResult.AsyncWaitHandle.WaitOne(SearchMonitoringHelper.GetDiagnosticInfoCallTimeout))
					{
						ex = new TimeoutException(Strings.SearchGetDiagnosticInfoTimeout((int)SearchMonitoringHelper.GetDiagnosticInfoCallTimeout.TotalSeconds));
					}
					SearchMonitoringHelper.diagnosticInfoCacheTimeoutTime = DateTime.UtcNow + SearchMonitoringHelper.DiagnosticInfoCacheTimeout;
					SearchMonitoringHelper.diagnosticInfoCache = diagnosticInformation;
					SearchMonitoringHelper.getDiagnosticInfoException = ex;
				}
			}
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x000AC844 File Offset: 0x000AAA44
		internal static ResponderDefinition CreateRestartSearchServiceResponderDefinition(MaintenanceWorkItem maintenanceWorkItem, MonitorDefinition monitorDefinition, ServiceHealthStatus healthState, bool enabled)
		{
			int serviceStopTimeoutInSeconds = int.Parse(maintenanceWorkItem.Definition.Attributes["RestartSearchServiceStopTimeoutInSeconds"]);
			string responderName = SearchStrings.SearchRestartServiceResponderName(monitorDefinition.Name);
			string monitorName = monitorDefinition.ConstructWorkItemResultName();
			ResponderDefinition responderDefinition = RestartServiceResponder.CreateDefinition(responderName, monitorName, SearchDiscovery.ServiceName, healthState, serviceStopTimeoutInSeconds, 120, 0, false, DumpMode.None, null, 15.0, 0, "Exchange", null, true, true, null, false);
			responderDefinition.ServiceName = ExchangeComponent.Search.Name;
			responderDefinition.TargetResource = monitorDefinition.TargetResource;
			responderDefinition.Enabled = enabled;
			responderDefinition.RecurrenceIntervalSeconds = 0;
			return responderDefinition;
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x000AC8D4 File Offset: 0x000AAAD4
		internal static ResponderDefinition CreateRestartHostControllerServiceResponderDefinition(MaintenanceWorkItem maintenanceWorkItem, MonitorDefinition monitorDefinition, ServiceHealthStatus healthState, bool enabled)
		{
			int serviceStopTimeoutInSeconds = int.Parse(maintenanceWorkItem.Definition.Attributes["RestartHostControllerServiceStopTimeoutInSeconds"]);
			string responderName = SearchStrings.SearchRestartHostControllerServiceResponderName(monitorDefinition.Name);
			string alertMask = monitorDefinition.ConstructWorkItemResultName();
			ResponderDefinition responderDefinition = RestartHostControllerServiceResponder2.CreateDefinition(responderName, alertMask, healthState, serviceStopTimeoutInSeconds, 120, 0, DumpMode.None, null, 15.0, 0, "Dag");
			responderDefinition.TargetResource = monitorDefinition.TargetResource;
			responderDefinition.Enabled = enabled;
			return responderDefinition;
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x000AC944 File Offset: 0x000AAB44
		internal static ResponderDefinition CreateRestartNodeResponderDefinition(MaintenanceWorkItem maintenanceWorkItem, MonitorDefinition monitorDefinition, string nodeNames, ServiceHealthStatus healthState, bool enabled)
		{
			int nodeStopTimeoutInSeconds = int.Parse(maintenanceWorkItem.Definition.Attributes["RestartNodeStopTimeoutInSeconds"]);
			string responderName = SearchStrings.HostControllerServiceRestartNodeResponderName(monitorDefinition.Name);
			string alertMask = monitorDefinition.ConstructWorkItemResultName();
			ResponderDefinition responderDefinition = RestartNodeResponder.CreateDefinition(responderName, alertMask, healthState, nodeNames, nodeStopTimeoutInSeconds, "Dag");
			responderDefinition.TargetResource = monitorDefinition.TargetResource;
			responderDefinition.Enabled = enabled;
			return responderDefinition;
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x000AC9A4 File Offset: 0x000AABA4
		internal static ResponderDefinition CreateDatabaseFailoverResponderDefinition(MaintenanceWorkItem maintenanceWorkItem, MonitorDefinition monitorDefinition, ServiceHealthStatus healthState, bool enabled)
		{
			string targetResource = monitorDefinition.TargetResource;
			Guid mailboxDatabaseGuid = SearchMonitoringHelper.GetDatabaseInfo(targetResource).MailboxDatabaseGuid;
			string alertMask = monitorDefinition.ConstructWorkItemResultName();
			ResponderDefinition responderDefinition = DatabaseFailoverResponder.CreateDefinition(SearchStrings.SearchDatabaseFailoverResponderName(monitorDefinition.Name), ExchangeComponent.Search.Name, "*", alertMask, targetResource, ExchangeComponent.Search.Name, mailboxDatabaseGuid, healthState);
			responderDefinition.TypeName = typeof(SearchDatabaseFailoverResponder).FullName;
			responderDefinition.RecurrenceIntervalSeconds = 0;
			responderDefinition.Enabled = enabled;
			return responderDefinition;
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x000ACA20 File Offset: 0x000AAC20
		internal static ResponderDefinition CreateEscalateResponderDefinition(MonitorDefinition monitorDefinition, string escalationMessage, bool enabled, ServiceHealthStatus serviceHealthStatus = ServiceHealthStatus.None, SearchEscalateResponder.EscalateModes escalateMode = SearchEscalateResponder.EscalateModes.Scheduled, bool urgentInTraining = true)
		{
			string name = SearchStrings.SearchEscalateResponderName(monitorDefinition.Name);
			string text = monitorDefinition.ConstructWorkItemResultName();
			string text2 = escalationMessage.Split(new char[]
			{
				'.'
			})[0];
			if (LocalEndpointManager.IsDataCenter)
			{
				text2 = string.Format("({0}) - {1}", text, text2);
			}
			ResponderDefinition responderDefinition = SearchEscalateResponder.CreateDefinition(name, ExchangeComponent.Search.Name, monitorDefinition.Name, text, monitorDefinition.TargetResource, serviceHealthStatus, SearchMonitoringHelper.escalationTeam, text2, escalationMessage, escalateMode, urgentInTraining);
			responderDefinition.Enabled = enabled;
			return responderDefinition;
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x000ACAA0 File Offset: 0x000AACA0
		internal static MonitorDefinition CreateOverallConsecutiveProbeFailuresMonitorDefinition(string monitorName, string sampleMask, string targetResource, int recurrenceIntervalSeconds, int monitoringThreshold, int monitoringInterval, bool enabled)
		{
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(monitorName, sampleMask, ExchangeComponent.Search.Name, ExchangeComponent.Search, monitoringThreshold, true, 300);
			monitorDefinition.TargetResource = targetResource;
			monitorDefinition.RecurrenceIntervalSeconds = 0;
			monitorDefinition.MonitoringIntervalSeconds = monitoringInterval;
			monitorDefinition.Enabled = enabled;
			return monitorDefinition;
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x000ACAEC File Offset: 0x000AACEC
		internal static MonitorDefinition CreateMonitorDefinition(string monitorName, Type monitorType, string sampleMask, string targetResource, int recurrenceIntervalSeconds, int monitoringIntervalSeconds, int monitoringThreshold, bool enabled)
		{
			MonitorDefinition monitorDefinition = new MonitorDefinition();
			monitorDefinition.Name = monitorName;
			monitorDefinition.AssemblyPath = monitorType.Assembly.Location;
			monitorDefinition.TypeName = monitorType.FullName;
			monitorDefinition.SampleMask = sampleMask;
			monitorDefinition.ServiceName = ExchangeComponent.Search.Name;
			monitorDefinition.TargetResource = targetResource;
			monitorDefinition.Component = ExchangeComponent.Search;
			monitorDefinition.RecurrenceIntervalSeconds = 0;
			monitorDefinition.InsufficientSamplesIntervalSeconds = Math.Max(5 * monitorDefinition.RecurrenceIntervalSeconds, Convert.ToInt32(ConfigurationManager.AppSettings["InsufficientSamplesIntervalInSeconds"]));
			monitorDefinition.TimeoutSeconds = recurrenceIntervalSeconds;
			monitorDefinition.MonitoringIntervalSeconds = monitoringIntervalSeconds;
			monitorDefinition.MonitoringThreshold = (double)monitoringThreshold;
			monitorDefinition.Enabled = enabled;
			return monitorDefinition;
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x000ACB9C File Offset: 0x000AAD9C
		internal static ProbeDefinition CreateProbeDefinition(string probeName, Type probeType, string targetResource, int recurrenceIntervalSeconds, bool enabled)
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = probeType.Assembly.Location;
			probeDefinition.TypeName = probeType.FullName;
			probeDefinition.Name = probeName;
			probeDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			probeDefinition.TimeoutSeconds = recurrenceIntervalSeconds;
			probeDefinition.MaxRetryAttempts = 3;
			probeDefinition.TargetResource = targetResource;
			probeDefinition.ServiceName = ExchangeComponent.Search.Name;
			probeDefinition.Enabled = enabled;
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.SearchTracer, SearchMonitoringHelper.traceContext, "SearchDiscovery.CreateProbeDefinition: Created ProbeDefinition '{0}' for '{1}'.", probeName, targetResource, null, "CreateProbeDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Search\\SearchMonitoringHelper.cs", 1151);
			return probeDefinition;
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000ACC30 File Offset: 0x000AAE30
		internal static void CreateResponderChainForMonitor(MaintenanceWorkItem maintenanceWorkItem, string settingPrefix, MonitorDefinition monitorDefinition, string escalationMessage, bool enabled, bool databaseFailoverNeeded = false, bool restartSearchServiceNeeded = false, bool restartNodesNeeded = false, bool restartHostControllerServiceNeeded = false, SearchEscalateResponder.EscalateModes escalateMode = SearchEscalateResponder.EscalateModes.Scheduled, bool urgentInTraining = true)
		{
			AttributeHelper attributeHelper = new AttributeHelper(maintenanceWorkItem.Definition);
			ServiceHealthStatus[] array = new ServiceHealthStatus[]
			{
				ServiceHealthStatus.Unhealthy,
				ServiceHealthStatus.Unhealthy1,
				ServiceHealthStatus.Unhealthy2
			};
			int num = 0;
			List<MonitorStateTransition> list = new List<MonitorStateTransition>();
			if (restartSearchServiceNeeded)
			{
				ServiceHealthStatus serviceHealthStatus = array[num];
				num++;
				int transitionTimeoutSeconds = 0;
				list.Add(new MonitorStateTransition(serviceHealthStatus, transitionTimeoutSeconds));
				bool @bool = attributeHelper.GetBool(settingPrefix + "RestartSearchServiceResponderEnabled", true, true);
				ResponderDefinition definition = SearchMonitoringHelper.CreateRestartSearchServiceResponderDefinition(maintenanceWorkItem, monitorDefinition, serviceHealthStatus, enabled && @bool);
				maintenanceWorkItem.Broker.AddWorkDefinition<ResponderDefinition>(definition, SearchMonitoringHelper.traceContext);
			}
			if (restartNodesNeeded)
			{
				ServiceHealthStatus serviceHealthStatus2 = array[num];
				num++;
				int transitionTimeoutSeconds2 = 0;
				if (serviceHealthStatus2 != ServiceHealthStatus.Unhealthy)
				{
					transitionTimeoutSeconds2 = attributeHelper.GetInt(string.Concat(new object[]
					{
						settingPrefix,
						"Monitor",
						serviceHealthStatus2,
						"StateSeconds"
					}), true, 0, null, null);
				}
				list.Add(new MonitorStateTransition(serviceHealthStatus2, transitionTimeoutSeconds2));
				bool bool2 = attributeHelper.GetBool(settingPrefix + "RestartNodesResponderEnabled", true, true);
				string @string = attributeHelper.GetString(settingPrefix + "RestartNodesNodeNames", false, string.Empty);
				ResponderDefinition definition2 = SearchMonitoringHelper.CreateRestartNodeResponderDefinition(maintenanceWorkItem, monitorDefinition, @string, serviceHealthStatus2, enabled && bool2);
				maintenanceWorkItem.Broker.AddWorkDefinition<ResponderDefinition>(definition2, SearchMonitoringHelper.traceContext);
			}
			if (restartHostControllerServiceNeeded)
			{
				ServiceHealthStatus serviceHealthStatus3 = array[num];
				num++;
				int transitionTimeoutSeconds3 = 0;
				if (serviceHealthStatus3 != ServiceHealthStatus.Unhealthy)
				{
					transitionTimeoutSeconds3 = attributeHelper.GetInt(string.Concat(new object[]
					{
						settingPrefix,
						"Monitor",
						serviceHealthStatus3,
						"StateSeconds"
					}), true, 0, null, null);
				}
				list.Add(new MonitorStateTransition(serviceHealthStatus3, transitionTimeoutSeconds3));
				bool bool3 = attributeHelper.GetBool(settingPrefix + "RestartHostControllerServiceResponderEnabled", true, true);
				ResponderDefinition definition3 = SearchMonitoringHelper.CreateRestartHostControllerServiceResponderDefinition(maintenanceWorkItem, monitorDefinition, serviceHealthStatus3, enabled && bool3);
				maintenanceWorkItem.Broker.AddWorkDefinition<ResponderDefinition>(definition3, SearchMonitoringHelper.traceContext);
			}
			if (databaseFailoverNeeded)
			{
				int transitionTimeoutSeconds4 = 0;
				if (num > 0)
				{
					transitionTimeoutSeconds4 = attributeHelper.GetInt(settingPrefix + "MonitorUnrecoverable1StateSeconds", true, 0, null, null);
				}
				list.Add(new MonitorStateTransition(ServiceHealthStatus.Unrecoverable1, transitionTimeoutSeconds4));
				bool bool4 = attributeHelper.GetBool(settingPrefix + "DatabaseFailoverResponderEnabled", true, true);
				ResponderDefinition definition4 = SearchMonitoringHelper.CreateDatabaseFailoverResponderDefinition(maintenanceWorkItem, monitorDefinition, ServiceHealthStatus.Unrecoverable1, enabled && bool4);
				maintenanceWorkItem.Broker.AddWorkDefinition<ResponderDefinition>(definition4, SearchMonitoringHelper.traceContext);
			}
			int @int = attributeHelper.GetInt(settingPrefix + "MonitorUnrecoverableStateSeconds", true, 0, null, null);
			list.Add(new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, @int));
			ResponderDefinition definition5 = SearchMonitoringHelper.CreateEscalateResponderDefinition(monitorDefinition, escalationMessage, enabled, ServiceHealthStatus.Unrecoverable, escalateMode, urgentInTraining);
			maintenanceWorkItem.Broker.AddWorkDefinition<ResponderDefinition>(definition5, SearchMonitoringHelper.traceContext);
			monitorDefinition.MonitorStateTransitions = list.ToArray();
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x000ACF2A File Offset: 0x000AB12A
		internal static void SetDiagnosticDefaults()
		{
			DiagnosticsSessionFactory.SetDefaults(Guid.Parse("a07f37cc-a2b2-4d4e-8dc6-8e198e8fa976"), "MSExchangeFastSearch", "Search Diagnostics Logs", Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\Search"), "SearchMonitoring_", "SearchMonitoringLogs");
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x000ACF5E File Offset: 0x000AB15E
		internal static void LogInfo(string message, params object[] messageArgs)
		{
			SearchMonitoringHelper.monitoringLogger.LogEvent(DateTime.UtcNow, message, messageArgs);
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x000ACF71 File Offset: 0x000AB171
		internal static void LogInfo(WorkItem workItem, string message, params object[] messageArgs)
		{
			SearchMonitoringHelper.monitoringLogger.LogEvent(DateTime.UtcNow, string.Format("{0}/{1}: ", workItem.Definition.Name, workItem.Definition.TargetResource) + message, messageArgs);
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x000ACFA9 File Offset: 0x000AB1A9
		internal static void LogRecoveryAction(WorkItem workItem, string message, params object[] messageArgs)
		{
			SearchMonitoringHelper.recoveryActionsLogger.LogEvent(DateTime.UtcNow, string.Format("{0}/{1}: ", workItem.Definition.Name, workItem.Definition.TargetResource) + message, messageArgs);
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x000ACFE1 File Offset: 0x000AB1E1
		internal static void LogStatusChange(string message, params object[] messageArgs)
		{
			SearchMonitoringHelper.copyStatusLogger.LogEvent(DateTime.UtcNow, message, messageArgs);
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x000ACFF4 File Offset: 0x000AB1F4
		internal static void LogNodeStateChange(string message, params object[] messageArgs)
		{
			SearchMonitoringHelper.nodeStateLogger.LogEvent(DateTime.UtcNow, message, messageArgs);
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x000AD008 File Offset: 0x000AB208
		internal static TimeSpan GetSystemUpTime()
		{
			if (SearchMonitoringHelper.machineBootTime == null)
			{
				using (PerformanceCounter performanceCounter = new PerformanceCounter("System", "System Up Time"))
				{
					performanceCounter.NextValue();
					double num = (double)performanceCounter.NextValue();
					SearchMonitoringHelper.machineBootTime = new DateTime?(DateTime.UtcNow.AddSeconds(-num));
				}
			}
			return DateTime.UtcNow - SearchMonitoringHelper.machineBootTime.Value;
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x000AD08C File Offset: 0x000AB28C
		internal static string GetAllLocalDatabaseCopyStatusString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in LocalEndpointManager.Instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
			{
				CopyStatusClientCachedEntry cachedLocalDatabaseCopyStatus = SearchMonitoringHelper.GetCachedLocalDatabaseCopyStatus(mailboxDatabaseInfo.MailboxDatabaseName);
				if (cachedLocalDatabaseCopyStatus != null && cachedLocalDatabaseCopyStatus.CopyStatus != null)
				{
					RpcDatabaseCopyStatus2 copyStatus = cachedLocalDatabaseCopyStatus.CopyStatus;
					if (copyStatus != null)
					{
						stringBuilder.AppendLine(Strings.SearchIndexCopyStatus(mailboxDatabaseInfo.MailboxDatabaseName, copyStatus.CopyStatus.ToString(), copyStatus.ContentIndexStatus.ToString(), copyStatus.ContentIndexErrorMessage));
					}
				}
			}
			if (stringBuilder.Length > 0)
			{
				return stringBuilder.ToString();
			}
			return Strings.SearchInformationNotAvailable.ToString();
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x000AD174 File Offset: 0x000AB374
		internal static string GetAllCopyStatusForDatabaseString(string databaseName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<CopyStatusClientCachedEntry> cachedDatabaseCopyStatus = SearchMonitoringHelper.GetCachedDatabaseCopyStatus(databaseName);
			if (cachedDatabaseCopyStatus != null)
			{
				foreach (CopyStatusClientCachedEntry copyStatusClientCachedEntry in cachedDatabaseCopyStatus)
				{
					if (copyStatusClientCachedEntry.Result != CopyStatusRpcResult.Success)
					{
						string copyName = string.Format("{0}\\{1}", databaseName, copyStatusClientCachedEntry.ServerContacted.NetbiosName);
						stringBuilder.AppendLine(Strings.SearchIndexCopyStatusError(copyName, copyStatusClientCachedEntry.Result.ToString(), (copyStatusClientCachedEntry.LastException != null) ? copyStatusClientCachedEntry.LastException.Message : string.Empty));
					}
					else
					{
						RpcDatabaseCopyStatus2 copyStatus = copyStatusClientCachedEntry.CopyStatus;
						string copyName = string.Format("{0}\\{1}", copyStatus.DBName, copyStatus.MailboxServer);
						stringBuilder.AppendLine(Strings.SearchIndexCopyStatus(copyName, copyStatus.CopyStatus.ToString(), copyStatus.ContentIndexStatus.ToString(), copyStatus.ContentIndexErrorMessage));
					}
				}
			}
			if (stringBuilder.Length > 0)
			{
				return stringBuilder.ToString();
			}
			return Strings.SearchInformationNotAvailable.ToString();
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x000AD2B4 File Offset: 0x000AB4B4
		internal static bool IsInMaintenance()
		{
			Server server = LocalServer.GetServer();
			return DirectoryAccessor.Instance.IsRecoveryActionsEnabledOffline(server.Name);
		}

		// Token: 0x040013F6 RID: 5110
		internal const string NumberOfDocumentsIndexedCrawlerCounterName = "Crawler: Items Processed";

		// Token: 0x040013F7 RID: 5111
		internal const string StorePerDatabasePerformanceCountersCategoryName = "MSExchangeIS Store";

		// Token: 0x040013F8 RID: 5112
		internal const string TotalSearchesCounterName = "Total searches";

		// Token: 0x040013F9 RID: 5113
		internal const string TotalSuccessfulSearchesCounterName = "Total number of successful search queries";

		// Token: 0x040013FA RID: 5114
		internal const string TotalSearchesQueriesGreaterThan60SecondsCounterName = "Total search queries completed in > 60 sec";

		// Token: 0x040013FB RID: 5115
		internal const string IndexAgentCountersCategoryName = "MSExchangeSearch Transport CTS Flow";

		// Token: 0x040013FC RID: 5116
		internal const string IndexAgentCountersInstanceName = "EdgeTransport";

		// Token: 0x040013FD RID: 5117
		internal const string NumberOfFailedDocumentsCounterName = "Number Of Failed Documents";

		// Token: 0x040013FE RID: 5118
		internal const string NumberOfProcessedDocumentsCounterName = "Number Of Processed Documents";

		// Token: 0x040013FF RID: 5119
		internal const string SearchContentProcessingCategoryName = "Search Content Processing";

		// Token: 0x04001400 RID: 5120
		internal const string SearchContentProcessingInstanceName = "ContentEngineNode1";

		// Token: 0x04001401 RID: 5121
		internal const string CompletedCallbacksTotalCounterName = "# Completed Callbacks Total";

		// Token: 0x04001402 RID: 5122
		internal const string SearchHostControllerCategoryName = "Search Host Controller";

		// Token: 0x04001403 RID: 5123
		internal const string ComponentRestartsCounterName = "Component Restarts";

		// Token: 0x04001404 RID: 5124
		internal const string SearchHostControllerCounterInstanceSuffix = " Fsis";

		// Token: 0x04001405 RID: 5125
		internal const string FailedCallbacksTotalCounterName = "# Failed Callbacks Total";

		// Token: 0x04001406 RID: 5126
		internal const int CmdletMaxRetry = 2;

		// Token: 0x04001407 RID: 5127
		internal const int CmdletRetryIntervalSeconds = 1;

		// Token: 0x04001408 RID: 5128
		internal const int MaxRetryAttempt = 3;

		// Token: 0x04001409 RID: 5129
		internal static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400140A RID: 5130
		internal static readonly TimeSpan GetDiagnosticInfoCallTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x0400140B RID: 5131
		private static readonly TimeSpan CopyStatusCacheTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x0400140C RID: 5132
		private static readonly TimeSpan IndexStatusCacheTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400140D RID: 5133
		private static readonly TimeSpan DiagnosticInfoCacheTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400140E RID: 5134
		private static readonly TracingContext traceContext = TracingContext.Default;

		// Token: 0x0400140F RID: 5135
		private static readonly object diagnosticInfoCacheLock = new object();

		// Token: 0x04001410 RID: 5136
		private static string escalationTeam = "Search";

		// Token: 0x04001411 RID: 5137
		private static Dictionary<Guid, IndexStatus> indexStatusCache;

		// Token: 0x04001412 RID: 5138
		private static Dictionary<Guid, DateTime> indexStatusCacheTimeoutTimes = new Dictionary<Guid, DateTime>();

		// Token: 0x04001413 RID: 5139
		private static DiagnosticInfo diagnosticInfoCache;

		// Token: 0x04001414 RID: 5140
		private static DateTime diagnosticInfoCacheTimeoutTime = DateTime.MinValue;

		// Token: 0x04001415 RID: 5141
		private static Exception getDiagnosticInfoException;

		// Token: 0x04001416 RID: 5142
		private static Dictionary<string, MailboxDatabaseInfo> databaseInfoDict = new Dictionary<string, MailboxDatabaseInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001417 RID: 5143
		private static Dictionary<string, PerformanceCounter> perfCounters = new Dictionary<string, PerformanceCounter>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001418 RID: 5144
		private static MonitoringLogger monitoringLogger;

		// Token: 0x04001419 RID: 5145
		private static MonitoringLogger copyStatusLogger;

		// Token: 0x0400141A RID: 5146
		private static MonitoringLogger recoveryActionsLogger;

		// Token: 0x0400141B RID: 5147
		private static MonitoringLogger nodeStateLogger;

		// Token: 0x0400141C RID: 5148
		private static DateTime? machineBootTime;

		// Token: 0x02000487 RID: 1159
		internal static class FastNodeNames
		{
			// Token: 0x06001D3E RID: 7486 RVA: 0x000AD2D7 File Offset: 0x000AB4D7
			internal static bool IsNodeNameValid(string nodeName)
			{
				return nodeName == "IndexNode1" || nodeName == "AdminNode1" || nodeName == "ContentEngineNode1" || nodeName == "InteractionEngineNode1";
			}

			// Token: 0x0400141E RID: 5150
			internal const string AdminNode1 = "AdminNode1";

			// Token: 0x0400141F RID: 5151
			internal const string ContentEngineNode1 = "ContentEngineNode1";

			// Token: 0x04001420 RID: 5152
			internal const string InteractionEngineNode1 = "InteractionEngineNode1";

			// Token: 0x04001421 RID: 5153
			internal const string IndexNode1 = "IndexNode1";
		}
	}
}
