using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Globalization;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.EdgeSync;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Logging;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x0200000F RID: 15
	internal class SynchronizerManager : Job
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00005B94 File Offset: 0x00003D94
		public SynchronizerManager(EdgeServer edgeServer, SynchronizationProvider provider, List<TypeSynchronizer> typeSynchronizers, ITopologyConfigurationSession configSession, IDirectorySession sourceSession, SyncNowState syncNowState, TimeSpan syncInterval, EdgeSyncLogSession logSession) : base(syncInterval)
		{
			this.provider = provider;
			this.syncNowState = syncNowState;
			this.edgeServer = edgeServer;
			this.configSession = configSession;
			this.sourceSession = sourceSession;
			this.cookies = new Dictionary<string, Cookie>();
			this.typeSynchronizers = typeSynchronizers;
			this.logSession = logSession;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00005BF4 File Offset: 0x00003DF4
		public EdgeSyncLogSession LogSession
		{
			get
			{
				return this.logSession;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00005BFC File Offset: 0x00003DFC
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00005C04 File Offset: 0x00003E04
		public bool ForceFullSync
		{
			get
			{
				return this.forceFullSync;
			}
			set
			{
				this.forceFullSync = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00005C0D File Offset: 0x00003E0D
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00005C15 File Offset: 0x00003E15
		public bool ForceUpdateCookie
		{
			get
			{
				return this.forceUpdateCookie;
			}
			set
			{
				this.forceUpdateCookie = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00005C1E File Offset: 0x00003E1E
		public List<TypeSynchronizer> TypeSynchronizers
		{
			get
			{
				return this.typeSynchronizers;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00005C26 File Offset: 0x00003E26
		internal EdgeServer EdgeServer
		{
			get
			{
				return this.edgeServer;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00005C2E File Offset: 0x00003E2E
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00005C36 File Offset: 0x00003E36
		protected TargetConnection TargetConnection
		{
			get
			{
				return this.targetConnection;
			}
			set
			{
				this.targetConnection = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00005C3F File Offset: 0x00003E3F
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00005C47 File Offset: 0x00003E47
		protected Connection SourceConnection
		{
			get
			{
				return this.sourceConnection;
			}
			set
			{
				this.sourceConnection = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00005C50 File Offset: 0x00003E50
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00005C58 File Offset: 0x00003E58
		protected Dictionary<string, Cookie> Cookies
		{
			get
			{
				return this.cookies;
			}
			set
			{
				this.cookies = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00005C61 File Offset: 0x00003E61
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00005C69 File Offset: 0x00003E69
		protected ITopologyConfigurationSession ConfigSession
		{
			get
			{
				return this.configSession;
			}
			set
			{
				this.configSession = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00005C72 File Offset: 0x00003E72
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00005C7A File Offset: 0x00003E7A
		protected Status Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00005C83 File Offset: 0x00003E83
		protected bool ShouldFailoverDc
		{
			get
			{
				return this.whenLastConnectFailed != DateTime.MaxValue && DateTime.UtcNow - this.whenLastConnectFailed > EdgeSyncSvc.EdgeSync.Config.ServiceConfig.FailoverDCInterval;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00005CC2 File Offset: 0x00003EC2
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00005CCA File Offset: 0x00003ECA
		protected EdgeSynchronizerPerfCountersInstance PerfCounters
		{
			get
			{
				return this.perfCounters;
			}
			set
			{
				this.perfCounters = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00005CD3 File Offset: 0x00003ED3
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00005CDB File Offset: 0x00003EDB
		protected SyncTreeType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005CE4 File Offset: 0x00003EE4
		public static void LoadTargetCache(TargetConnection targetConnection, TestShutdownAndLeaseDelegate testShutdownAndLease, object state)
		{
			TypeSynchronizer typeSynchronizer = (TypeSynchronizer)state;
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>(0L, "Loading target cache for type {0}", typeSynchronizer.Name);
			foreach (ExSearchResultEntry exSearchResultEntry in ((LdapTargetConnection)targetConnection).PagedScan(null, typeSynchronizer.TargetQueryFilter, SearchScope.Subtree, typeSynchronizer.ReadTargetAttributes))
			{
				if (testShutdownAndLease())
				{
					throw new ExDirectoryException("Shutdown or lost the lease", null);
				}
				if (exSearchResultEntry.Attributes.ContainsKey("msExchEdgeSyncSourceGuid"))
				{
					DirectoryAttribute directoryAttribute = exSearchResultEntry.Attributes["msExchEdgeSyncSourceGuid"];
					byte[] array = (byte[])directoryAttribute.GetValues(typeof(byte[]))[0];
					SortedList<string, DirectoryAttribute> attributesToCopy = SynchronizerManager.RestrictToCopyAttributes(exSearchResultEntry.Attributes, typeSynchronizer.CopyAttributes);
					byte[] value = Util.ComputeHash(attributesToCopy);
					typeSynchronizer.TargetCache[array] = value;
					if (ExTraceGlobals.SynchronizationJobTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string, Guid>(0L, "Add {0}, sourceGuid = {1} to target cache", exSearchResultEntry.DistinguishedName, new Guid(array));
					}
				}
				else
				{
					byte[] array2 = Guid.NewGuid().ToByteArray();
					((LdapTargetConnection)targetConnection).WriteSourceGuid(exSearchResultEntry.DistinguishedName, array2);
					typeSynchronizer.TargetCache[array2] = null;
					if (ExTraceGlobals.SynchronizationJobTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string, Guid>(0L, "{0} doesn't have sourceGuid, assign value {1}", exSearchResultEntry.DistinguishedName, new Guid(array2));
					}
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005E74 File Offset: 0x00004074
		public static CacheLookupResult TargetCacheLookup(ExSearchResultEntry entry, object state)
		{
			TypeSynchronizer typeSynchronizer = (TypeSynchronizer)state;
			DirectoryAttribute directoryAttribute = entry.Attributes["objectGUID"];
			byte[] key = (byte[])directoryAttribute.GetValues(typeof(byte[]))[0];
			CacheLookupResult cacheLookupResult;
			if (typeSynchronizer.TargetCache.ContainsKey(key))
			{
				SortedList<string, DirectoryAttribute> attributesToCopy = SynchronizerManager.RestrictToCopyAttributes(entry.Attributes, typeSynchronizer.CopyAttributes);
				byte[] x = typeSynchronizer.TargetCache[key];
				byte[] y = Util.ComputeHash(attributesToCopy);
				if (!ArrayComparer<byte>.Comparer.Equals(x, y))
				{
					cacheLookupResult = CacheLookupResult.Changed;
				}
				else
				{
					cacheLookupResult = CacheLookupResult.Unchanged;
				}
				typeSynchronizer.TargetCache.Remove(key);
			}
			else
			{
				cacheLookupResult = CacheLookupResult.NotPresent;
			}
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string, CacheLookupResult>(0L, "TargetCacheLookup result for {0} is {1}", entry.DistinguishedName, cacheLookupResult);
			return cacheLookupResult;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005F2C File Offset: 0x0000412C
		public static bool TargetCacheRemoveTargetOnlyEntries(TargetConnection targetConnection, TestShutdownAndLeaseDelegate testShutdownAndLease, object state)
		{
			TypeSynchronizer typeSynchronizer = (TypeSynchronizer)state;
			bool result = true;
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>(0L, "Remove target only objects for type {0}", typeSynchronizer.Name);
			foreach (byte[] array in typeSynchronizer.TargetCache.Keys)
			{
				if (testShutdownAndLease())
				{
					return false;
				}
				try
				{
					((LdapTargetConnection)targetConnection).OnDeleteEntry(array);
				}
				catch (ExDirectoryException arg)
				{
					ExTraceGlobals.SynchronizationJobTracer.TraceError<byte[], ExDirectoryException>((long)typeSynchronizer.GetHashCode(), "Failed to delete target only item with sourceGuid = {0}, because {1}", array, arg);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005FE8 File Offset: 0x000041E8
		protected virtual bool TryInitializeCookie()
		{
			return false;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005FEC File Offset: 0x000041EC
		protected bool IsCookieExpired(Cookie cookie)
		{
			if (DateTime.UtcNow - cookie.LastUpdated > EdgeSyncSvc.EdgeSync.Config.ServiceConfig.CookieValidDuration)
			{
				ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string, DateTime>((long)this.GetHashCode(), "Cookie for {0} expired because it was last updated at {1}", cookie.BaseDN, cookie.LastUpdated);
				return true;
			}
			return false;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000604C File Offset: 0x0000424C
		protected virtual bool TryLeaseTarget()
		{
			Server localServer = EdgeSyncSvc.EdgeSync.Topology.LocalServer;
			lock (this.edgeServer)
			{
				if (this.edgeServer.EdgeLease == null)
				{
					this.edgeServer.InitializeLease(localServer.DistinguishedName, localServer.VersionNumber, this.LogSession, new TestShutdownDelegate(this.TestShutdown));
				}
			}
			return this.edgeServer.EdgeLease.TryAddLock(this.targetConnection, this.provider, this.startNow ? LeaseAcquisitionMode.OverrideOptions : LeaseAcquisitionMode.RespectOptions);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000060F4 File Offset: 0x000042F4
		protected override JobBackgroundResult BackgroundExecute()
		{
			this.status.Starting();
			this.intermediateResultCount = 0;
			try
			{
				this.targetConnection = this.provider.CreateTargetConnection(this.edgeServer.Config, this.type, new TestShutdownAndLeaseDelegate(this.TestShutdownAndLease), this.LogSession);
				if (this.targetConnection == null)
				{
					ExTraceGlobals.SynchronizationJobTracer.TraceError<string>((long)this.GetHashCode(), "Failed to create target connection for {0}", this.edgeServer.Host);
					this.status.Finished(StatusResult.CouldNotConnect);
					return JobBackgroundResult.ReSchedule;
				}
				if (this.targetConnection.SkipSyncCycle)
				{
					ExTraceGlobals.SynchronizationJobTracer.Information<string>((long)this.GetHashCode(), "Skipping the sync cycle for Target connection {0}", this.edgeServer.Host);
					this.status.Finished(StatusResult.SyncDisabled);
					return JobBackgroundResult.ReSchedule;
				}
				using (this.targetConnection)
				{
					if (!this.TryLeaseTarget())
					{
						ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Unable to lease {0}, lease held by {1}", this.edgeServer.Host, this.edgeServer.EdgeLease.LeaseHeldBy);
						this.status.FailureDetails = this.edgeServer.EdgeLease.LeaseHeldBy;
						this.status.Finished(StatusResult.CouldNotLease);
						return JobBackgroundResult.ReSchedule;
					}
					try
					{
						if (!this.TryInitializeCookie())
						{
							this.status.Finished(StatusResult.Incomplete);
							return JobBackgroundResult.ReSchedule;
						}
						if (!this.targetConnection.OnSynchronizing())
						{
							this.status.Finished(StatusResult.Incomplete);
							return JobBackgroundResult.ReSchedule;
						}
						StatusResult statusResult = this.InternalSynchronize();
						this.status.Finished(statusResult);
						if (statusResult == StatusResult.Success)
						{
							this.cycles++;
							this.perfCounters.AddedPerCycle.RawValue = this.perfCounters.Added.RawValue / (long)this.cycles;
							this.perfCounters.UpdatedPerCycle.RawValue = this.perfCounters.Updated.RawValue / (long)this.cycles;
							this.perfCounters.DeletedPerCycle.RawValue = this.perfCounters.Deleted.RawValue / (long)this.cycles;
						}
					}
					catch (EdgeSyncCycleFailedException ex)
					{
						ExTraceGlobals.SynchronizationJobTracer.TraceError<EdgeSyncCycleFailedException>((long)this.GetHashCode(), "EdgeSyncCycleFailedException occurred and the sync cycle will be aborted; exception details: {0}", ex);
						this.status.FailureDetails = ex.Message;
						this.status.Finished(StatusResult.Incomplete);
						return JobBackgroundResult.ReSchedule;
					}
					finally
					{
						this.edgeServer.EdgeLease.ReleaseLock(this.targetConnection, this.provider);
						if (this.sourceConnection != null)
						{
							this.sourceConnection.Dispose();
							this.sourceConnection = null;
						}
					}
				}
			}
			catch (ExDirectoryException ex2)
			{
				this.LogSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, ex2, "Failed to create target connection for " + this.edgeServer.Host);
				EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_EdgeLeaseException, null, new object[]
				{
					ex2.Message,
					this.edgeServer.Host,
					this.edgeServer.Port
				});
				ExTraceGlobals.SynchronizationJobTracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to lease {0} because {1}", this.edgeServer.Host, ex2.Message);
				this.status.FailureDetails = ex2.Message;
				this.status.Finished(StatusResult.CouldNotConnect);
			}
			finally
			{
				if (this.syncNowState != null)
				{
					this.syncNowState.RecordResult(this.status);
				}
				this.startNow = false;
				this.forceFullSync = false;
				this.forceUpdateCookie = false;
			}
			return JobBackgroundResult.ReSchedule;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000064FC File Offset: 0x000046FC
		protected virtual StatusResult InternalSynchronize()
		{
			StatusResult statusResult = StatusResult.InProgress;
			bool flag = true;
			Dictionary<string, Cookie> dictionary = new Dictionary<string, Cookie>();
			this.syncCycleNumber += 1UL;
			this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, this.type.ToString(), string.Format(CultureInfo.InvariantCulture, "Start Synchronization Cycle# {0} ForceFullSync {1} ForceCookieUpdate {2}; CookieDetails {3}", new object[]
			{
				this.syncCycleNumber,
				this.forceFullSync,
				this.forceUpdateCookie,
				Util.GetCookieInformationToLog(this.cookies)
			}));
			foreach (TypeSynchronizer typeSynchronizer in this.typeSynchronizers)
			{
				if (typeSynchronizer.TargetCacheEnabled)
				{
					typeSynchronizer.ResetTargetCacheState(false);
				}
			}
			foreach (string text in this.cookies.Keys)
			{
				Cookie cookie = this.cookies[text];
				this.fullSyncMode = (cookie.CookieValue == null);
				if (this.fullSyncMode)
				{
					this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, cookie.BaseDN, "FullSync mode used");
				}
				Cookie cookie2 = null;
				Cookie cookie3 = null;
				if (!this.TryConnectToSource(cookie))
				{
					this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, this.type.ToString(), string.Format(CultureInfo.InvariantCulture, "End Synchronization Cycle# {0} with error unable to connect to source Active Directory", new object[]
					{
						this.syncCycleNumber
					}));
					return StatusResult.CouldNotConnect;
				}
				this.targetConnection.OnConnectedToSource(this.sourceConnection);
				this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.SyncEngine, this.type.ToString(), string.Format(CultureInfo.InvariantCulture, "Synchronize domain {0}, fullSync {1}, Cookie {2}", new object[]
				{
					text,
					this.fullSyncMode,
					cookie.ToString()
				}));
				foreach (TypeSynchronizer typeSynchronizer2 in this.typeSynchronizers)
				{
					this.currentTypeSynchronizer = typeSynchronizer2;
					bool flag2 = this.SynchronizeOneType(cookie, out cookie2);
					if (cookie2 != null && cookie3 == null)
					{
						cookie3 = cookie2;
					}
					if (!flag2)
					{
						ExTraceGlobals.SynchronizationJobTracer.TraceError<string>((long)this.GetHashCode(), "Failed to synchronize domain {0}", text);
						statusResult = StatusResult.Incomplete;
						if (!this.forceUpdateCookie)
						{
							flag = false;
						}
					}
				}
				if (cookie3 == null)
				{
					this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, cookie.BaseDN, "Failed to get back a new cookie value");
					flag = false;
					statusResult = StatusResult.Incomplete;
				}
				if (flag)
				{
					cookie3.DomainController = this.sourceConnection.Fqdn;
					cookie3.LastUpdated = DateTime.UtcNow;
					dictionary[cookie3.BaseDN] = cookie3;
					ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "Successfully synchronized domain {0}", text);
				}
			}
			bool flag3 = false;
			foreach (TypeSynchronizer typeSynchronizer3 in this.typeSynchronizers)
			{
				if (typeSynchronizer3.TargetCacheEnabled && this.fullSyncMode)
				{
					if (!typeSynchronizer3.HasTargetCacheFullSyncError)
					{
						this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, string.Format(CultureInfo.InvariantCulture, "Type: {0}, Size: {1}", new object[]
						{
							typeSynchronizer3.Name,
							typeSynchronizer3.TargetCache.Count
						}), "Attempt to remove target-only entries from TargetCache");
						if (!typeSynchronizer3.TargetCacheRemoveTargetOnlyEntries(this.targetConnection, new TestShutdownAndLeaseDelegate(this.TestShutdown), typeSynchronizer3))
						{
							this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, typeSynchronizer3.Name, "Failed to remove target-only entries from TargetCache");
							flag3 = true;
						}
						else
						{
							this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, typeSynchronizer3.Name, "Successfully removed target-only entries from TargetCache");
						}
					}
					else
					{
						this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, typeSynchronizer3.Name, "Failed to complete target cache processing");
						flag3 = true;
					}
				}
				typeSynchronizer3.ResetTargetCacheState(false);
			}
			if (flag3)
			{
				statusResult = StatusResult.Incomplete;
				if (!this.forceUpdateCookie)
				{
					flag = false;
				}
			}
			if (!this.targetConnection.OnSynchronized())
			{
				statusResult = StatusResult.Incomplete;
				if (!this.forceUpdateCookie)
				{
					flag = false;
				}
			}
			if (flag)
			{
				this.cookies = dictionary;
				if (!this.TrySaveCookie())
				{
					statusResult = StatusResult.Incomplete;
					ExTraceGlobals.SynchronizationJobTracer.TraceError((long)this.GetHashCode(), "Failed to write cookie table back to target");
					this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, this.type.ToString(), "Failed to save cookies");
				}
				else
				{
					if (this.fullSyncMode)
					{
						this.perfCounters.FullSyncs.Increment();
					}
					foreach (string arg in this.cookies.Keys)
					{
						ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "Successfully updated cookie table for {0}", arg);
					}
				}
			}
			StatusResult statusResult2 = (statusResult == StatusResult.InProgress) ? StatusResult.Success : statusResult;
			this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, this.type.ToString(), string.Format(CultureInfo.InvariantCulture, "End Synchronization Cycle# {0} with SyncResult {1}, Scanned {2}, Added {3}, Updated {4}, Deleted {5}", new object[]
			{
				this.syncCycleNumber,
				statusResult2,
				this.status.Scanned,
				this.status.Added,
				this.status.Updated,
				this.status.Deleted
			}));
			return statusResult2;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006B28 File Offset: 0x00004D28
		private static SortedList<string, DirectoryAttribute> RestrictToCopyAttributes(Dictionary<string, DirectoryAttribute> attributes, string[] attributesToCopy)
		{
			SortedList<string, DirectoryAttribute> sortedList = new SortedList<string, DirectoryAttribute>();
			foreach (KeyValuePair<string, DirectoryAttribute> keyValuePair in attributes)
			{
				if (Array.IndexOf<string>(attributesToCopy, keyValuePair.Key) != -1 && !keyValuePair.Key.Equals("objectClass", StringComparison.OrdinalIgnoreCase))
				{
					sortedList.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return sortedList;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006BB0 File Offset: 0x00004DB0
		private bool SynchronizeOneType(Cookie cookie, out Cookie retCookie)
		{
			retCookie = Cookie.Clone(cookie);
			bool result = true;
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "Synchronize type {0}", this.currentTypeSynchronizer.Name);
			try
			{
				if (this.fullSyncMode && this.currentTypeSynchronizer.TargetCacheEnabled && !this.currentTypeSynchronizer.TargetCacheFullyLoaded)
				{
					this.currentTypeSynchronizer.LoadTargetCache(this.targetConnection, new TestShutdownAndLeaseDelegate(this.TestShutdownAndLease), this.currentTypeSynchronizer);
					this.currentTypeSynchronizer.TargetCacheFullyLoaded = true;
					this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, string.Format(CultureInfo.InvariantCulture, "Type: {0}, Size: {1}", new object[]
					{
						this.currentTypeSynchronizer.Name,
						this.currentTypeSynchronizer.TargetCache.Count
					}), "Successfully loaded TargetCache");
				}
			}
			catch (ExDirectoryException ex)
			{
				ExTraceGlobals.SynchronizationJobTracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to load target cache for {0} because of {1}", this.currentTypeSynchronizer.SourceQueryFilter, ex.Message);
				this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, ex, "Failed to load TargetCache for " + this.currentTypeSynchronizer.Name);
				this.currentTypeSynchronizer.ResetTargetCacheState(true);
				return false;
			}
			try
			{
				foreach (ExSearchResultEntry exSearchResultEntry in this.sourceConnection.DirSyncScan(retCookie, this.currentTypeSynchronizer.SourceQueryFilter, this.currentTypeSynchronizer.SearchScope, this.currentTypeSynchronizer.ReadSourceAttributes))
				{
					if (this.TestShutdownAndLease())
					{
						if (this.fullSyncMode && this.currentTypeSynchronizer.TargetCacheEnabled)
						{
							this.currentTypeSynchronizer.HasTargetCacheFullSyncError = true;
						}
						return false;
					}
					try
					{
						if (this.currentTypeSynchronizer.Filter != null && !exSearchResultEntry.IsDeleted)
						{
							switch (this.currentTypeSynchronizer.Filter(exSearchResultEntry, this.sourceConnection, this.targetConnection))
							{
							case FilterResult.Skip:
								this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.SyncEngine, exSearchResultEntry.DistinguishedName, "Filter and skip the entry");
								continue;
							case FilterResult.SkipAndRemoveFromTarget:
								this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.SyncEngine, exSearchResultEntry.DistinguishedName, "Filter and remove the entry from target");
								this.OnDeleteEntry(exSearchResultEntry);
								continue;
							}
						}
					}
					catch (ExDirectoryException exception)
					{
						this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, exception, "Failed to filter object " + exSearchResultEntry.DistinguishedName);
						result = false;
						continue;
					}
					this.ReportIntermediateStatus();
					this.status.ObjectScanned();
					try
					{
						this.SynchronizeOneEntry(exSearchResultEntry);
					}
					catch (ExDirectoryException ex2)
					{
						ExTraceGlobals.SynchronizationJobTracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to sync entry {0} because of {1}", exSearchResultEntry.DistinguishedName, ex2.Message);
						this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, ex2, "Failed to synchronize entry " + exSearchResultEntry.DistinguishedName);
						result = false;
					}
				}
			}
			catch (ExDirectoryException ex3)
			{
				ExTraceGlobals.SynchronizationJobTracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to read active directory for replication data {0} because of {1}", this.currentTypeSynchronizer.SourceQueryFilter, ex3.Message);
				this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, ex3, "Failed to read active directory for replication data " + this.currentTypeSynchronizer.SourceQueryFilter);
				if (this.fullSyncMode && this.currentTypeSynchronizer.TargetCacheEnabled)
				{
					this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, this.currentTypeSynchronizer.Name, "Failed to fullsync entries for current type");
					this.currentTypeSynchronizer.HasTargetCacheFullSyncError = true;
				}
				return false;
			}
			if (this.fullSyncMode && this.currentTypeSynchronizer.TargetCacheEnabled)
			{
				this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, this.currentTypeSynchronizer.Name, "Successful fullsync entries for current type under domain " + cookie.BaseDN);
			}
			return result;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006FF0 File Offset: 0x000051F0
		private void SynchronizeOneEntry(ExSearchResultEntry entry)
		{
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "Synchronize entry {0}", entry.DistinguishedName);
			if (this.currentTypeSynchronizer.PreDecorate != null && !this.currentTypeSynchronizer.PreDecorate(entry, this.sourceConnection, this.targetConnection, this))
			{
				ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "PreDecorate returned false for entry {0}; ignoring the entry", entry.DistinguishedName);
				return;
			}
			SortedList<string, DirectoryAttribute> sourceAttributes = null;
			if (!entry.IsDeleted)
			{
				sourceAttributes = SynchronizerManager.RestrictToCopyAttributes(entry.Attributes, this.currentTypeSynchronizer.CopyAttributes);
			}
			if (this.fullSyncMode)
			{
				if (entry.IsDeleted && this.currentTypeSynchronizer.SkipDeletedEntriesInFullSyncMode)
				{
					return;
				}
				if (this.currentTypeSynchronizer.TargetCacheEnabled)
				{
					CacheLookupResult cacheLookupResult = this.currentTypeSynchronizer.TargetCacheLookup(entry, this.currentTypeSynchronizer);
					if (cacheLookupResult == CacheLookupResult.Changed)
					{
						this.OnModifyEntry(entry, sourceAttributes);
					}
					else if (cacheLookupResult == CacheLookupResult.NotPresent)
					{
						this.OnAddEntry(entry, sourceAttributes);
					}
				}
				else if (entry.IsDeleted)
				{
					this.OnDeleteEntry(entry);
				}
				else if (entry.IsNew)
				{
					this.OnAddEntry(entry, sourceAttributes);
				}
				else
				{
					this.OnModifyEntry(entry, sourceAttributes);
				}
			}
			else if (entry.IsDeleted)
			{
				this.OnDeleteEntry(entry);
			}
			else if (entry.IsNew)
			{
				this.OnAddEntry(entry, sourceAttributes);
			}
			else
			{
				if (entry.IsRenamed)
				{
					this.targetConnection.OnRenameEntry(entry);
				}
				this.OnModifyEntry(entry, sourceAttributes);
			}
			if (this.currentTypeSynchronizer.PostDecorate != null && !entry.IsDeleted)
			{
				this.currentTypeSynchronizer.PostDecorate(entry, this.sourceConnection, this.targetConnection, this);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000071EC File Offset: 0x000053EC
		private bool TryConnectToSource(Cookie cookie)
		{
			if (this.sourceConnection == null)
			{
				ADObjectId rootId = null;
				PooledLdapConnection sourcePooledConnection = null;
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					sourcePooledConnection = this.sourceSession.GetReadConnection(this.ShouldFailoverDc ? null : cookie.DomainController, ref rootId);
				}, 3);
				if (!adoperationResult.Succeeded)
				{
					ExTraceGlobals.SynchronizationJobTracer.TraceError<string>((long)this.GetHashCode(), "GetReadConnection failed to connect to DC/GC with error {1}", adoperationResult.Exception.Message);
					EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_FailedToReadEntriesFromAD, null, new object[]
					{
						this.sourceSession.DomainController,
						this.sourceSession.UseGlobalCatalog ? "3286" : "389",
						adoperationResult.Exception.Message
					});
					this.LogSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.SyncEngine, adoperationResult.Exception, "Failed to connect to any DC");
					cookie.DomainController = null;
					if (this.whenLastConnectFailed == DateTime.MaxValue)
					{
						this.whenLastConnectFailed = DateTime.UtcNow;
					}
					return false;
				}
				this.sourceConnection = new Connection(sourcePooledConnection, EdgeSyncSvc.EdgeSync.AppConfig);
				this.whenLastConnectFailed = DateTime.MaxValue;
			}
			return true;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00007330 File Offset: 0x00005530
		private bool TrySaveCookie()
		{
			bool result;
			lock (this.edgeServer)
			{
				Dictionary<string, Cookie> dictionary = null;
				if (!this.TargetConnection.TryReadCookie(out dictionary))
				{
					ExTraceGlobals.SynchronizationJobTracer.TraceError<string>((long)this.GetHashCode(), "Failed to read cookie record for {0}", this.EdgeServer.Host);
					result = false;
				}
				else
				{
					foreach (Cookie cookie in this.Cookies.Values)
					{
						dictionary[cookie.BaseDN] = cookie;
					}
					result = this.targetConnection.TrySaveCookie(dictionary);
				}
			}
			return result;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00007400 File Offset: 0x00005600
		private bool TestShutdown()
		{
			if (this.shuttingDown)
			{
				this.status.Finished(StatusResult.Aborted);
			}
			return this.shuttingDown;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00007420 File Offset: 0x00005620
		private bool TestShutdownAndLease()
		{
			if (this.shuttingDown)
			{
				this.status.Finished(StatusResult.Aborted);
				return true;
			}
			if (this.edgeServer.EdgeLease != null && !this.edgeServer.EdgeLease.TryRefreshLock(this.targetConnection, this.provider))
			{
				this.status.FailureDetails = this.edgeServer.EdgeLease.LeaseHeldBy;
				this.status.Finished(StatusResult.LostLease);
				return true;
			}
			return false;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000749A File Offset: 0x0000569A
		private void OnAddEntry(ExSearchResultEntry entry, SortedList<string, DirectoryAttribute> sourceAttributes)
		{
			if (this.targetConnection.OnAddEntry(entry, sourceAttributes) == SyncResult.Added)
			{
				this.perfCounters.Added.Increment();
				this.status.ObjectAdded();
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000074C8 File Offset: 0x000056C8
		private void OnDeleteEntry(ExSearchResultEntry entry)
		{
			if (this.targetConnection.OnDeleteEntry(entry) == SyncResult.Deleted)
			{
				this.perfCounters.Deleted.Increment();
				this.status.ObjectDeleted();
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000074F8 File Offset: 0x000056F8
		private void OnModifyEntry(ExSearchResultEntry entry, SortedList<string, DirectoryAttribute> sourceAttributes)
		{
			try
			{
				this.targetConnection.OnModifyEntry(entry, sourceAttributes);
			}
			catch (ExDirectoryException ex)
			{
				if (ex.ResultCode != ResultCode.NoSuchObject)
				{
					ExTraceGlobals.SynchronizationJobTracer.TraceError<string>((long)this.GetHashCode(), "Failed to modify entry {0}", entry.DistinguishedName);
					throw;
				}
				ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "Modify entry doesn't exist for {0}. Add the object instead", entry.DistinguishedName);
				entry = this.sourceConnection.ReadObjectEntry(entry.DistinguishedName, this.currentTypeSynchronizer.ReadSourceAttributes);
				if (entry == null)
				{
					return;
				}
				if (this.currentTypeSynchronizer.PreDecorate != null && !this.currentTypeSynchronizer.PreDecorate(entry, this.sourceConnection, this.targetConnection, this))
				{
					ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "PreDecorate returned false for re-read entry {0}; ignoring the entry", entry.DistinguishedName);
					return;
				}
				SortedList<string, DirectoryAttribute> sourceAttributes2 = SynchronizerManager.RestrictToCopyAttributes(entry.Attributes, this.currentTypeSynchronizer.CopyAttributes);
				if (this.targetConnection.OnAddEntry(entry, sourceAttributes2) == SyncResult.Added)
				{
					this.perfCounters.Added.Increment();
					this.status.ObjectAdded();
				}
				return;
			}
			this.perfCounters.Updated.Increment();
			this.status.ObjectUpdated();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007644 File Offset: 0x00005844
		private void ReportIntermediateStatus()
		{
			if (this.intermediateResultCount++ > 1000)
			{
				this.intermediateResultCount = 0;
				this.syncNowState.RecordResultIntermediate(this.status);
			}
		}

		// Token: 0x04000052 RID: 82
		private const int IntermediateResultFrequency = 1000;

		// Token: 0x04000053 RID: 83
		private EdgeSyncLogSession logSession;

		// Token: 0x04000054 RID: 84
		private ulong syncCycleNumber;

		// Token: 0x04000055 RID: 85
		private SyncTreeType type;

		// Token: 0x04000056 RID: 86
		private SynchronizationProvider provider;

		// Token: 0x04000057 RID: 87
		private TargetConnection targetConnection;

		// Token: 0x04000058 RID: 88
		private Connection sourceConnection;

		// Token: 0x04000059 RID: 89
		private Status status;

		// Token: 0x0400005A RID: 90
		private Dictionary<string, Cookie> cookies;

		// Token: 0x0400005B RID: 91
		private SyncNowState syncNowState;

		// Token: 0x0400005C RID: 92
		private bool forceFullSync;

		// Token: 0x0400005D RID: 93
		private bool forceUpdateCookie;

		// Token: 0x0400005E RID: 94
		private IDirectorySession sourceSession;

		// Token: 0x0400005F RID: 95
		private ITopologyConfigurationSession configSession;

		// Token: 0x04000060 RID: 96
		private EdgeServer edgeServer;

		// Token: 0x04000061 RID: 97
		private List<TypeSynchronizer> typeSynchronizers;

		// Token: 0x04000062 RID: 98
		private TypeSynchronizer currentTypeSynchronizer;

		// Token: 0x04000063 RID: 99
		private DateTime whenLastConnectFailed = DateTime.MaxValue;

		// Token: 0x04000064 RID: 100
		private bool fullSyncMode;

		// Token: 0x04000065 RID: 101
		private EdgeSynchronizerPerfCountersInstance perfCounters;

		// Token: 0x04000066 RID: 102
		private int intermediateResultCount;

		// Token: 0x04000067 RID: 103
		private int cycles;
	}
}
