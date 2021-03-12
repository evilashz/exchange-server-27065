using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Win32;

namespace Microsoft.Exchange.Transport.Sync.Worker.Health
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RemoteServerHealthManager : DisposeTrackableBase
	{
		// Token: 0x06000228 RID: 552 RVA: 0x00009BF8 File Offset: 0x00007DF8
		internal RemoteServerHealthManager(SyncLogSession syncLogSession, IRemoteServerHealthConfiguration configuration, Action<EventLogEntry> eventLoggerDelegate, Action<RemoteServerHealthData> healthLoggerDelegate)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			SyncUtilities.ThrowIfArgumentNull("configuration", configuration);
			SyncUtilities.ThrowIfArgumentNull("eventLoggerDelegate", eventLoggerDelegate);
			SyncUtilities.ThrowIfArgumentNull("healthLoggerDelegate", healthLoggerDelegate);
			this.syncLogSession = syncLogSession;
			this.remoteServerHealthManagementEnabled = configuration.RemoteServerHealthManagementEnabled;
			this.remoteServerPoisonMarkingEnabled = configuration.RemoteServerPoisonMarkingEnabled;
			this.remoteServerLatencySlidingCounterWindowSize = configuration.RemoteServerLatencySlidingCounterWindowSize;
			this.remoteServerLatencySlidingCounterBucketLength = configuration.RemoteServerLatencySlidingCounterBucketLength;
			this.remoteServerLatencyThreshold = configuration.RemoteServerLatencyThreshold;
			this.remoteServerBackoffCountLimit = configuration.RemoteServerBackoffCountLimit;
			this.remoteServerBackoffTimeSpan = configuration.RemoteServerBackoffTimeSpan;
			this.remoteServerHealthDataExpiryPeriod = configuration.RemoteServerHealthDataExpiryPeriod;
			this.remoteServerCapacityUsageThreshold = configuration.RemoteServerCapacityUsageThreshold;
			this.eventLoggerDelegate = eventLoggerDelegate;
			this.healthLoggerDelegate = healthLoggerDelegate;
			this.remoteServerHealthDataList = new Dictionary<string, RemoteServerHealthData>(StringComparer.InvariantCultureIgnoreCase);
			this.LoadHealthData();
			if (this.remoteServerHealthManagementEnabled)
			{
				this.remoteServerHealthDataExpiryAndPersistanceTimer = new GuardedTimer(new TimerCallback(this.ExpireAndPersistHealthData), null, configuration.RemoteServerHealthDataExpiryAndPersistanceFrequency, configuration.RemoteServerHealthDataExpiryAndPersistanceFrequency);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00009CFD File Offset: 0x00007EFD
		protected Dictionary<string, RemoteServerHealthData> RemoteServerHealthDataList
		{
			get
			{
				return this.remoteServerHealthDataList;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00009D05 File Offset: 0x00007F05
		protected SyncLogSession SyncLogSession
		{
			get
			{
				return this.syncLogSession;
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009D0D File Offset: 0x00007F0D
		protected static void CleanUpExistingHealthDataInRegistry()
		{
			Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\Sync\\RemoteServerHealthData\\");
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009D20 File Offset: 0x00007F20
		internal XElement GetDiagnosticInfo(bool verbose)
		{
			XElement xelement = new XElement("RemoteServerHealthManager");
			xelement.Add(new XElement("remoteServersCount", this.remoteServerHealthDataList.Count));
			if (verbose)
			{
				XElement xelement2 = new XElement("RemoteServerHealthList");
				lock (this.remoteServerHealthDataList)
				{
					foreach (RemoteServerHealthData remoteServerHealthData in this.remoteServerHealthDataList.Values)
					{
						lock (remoteServerHealthData)
						{
							XElement diagnosticInfo = remoteServerHealthData.GetDiagnosticInfo();
							xelement2.Add(diagnosticInfo);
						}
					}
				}
				xelement.Add(xelement2);
			}
			return xelement;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009E28 File Offset: 0x00008028
		internal void RecordRemoteServerLatency(string serverName, long serverLatency)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("serverName", serverName);
			SyncUtilities.ThrowIfArgumentLessThanZero("serverLatency", serverLatency);
			if (!this.remoteServerHealthManagementEnabled)
			{
				this.syncLogSession.LogDebugging((TSLID)1324UL, "RemoteServerHealthManagement is not Enabled, skip recording latency for Remote Server:{0}.", new object[]
				{
					serverName
				});
				return;
			}
			this.syncLogSession.LogDebugging((TSLID)1325UL, "Recording latency:{0} for Remote Server:{1}.", new object[]
			{
				serverLatency,
				serverName
			});
			RemoteServerHealthData remoteServerHealthData = null;
			lock (this.remoteServerHealthDataList)
			{
				if (!this.remoteServerHealthDataList.TryGetValue(serverName, out remoteServerHealthData))
				{
					remoteServerHealthData = new RemoteServerHealthData(serverName, this.remoteServerLatencySlidingCounterWindowSize, this.remoteServerLatencySlidingCounterBucketLength);
					this.remoteServerHealthDataList.Add(serverName, remoteServerHealthData);
				}
			}
			lock (remoteServerHealthData)
			{
				remoteServerHealthData.RecordServerLatency(serverLatency);
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009F40 File Offset: 0x00008140
		internal RemoteServerHealthState CalculateRemoteServerHealth(string serverName, bool isPartnerRemoteServer)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("serverName", serverName);
			if (!this.remoteServerHealthManagementEnabled)
			{
				this.syncLogSession.LogDebugging((TSLID)1326UL, "RemoteServerHealthManagement is not Enabled, return Clean State for Remote Server:{0}.", new object[]
				{
					serverName
				});
				return RemoteServerHealthState.Clean;
			}
			RemoteServerHealthData remoteServerHealthData = null;
			bool flag = false;
			lock (this.remoteServerHealthDataList)
			{
				flag = this.remoteServerHealthDataList.TryGetValue(serverName, out remoteServerHealthData);
			}
			if (!flag)
			{
				this.syncLogSession.LogDebugging((TSLID)1327UL, "No Entry Found in our Health Management List, return Clean State for Remote Server: {0}.", new object[]
				{
					serverName
				});
				return RemoteServerHealthState.Clean;
			}
			RemoteServerHealthState state2;
			lock (remoteServerHealthData)
			{
				long num = -1L;
				long num2 = 0L;
				double num3 = 0.0;
				RemoteServerHealthState state = remoteServerHealthData.State;
				switch (remoteServerHealthData.State)
				{
				case RemoteServerHealthState.Clean:
					num = remoteServerHealthData.GetAverageLantency(out num2);
					num3 = (double)(num * num2);
					if ((double)num >= this.remoteServerLatencyThreshold.TotalMilliseconds && num3 >= this.remoteServerCapacityUsageThreshold.TotalMilliseconds)
					{
						this.syncLogSession.LogVerbose((TSLID)1412UL, "Latency Violation Found, backoff Remote Server:{0}. AverageLatency (in Milliseconds):{1}, TotalCapacityUsage:{2}", new object[]
						{
							serverName,
							num,
							num3
						});
						remoteServerHealthData.MarkAsBackedOff();
						if (remoteServerHealthData.BackOffCount >= this.remoteServerBackoffCountLimit)
						{
							if (this.remoteServerPoisonMarkingEnabled)
							{
								if (isPartnerRemoteServer)
								{
									this.syncLogSession.LogDebugging((TSLID)1413UL, "Skipping MarkAsPoisonous for RemoteServer ({0}) since this is our partner remote server.", new object[]
									{
										serverName
									});
								}
								else
								{
									this.syncLogSession.LogVerbose((TSLID)1414UL, "Max number of backOffs reached, Mark Remote Server ({0}) as Poisonous.", new object[]
									{
										serverName
									});
									remoteServerHealthData.MarkAsPoisonous();
								}
							}
							else
							{
								this.syncLogSession.LogDebugging((TSLID)1415UL, "Skipping MarkAsPoisonous for RemoteServer ({0}) because Poison Marking is disabled.", new object[]
								{
									serverName
								});
							}
						}
					}
					break;
				case RemoteServerHealthState.BackedOff:
					if (remoteServerHealthData.TimeSinceLastBackOff() >= this.remoteServerBackoffTimeSpan)
					{
						this.syncLogSession.LogVerbose((TSLID)1416UL, "Remote Server ({0}) Backoff time has elapsed, mark as clean again.", new object[]
						{
							serverName
						});
						remoteServerHealthData.MarkAsClean();
					}
					break;
				case RemoteServerHealthState.Poisonous:
					break;
				default:
					throw new InvalidOperationException("Unknown RemoteServerHealthState value: " + remoteServerHealthData.State);
				}
				if (remoteServerHealthData.State != state)
				{
					this.healthLoggerDelegate(remoteServerHealthData);
				}
				this.syncLogSession.LogDebugging((TSLID)1417UL, "CalculateRemoteServerHealth:: Remote Server:{0}, Old State:{1}, New State:{2}, Avg Latency(ms):{3}, TotalCapacityUsage(ms):{4}", new object[]
				{
					serverName,
					state,
					remoteServerHealthData.State,
					num,
					num3
				});
				state2 = remoteServerHealthData.State;
			}
			return state2;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A280 File Offset: 0x00008480
		protected virtual void ExpireAndPersistHealthData(object state)
		{
			this.syncLogSession.LogDebugging((TSLID)1418UL, "ExpireAndPersistHealthData Invoked", new object[0]);
			List<RemoteServerHealthData> list = new List<RemoteServerHealthData>();
			lock (this.remoteServerHealthDataList)
			{
				List<string> list2 = new List<string>();
				foreach (KeyValuePair<string, RemoteServerHealthData> keyValuePair in this.remoteServerHealthDataList)
				{
					lock (keyValuePair.Value)
					{
						TimeSpan timeSpan = keyValuePair.Value.TimeSinceLastUpdate();
						if (timeSpan >= this.remoteServerHealthDataExpiryPeriod)
						{
							this.syncLogSession.LogDebugging((TSLID)1419UL, "No activity for Remote Server:{0} since:{1}, removing it from our records.", new object[]
							{
								keyValuePair.Value.ServerName,
								timeSpan
							});
							list2.Add(keyValuePair.Key);
						}
						else if (keyValuePair.Value.BackOffCount > 0 && keyValuePair.Value.TimeSinceLastBackOff() >= this.remoteServerHealthDataExpiryPeriod)
						{
							this.syncLogSession.LogDebugging((TSLID)1420UL, "No more backoffs for Remote Server:{0} since:{1}, reseting its state.", new object[]
							{
								keyValuePair.Value.ServerName,
								keyValuePair.Value.TimeSinceLastBackOff()
							});
							keyValuePair.Value.Reset();
							this.healthLoggerDelegate(keyValuePair.Value);
						}
						else if (keyValuePair.Value.BackOffCount > 0)
						{
							RemoteServerHealthData item = RemoteServerHealthData.CreateRemoteServerHealthDataForViolatingServer(keyValuePair.Value.ServerName, keyValuePair.Value.State, keyValuePair.Value.BackOffCount, keyValuePair.Value.LastUpdateTime, keyValuePair.Value.LastBackOffStartTime.Value, this.remoteServerLatencySlidingCounterWindowSize, this.remoteServerLatencySlidingCounterBucketLength);
							list.Add(item);
						}
					}
				}
				foreach (string key in list2)
				{
					this.remoteServerHealthDataList.Remove(key);
				}
			}
			this.DoRegistryOperationAndHandleAccessExceptions<List<RemoteServerHealthData>>(new Action<List<RemoteServerHealthData>>(this.PersistHealthDataIntoRegistry), list);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000A558 File Offset: 0x00008758
		protected virtual void PersistHealthDataIntoRegistry(List<RemoteServerHealthData> healthDataToBePersisted)
		{
			this.syncLogSession.LogDebugging((TSLID)1421UL, "PersistHealthDataIntoRegistry Invoked.", new object[0]);
			try
			{
				RemoteServerHealthManager.CleanUpExistingHealthDataInRegistry();
			}
			catch (ArgumentException ex)
			{
				this.syncLogSession.LogVerbose((TSLID)1422UL, "DeleteSubKeyTree failed for:{0} due to: {1}, continue with persisting new ones.", new object[]
				{
					"SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\Sync\\RemoteServerHealthData\\",
					ex
				});
			}
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\Sync\\RemoteServerHealthData\\"))
			{
				foreach (RemoteServerHealthData remoteServerHealthData in healthDataToBePersisted)
				{
					string subkey = Guid.NewGuid().ToString();
					using (RegistryKey registryKey2 = registryKey.CreateSubKey(subkey))
					{
						this.syncLogSession.LogDebugging((TSLID)1423UL, "Persisting HealthData:{0}.", new object[]
						{
							remoteServerHealthData
						});
						registryKey2.SetValue("ServerName", remoteServerHealthData.ServerName);
						registryKey2.SetValue("State", remoteServerHealthData.State);
						registryKey2.SetValue("BackOffCount", remoteServerHealthData.BackOffCount);
						registryKey2.SetValue("LastUpdateTime", remoteServerHealthData.LastUpdateTime.ToString());
						registryKey2.SetValue("LastBackOffStartTime", remoteServerHealthData.LastBackOffStartTime.Value.ToString());
					}
				}
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000A758 File Offset: 0x00008958
		protected void LoadHealthData()
		{
			this.syncLogSession.LogDebugging((TSLID)1424UL, "LoadHealthData Invoked.", new object[0]);
			if (!this.remoteServerHealthManagementEnabled)
			{
				this.syncLogSession.LogDebugging((TSLID)1425UL, "RemoteServerHealthManagement is NOT Enabled, no need to Load any server data.", new object[0]);
				return;
			}
			this.DoRegistryOperationAndHandleAccessExceptions<object>(new Action<object>(this.LoadHealthDataFromRegistry), null);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A7C4 File Offset: 0x000089C4
		protected virtual void LoadHealthDataFromRegistry(object stateObject)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\Sync\\RemoteServerHealthData\\"))
			{
				if (registryKey == null)
				{
					this.syncLogSession.LogVerbose((TSLID)1426UL, "No registry entry found while Loading health data.", new object[0]);
				}
				else
				{
					foreach (string text in registryKey.GetSubKeyNames())
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(text))
						{
							if (registryKey2 == null)
							{
								this.syncLogSession.LogError((TSLID)1427UL, "Registry entry for healthDataEntry Key ({0}) no longer exists, continue with the next one.", new object[]
								{
									text
								});
							}
							else
							{
								bool flag = true;
								string text2 = registryKey2.GetValue("ServerName") as string;
								string text3 = registryKey2.GetValue("State") as string;
								object value = registryKey2.GetValue("BackOffCount");
								string text4 = registryKey2.GetValue("LastUpdateTime") as string;
								string text5 = registryKey2.GetValue("LastBackOffStartTime") as string;
								flag &= !string.IsNullOrEmpty(text2);
								RemoteServerHealthState state = RemoteServerHealthState.Clean;
								flag &= (!string.IsNullOrEmpty(text3) && EnumValidator.TryParse<RemoteServerHealthState>(text3, EnumParseOptions.Default, out state));
								flag &= (value != null && value is int);
								ExDateTime utcNow = ExDateTime.UtcNow;
								flag &= (!string.IsNullOrEmpty(text4) && ExDateTime.TryParse(text4, out utcNow));
								ExDateTime utcNow2 = ExDateTime.UtcNow;
								if (!(flag & (!string.IsNullOrEmpty(text5) && ExDateTime.TryParse(text5, out utcNow2))))
								{
									this.syncLogSession.LogError((TSLID)1428UL, "Invalid Health Data Entry Found [ServerName:{0}, State:{1}, BackoffCount:{2}, LastUpdateTime:{3}, LastBackOffStartTime:{4}], continue with the next one.", new object[]
									{
										text2,
										text3,
										value,
										text4,
										text5
									});
								}
								else
								{
									RemoteServerHealthData remoteServerHealthData = null;
									Exception ex = null;
									if (!RemoteServerHealthData.TryCreateRemoteServerHealthDataForViolatingServer(text2, state, (int)value, utcNow, utcNow2, this.remoteServerLatencySlidingCounterWindowSize, this.remoteServerLatencySlidingCounterBucketLength, out remoteServerHealthData, out ex))
									{
										this.syncLogSession.LogError((TSLID)1429UL, "Failed to CreateRemoteServerHealthDataForViolatingServer for server:{0}, due to exception:{1}, continue with the next one.", new object[]
										{
											text2,
											ex
										});
									}
									else
									{
										lock (this.remoteServerHealthDataList)
										{
											if (this.remoteServerHealthDataList.ContainsKey(text2))
											{
												this.syncLogSession.LogError((TSLID)1430UL, "RemoteServerHealthDataList already has an entry for remote server:{0}, continue with the next one.", new object[]
												{
													text2
												});
											}
											else
											{
												this.syncLogSession.LogDebugging((TSLID)1431UL, "Loading Health Data:{0}.", new object[]
												{
													remoteServerHealthData
												});
												this.remoteServerHealthDataList.Add(text2, remoteServerHealthData);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		private void DoRegistryOperationAndHandleAccessExceptions<T>(Action<T> registryOperation, T argument)
		{
			try
			{
				registryOperation(argument);
			}
			catch (SecurityException exception)
			{
				this.HandleRegistryAccessError(exception);
			}
			catch (UnauthorizedAccessException exception2)
			{
				this.HandleRegistryAccessError(exception2);
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000AB30 File Offset: 0x00008D30
		private void HandleRegistryAccessError(Exception exception)
		{
			this.syncLogSession.LogError((TSLID)1432UL, "Registry Access failed with error: {0}", new object[]
			{
				exception
			});
			this.eventLoggerDelegate(new EventLogEntry(TransportSyncWorkerEventLogConstants.Tuple_RegistryAccessDenied, null, new object[]
			{
				exception
			}));
			EventNotificationHelper.PublishTransportEventNotificationItem(TransportSyncNotificationEvent.RegistryAccessDenied.ToString(), exception);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000AB97 File Offset: 0x00008D97
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.remoteServerHealthDataExpiryAndPersistanceTimer != null)
			{
				this.remoteServerHealthDataExpiryAndPersistanceTimer.Dispose(true);
				this.remoteServerHealthDataExpiryAndPersistanceTimer = null;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000ABB7 File Offset: 0x00008DB7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RemoteServerHealthManager>(this);
		}

		// Token: 0x0400011F RID: 287
		private const string RemoteServerHealthDataLocation = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\Sync\\RemoteServerHealthData\\";

		// Token: 0x04000120 RID: 288
		private const string ServerNameRegistryKeyName = "ServerName";

		// Token: 0x04000121 RID: 289
		private const string StateRegistryKeyName = "State";

		// Token: 0x04000122 RID: 290
		private const string BackOffCountRegistryKeyName = "BackOffCount";

		// Token: 0x04000123 RID: 291
		private const string LastUpdateTimeRegistryKeyName = "LastUpdateTime";

		// Token: 0x04000124 RID: 292
		private const string LastBackOffStartTimeRegistryKeyName = "LastBackOffStartTime";

		// Token: 0x04000125 RID: 293
		private readonly Dictionary<string, RemoteServerHealthData> remoteServerHealthDataList;

		// Token: 0x04000126 RID: 294
		private readonly SyncLogSession syncLogSession;

		// Token: 0x04000127 RID: 295
		private readonly TimeSpan remoteServerLatencySlidingCounterWindowSize;

		// Token: 0x04000128 RID: 296
		private readonly TimeSpan remoteServerLatencySlidingCounterBucketLength;

		// Token: 0x04000129 RID: 297
		private readonly TimeSpan remoteServerLatencyThreshold;

		// Token: 0x0400012A RID: 298
		private readonly int remoteServerBackoffCountLimit;

		// Token: 0x0400012B RID: 299
		private readonly TimeSpan remoteServerBackoffTimeSpan;

		// Token: 0x0400012C RID: 300
		private readonly TimeSpan remoteServerHealthDataExpiryPeriod;

		// Token: 0x0400012D RID: 301
		private readonly TimeSpan remoteServerCapacityUsageThreshold;

		// Token: 0x0400012E RID: 302
		private readonly bool remoteServerHealthManagementEnabled;

		// Token: 0x0400012F RID: 303
		private readonly bool remoteServerPoisonMarkingEnabled;

		// Token: 0x04000130 RID: 304
		private readonly Action<EventLogEntry> eventLoggerDelegate;

		// Token: 0x04000131 RID: 305
		private readonly Action<RemoteServerHealthData> healthLoggerDelegate;

		// Token: 0x04000132 RID: 306
		private GuardedTimer remoteServerHealthDataExpiryAndPersistanceTimer;
	}
}
