using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000023 RID: 35
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GlobalDatabaseHandler
	{
		// Token: 0x170000C2 RID: 194
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000D988 File Offset: 0x0000BB88
		internal ICoreGlobalDatabaseHandler CoreGlobalDatabaseHandlerClass
		{
			set
			{
				this.coreGlobalDatabaseHandler = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000D991 File Offset: 0x0000BB91
		internal SortedDictionary<Guid, DatabaseManager> DatabasesDictionary
		{
			get
			{
				return this.databasesDictionary;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000D999 File Offset: 0x0000BB99
		internal static bool IsSessionReusableAfterException(Exception e)
		{
			return CacheExceptionUtilities.Instance.IsSessionReusableAfterException(e);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000D9A6 File Offset: 0x0000BBA6
		internal static CacheTransientException CreateCacheTransientException(Trace tracer, int objectHash, Guid databaseGuid, Guid mailboxGuid, LocalizedString exceptionInfo)
		{
			return CacheExceptionUtilities.Instance.CreateCacheTransientException(tracer, objectHash, databaseGuid, mailboxGuid, exceptionInfo);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		internal static Exception ConvertToCacheException(Trace tracer, int objectHash, Guid databaseGuid, Guid mailboxGuid, Exception exception, out bool reuseSession)
		{
			Exception result = CacheExceptionUtilities.Instance.ConvertToCacheException(tracer, objectHash, databaseGuid, mailboxGuid, exception, out reuseSession);
			if (ExceptionUtilities.IndicatesDatabaseDismount(exception))
			{
				ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)1297UL, GlobalDatabaseHandler.Tracer, "Exception {0} encountered. Treating the database {1} as dismounted.", new object[]
				{
					exception.GetType(),
					databaseGuid
				});
				DataAccessLayer.DatabaseHandler.ApplyDatabaseStateChange(databaseGuid, false);
				DataAccessLayer.TriggerOnDatabaseDismountedEvent(databaseGuid);
			}
			return result;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000DA30 File Offset: 0x0000BC30
		internal Guid[] GetDatabases()
		{
			Guid[] result;
			lock (this.syncObject)
			{
				if (this.Initialized())
				{
					result = this.databaseGuidLookupTable.ToArray();
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000DA84 File Offset: 0x0000BC84
		internal SubscriptionCacheManager GetCacheManager(Guid databaseGuid)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			DatabaseManager databaseManager = null;
			lock (this.syncObject)
			{
				this.databasesDictionary.TryGetValue(databaseGuid, out databaseManager);
				if (databaseManager != null)
				{
					return databaseManager.SubscriptionCacheManager;
				}
			}
			return null;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000DAEC File Offset: 0x0000BCEC
		internal Guid GetDatabaseGuid(int databaseManagerIndex)
		{
			Guid result;
			lock (this.syncObject)
			{
				result = this.databaseGuidLookupTable[databaseManagerIndex];
			}
			return result;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000DB34 File Offset: 0x0000BD34
		internal DatabaseManager GetDatabaseManager(int databaseManagerIndex)
		{
			DatabaseManager result = null;
			lock (this.syncObject)
			{
				this.databasesDictionary.TryGetValue(this.databaseGuidLookupTable[databaseManagerIndex], out result);
			}
			return result;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		internal DatabaseManager GetDatabaseManager(Guid databaseGuid)
		{
			DatabaseManager result = null;
			lock (this.syncObject)
			{
				this.databasesDictionary.TryGetValue(databaseGuid, out result);
			}
			return result;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		internal int GetDatabaseCount()
		{
			int count;
			lock (this.syncObject)
			{
				count = this.databaseGuidLookupTable.Count;
			}
			return count;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000DC20 File Offset: 0x0000BE20
		internal void Initialize()
		{
			if (this.coreGlobalDatabaseHandler == null)
			{
				this.coreGlobalDatabaseHandler = new GlobalDatabaseHandler.CoreGlobalDatabaseHandler();
			}
			this.databasesDictionary = new SortedDictionary<Guid, DatabaseManager>();
			DatabaseManager.MailboxCrawlerInstance.StartCrawl();
			this.PollDatabases();
			this.findDatabasesTimer = new GuardedTimer(new TimerCallback(this.OnFindDatabasesTimerCallback), null, ContentAggregationConfig.DatabasePollingInterval, TimeSpan.FromMilliseconds(-1.0));
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000DC88 File Offset: 0x0000BE88
		internal void OnFindDatabasesTimerCallback(object state)
		{
			lock (this.syncObject)
			{
				if (this.findDatabasesTimer == null)
				{
					return;
				}
			}
			this.PollDatabases();
			lock (this.syncObject)
			{
				if (this.findDatabasesTimer != null)
				{
					this.findDatabasesTimer.Change(ContentAggregationConfig.DatabasePollingInterval, TimeSpan.FromMilliseconds(-1.0));
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000DD24 File Offset: 0x0000BF24
		internal void Shutdown()
		{
			List<DatabaseManager> list = new List<DatabaseManager>();
			lock (this.syncObject)
			{
				this.findDatabasesTimer.Dispose(false);
				this.findDatabasesTimer = null;
				foreach (KeyValuePair<Guid, DatabaseManager> keyValuePair in this.databasesDictionary)
				{
					if (keyValuePair.Value.Enabled)
					{
						list.Add(keyValuePair.Value);
					}
				}
				this.databasesDictionary.Clear();
			}
			DatabaseManager.MailboxCrawlerInstance.StopCrawl();
			foreach (DatabaseManager databaseManager in list)
			{
				databaseManager.Shutdown();
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000DE24 File Offset: 0x0000C024
		internal SortedDictionary<Guid, bool> FindLocalDatabases(out string dbPollingSource)
		{
			SortedDictionary<Guid, bool> sortedDictionary = this.FindLocalDatabasesFromAdminRPC();
			if (sortedDictionary == null)
			{
				sortedDictionary = this.FindLocalDatabasesFromAD();
				dbPollingSource = "AD";
			}
			else
			{
				dbPollingSource = "AdminRPC";
			}
			return sortedDictionary;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000DE54 File Offset: 0x0000C054
		internal void ApplyNewDatabaseTest(Guid databaseGuid, Guid systemMailboxGuid, bool enabled)
		{
			this.ApplyNewDatabase(databaseGuid, enabled);
			DatabaseManager databaseManager = this.GetDatabaseManager(databaseGuid);
			databaseManager.SystemMailboxGuid = systemMailboxGuid;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000DE78 File Offset: 0x0000C078
		internal void ApplyDeletedDatabaseTest(Guid databaseGuid)
		{
			this.ApplyDeletedDatabase(databaseGuid);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000DE84 File Offset: 0x0000C084
		internal void ApplyDatabaseStateChangeTest(Guid databaseGuid)
		{
			DatabaseManager databaseManager = this.GetDatabaseManager(databaseGuid);
			this.ApplyDatabaseStateChange(databaseGuid, !databaseManager.Enabled);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000DEAC File Offset: 0x0000C0AC
		internal void OnDatabaseDismounted(object sender, OnDatabaseDismountedEventArgs databaseDismountedArgs)
		{
			Guid databaseGuid = databaseDismountedArgs.DatabaseGuid;
			this.ApplyDatabaseStateChange(databaseGuid, false);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
		internal XElement GetDiagnosticInfo(SyncDiagnosticMode mode)
		{
			XElement xelement = new XElement("GlobalDatabaseHandler");
			xelement.Add(new XElement("DatabaseCount", this.GetDatabaseCount()));
			xelement.Add(new XElement("LastDatabaseDiscoveryStartTime", (this.lastDatabaseDiscoveryStartTime != null) ? this.lastDatabaseDiscoveryStartTime.Value.ToString("o") : string.Empty));
			DatabaseManager.AddMailboxCrawlerDiagnosticInfoTo(xelement);
			XElement xelement2 = new XElement("Databases");
			lock (this.syncObject)
			{
				foreach (DatabaseManager databaseManager in this.databasesDictionary.Values)
				{
					xelement2.Add(databaseManager.GetDiagnosticInfo(mode));
				}
			}
			if (mode == SyncDiagnosticMode.Verbose)
			{
				XElement xelement3 = new XElement("MailboxCrawlQueue");
				DatabaseManager.AddMailboxCrawlerQueueEntriesDiagnosticInfoTo(xelement3);
				xelement.Add(xelement3);
			}
			xelement.Add(xelement2);
			return xelement;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000E008 File Offset: 0x0000C208
		private static string DBStateString(bool state)
		{
			if (!state)
			{
				return "Disabled";
			}
			return "Enabled";
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000E018 File Offset: 0x0000C218
		private void PollDatabases()
		{
			try
			{
				string dbPollingSource = null;
				SortedDictionary<Guid, bool> databaseList = this.FindLocalDatabases(out dbPollingSource);
				this.ApplyLatestDatabaseStatus(databaseList, dbPollingSource);
			}
			catch (Exception exception)
			{
				DataAccessLayer.ReportWatson("Exception during DB Discovery", exception);
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000E058 File Offset: 0x0000C258
		private void ApplyNewDatabase(Guid databaseGuid, bool enabled)
		{
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)278UL, GlobalDatabaseHandler.Tracer, "ApplyNewDatabase: Found new database {0} in {1} state.", new object[]
			{
				databaseGuid,
				enabled ? "enabled" : "disabled"
			});
			DatabaseManager databaseManager = null;
			lock (this.syncObject)
			{
				databaseManager = this.NewDatabaseManager(databaseGuid);
			}
			if (databaseManager != null && enabled)
			{
				databaseManager.Initialize();
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000E0EC File Offset: 0x0000C2EC
		private void ApplyDeletedDatabase(Guid databaseGuid)
		{
			DatabaseManager databaseManager = this.GetDatabaseManager(databaseGuid);
			if (databaseManager != null)
			{
				lock (this.syncObject)
				{
					bool enabled = databaseManager.Enabled;
					ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)279UL, GlobalDatabaseHandler.Tracer, "ApplyDeletedDatabase: Database {0} which was in {1} state does not exist anymore.", new object[]
					{
						databaseGuid,
						enabled
					});
					this.DeleteDatabaseManager(databaseManager);
				}
				if (databaseManager != null && databaseManager.Enabled)
				{
					databaseManager.Shutdown();
				}
			}
			DataAccessLayer.TriggerOnDatabaseDismountedEvent(databaseGuid);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000E194 File Offset: 0x0000C394
		private void ApplyDatabaseStateChange(Guid databaseGuid, bool enabled)
		{
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)280UL, GlobalDatabaseHandler.Tracer, "ApplyDatabaseStateChange: Database {0} changed status from {1} to {2}", new object[]
			{
				databaseGuid,
				enabled ? "disabled" : "enabled",
				enabled ? "enabled" : "disabled"
			});
			DatabaseManager databaseManager = null;
			lock (this.syncObject)
			{
				databaseManager = this.GetDatabaseManager(databaseGuid);
				if (databaseManager == null || databaseManager.Enabled == enabled)
				{
					return;
				}
			}
			if (!enabled)
			{
				databaseManager.Shutdown();
				return;
			}
			databaseManager.Initialize();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000E24C File Offset: 0x0000C44C
		private void ApplyLatestDatabaseStatus(SortedDictionary<Guid, bool> databaseList, string dbPollingSource)
		{
			this.lastDatabaseDiscoveryStartTime = new ExDateTime?(ExDateTime.UtcNow);
			if (databaseList == null)
			{
				ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)281UL, GlobalDatabaseHandler.Tracer, "ApplyLatestDatabaseStatus skipped. Failed to read the local databases.", new object[0]);
				return;
			}
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)282UL, GlobalDatabaseHandler.Tracer, "ApplyLatestDatabaseStatus called with {0} databases in the database list.", new object[]
			{
				databaseList.Count
			});
			IEnumerator<KeyValuePair<Guid, bool>> enumerator = databaseList.GetEnumerator();
			bool flag = enumerator.MoveNext();
			int num = 0;
			Dictionary<Guid, bool> dictionary = new Dictionary<Guid, bool>();
			List<Guid> list = new List<Guid>();
			List<KeyValuePair<Guid, bool>> list2 = new List<KeyValuePair<Guid, bool>>();
			int num2 = 0;
			lock (this.syncObject)
			{
				using (SortedDictionary<Guid, DatabaseManager>.Enumerator enumerator2 = this.databasesDictionary.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<Guid, DatabaseManager> keyValuePair = enumerator2.Current;
						while (flag)
						{
							KeyValuePair<Guid, bool> keyValuePair2 = enumerator.Current;
							if (keyValuePair2.Key.CompareTo(keyValuePair.Key) >= 0)
							{
								break;
							}
							Dictionary<Guid, bool> dictionary2 = dictionary;
							KeyValuePair<Guid, bool> keyValuePair3 = enumerator.Current;
							Guid key = keyValuePair3.Key;
							KeyValuePair<Guid, bool> keyValuePair4 = enumerator.Current;
							dictionary2.Add(key, keyValuePair4.Value);
							KeyValuePair<Guid, bool> keyValuePair5 = enumerator.Current;
							if (keyValuePair5.Value)
							{
								num2++;
							}
							flag = enumerator.MoveNext();
						}
						if (flag)
						{
							KeyValuePair<Guid, bool> keyValuePair6 = enumerator.Current;
							if (keyValuePair6.Key == keyValuePair.Key)
							{
								KeyValuePair<Guid, bool> keyValuePair7 = enumerator.Current;
								if (keyValuePair7.Value != keyValuePair.Value.Enabled)
								{
									list2.Add(enumerator.Current);
								}
								else
								{
									num++;
								}
								KeyValuePair<Guid, bool> keyValuePair8 = enumerator.Current;
								if (keyValuePair8.Value)
								{
									num2++;
								}
								flag = enumerator.MoveNext();
								continue;
							}
						}
						list.Add(keyValuePair.Key);
					}
					goto IL_1F9;
				}
				IL_1CE:
				Dictionary<Guid, bool> dictionary3 = dictionary;
				KeyValuePair<Guid, bool> keyValuePair9 = enumerator.Current;
				Guid key2 = keyValuePair9.Key;
				KeyValuePair<Guid, bool> keyValuePair10 = enumerator.Current;
				dictionary3.Add(key2, keyValuePair10.Value);
				flag = enumerator.MoveNext();
				IL_1F9:
				if (flag)
				{
					goto IL_1CE;
				}
			}
			foreach (KeyValuePair<Guid, bool> keyValuePair11 in dictionary)
			{
				this.ApplyNewDatabase(keyValuePair11.Key, keyValuePair11.Value);
				SyncHealthLogManager.TryLogDatabaseDiscovery(this.lastDatabaseDiscoveryStartTime.Value, dbPollingSource, databaseList.Count, num2, keyValuePair11.Key.ToString(), "Add", GlobalDatabaseHandler.DBStateString(keyValuePair11.Value));
			}
			foreach (Guid databaseGuid in list)
			{
				this.ApplyDeletedDatabase(databaseGuid);
				SyncHealthLogManager.TryLogDatabaseDiscovery(this.lastDatabaseDiscoveryStartTime.Value, dbPollingSource, databaseList.Count, num2, databaseGuid.ToString(), "Delete", GlobalDatabaseHandler.DBStateString(false));
			}
			foreach (KeyValuePair<Guid, bool> keyValuePair12 in list2)
			{
				this.ApplyDatabaseStateChange(keyValuePair12.Key, keyValuePair12.Value);
				SyncHealthLogManager.TryLogDatabaseDiscovery(this.lastDatabaseDiscoveryStartTime.Value, dbPollingSource, databaseList.Count, num2, keyValuePair12.Key.ToString(), "StateChange", GlobalDatabaseHandler.DBStateString(keyValuePair12.Value));
			}
			ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)283UL, GlobalDatabaseHandler.Tracer, "ApplyLatestDatabaseStatus exiting. Processed {0} additions, {1} deletions, {2} state changes, {3} unchanged.", new object[]
			{
				dictionary.Count,
				list.Count,
				list2.Count,
				num
			});
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		private SortedDictionary<Guid, bool> FindLocalDatabasesFromAdminRPC()
		{
			return this.coreGlobalDatabaseHandler.FindLocalDatabasesFromAdminRPC();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000E6B9 File Offset: 0x0000C8B9
		private SortedDictionary<Guid, bool> FindLocalDatabasesFromAD()
		{
			return this.coreGlobalDatabaseHandler.FindLocalDatabasesFromAD();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
		private DatabaseManager NewDatabaseManager(Guid databaseGuid)
		{
			if (!this.databaseGuidLookupTable.Contains(databaseGuid))
			{
				this.databaseGuidLookupTable.Add(databaseGuid);
			}
			byte databaseLookupIndex = (byte)this.databaseGuidLookupTable.IndexOf(databaseGuid);
			DatabaseManager databaseManager = new DatabaseManager(databaseGuid, databaseLookupIndex);
			this.databasesDictionary.Add(databaseGuid, databaseManager);
			this.coreGlobalDatabaseHandler.OnNewDatabaseManager(databaseManager);
			return databaseManager;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000E721 File Offset: 0x0000C921
		private void DeleteDatabaseManager(DatabaseManager databaseManager)
		{
			this.databasesDictionary.Remove(databaseManager.DatabaseGuid);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000E738 File Offset: 0x0000C938
		private bool Initialized()
		{
			bool result;
			lock (this.syncObject)
			{
				result = (this.findDatabasesTimer != null);
			}
			return result;
		}

		// Token: 0x0400012B RID: 299
		private const string EnabledState = "Enabled";

		// Token: 0x0400012C RID: 300
		private const string DisabledState = "Disabled";

		// Token: 0x0400012D RID: 301
		private static readonly Trace Tracer = ExTraceGlobals.GlobalDatabaseHandlerTracer;

		// Token: 0x0400012E RID: 302
		private object syncObject = new object();

		// Token: 0x0400012F RID: 303
		private ICoreGlobalDatabaseHandler coreGlobalDatabaseHandler;

		// Token: 0x04000130 RID: 304
		private GuardedTimer findDatabasesTimer;

		// Token: 0x04000131 RID: 305
		private SortedDictionary<Guid, DatabaseManager> databasesDictionary;

		// Token: 0x04000132 RID: 306
		private List<Guid> databaseGuidLookupTable = new List<Guid>();

		// Token: 0x04000133 RID: 307
		private ExDateTime? lastDatabaseDiscoveryStartTime = null;

		// Token: 0x02000025 RID: 37
		private class CoreGlobalDatabaseHandler : ICoreGlobalDatabaseHandler
		{
			// Token: 0x06000228 RID: 552 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
			public SortedDictionary<Guid, bool> FindLocalDatabasesFromAD()
			{
				if (ContentAggregationConfig.LocalServer == null)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)284UL, GlobalDatabaseHandler.Tracer, "Cannot read the local databases from AD. Failed to load the LocalServer info", new object[0]);
					return null;
				}
				MailboxDatabase[] databases = null;
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					databases = ContentAggregationConfig.LocalServer.GetMailboxDatabases();
				});
				if (adoperationResult.ErrorCode != ADOperationErrorCode.Success)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)285UL, GlobalDatabaseHandler.Tracer, "{0} error when looking for local databases {1}", new object[]
					{
						(adoperationResult.ErrorCode == ADOperationErrorCode.RetryableError) ? "Retryable" : "Permanent",
						adoperationResult.Exception
					});
					return null;
				}
				if (databases.Length == 0)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)286UL, GlobalDatabaseHandler.Tracer, "No databases found in AD for the local server", new object[0]);
					return null;
				}
				SortedDictionary<Guid, bool> sortedDictionary = new SortedDictionary<Guid, bool>();
				foreach (MailboxDatabase mailboxDatabase in databases)
				{
					bool flag = mailboxDatabase.IsValid && mailboxDatabase.Mounted != null && mailboxDatabase.Mounted.Value;
					ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)287UL, GlobalDatabaseHandler.Tracer, "AD DB discovery: Found DB {0} in {1} state", new object[]
					{
						mailboxDatabase.Guid,
						flag ? "enabled" : "disabled"
					});
					sortedDictionary.Add(mailboxDatabase.Guid, flag);
				}
				return sortedDictionary;
			}

			// Token: 0x06000229 RID: 553 RVA: 0x0000E968 File Offset: 0x0000CB68
			public SortedDictionary<Guid, bool> FindLocalDatabasesFromAdminRPC()
			{
				if (ContentAggregationConfig.LocalServer == null)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)288UL, GlobalDatabaseHandler.Tracer, "Cannot read the local databases from Admin RPC. Failed to load the LocalServer info", new object[0]);
					return null;
				}
				SortedDictionary<Guid, bool> sortedDictionary = new SortedDictionary<Guid, bool>();
				try
				{
					using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=TransportSync", ContentAggregationConfig.LocalServer.Name, null, null, null))
					{
						MdbStatus[] array = exRpcAdmin.ListMdbStatus(true);
						if (array == null || array.Length == 0)
						{
							ContentAggregationConfig.SyncLogSession.LogError((TSLID)289UL, GlobalDatabaseHandler.Tracer, "No databases found in Admin RPC for the local server", new object[0]);
							return null;
						}
						foreach (MdbStatus mdbStatus in array)
						{
							bool flag = (mdbStatus.Status & MdbStatusFlags.Online) == MdbStatusFlags.Online;
							ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)290UL, GlobalDatabaseHandler.Tracer, "Admin RPC DB discovery: Found DB {0} in {1} state", new object[]
							{
								mdbStatus.MdbGuid,
								flag ? "enabled" : "disabled"
							});
							sortedDictionary.Add(mdbStatus.MdbGuid, flag);
						}
					}
				}
				catch (MapiPermanentException ex)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)291UL, GlobalDatabaseHandler.Tracer, "Permanent error when looking for local databases {0}", new object[]
					{
						ex
					});
					return null;
				}
				catch (MapiRetryableException ex2)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)292UL, GlobalDatabaseHandler.Tracer, "Retryable error when looking for local databases {0}", new object[]
					{
						ex2
					});
					return null;
				}
				return sortedDictionary;
			}

			// Token: 0x0600022A RID: 554 RVA: 0x0000EB28 File Offset: 0x0000CD28
			public void OnNewDatabaseManager(DatabaseManager databaseManager)
			{
			}
		}
	}
}
