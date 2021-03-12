using System;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Replay.Dumpster;
using Microsoft.Exchange.Cluster.Replay.MountPoint;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000158 RID: 344
	internal sealed class ReplayState
	{
		// Token: 0x06000D5A RID: 3418 RVA: 0x0003A692 File Offset: 0x00038892
		internal static ReplayState GetReplayState(string nodeName, string sourceNodeName, LockType lockType, string identity, string databaseName)
		{
			return new ReplayState(nodeName, sourceNodeName, lockType, identity, databaseName);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0003A69F File Offset: 0x0003889F
		internal static ReplayState TestGetReplayState(string serverName, string identity, bool fservice)
		{
			return ReplayState.TestGetReplayState(serverName, identity, string.Empty, fservice);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0003A6AE File Offset: 0x000388AE
		internal static ReplayState TestGetReplayState(string serverName, string identity, string databaseName, bool fservice)
		{
			return new ReplayState(Environment.MachineName, Environment.MachineName, fservice ? LockType.ReplayService : LockType.Remote, identity, databaseName);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0003A6C8 File Offset: 0x000388C8
		internal static void DeleteState(string nodeName, Database db)
		{
			ReplayState.DeleteState(nodeName, db, true);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0003A6D4 File Offset: 0x000388D4
		internal static void DeleteState(string nodeName, Database db, bool fDeleteGlobalAlso)
		{
			ReplayState replayState = ReplayState.GetReplayState(nodeName, nodeName, LockType.Remote, ReplayConfiguration.GetIdentityFromGuid(db.Guid), db.Name);
			replayState.m_stateIO.DeleteState();
			if (fDeleteGlobalAlso)
			{
				replayState.m_stateIOGlobal.DeleteState();
				return;
			}
			ExTraceGlobals.StateTracer.TraceDebug<string>((long)replayState.GetHashCode(), "DeleteState(): Skipping deletion of global state m_stateIOGlobal for DB '{0}'.", db.Name);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0003A734 File Offset: 0x00038934
		internal static void LogCrimsonEventOnStateChange<T>(string databaseName, string databaseGuidStr, string serverName, string stateName, T oldValue, T newValue)
		{
			if (oldValue == null || !oldValue.Equals(newValue))
			{
				ReplayCrimsonEvents.ReplayStateChange.Log<string, string, string, string, string, string>(databaseName, databaseGuidStr, serverName, stateName, (oldValue != null) ? oldValue.ToString() : "<Null>", (newValue != null) ? newValue.ToString() : "<Null>");
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0003A7A8 File Offset: 0x000389A8
		private ReplayState(string nodeName, string sourceNodeName, LockType lockType, string identity, string databaseName)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (sourceNodeName == null)
			{
				throw new ArgumentNullException("sourceNodeName");
			}
			this.m_nodeName = nodeName;
			this.m_sourceNodeName = sourceNodeName;
			this.m_replayIdentity = identity;
			this.m_replayIdentityGuid = new Guid(this.m_replayIdentity);
			this.m_databaseName = databaseName;
			string machineName = null;
			if (LockType.Remote == lockType)
			{
				machineName = this.m_nodeName;
			}
			this.m_stateIO = new RegistryStateIO(machineName, this.m_replayIdentity, false);
			this.m_stateIOGlobal = new RegistryStateIO(machineName, this.m_replayIdentity, true);
			this.m_safetyNetTable = new SafetyNetInfoCache(this.m_replayIdentity, this.m_databaseName);
			if (lockType == LockType.ReplayService)
			{
				this.m_suspendLock = StateLock.GetNewOrExistingStateLock(this.m_databaseName, this.m_replayIdentity);
				this.m_suspendLockRemote = new StateLockRemote("Suspend", this.m_databaseName, this.m_stateIOGlobal);
				return;
			}
			if (lockType == LockType.Remote)
			{
				this.m_suspendLockRemote = new StateLockRemote("Suspend", this.m_databaseName, this.m_stateIOGlobal);
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0003A8D9 File Offset: 0x00038AD9
		internal void UseSetBrokenForIOFailures(ISetBroken setBroken)
		{
			this.m_stateIO.UseSetBrokenForIOFailures(setBroken);
			this.m_stateIOGlobal.UseSetBrokenForIOFailures(setBroken);
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0003A8F3 File Offset: 0x00038AF3
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x0003A8FB File Offset: 0x00038AFB
		internal IStateIO StateIOTestHook
		{
			get
			{
				return this.m_stateIO;
			}
			set
			{
				this.m_stateIO = value;
			}
		}

		// Token: 0x1700033D RID: 829
		// (set) Token: 0x06000D64 RID: 3428 RVA: 0x0003A904 File Offset: 0x00038B04
		internal IPerfmonCounters PerfmonCounters
		{
			set
			{
				this.m_perfmonCounters = value;
				if (this.m_perfmonCounters != null)
				{
					this.m_perfmonCounters.ActivationSuspended = (this.ActivationSuspended ? 1L : 0L);
					this.m_perfmonCounters.ReplayLagDisabled = (this.ReplayLagDisabled ? 1L : 0L);
				}
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0003A950 File Offset: 0x00038B50
		public LockOwner SuspendLockOwner
		{
			get
			{
				return this.m_suspendLock.CurrentOwner;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0003A95D File Offset: 0x00038B5D
		public StateLock SuspendLock
		{
			get
			{
				return this.m_suspendLock;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x0003A965 File Offset: 0x00038B65
		public StateLockRemote SuspendLockRemote
		{
			get
			{
				return this.m_suspendLockRemote;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0003A970 File Offset: 0x00038B70
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x0003A992 File Offset: 0x00038B92
		public string SuspendMessage
		{
			get
			{
				string result;
				this.m_stateIOGlobal.TryReadString("SuspendMessage", null, out result);
				return result;
			}
			set
			{
				this.m_stateIOGlobal.WriteString("SuspendMessage", value, false);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x0003A9A8 File Offset: 0x00038BA8
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x0003A9CC File Offset: 0x00038BCC
		public bool ConfigBroken
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("ConfigBroken", false, out result);
				return result;
			}
			set
			{
				this.LogCrimsonEventOnStateChange<bool>("ConfigBroken", this.ConfigBroken, value);
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "ConfigBroken is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteBool("ConfigBroken", value, false);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x0003AA1A File Offset: 0x00038C1A
		// (set) Token: 0x06000D6D RID: 3437 RVA: 0x0003AA42 File Offset: 0x00038C42
		public string ConfigBrokenMessage
		{
			get
			{
				if (this.m_configBrokenMessage == null)
				{
					this.m_stateIO.TryReadString("ConfigBrokenMessage", null, out this.m_configBrokenMessage);
				}
				return this.m_configBrokenMessage;
			}
			set
			{
				this.m_configBrokenMessage = value;
				this.m_stateIO.WriteString("ConfigBrokenMessage", value, false);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0003AA60 File Offset: 0x00038C60
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x0003AA83 File Offset: 0x00038C83
		public long ConfigBrokenEventId
		{
			get
			{
				long result;
				this.m_stateIO.TryReadLong("ConfigBrokenEventId", 0L, out result);
				return result;
			}
			set
			{
				this.m_stateIO.WriteLong("ConfigBrokenEventId", value, false);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0003AA98 File Offset: 0x00038C98
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x0003AADC File Offset: 0x00038CDC
		public ExtendedErrorInfo ConfigBrokenExtendedErrorInfo
		{
			get
			{
				if (this.m_configBrokenExtendedErrorInfo == null)
				{
					string text;
					this.m_stateIO.TryReadString("ConfigBrokenExtendedErrorInfo", null, out text);
					if (text != null)
					{
						this.m_configBrokenExtendedErrorInfo = ExtendedErrorInfo.Deserialize(text);
					}
				}
				return this.m_configBrokenExtendedErrorInfo;
			}
			set
			{
				this.m_configBrokenExtendedErrorInfo = value;
				string value2 = null;
				if (value != null)
				{
					value2 = this.m_configBrokenExtendedErrorInfo.SerializeToString();
				}
				this.m_stateIO.WriteString("ConfigBrokenExtendedErrorInfo", value2, false);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0003AB19 File Offset: 0x00038D19
		public bool Suspended
		{
			get
			{
				return this.SuspendLockOwner == LockOwner.Suspend;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0003AB28 File Offset: 0x00038D28
		// (set) Token: 0x06000D74 RID: 3444 RVA: 0x0003AB4C File Offset: 0x00038D4C
		public bool ActivationSuspended
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("ActivationSuspended", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "ActivationSuspended is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<bool>("ActivationSuspended", this.ActivationSuspended, value);
				if (this.m_perfmonCounters != null)
				{
					this.m_perfmonCounters.ActivationSuspended = (value ? 1L : 0L);
				}
				this.m_stateIO.WriteBool("ActivationSuspended", value, false);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0003ABB5 File Offset: 0x00038DB5
		// (set) Token: 0x06000D76 RID: 3446 RVA: 0x0003ABBD File Offset: 0x00038DBD
		public bool ReplaySuspended
		{
			get
			{
				return this.m_replaySuspended;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "ReplaySuspended is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<bool>("ReplaySuspended", this.ReplaySuspended, value);
				this.m_replaySuspended = value;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x0003ABF5 File Offset: 0x00038DF5
		// (set) Token: 0x06000D78 RID: 3448 RVA: 0x0003ABFD File Offset: 0x00038DFD
		public LogReplayPlayDownReason ReplayLagPlayDownReason
		{
			get
			{
				return this.m_replayLagPlayDownReason;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<LogReplayPlayDownReason, string>((long)this.GetHashCode(), "ReplayLagPlayDownReason is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<LogReplayPlayDownReason>("ReplayLagPlayDownReason", this.ReplayLagPlayDownReason, value);
				this.m_replayLagPlayDownReason = value;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x0003AC38 File Offset: 0x00038E38
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x0003AC5C File Offset: 0x00038E5C
		public bool ReplayLagDisabled
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("ReplayLagDisabled", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "ReplayLagDisabled is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<bool>("ReplayLagDisabled", this.ReplayLagDisabled, value);
				if (this.m_perfmonCounters != null)
				{
					this.m_perfmonCounters.ReplayLagDisabled = (value ? 1L : 0L);
				}
				this.m_stateIO.WriteBool("ReplayLagDisabled", value, false);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x0003ACC8 File Offset: 0x00038EC8
		// (set) Token: 0x06000D7C RID: 3452 RVA: 0x0003ACEA File Offset: 0x00038EEA
		internal ActionInitiatorType ReplayLagActionInitiator
		{
			get
			{
				ActionInitiatorType result;
				this.m_stateIO.TryReadEnum<ActionInitiatorType>("ReplayLagActionInitiator", ActionInitiatorType.Unknown, out result);
				return result;
			}
			set
			{
				this.m_stateIO.WriteEnum<ActionInitiatorType>("ReplayLagActionInitiator", value, false);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x0003AD00 File Offset: 0x00038F00
		// (set) Token: 0x06000D7E RID: 3454 RVA: 0x0003AD22 File Offset: 0x00038F22
		public string ReplayLagDisabledReason
		{
			get
			{
				string result;
				this.m_stateIO.TryReadString("ReplayLagDisabledReason", null, out result);
				return result;
			}
			set
			{
				this.m_stateIO.WriteString("ReplayLagDisabledReason", value, false);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x0003AD38 File Offset: 0x00038F38
		// (set) Token: 0x06000D80 RID: 3456 RVA: 0x0003AD5C File Offset: 0x00038F5C
		public bool ResumeBlocked
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("ResumeBlocked", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "ResumeBlocked is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<bool>("ResumeBlocked", this.ResumeBlocked, value);
				this.m_stateIO.WriteBool("ResumeBlocked", value, true);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x0003ADAC File Offset: 0x00038FAC
		// (set) Token: 0x06000D82 RID: 3458 RVA: 0x0003ADD0 File Offset: 0x00038FD0
		public bool ReseedBlocked
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("ReseedBlocked", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "ReseedBlocked is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<bool>("ReseedBlocked", this.ReseedBlocked, value);
				this.m_stateIO.WriteBool("ReseedBlocked", value, true);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x0003AE20 File Offset: 0x00039020
		// (set) Token: 0x06000D84 RID: 3460 RVA: 0x0003AE44 File Offset: 0x00039044
		public bool InPlaceReseedBlocked
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("InPlaceReseedBlocked", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "InPlaceReseedBlocked is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<bool>("InPlaceReseedBlocked", this.InPlaceReseedBlocked, value);
				this.m_stateIO.WriteBool("InPlaceReseedBlocked", value, true);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0003AE94 File Offset: 0x00039094
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x0003AEB8 File Offset: 0x000390B8
		public bool LostWrite
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("LostWrite", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "LostWrite is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<bool>("LostWrite", this.LostWrite, value);
				this.m_stateIO.WriteBool("LostWrite", value, false);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0003AF08 File Offset: 0x00039108
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x0003AF2B File Offset: 0x0003912B
		public LogRepairMode LogRepairMode
		{
			get
			{
				LogRepairMode result;
				this.m_stateIO.TryReadEnum<LogRepairMode>("LogRepairMode", LogRepairMode.Off, out result);
				return result;
			}
			set
			{
				this.m_stateIO.WriteEnum<LogRepairMode>("LogRepairMode", value, true);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0003AF40 File Offset: 0x00039140
		// (set) Token: 0x06000D8A RID: 3466 RVA: 0x0003AF63 File Offset: 0x00039163
		public long LogRepairRetryCount
		{
			get
			{
				long result;
				this.m_stateIO.TryReadLong("LogRepairRetryCount", 0L, out result);
				return result;
			}
			set
			{
				this.m_stateIO.WriteLong("LogRepairRetryCount", value, true);
			}
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0003AFD4 File Offset: 0x000391D4
		public LastLogInfo GetLastLogInfo()
		{
			LastLogInfo result = new LastLogInfo();
			result.CollectionTime = DateTime.UtcNow;
			result.ClusterTimeIsMissing = false;
			result.ClusterLastLogException = AmHelper.RunAmClusterOperation(delegate
			{
				result.ClusterLastLogGen = this.GetLastLogCommittedGenerationNumberFromCluster();
				bool flag = false;
				result.ClusterLastLogTime = (DateTime)this.GetLatestLogGenerationTimeStampFromCluster(out flag);
				if (!flag)
				{
					result.ClusterTimeIsMissing = true;
				}
			});
			if (result.ClusterLastLogException != null)
			{
				ExTraceGlobals.StateTracer.TraceError<string, Exception>((long)this.GetHashCode(), "LastLogInfo for db '{0}' failed to read from cluster: {1}", this.m_databaseName, result.ClusterLastLogException);
			}
			result.ReplLastLogGen = this.CopyNotificationGenerationNumber;
			result.ReplLastLogTime = this.LatestCopierContactTime;
			if (RegistryParameters.UnboundedDatalossDisableClusterInput && RegistryParameters.UnboundedDatalossDisableReplicationInput)
			{
				result.StaleCheckTime = result.CollectionTime;
			}
			else
			{
				if (RegistryParameters.UnboundedDatalossDisableClusterInput)
				{
					result.StaleCheckTime = result.ReplLastLogTime;
				}
				else if (RegistryParameters.UnboundedDatalossDisableReplicationInput)
				{
					result.StaleCheckTime = result.ClusterLastLogTime;
				}
				else if (result.ReplLastLogTime > result.ClusterLastLogTime)
				{
					result.StaleCheckTime = result.ReplLastLogTime;
				}
				else
				{
					result.StaleCheckTime = result.ClusterLastLogTime;
				}
				if (!result.ClusterTimeIsMissing || RegistryParameters.UnboundedDatalossDisableClusterInput)
				{
					TimeSpan timeSpan = DateTime.UtcNow - result.StaleCheckTime;
					if (timeSpan.TotalSeconds > (double)RegistryParameters.UnboundedDatalossSafeGuardDurationInSec)
					{
						result.IsStale = true;
						ExTraceGlobals.ReplicaInstanceTracer.TraceError((long)this.GetHashCode(), "GetLastLogInfo reports that unbounded dataloss may occur for for db '{0}'LastHeardSpan={1} staleCheckTime={2} replTime={3} clusterTime={4}", new object[]
						{
							this.m_databaseName,
							timeSpan,
							result.StaleCheckTime,
							result.ReplLastLogTime,
							result.ClusterLastLogTime
						});
					}
				}
			}
			if (result.IsStale)
			{
				result.LastLogGenToReport = long.MaxValue;
			}
			else
			{
				result.LastLogGenToReport = Math.Max(result.ReplLastLogGen, result.ClusterLastLogGen);
			}
			return result;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0003B23C File Offset: 0x0003943C
		public ExDateTime GetLatestLogGenerationTimeStampFromCluster(out bool doesValueExist)
		{
			ExDateTime lastLogGenerationTimeStamp = ActiveManagerCore.GetLastLogGenerationTimeStamp(this.m_replayIdentityGuid, out doesValueExist);
			ExTraceGlobals.StateTracer.TraceDebug<ExDateTime, bool>((long)this.GetHashCode(), "LatestLogGenerationTimeStamp {0}, DoesValueExist {1}", lastLogGenerationTimeStamp, doesValueExist);
			return lastLogGenerationTimeStamp;
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x0003B270 File Offset: 0x00039470
		// (set) Token: 0x06000D8E RID: 3470 RVA: 0x0003B2B8 File Offset: 0x000394B8
		public long CopyNotificationGenerationNumber
		{
			get
			{
				if (this.m_copyNotificationGenerationNumber != null)
				{
					return this.m_copyNotificationGenerationNumber.Value;
				}
				long num;
				this.m_stateIO.TryReadLong("CopyNotificationGenerationNumber", 0L, out num);
				this.m_copyNotificationGenerationNumber = new long?(num);
				return num;
			}
			set
			{
				this.m_copyNotificationGenerationNumber = new long?(value);
				ExTraceGlobals.StateTracer.TraceDebug<long, string>((long)this.GetHashCode(), "CopyNotificationGenerationNumber is changing to {0} on replica {1}", value, this.m_replayIdentity);
				if (this.m_perfmonCounters != null)
				{
					this.m_perfmonCounters.CopyNotificationGenerationNumber = value;
				}
				this.m_stateIO.WriteLong("CopyNotificationGenerationNumber", value, false);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0003B314 File Offset: 0x00039514
		// (set) Token: 0x06000D90 RID: 3472 RVA: 0x0003B337 File Offset: 0x00039537
		public long LogStreamStartGeneration
		{
			get
			{
				long result;
				this.m_stateIO.TryReadLong("LogStreamStartGeneration", 0L, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<long, string>((long)this.GetHashCode(), "LogStreamStartGeneration is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteLong("LogStreamStartGeneration", value, true);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x0003B368 File Offset: 0x00039568
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x0003B3B0 File Offset: 0x000395B0
		public long CopyGenerationNumber
		{
			get
			{
				if (this.m_copyGenerationNumber != null)
				{
					return this.m_copyGenerationNumber.Value;
				}
				long num;
				this.m_stateIO.TryReadLong("CopyGenerationNumber", 0L, out num);
				this.m_copyGenerationNumber = new long?(num);
				return num;
			}
			set
			{
				this.m_copyGenerationNumber = new long?(value);
				ExTraceGlobals.StateTracer.TraceDebug<long, string>((long)this.GetHashCode(), "CopyGenerationNumber is changing to {0} on replica {1}", value, this.m_replayIdentity);
				if (this.m_perfmonCounters != null)
				{
					this.m_perfmonCounters.CopyGenerationNumber = value;
				}
				this.m_stateIO.WriteLong("CopyGenerationNumber", value, false);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0003B40C File Offset: 0x0003960C
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x0003B454 File Offset: 0x00039654
		public long InspectorGenerationNumber
		{
			get
			{
				if (this.m_inspectorGenerationNumber != null)
				{
					return this.m_inspectorGenerationNumber.Value;
				}
				long num;
				this.m_stateIO.TryReadLong("InspectorGenerationNumber", 0L, out num);
				this.m_inspectorGenerationNumber = new long?(num);
				return num;
			}
			set
			{
				this.m_inspectorGenerationNumber = new long?(value);
				ExTraceGlobals.StateTracer.TraceDebug<long, string>((long)this.GetHashCode(), "InspectorGenerationNumber is changing to {0} on replica {1}", value, this.m_replayIdentity);
				if (this.m_perfmonCounters != null)
				{
					this.m_perfmonCounters.InspectorGenerationNumber = value;
				}
				this.m_stateIO.WriteLong("InspectorGenerationNumber", value, false);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x0003B4B0 File Offset: 0x000396B0
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x0003B4F8 File Offset: 0x000396F8
		public long ReplayGenerationNumber
		{
			get
			{
				if (this.m_replayGenerationNumber != null)
				{
					return this.m_replayGenerationNumber.Value;
				}
				long num;
				this.m_stateIO.TryReadLong("ReplayGenerationNumber", 0L, out num);
				this.m_replayGenerationNumber = new long?(num);
				return num;
			}
			set
			{
				this.m_replayGenerationNumber = new long?(value);
				ExTraceGlobals.StateTracer.TraceDebug<long, string>((long)this.GetHashCode(), "ReplayGenerationNumber is changing to {0} on replica {1}", value, this.m_replayIdentity);
				if (this.m_perfmonCounters != null)
				{
					this.m_perfmonCounters.ReplayGenerationNumber = value;
				}
				this.m_stateIO.WriteLong("ReplayGenerationNumber", value, false);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x0003B554 File Offset: 0x00039754
		public DateTime LatestDataWriteTime
		{
			get
			{
				DateTime latestCopyNotificationTime = this.LatestCopyNotificationTime;
				ExTraceGlobals.StateTracer.TraceDebug<DateTime>((long)this.GetHashCode(), "LatestDataWriteTime from LatestCopyNotificationTime: {0}", latestCopyNotificationTime);
				return latestCopyNotificationTime;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0003B580 File Offset: 0x00039780
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x0003B5A8 File Offset: 0x000397A8
		public DateTime LatestCopyNotificationTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("LatestCopyNotificationTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "LatestCopyNotificationTime is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteDateTime("LatestCopyNotificationTime", value, false);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0003B5DC File Offset: 0x000397DC
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x0003B604 File Offset: 0x00039804
		public DateTime LatestCopierContactTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("LatestCopierContactTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "LatestCopierContactTime is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteDateTime("LatestCopierContactTime", value, false);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0003B638 File Offset: 0x00039838
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x0003B660 File Offset: 0x00039860
		public DateTime LatestCopyTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("LatestCopyTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "LatestCopyTime is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteDateTime("LatestCopyTime", value, false);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0003B694 File Offset: 0x00039894
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x0003B6BC File Offset: 0x000398BC
		public DateTime LatestInspectorTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("LatestInspectorTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "LatestInspectorTime is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteDateTime("LatestInspectorTime", value, false);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0003B6F0 File Offset: 0x000398F0
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x0003B718 File Offset: 0x00039918
		public DateTime LatestReplayTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("LatestReplayTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "LatestReplayTime is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteDateTime("LatestReplayTime", value, false);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0003B74C File Offset: 0x0003994C
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x0003B774 File Offset: 0x00039974
		public DateTime CurrentReplayTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("CurrentReplayTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "CurrentReplayTime is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteDateTime("CurrentReplayTime", value, false);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0003B7A8 File Offset: 0x000399A8
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x0003B7D0 File Offset: 0x000399D0
		public DateTime LastAttemptCopyLastLogsEndTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("LastAttemptCopyLastLogsEndTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "LastAttemptCopyLastLogsEndTime is changing to {0} on replica {1}", value, this.m_replayIdentity);
				if (value != ReplayState.ZeroFileTime)
				{
					this.LogCrimsonEventOnStateChange<DateTime>("LastAttemptCopyLastLogsEndTime", this.LastAttemptCopyLastLogsEndTime, value);
				}
				this.m_stateIO.WriteDateTime("LastAttemptCopyLastLogsEndTime", value, false);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0003B82C File Offset: 0x00039A2C
		// (set) Token: 0x06000DA7 RID: 3495 RVA: 0x0003B84F File Offset: 0x00039A4F
		public long LastAcllLossAmount
		{
			get
			{
				long result;
				this.m_stateIO.TryReadLong("LastAcllLossAmount", 0L, out result);
				return result;
			}
			set
			{
				this.m_stateIO.WriteLong("LastAcllLossAmount", value, true);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0003B864 File Offset: 0x00039A64
		// (set) Token: 0x06000DA9 RID: 3497 RVA: 0x0003B886 File Offset: 0x00039A86
		public bool LastAcllRunWithSkipHealthChecks
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("LastAcllRunWithSkipHealthChecks", false, out result);
				return result;
			}
			set
			{
				this.m_stateIO.WriteBool("LastAcllRunWithSkipHealthChecks", value, true);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x0003B89C File Offset: 0x00039A9C
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x0003B8C4 File Offset: 0x00039AC4
		public DateTime LastStatusTransitionTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("LastStatusTransitionTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "LastStatusTransitionTime is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<DateTime>("LastStatusTransitionTime", this.LastStatusTransitionTime, value);
				this.m_stateIO.WriteDateTime("LastStatusTransitionTime", value, false);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0003B914 File Offset: 0x00039B14
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x0003B938 File Offset: 0x00039B38
		internal bool SinglePageRestore
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("SinglePageRestore", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "SinglePageRestore is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<bool>("SinglePageRestore", this.SinglePageRestore, value);
				this.m_stateIO.WriteBool("SinglePageRestore", value, true);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x0003B988 File Offset: 0x00039B88
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x0003B9AB File Offset: 0x00039BAB
		internal long SinglePageRestoreNumber
		{
			get
			{
				long result;
				this.m_stateIO.TryReadLong("SinglePageRestoreNumber", 0L, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<long, string>((long)this.GetHashCode(), "SinglePageRestoreNumber is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteLong("SinglePageNumber", value, true);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0003B9DC File Offset: 0x00039BDC
		public bool SafetyNetRedeliveryRequired
		{
			get
			{
				return this.m_safetyNetTable.IsRedeliveryRequired(false, false);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x0003B9EB File Offset: 0x00039BEB
		public bool DumpsterRedeliveryRequired
		{
			get
			{
				return this.SafetyNetRedeliveryRequired;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x0003B9F3 File Offset: 0x00039BF3
		public string DumpsterRedeliveryServers
		{
			get
			{
				return this.m_safetyNetTable.RedeliveryServers;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0003BA00 File Offset: 0x00039C00
		public DateTime DumpsterRedeliveryStartTime
		{
			get
			{
				return this.m_safetyNetTable.RedeliveryStartTime;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0003BA0D File Offset: 0x00039C0D
		public DateTime DumpsterRedeliveryEndTime
		{
			get
			{
				return this.m_safetyNetTable.RedeliveryEndTime;
			}
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0003BA1A File Offset: 0x00039C1A
		public SafetyNetInfoCache GetSafetyNetTable()
		{
			return this.m_safetyNetTable;
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0003BA24 File Offset: 0x00039C24
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x0003BA4C File Offset: 0x00039C4C
		public DateTime LatestFullBackupTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("LatestFullBackupTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				this.m_stateIO.WriteDateTime("LatestFullBackupTime", value, false);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0003BA60 File Offset: 0x00039C60
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x0003BA88 File Offset: 0x00039C88
		public DateTime LatestIncrementalBackupTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("LatestIncrementalBackupTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "LatestIncrementalBackupTime is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteDateTime("LatestIncrementalBackupTime", value, false);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0003BABC File Offset: 0x00039CBC
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x0003BAE4 File Offset: 0x00039CE4
		public DateTime LatestDifferentialBackupTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("LatestDifferentialBackupTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "LatestDifferentialBackupTime is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteDateTime("LatestDifferentialBackupTime", value, false);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x0003BB18 File Offset: 0x00039D18
		// (set) Token: 0x06000DBD RID: 3517 RVA: 0x0003BB40 File Offset: 0x00039D40
		public DateTime LatestCopyBackupTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("LatestCopyBackupTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "LatestCopyBackupTime is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteDateTime("LatestCopyBackupTime", value, false);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x0003BB74 File Offset: 0x00039D74
		// (set) Token: 0x06000DBF RID: 3519 RVA: 0x0003BB96 File Offset: 0x00039D96
		public bool SnapshotBackup
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("SnapshotBackup", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "SnapshotBackup is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteBool("SnapshotBackup", value, false);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0003BBC8 File Offset: 0x00039DC8
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x0003BBEA File Offset: 0x00039DEA
		public bool SnapshotLatestFullBackup
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("SnapshotLatestFullBackup", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "SnapshotLatestFullBackup is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteBool("SnapshotLatestFullBackup", value, false);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0003BC1C File Offset: 0x00039E1C
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x0003BC3E File Offset: 0x00039E3E
		public bool SnapshotLatestIncrementalBackup
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("SnapshotLatestIncrementalBackup", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "SnapshotLatestIncrementalBackup is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteBool("SnapshotLatestIncrementalBackup", value, false);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0003BC70 File Offset: 0x00039E70
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x0003BC92 File Offset: 0x00039E92
		public bool SnapshotLatestDifferentialBackup
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("SnapshotLatestDifferentialBackup", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "SnapshotLatestDifferentialBackup is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteBool("SnapshotLatestDifferentialBackup", value, false);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0003BCC4 File Offset: 0x00039EC4
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x0003BCE6 File Offset: 0x00039EE6
		public bool SnapshotLatestCopyBackup
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("SnapshotLatestCopyBackup", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "SnapshotLatestCopyBackup is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteBool("SnapshotLatestCopyBackup", value, false);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0003BD18 File Offset: 0x00039F18
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x0003BD3A File Offset: 0x00039F3A
		internal ActionInitiatorType ActionInitiator
		{
			get
			{
				ActionInitiatorType result;
				this.m_stateIO.TryReadEnum<ActionInitiatorType>("ActionInitiator", ActionInitiatorType.Unknown, out result);
				return result;
			}
			set
			{
				this.m_stateIO.WriteEnum<ActionInitiatorType>("ActionInitiator", value, false);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0003BD50 File Offset: 0x00039F50
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x0003BD88 File Offset: 0x00039F88
		internal int WorkerProcessId
		{
			get
			{
				if (this.m_workerProcessId == 0)
				{
					long num;
					this.m_stateIO.TryReadLong("WorkerProcessId", 0L, out num);
					this.m_workerProcessId = (int)num;
				}
				return this.m_workerProcessId;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<int, string>((long)this.GetHashCode(), "StoreWorkerProcessId is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_workerProcessId = value;
				this.m_stateIO.WriteLong("WorkerProcessId", (long)value, false);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0003BDC4 File Offset: 0x00039FC4
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0003BDEC File Offset: 0x00039FEC
		internal DateTime LastCopyAvailabilityChecksPassedTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("LastCopyAvailabilityChecksPassedTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "LastCopyAvailabilityChecksPassedTime is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteDateTime("LastCopyAvailabilityChecksPassedTime", value, false);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0003BE20 File Offset: 0x0003A020
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x0003BE44 File Offset: 0x0003A044
		internal bool IsLastCopyAvailabilityChecksPassed
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("IsLastCopyAvailabilityChecksPassed", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "IsLastCopyAvailabilityChecksPassed is being set to '{0}' on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<bool>("IsLastCopyAvailabilityChecksPassed", this.IsLastCopyAvailabilityChecksPassed, value);
				this.m_stateIO.WriteBool("IsLastCopyAvailabilityChecksPassed", value, true);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0003BE94 File Offset: 0x0003A094
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x0003BEBC File Offset: 0x0003A0BC
		internal DateTime LastCopyRedundancyChecksPassedTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("LastCopyRedundancyChecksPassedTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "LastCopyRedundancyChecksPassedTime is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.m_stateIO.WriteDateTime("LastCopyRedundancyChecksPassedTime", value, false);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0003BEF0 File Offset: 0x0003A0F0
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0003BF14 File Offset: 0x0003A114
		internal bool IsLastCopyRedundancyChecksPassed
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("IsLastCopyRedundancyChecksPassed", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "IsLastCopyRedundancyChecksPassed is being set to '{0}' on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<bool>("IsLastCopyRedundancyChecksPassed", this.IsLastCopyRedundancyChecksPassed, value);
				this.m_stateIO.WriteBool("IsLastCopyRedundancyChecksPassed", value, true);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0003BF64 File Offset: 0x0003A164
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0003BF88 File Offset: 0x0003A188
		internal string LastDatabaseVolumeName
		{
			get
			{
				string result;
				this.m_stateIO.TryReadString("LastDatabaseVolumeName", null, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<string, string>((long)this.GetHashCode(), "LastDatabaseVolumeName is being set to '{0}' on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<string>("LastDatabaseVolumeName", this.LastDatabaseVolumeName, value);
				this.m_stateIO.WriteString("LastDatabaseVolumeName", value, false);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0003BFD8 File Offset: 0x0003A1D8
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x0003C000 File Offset: 0x0003A200
		public DateTime LastDatabaseVolumeNameTransitionTime
		{
			get
			{
				DateTime result;
				this.m_stateIO.TryReadDateTime("LastDatabaseVolumeNameTransitionTime", DateTime.FromFileTimeUtc(0L), out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "LastDatabaseVolumeNameTransitionTime is changing to {0} on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<DateTime>("LastDatabaseVolumeNameTransitionTime", this.LastDatabaseVolumeNameTransitionTime, value);
				this.m_stateIO.WriteDateTime("LastDatabaseVolumeNameTransitionTime", value, false);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0003C050 File Offset: 0x0003A250
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0003C074 File Offset: 0x0003A274
		internal string ExchangeVolumeMountPoint
		{
			get
			{
				string result;
				this.m_stateIO.TryReadString("ExchangeVolumeMountPoint", null, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ExchangeVolumeMountPoint is being set to '{0}' on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<string>("ExchangeVolumeMountPoint", this.ExchangeVolumeMountPoint, value);
				this.m_stateIO.WriteString("ExchangeVolumeMountPoint", value, true);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0003C0C4 File Offset: 0x0003A2C4
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x0003C0E8 File Offset: 0x0003A2E8
		internal string DatabaseVolumeMountPoint
		{
			get
			{
				string result;
				this.m_stateIO.TryReadString("DatabaseVolumeMountPoint", null, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<string, string>((long)this.GetHashCode(), "DatabaseVolumeMountPoint is being set to '{0}' on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<string>("DatabaseVolumeMountPoint", this.DatabaseVolumeMountPoint, value);
				this.m_stateIO.WriteString("DatabaseVolumeMountPoint", value, true);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0003C138 File Offset: 0x0003A338
		// (set) Token: 0x06000DDD RID: 3549 RVA: 0x0003C15C File Offset: 0x0003A35C
		internal string DatabaseVolumeName
		{
			get
			{
				string result;
				this.m_stateIO.TryReadString("DatabaseVolumeName", null, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<string, string>((long)this.GetHashCode(), "DatabaseVolumeName is being set to '{0}' on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<string>("DatabaseVolumeName", this.DatabaseVolumeName, value);
				this.m_stateIO.WriteString("DatabaseVolumeName", value, true);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0003C1AC File Offset: 0x0003A3AC
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x0003C1D0 File Offset: 0x0003A3D0
		internal bool IsDatabasePathOnMountedFolder
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("IsDatabasePathOnMountedFolder", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "IsDatabasePathOnMountedFolder is being set to '{0}' on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<bool>("IsDatabasePathOnMountedFolder", this.IsDatabasePathOnMountedFolder, value);
				this.m_stateIO.WriteBool("IsDatabasePathOnMountedFolder", value, true);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0003C220 File Offset: 0x0003A420
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x0003C244 File Offset: 0x0003A444
		internal string LogVolumeMountPoint
		{
			get
			{
				string result;
				this.m_stateIO.TryReadString("LogVolumeMountPoint", null, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<string, string>((long)this.GetHashCode(), "LogVolumeMountPoint is being set to '{0}' on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<string>("LogVolumeMountPoint", this.LogVolumeMountPoint, value);
				this.m_stateIO.WriteString("LogVolumeMountPoint", value, true);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0003C294 File Offset: 0x0003A494
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x0003C2B8 File Offset: 0x0003A4B8
		internal string LogVolumeName
		{
			get
			{
				string result;
				this.m_stateIO.TryReadString("LogVolumeName", null, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<string, string>((long)this.GetHashCode(), "LogVolumeName is being set to '{0}' on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<string>("LogVolumeName", this.LogVolumeName, value);
				this.m_stateIO.WriteString("LogVolumeName", value, true);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0003C308 File Offset: 0x0003A508
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x0003C32C File Offset: 0x0003A52C
		internal bool IsLogPathOnMountedFolder
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("IsLogPathOnMountedFolder", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "IsLogPathOnMountedFolder is being set to '{0}' on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<bool>("IsLogPathOnMountedFolder", this.IsLogPathOnMountedFolder, value);
				this.m_stateIO.WriteBool("IsLogPathOnMountedFolder", value, true);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0003C37C File Offset: 0x0003A57C
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x0003C3A0 File Offset: 0x0003A5A0
		internal bool VolumeInfoIsValid
		{
			get
			{
				bool result;
				this.m_stateIO.TryReadBool("VolumeInfoIsValid", false, out result);
				return result;
			}
			set
			{
				ExTraceGlobals.StateTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "VolumeInfoIsValid is being set to '{0}' on replica {1}", value, this.m_replayIdentity);
				this.LogCrimsonEventOnStateChange<bool>("VolumeInfoIsValid", this.VolumeInfoIsValid, value);
				this.m_stateIO.WriteBool("VolumeInfoIsValid", value, true);
			}
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0003C3F0 File Offset: 0x0003A5F0
		internal void SetVolumeInfoIfValid(DatabaseVolumeInfo vi)
		{
			if (!vi.IsValid)
			{
				ExTraceGlobals.StateTracer.TraceError<string>((long)this.GetHashCode(), "DatabaseVolumeInfo is not valid for replica {0}, so skipping persisting it.", this.m_replayIdentity);
				return;
			}
			this.VolumeInfoIsValid = false;
			if (this.m_stateIO.IOFailures)
			{
				ExTraceGlobals.StateTracer.TraceError<string>((long)this.GetHashCode(), "An error occurred when clearing VolumeInfoIsValid state for replica {0}.", this.m_replayIdentity);
				return;
			}
			this.DatabaseVolumeMountPoint = vi.DatabaseVolumeMountPoint.Path;
			if (this.m_stateIO.IOFailures)
			{
				ExTraceGlobals.StateTracer.TraceError<string>((long)this.GetHashCode(), "An error occurred when writing DatabaseVolumeMountPoint state for replica {0}.", this.m_replayIdentity);
				return;
			}
			this.DatabaseVolumeName = vi.DatabaseVolumeName.Path;
			if (this.m_stateIO.IOFailures)
			{
				ExTraceGlobals.StateTracer.TraceError<string>((long)this.GetHashCode(), "An error occurred when writing DatabaseVolumeName state for replica {0}.", this.m_replayIdentity);
				return;
			}
			this.IsDatabasePathOnMountedFolder = vi.IsDatabasePathOnMountedFolder;
			if (this.m_stateIO.IOFailures)
			{
				ExTraceGlobals.StateTracer.TraceError<string>((long)this.GetHashCode(), "An error occurred when writing IsDatabasePathOnMountedFolder state for replica {0}.", this.m_replayIdentity);
				return;
			}
			this.LogVolumeMountPoint = vi.LogVolumeMountPoint.Path;
			if (this.m_stateIO.IOFailures)
			{
				ExTraceGlobals.StateTracer.TraceError<string>((long)this.GetHashCode(), "An error occurred when writing LogVolumeMountPoint state for replica {0}.", this.m_replayIdentity);
				return;
			}
			this.LogVolumeName = vi.LogVolumeName.Path;
			if (this.m_stateIO.IOFailures)
			{
				ExTraceGlobals.StateTracer.TraceError<string>((long)this.GetHashCode(), "An error occurred when writing LogVolumeName state for replica {0}.", this.m_replayIdentity);
				return;
			}
			this.IsLogPathOnMountedFolder = vi.IsLogPathOnMountedFolder;
			if (this.m_stateIO.IOFailures)
			{
				ExTraceGlobals.StateTracer.TraceError<string>((long)this.GetHashCode(), "An error occurred when writing IsLogPathOnMountedFolder state for replica {0}.", this.m_replayIdentity);
				return;
			}
			if (vi.IsExchangeVolumeMountPointValid)
			{
				this.ExchangeVolumeMountPoint = vi.ExchangeVolumeMountPoint.Path;
				if (this.m_stateIO.IOFailures)
				{
					ExTraceGlobals.StateTracer.TraceError<string>((long)this.GetHashCode(), "An error occurred when writing ExchangeVolumeMountPoint state for replica {0}.", this.m_replayIdentity);
					return;
				}
			}
			this.VolumeInfoIsValid = true;
			if (this.m_stateIO.IOFailures)
			{
				ExTraceGlobals.StateTracer.TraceError<string>((long)this.GetHashCode(), "An error occurred when setting VolumeInfoIsValid state to true for replica {0}.", this.m_replayIdentity);
			}
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0003C620 File Offset: 0x0003A820
		internal long GetLastLogCommittedGenerationNumberFromCluster()
		{
			long num = long.MaxValue;
			long lastLogGenerationNumber = ActiveManagerCore.GetLastLogGenerationNumber(this.m_replayIdentityGuid);
			if (lastLogGenerationNumber != 9223372036854775807L)
			{
				num = lastLogGenerationNumber;
			}
			ExTraceGlobals.StateTracer.TraceDebug<long, long>((long)this.GetHashCode(), "LastLogCommittedGenerationNumber {0}; ClusdbLastLog {1}.", num, lastLogGenerationNumber);
			return num;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0003C674 File Offset: 0x0003A874
		private void LogCrimsonEventOnStateChange<T>(string stateName, T oldValue, T newValue)
		{
			ReplayState.LogCrimsonEventOnStateChange<T>(this.m_databaseName, this.m_replayIdentity, this.m_nodeName, stateName, oldValue, newValue);
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0003C690 File Offset: 0x0003A890
		internal static DateTime ZeroTicksTimeUtc
		{
			get
			{
				return new DateTime(0L).ToUniversalTime();
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0003C6AC File Offset: 0x0003A8AC
		internal static DateTime ZeroFileTime
		{
			get
			{
				return DateTime.FromFileTimeUtc(0L);
			}
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0003C6B8 File Offset: 0x0003A8B8
		internal void ResetForSeed()
		{
			this.CopyGenerationNumber = 0L;
			this.CopyNotificationGenerationNumber = 0L;
			this.CurrentReplayTime = ReplayState.ZeroFileTime;
			this.InspectorGenerationNumber = 0L;
			this.LastAttemptCopyLastLogsEndTime = ReplayState.ZeroFileTime;
			this.LatestCopyNotificationTime = ReplayState.ZeroFileTime;
			this.LatestCopyTime = ReplayState.ZeroFileTime;
			this.LatestInspectorTime = ReplayState.ZeroFileTime;
			this.LatestReplayTime = ReplayState.ZeroFileTime;
			this.ReplayGenerationNumber = 0L;
			this.LogRepairMode = LogRepairMode.Off;
			this.LogRepairRetryCount = 0L;
			this.ResumeBlocked = false;
			this.ReseedBlocked = false;
			this.InPlaceReseedBlocked = false;
		}

		// Token: 0x040005A5 RID: 1445
		public const string LogRepairModeValueName = "LogRepairMode";

		// Token: 0x040005A6 RID: 1446
		public const string LogRepairRetryCountValueName = "LogRepairRetryCount";

		// Token: 0x040005A7 RID: 1447
		internal const string LastCopyAvailabilityChecksPassedTime_Name = "LastCopyAvailabilityChecksPassedTime";

		// Token: 0x040005A8 RID: 1448
		internal const string LastCopyRedundancyChecksPassedTime_Name = "LastCopyRedundancyChecksPassedTime";

		// Token: 0x040005A9 RID: 1449
		internal const string IsLastCopyAvailabilityChecksPassed_Name = "IsLastCopyAvailabilityChecksPassed";

		// Token: 0x040005AA RID: 1450
		internal const string IsLastCopyRedundancyChecksPassed_Name = "IsLastCopyRedundancyChecksPassed";

		// Token: 0x040005AB RID: 1451
		private string m_nodeName;

		// Token: 0x040005AC RID: 1452
		private string m_sourceNodeName;

		// Token: 0x040005AD RID: 1453
		private string m_replayIdentity;

		// Token: 0x040005AE RID: 1454
		private string m_databaseName;

		// Token: 0x040005AF RID: 1455
		private Guid m_replayIdentityGuid;

		// Token: 0x040005B0 RID: 1456
		private StateLock m_suspendLock;

		// Token: 0x040005B1 RID: 1457
		private StateLockRemote m_suspendLockRemote;

		// Token: 0x040005B2 RID: 1458
		private IStateIO m_stateIO;

		// Token: 0x040005B3 RID: 1459
		private IStateIO m_stateIOGlobal;

		// Token: 0x040005B4 RID: 1460
		private SafetyNetInfoCache m_safetyNetTable;

		// Token: 0x040005B5 RID: 1461
		private string m_configBrokenMessage;

		// Token: 0x040005B6 RID: 1462
		private ExtendedErrorInfo m_configBrokenExtendedErrorInfo;

		// Token: 0x040005B7 RID: 1463
		private long? m_copyNotificationGenerationNumber = null;

		// Token: 0x040005B8 RID: 1464
		private long? m_copyGenerationNumber = null;

		// Token: 0x040005B9 RID: 1465
		private long? m_inspectorGenerationNumber = null;

		// Token: 0x040005BA RID: 1466
		private long? m_replayGenerationNumber = null;

		// Token: 0x040005BB RID: 1467
		private IPerfmonCounters m_perfmonCounters;

		// Token: 0x040005BC RID: 1468
		private bool m_replaySuspended;

		// Token: 0x040005BD RID: 1469
		private LogReplayPlayDownReason m_replayLagPlayDownReason;

		// Token: 0x040005BE RID: 1470
		private int m_workerProcessId;
	}
}
