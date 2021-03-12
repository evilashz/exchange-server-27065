using System;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay.MountPoint;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.HA.FailureItem;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200014A RID: 330
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplicaInstanceContext : IReplicaInstanceContext, ISetBroken, ISetDisconnected, ISetViable, ISetSeeding, ISetPassiveSeeding, IReplicaProgress, ISetGeneration, ISetActiveSeeding, IGetStatus, ISetVolumeInfo
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x00037579 File Offset: 0x00035779
		public ReplicaInstanceContext()
		{
			this.FailureInfo = new FailureInfo();
			this.m_PassiveSeedingSourceContext = PassiveSeedingSourceContextEnum.None;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00037593 File Offset: 0x00035793
		public string Identity
		{
			get
			{
				return this.m_identity;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0003759B File Offset: 0x0003579B
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x000375A3 File Offset: 0x000357A3
		internal FailureInfo FailureInfo { get; private set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x000375AC File Offset: 0x000357AC
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x000375B4 File Offset: 0x000357B4
		public DatabaseVolumeInfo DatabaseVolumeInfo { get; private set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x000375C0 File Offset: 0x000357C0
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x00037604 File Offset: 0x00035804
		public bool RestartSoon
		{
			get
			{
				bool fRestartSoon;
				lock (this.m_instance)
				{
					fRestartSoon = this.m_fRestartSoon;
				}
				return fRestartSoon;
			}
			set
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "{0}: RestartSoon is being set to: {1}", this.m_displayName, value);
				this.LogCrimsonEventOnStateChange<bool>("RestartSoon", this.m_fRestartSoon, value);
				this.m_fRestartSoon = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0003763C File Offset: 0x0003583C
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x00037680 File Offset: 0x00035880
		public bool DoNotRestart
		{
			get
			{
				bool fDoNotRestart;
				lock (this.m_instance)
				{
					fDoNotRestart = this.m_fDoNotRestart;
				}
				return fDoNotRestart;
			}
			set
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "{0}: DoNotRestart is being set to: {1}", this.m_displayName, value);
				this.LogCrimsonEventOnStateChange<bool>("DoNotRestart", this.m_fDoNotRestart, value);
				this.m_fDoNotRestart = value;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x000376B8 File Offset: 0x000358B8
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x000376FC File Offset: 0x000358FC
		public bool Suspended
		{
			get
			{
				bool fSuspended;
				lock (this.m_instance)
				{
					fSuspended = this.m_fSuspended;
				}
				return fSuspended;
			}
			private set
			{
				this.m_fSuspended = value;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00037708 File Offset: 0x00035908
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x0003774C File Offset: 0x0003594C
		public bool Seeding
		{
			get
			{
				bool fSeeding;
				lock (this.m_instance)
				{
					fSeeding = this.m_fSeeding;
				}
				return fSeeding;
			}
			private set
			{
				if (this.m_perfmonCounters != null)
				{
					if (value)
					{
						this.m_perfmonCounters.Seeding = 1L;
					}
					else
					{
						this.m_perfmonCounters.Seeding = 0L;
					}
				}
				this.m_fSeeding = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0003777C File Offset: 0x0003597C
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x000377C0 File Offset: 0x000359C0
		public PassiveSeedingSourceContextEnum PassiveSeedingSourceContext
		{
			get
			{
				PassiveSeedingSourceContextEnum passiveSeedingSourceContext;
				lock (this.m_instance)
				{
					passiveSeedingSourceContext = this.m_PassiveSeedingSourceContext;
				}
				return passiveSeedingSourceContext;
			}
			private set
			{
				if (this.m_perfmonCounters != null)
				{
					if (value != PassiveSeedingSourceContextEnum.None)
					{
						this.m_perfmonCounters.PassiveSeedingSource = 1L;
					}
					else
					{
						this.m_perfmonCounters.PassiveSeedingSource = 0L;
					}
				}
				this.m_PassiveSeedingSourceContext = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x000377F0 File Offset: 0x000359F0
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x00037834 File Offset: 0x00035A34
		public bool ActiveSeedingSource
		{
			get
			{
				bool fActiveSeedingSource;
				lock (this.m_instance)
				{
					fActiveSeedingSource = this.m_fActiveSeedingSource;
				}
				return fActiveSeedingSource;
			}
			private set
			{
				this.m_fActiveSeedingSource = value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0003783D File Offset: 0x00035A3D
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x00037845 File Offset: 0x00035A45
		public bool IsSeedingSourceForDB { get; private set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0003784E File Offset: 0x00035A4E
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x00037856 File Offset: 0x00035A56
		public bool IsSeedingSourceForCI { get; private set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00037860 File Offset: 0x00035A60
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x000378A4 File Offset: 0x00035AA4
		public bool Viable
		{
			get
			{
				bool fViable;
				lock (this.m_instance)
				{
					fViable = this.m_fViable;
				}
				return fViable;
			}
			private set
			{
				this.m_fViable = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x000378B0 File Offset: 0x00035AB0
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x000378F4 File Offset: 0x00035AF4
		public bool AdminVisibleRestart
		{
			get
			{
				bool fAdminVisibleRestart;
				lock (this.m_instance)
				{
					fAdminVisibleRestart = this.m_fAdminVisibleRestart;
				}
				return fAdminVisibleRestart;
			}
			private set
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "{0}: AdminVisibleRestart is being set to: {1}", this.m_displayName, value);
				this.LogCrimsonEventOnStateChange<bool>("AdminVisibleRestart", this.m_fAdminVisibleRestart, value);
				this.m_fAdminVisibleRestart = value;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0003792C File Offset: 0x00035B2C
		public bool InAttemptCopyLastLogs
		{
			get
			{
				return this.m_numThreadsInAcll > 0;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00037937 File Offset: 0x00035B37
		public long LogsCopiedSinceInstanceStart
		{
			get
			{
				return this.m_countLogsCopied;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0003793F File Offset: 0x00035B3F
		public long LogsReplayedSinceInstanceStart
		{
			get
			{
				return this.m_countLogsReplayed;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x00037947 File Offset: 0x00035B47
		public ReplicaInstance ReplicaInstance
		{
			get
			{
				return this.m_instance;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0003794F File Offset: 0x00035B4F
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x00037957 File Offset: 0x00035B57
		public bool IsReplayDatabaseDismountPending { get; set; }

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00037960 File Offset: 0x00035B60
		public void InitializeContext(ReplicaInstance instance)
		{
			this.m_instance = instance;
			this.m_replayState = instance.Configuration.ReplayState;
			this.m_identity = instance.Configuration.Identity;
			this.m_databaseName = instance.Configuration.DatabaseName;
			this.m_displayName = instance.Configuration.DisplayName;
			this.m_guid = instance.Configuration.IdentityGuid;
			this.m_perfmonCounters = instance.PerfmonCounters;
			this.ExternalStatus = new ExternalReplicaInstanceStatus(this, this.m_instance.PreviousContext, this.m_instance.Configuration.Type, this.m_perfmonCounters, this.m_replayState);
			if (instance.IsThirdPartyReplicationEnabled && instance.IsTarget)
			{
				this.m_progressStage = ReplicaInstanceStage.Running;
			}
			SeederServerContext seederServerContext = SourceSeedTable.Instance.TryGetContext(this.m_guid);
			if (seederServerContext != null)
			{
				if (instance.Configuration.IsPassiveCopy)
				{
					seederServerContext.LinkWithNewPassiveRIStatus(this);
				}
				else
				{
					seederServerContext.LinkWithNewActiveRIStatus(this);
				}
			}
			this.m_countLogsThreshold = (long)RegistryParameters.ReplicaProgressNumberOfLogsThreshold;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00037A5C File Offset: 0x00035C5C
		public void CarryOverPreviousStatus(ReplicaInstanceContextMinimal previousContext)
		{
			if (previousContext != null)
			{
				this.ExternalStatus.CarryOverPreviousStatus(previousContext.LastCopyStatus);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00037A72 File Offset: 0x00035C72
		public string DisplayName
		{
			get
			{
				return this.m_displayName;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00037A7A File Offset: 0x00035C7A
		public string DatabaseName
		{
			get
			{
				return this.m_databaseName;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x00037A82 File Offset: 0x00035C82
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x00037A90 File Offset: 0x00035C90
		public bool Initializing
		{
			get
			{
				return this.ProgressStage == ReplicaInstanceStage.Initializing;
			}
			private set
			{
				if (value)
				{
					ExTraceGlobals.StateTracer.TraceDebug<string>((long)this.GetHashCode(), "TargetState is changing to Initializing on replica {0}", this.m_identity);
					this.ProgressStage = ReplicaInstanceStage.Initializing;
					if (!this.m_replayState.ConfigBroken && this.m_perfmonCounters != null)
					{
						this.m_perfmonCounters.Initializing = 1L;
						this.m_perfmonCounters.Resynchronizing = 0L;
					}
				}
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00037AF2 File Offset: 0x00035CF2
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x00037B00 File Offset: 0x00035D00
		public bool Resynchronizing
		{
			get
			{
				return this.ProgressStage == ReplicaInstanceStage.Resynchronizing;
			}
			private set
			{
				if (value)
				{
					ExTraceGlobals.StateTracer.TraceDebug<string>((long)this.GetHashCode(), "TargetState is changing to Resynchronizing on replica {0}", this.m_identity);
					this.ProgressStage = ReplicaInstanceStage.Resynchronizing;
					if (!this.m_replayState.ConfigBroken && this.m_perfmonCounters != null)
					{
						this.m_perfmonCounters.Initializing = 0L;
						this.m_perfmonCounters.Resynchronizing = 1L;
					}
				}
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00037B62 File Offset: 0x00035D62
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x00037B70 File Offset: 0x00035D70
		public bool Running
		{
			get
			{
				return this.ProgressStage == ReplicaInstanceStage.Running;
			}
			private set
			{
				if (value)
				{
					ExTraceGlobals.StateTracer.TraceDebug<string>((long)this.GetHashCode(), "TargetState is changing to Running on replica {0}", this.m_identity);
					this.ProgressStage = ReplicaInstanceStage.Running;
					if (this.m_perfmonCounters != null)
					{
						this.m_perfmonCounters.Initializing = 0L;
						this.m_perfmonCounters.Resynchronizing = 0L;
					}
				}
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00037BC8 File Offset: 0x00035DC8
		public LocalizedString ErrorMessage
		{
			get
			{
				LocalizedString errorMessage;
				lock (this.m_instance)
				{
					errorMessage = this.FailureInfo.ErrorMessage;
				}
				return errorMessage;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00037C10 File Offset: 0x00035E10
		public uint ErrorEventId
		{
			get
			{
				uint errorEventId;
				lock (this.m_instance)
				{
					errorEventId = this.FailureInfo.ErrorEventId;
				}
				return errorEventId;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00037C58 File Offset: 0x00035E58
		public ExtendedErrorInfo ExtendedErrorInfo
		{
			get
			{
				ExtendedErrorInfo extendedErrorInfo;
				lock (this.m_instance)
				{
					extendedErrorInfo = this.FailureInfo.ExtendedErrorInfo;
				}
				return extendedErrorInfo;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x00037CA0 File Offset: 0x00035EA0
		public bool IsDisconnected
		{
			get
			{
				bool isDisconnected;
				lock (this.m_instance)
				{
					isDisconnected = this.FailureInfo.IsDisconnected;
				}
				return isDisconnected;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00037CE8 File Offset: 0x00035EE8
		public bool IsBroken
		{
			get
			{
				bool isFailed;
				lock (this.m_instance)
				{
					isFailed = this.FailureInfo.IsFailed;
				}
				return isFailed;
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00037D30 File Offset: 0x00035F30
		public CopyStatusEnum GetStatus()
		{
			CopyStatusEnum lastCopyStatus;
			lock (this.m_instance)
			{
				lastCopyStatus = this.ExternalStatus.LastCopyStatus;
			}
			return lastCopyStatus;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00037D78 File Offset: 0x00035F78
		public void SetViable()
		{
			lock (this.m_instance)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} Viable is now 'true'.", this.m_displayName);
				if (!this.m_replayState.ConfigBroken)
				{
					this.LogCrimsonEventOnStateChange<bool>("Viable", this.Viable, true);
				}
				this.Viable = true;
				this.ExternalStatus.Refresh();
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00037E00 File Offset: 0x00036000
		public void ClearViable()
		{
			lock (this.m_instance)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} Viable is now 'false'.", this.m_displayName);
				if (!this.m_replayState.ConfigBroken)
				{
					this.LogCrimsonEventOnStateChange<bool>("Viable", this.Viable, false);
				}
				this.Viable = false;
				this.ExternalStatus.Refresh();
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00037E88 File Offset: 0x00036088
		public void SetCopyGeneration(long generation, DateTime writeTime)
		{
			if (generation > this.m_replayState.CopyGenerationNumber)
			{
				this.m_replayState.CopyGenerationNumber = generation;
			}
			if (writeTime > this.m_replayState.LatestCopyTime)
			{
				this.m_replayState.LatestCopyTime = writeTime;
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00037EC3 File Offset: 0x000360C3
		public void SetInspectGeneration(long generation, DateTime writeTime)
		{
			if (generation > this.m_replayState.InspectorGenerationNumber)
			{
				this.m_replayState.InspectorGenerationNumber = generation;
			}
			if (generation == 0L || writeTime > this.m_replayState.LatestInspectorTime)
			{
				this.m_replayState.LatestInspectorTime = writeTime;
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00037F03 File Offset: 0x00036103
		public void SetCopyNotificationGeneration(long generation, DateTime writeTime)
		{
			if (generation > this.m_replayState.CopyNotificationGenerationNumber)
			{
				this.m_replayState.CopyNotificationGenerationNumber = generation;
			}
			if (writeTime > this.m_replayState.LatestCopyNotificationTime)
			{
				this.m_replayState.LatestCopyNotificationTime = writeTime;
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00037F40 File Offset: 0x00036140
		public void SetLogStreamStartGeneration(long generation)
		{
			lock (this.m_instance)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, long>((long)this.GetHashCode(), "{0} SetLogStreamStartGeneration( 0x{1:X} ) called.", this.m_displayName, generation);
				this.LogCrimsonEventOnStateChange<long>("LogStreamStartGeneration", this.m_replayState.LogStreamStartGeneration, generation);
				this.m_replayState.LogStreamStartGeneration = generation;
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00037FBC File Offset: 0x000361BC
		public void ClearLogStreamStartGeneration()
		{
			lock (this.m_instance)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} ClearLogStreamStartGeneration() called.", this.m_displayName);
				this.LogCrimsonEventOnStateChange<long>("LogStreamStartGeneration", this.m_replayState.LogStreamStartGeneration, 0L);
				this.m_replayState.LogStreamStartGeneration = 0L;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00038038 File Offset: 0x00036238
		public bool IsLogStreamStartGenerationResetPending
		{
			get
			{
				bool result;
				lock (this.m_instance)
				{
					result = (this.m_replayState.LogStreamStartGeneration > 0L);
				}
				return result;
			}
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00038084 File Offset: 0x00036284
		public void AttemptCopyLastLogsEnter()
		{
			Interlocked.Increment(ref this.m_numThreadsInAcll);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00038092 File Offset: 0x00036292
		public void AttemptCopyLastLogsLeave()
		{
			Interlocked.Decrement(ref this.m_numThreadsInAcll);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x000380A0 File Offset: 0x000362A0
		public void ClearAttemptCopyLastLogsEndTime()
		{
			this.m_replayState.LastAttemptCopyLastLogsEndTime = ReplayState.ZeroFileTime;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x000380B2 File Offset: 0x000362B2
		public void SetAttemptCopyLastLogsEndTime()
		{
			this.m_replayState.LastAttemptCopyLastLogsEndTime = DateTime.UtcNow;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000380C4 File Offset: 0x000362C4
		public bool IsFailoverPending()
		{
			bool result;
			lock (this.m_instance)
			{
				result = (this.InAttemptCopyLastLogs || this.m_replayState.SuspendLockOwner == LockOwner.AttemptCopyLastLogs);
			}
			return result;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0003811C File Offset: 0x0003631C
		public bool ShouldNotRestartInstanceDueToAcll()
		{
			bool result;
			lock (this.m_instance)
			{
				bool flag2 = this.IsFailoverPending();
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "ShouldNotRestartInstanceDueToAcll() {0}: IsFailoverPending() returned {1}.", this.m_displayName, flag2);
				result = flag2;
			}
			return result;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00038180 File Offset: 0x00036380
		public void SetDisconnected(FailureTag failureTag, ExEventLog.EventTuple setDisconnectedEventTuple, params string[] setDisconnectedArgs)
		{
			this.SetDisconnectedInternal(failureTag, setDisconnectedEventTuple, setDisconnectedArgs);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0003818C File Offset: 0x0003638C
		public void ClearDisconnected()
		{
			lock (this.m_instance)
			{
				this.ClearPreviousDisconnectedState();
				if (this.IsDisconnected)
				{
					this.LogCrimsonEventOnStateChange<bool>("Disconnected", this.IsDisconnected, false);
					this.FailureInfo.Reset();
					if (this.m_perfmonCounters != null)
					{
						this.m_perfmonCounters.Disconnected = 0L;
					}
					ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} Disconnected state cleared.", this.m_displayName);
				}
				else
				{
					ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "{0} ClearDisconnected() ignored because RI is not currently disconnected. FailureInfo.Failed = '{1}'.", this.m_displayName, this.FailureInfo.IsFailed);
				}
				this.ExternalStatus.Refresh();
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00038258 File Offset: 0x00036458
		public void SetBroken(FailureTag failureTag, ExEventLog.EventTuple setBrokenEventTuple, params string[] setBrokenArgs)
		{
			this.SetBroken(failureTag, setBrokenEventTuple, null, setBrokenArgs);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00038264 File Offset: 0x00036464
		public void SetBroken(FailureTag failureTag, ExEventLog.EventTuple setBrokenEventTuple, Exception exception, params string[] setBrokenArgs)
		{
			string[] argumentsWithDb = ReplicaInstanceContext.GetArgumentsWithDb(setBrokenArgs, this.m_displayName);
			this.SetBrokenInternal(failureTag, setBrokenEventTuple, new ExtendedErrorInfo(exception), argumentsWithDb);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00038290 File Offset: 0x00036490
		public void SetBrokenAndThrow(FailureTag failureTag, ExEventLog.EventTuple setBrokenEventTuple, Exception exception, params string[] setBrokenArgs)
		{
			string[] argumentsWithDb = ReplicaInstanceContext.GetArgumentsWithDb(setBrokenArgs, this.m_displayName);
			this.SetBrokenInternal(failureTag, setBrokenEventTuple, new ExtendedErrorInfo(exception), argumentsWithDb);
			throw new SetBrokenControlTransferException();
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x000382C0 File Offset: 0x000364C0
		public void ClearBroken()
		{
			lock (this.m_instance)
			{
				this.ClearPreviousFailedState();
				if (!this.IsBroken && !this.IsDisconnected)
				{
					this.ClearCurrentFailedState();
				}
				else
				{
					ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: We did not actually clear: Config is broken we won't start the run", this.m_displayName);
				}
				if (this.m_instance.Configuration.Type == ReplayConfigType.RemoteCopyTarget && !this.m_instance.PrepareToStopCalled)
				{
					ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: ClearBroken(): State is set to running(healthy)", this.m_displayName);
					this.UpdateInstanceProgress(ReplicaInstanceStage.Running);
				}
				this.ExternalStatus.Refresh();
			}
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00038384 File Offset: 0x00036584
		public void RestartInstanceSoon(bool fPrepareToStop)
		{
			this.RestartInstanceSoonInternal(false, fPrepareToStop);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0003838E File Offset: 0x0003658E
		public void RestartInstanceSoonAdminVisible()
		{
			this.RestartInstanceSoonInternal(true, true);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00038398 File Offset: 0x00036598
		public void RestartInstanceNow(ReplayConfigChangeHints restartReason)
		{
			this.RestartInstanceSoonInternal(false, true);
			Dependencies.ConfigurationUpdater.NotifyChangedReplayConfiguration(this.m_guid, false, true, true, true, restartReason, -1);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000383BC File Offset: 0x000365BC
		public void UpdateInstanceProgress(ReplicaInstanceStage stage)
		{
			lock (this.m_instance)
			{
				switch (stage)
				{
				case ReplicaInstanceStage.Initializing:
					this.Initializing = true;
					break;
				case ReplicaInstanceStage.Resynchronizing:
					this.Resynchronizing = true;
					break;
				case ReplicaInstanceStage.Running:
					this.Running = true;
					break;
				default:
					DiagCore.RetailAssert(false, "Invalid ReplicaInstanceStage: {0}", new object[]
					{
						stage.ToString()
					});
					break;
				}
				this.ExternalStatus.Refresh();
			}
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00038458 File Offset: 0x00036658
		public void SetSuspended()
		{
			lock (this.m_instance)
			{
				this.SetSuspendedInternal();
				this.ExternalStatus.Refresh();
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x000384A4 File Offset: 0x000366A4
		public void SetFailedAndSuspended(uint failureEventId, LocalizedString errorMessage, ExtendedErrorInfo errorInfo)
		{
			lock (this.m_instance)
			{
				if (this.m_perfmonCounters != null)
				{
					this.m_perfmonCounters.Failed = 1L;
					this.m_perfmonCounters.FailedSuspended = 1L;
					this.m_perfmonCounters.Disconnected = 0L;
				}
				this.SetSuspendedInternal();
				this.FailureInfo.SetBroken(new uint?(failureEventId), errorMessage, errorInfo);
				this.ExternalStatus.Refresh();
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00038534 File Offset: 0x00036734
		public void InitializeForSource()
		{
			if (this.m_replayState.ConfigBroken)
			{
				this.FailureInfo.SetBroken(new uint?((uint)this.m_replayState.ConfigBrokenEventId), new LocalizedString(this.m_replayState.ConfigBrokenMessage), this.m_replayState.ConfigBrokenExtendedErrorInfo);
				if (this.m_perfmonCounters != null)
				{
					this.m_perfmonCounters.Failed = 1L;
					this.m_perfmonCounters.Disconnected = 0L;
					if (this.m_replayState.Suspended)
					{
						this.m_perfmonCounters.FailedSuspended = 1L;
					}
				}
				if (this.m_replayState.Suspended)
				{
					this.SetSuspendedInternal();
				}
				this.ExternalStatus.Refresh();
			}
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x000385E4 File Offset: 0x000367E4
		public void PersistFailure(uint errorEventId, LocalizedString errorMessage)
		{
			lock (this.m_instance)
			{
				this.FailureInfo.SetBroken(new uint?(errorEventId), errorMessage, null);
				this.FailureInfo.PersistFailure(this.m_replayState);
				if (this.m_perfmonCounters != null)
				{
					this.m_perfmonCounters.Failed = 1L;
					this.m_perfmonCounters.Disconnected = 0L;
					if (this.Suspended)
					{
						this.m_perfmonCounters.FailedSuspended = 1L;
					}
				}
				this.ExternalStatus.Refresh();
			}
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00038684 File Offset: 0x00036884
		public void ClearSuspended(bool isActiveCopy, bool restartInstance, bool syncOnly)
		{
			lock (this.m_instance)
			{
				this.LogCrimsonEventOnStateChange<bool>("Suspended", this.Suspended, false);
				this.Suspended = false;
				if (this.m_perfmonCounters != null)
				{
					this.m_perfmonCounters.Suspended = 0L;
					this.m_perfmonCounters.SuspendedAndNotSeeding = 0L;
				}
				if (!syncOnly)
				{
					this.ClearCurrentFailedState();
					this.ClearPreviousFailedOrDisconnectedState();
					this.DoNotRestart = false;
				}
				else
				{
					ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "ClearSuspended: {0}: Skipping clearing failure states because 'syncOnly == true'.", this.m_displayName);
				}
				if (!isActiveCopy && restartInstance)
				{
					this.AdminVisibleRestart = true;
					this.RestartSoon = true;
					ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "ClearSuspended: {0}: Setting RestartSoon flag to ensure RI gets restarted.", this.m_displayName);
				}
				this.ExternalStatus.Refresh();
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00038768 File Offset: 0x00036968
		public void CheckReseedBlocked()
		{
			lock (this.m_instance)
			{
				if (this.m_replayState.ReseedBlocked)
				{
					ExTraceGlobals.ReplicaInstanceTracer.TraceError<string>((long)this.GetHashCode(), "CheckReseedBlocked: {0}: ReseedBlocked is set, so throwing.", this.m_displayName);
					throw new SeederInstanceReseedBlockedException(this.m_displayName, AmExceptionHelper.GetMessageOrNoneString(this.ErrorMessage));
				}
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000387E8 File Offset: 0x000369E8
		public bool TryBeginDbSeed(RpcSeederArgs rpcSeederArgs)
		{
			bool result;
			lock (this.m_instance)
			{
				if (!this.Suspended)
				{
					ExTraceGlobals.ReplicaInstanceTracer.TraceError<string>((long)this.GetHashCode(), "TryBeginDbSeed: {0}: Could not mark RI for seed because it is not suspended.", this.m_displayName);
					result = false;
				}
				else
				{
					this.CheckReseedBlocked();
					this.ClearViable();
					this.LogCrimsonEventOnStateChange<bool>("Seeding", this.Seeding, true);
					this.Seeding = true;
					if (this.m_perfmonCounters != null)
					{
						this.m_perfmonCounters.Initializing = 0L;
						this.m_perfmonCounters.Resynchronizing = 0L;
						this.m_perfmonCounters.SuspendedAndNotSeeding = 0L;
					}
					this.m_replayState.ResetForSeed();
					this.InitializeVolumeInfo();
					this.ClearLogStreamStartGeneration();
					this.ClearCurrentFailedState();
					this.ClearPreviousFailedOrDisconnectedState();
					this.SetSeedingStartedErrorMessage(rpcSeederArgs);
					this.ExternalStatus.Refresh();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x000388DC File Offset: 0x00036ADC
		public void EndDbSeed()
		{
			lock (this.m_instance)
			{
				this.LogCrimsonEventOnStateChange<bool>("Seeding", this.Seeding, false);
				this.Seeding = false;
				if (this.Suspended)
				{
					this.SetSuspendedInternal();
				}
				this.ClearCurrentFailedState();
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Seeding has been finished.", this.m_displayName);
				this.ExternalStatus.Refresh();
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0003896C File Offset: 0x00036B6C
		public void FailedDbSeed(ExEventLog.EventTuple errorEventTuple, LocalizedString errorMessage, ExtendedErrorInfo errorInfo)
		{
			lock (this.m_instance)
			{
				this.LogCrimsonEventOnStateChange<bool>("Seeding", this.Seeding, false);
				this.Seeding = false;
				if (this.m_perfmonCounters != null)
				{
					this.m_perfmonCounters.Failed = 1L;
					this.m_perfmonCounters.Disconnected = 0L;
					if (this.Suspended)
					{
						this.m_perfmonCounters.FailedSuspended = 1L;
					}
				}
				if (this.Suspended)
				{
					this.SetSuspendedInternal();
				}
				this.FailureInfo.SetBroken(errorEventTuple, errorMessage, errorInfo);
				this.FailureInfo.PersistFailure(this.m_replayState);
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, LocalizedString>((long)this.GetHashCode(), "{0}: Seeding failed with error: {1}", this.m_displayName, errorMessage);
				this.ExternalStatus.Refresh();
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00038A4C File Offset: 0x00036C4C
		public void BeginPassiveSeeding(PassiveSeedingSourceContextEnum passiveSeedingSourceContext, bool invokedForRestart)
		{
			lock (this.m_instance)
			{
				if (!invokedForRestart && this.ExternalStatus.LastCopyStatus != CopyStatusEnum.Healthy && this.ExternalStatus.LastCopyStatus != CopyStatusEnum.DisconnectedAndHealthy)
				{
					throw new CannotBeginSeedingInstanceNotInStateException(this.DisplayName, this.ExternalStatus.LastCopyStatus.ToString());
				}
				this.LogCrimsonEventOnStateChange<PassiveSeedingSourceContextEnum>("PassiveSeedingSourceContext", this.PassiveSeedingSourceContext, passiveSeedingSourceContext);
				this.PassiveSeedingSourceContext = passiveSeedingSourceContext;
				this.ExternalStatus.Refresh();
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Passive Seeding has been started and this server is the source.", this.m_displayName);
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00038B08 File Offset: 0x00036D08
		public void EndPassiveSeeding()
		{
			lock (this.m_instance)
			{
				this.LogCrimsonEventOnStateChange<PassiveSeedingSourceContextEnum>("PassiveSeedingSourceContext", this.PassiveSeedingSourceContext, PassiveSeedingSourceContextEnum.None);
				this.PassiveSeedingSourceContext = PassiveSeedingSourceContextEnum.None;
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Passive Seeding has been finished and this server was the source.", this.m_displayName);
				this.ExternalStatus.Refresh();
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00038B84 File Offset: 0x00036D84
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x00038B8C File Offset: 0x00036D8C
		public SeedType SeedType { get; private set; }

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00038B98 File Offset: 0x00036D98
		public void BeginActiveSeeding(SeedType seedType)
		{
			lock (this.m_instance)
			{
				this.LogCrimsonEventOnStateChange<bool>("ActiveSeedingSource", this.ActiveSeedingSource, true);
				this.ActiveSeedingSource = true;
				this.SeedType = seedType;
				switch (seedType)
				{
				case SeedType.Database:
					this.IsSeedingSourceForDB = true;
					break;
				case SeedType.Catalog:
					this.IsSeedingSourceForCI = true;
					break;
				}
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Active Seeding has been started and this server is the source.", this.m_displayName);
			}
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00038C34 File Offset: 0x00036E34
		public void EndActiveSeeding()
		{
			lock (this.m_instance)
			{
				this.LogCrimsonEventOnStateChange<bool>("ActiveSeedingSource", this.ActiveSeedingSource, false);
				this.ActiveSeedingSource = false;
				this.IsSeedingSourceForDB = false;
				this.IsSeedingSourceForCI = false;
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Active Seeding has been finished and this server was the source.", this.m_displayName);
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00038CB4 File Offset: 0x00036EB4
		public void InitializeVolumeInfo()
		{
			ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: InitializeVolumeInfo() called.", this.m_displayName);
			DatabaseVolumeInfo instance = DatabaseVolumeInfo.GetInstance(this.m_instance.Configuration);
			lock (this.m_instance)
			{
				this.DatabaseVolumeInfo = instance;
				this.m_replayState.SetVolumeInfoIfValid(instance);
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00038D30 File Offset: 0x00036F30
		public void UpdateVolumeInfo()
		{
			DatabaseVolumeInfo databaseVolumeInfo = this.DatabaseVolumeInfo;
			MountedFolderPath mountedFolderPath = databaseVolumeInfo.DatabaseVolumeName;
			if (MountedFolderPath.IsNullOrEmpty(mountedFolderPath) && this.m_replayState.VolumeInfoIsValid)
			{
				mountedFolderPath = new MountedFolderPath(this.m_replayState.DatabaseVolumeName);
			}
			DatabaseVolumeInfo instance = DatabaseVolumeInfo.GetInstance(this.m_instance.Configuration);
			lock (this.m_instance)
			{
				this.DatabaseVolumeInfo = instance;
				this.m_replayState.SetVolumeInfoIfValid(instance);
				if (!MountedFolderPath.IsNullOrEmpty(instance.DatabaseVolumeName) && !MountedFolderPath.IsEqual(instance.DatabaseVolumeName, mountedFolderPath))
				{
					this.m_replayState.LastDatabaseVolumeName = mountedFolderPath.Path;
					this.m_replayState.LastDatabaseVolumeNameTransitionTime = DateTime.UtcNow;
				}
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00038E00 File Offset: 0x00037000
		// (set) Token: 0x06000CEC RID: 3308 RVA: 0x00038E44 File Offset: 0x00037044
		public ReplicaInstanceStage ProgressStage
		{
			get
			{
				ReplicaInstanceStage progressStage;
				lock (this.m_instance)
				{
					progressStage = this.m_progressStage;
				}
				return progressStage;
			}
			private set
			{
				lock (this.m_instance)
				{
					if (!this.m_replayState.ConfigBroken)
					{
						this.LogCrimsonEventOnStateChange<ReplicaInstanceStage>("ReplicaInstanceStage", this.m_progressStage, value);
					}
					this.m_progressStage = value;
				}
			}
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00038EA8 File Offset: 0x000370A8
		public void ReportOneLogCopied()
		{
			Interlocked.Increment(ref this.m_countLogsCopied);
			this.LogGreenEventIfNecessary();
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00038EBC File Offset: 0x000370BC
		public void ReportLogsReplayed(long numLogs)
		{
			Interlocked.Add(ref this.m_countLogsReplayed, numLogs);
			this.LogGreenEventIfNecessary();
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00038ED4 File Offset: 0x000370D4
		public void LogGreenEventIfNecessary()
		{
			if (this.m_greenEventLogged == 0L && this.m_countLogsCopied > this.m_countLogsThreshold && this.m_countLogsReplayed > this.m_countLogsThreshold && Interlocked.CompareExchange(ref this.m_greenEventLogged, 1L, 0L) == 0L)
			{
				ReplayEventLogConstants.Tuple_ReplicaInstanceMadeProgress.LogEvent(null, new object[]
				{
					this.m_databaseName
				});
			}
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00038F38 File Offset: 0x00037138
		internal void BestEffortDismountReplayDatabase()
		{
			if (this.IsReplayDatabaseDismountPending)
			{
				try
				{
					LogReplayer.DismountReplayDatabase(this.m_guid, this.m_identity, this.m_databaseName, null);
				}
				finally
				{
					this.IsReplayDatabaseDismountPending = false;
				}
			}
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00038F80 File Offset: 0x00037180
		private static string[] GetArgumentsWithDb(string[] argumentsWithoutDb, string database)
		{
			string[] array = new string[argumentsWithoutDb.Length + 1];
			array[0] = database;
			if (argumentsWithoutDb.Length > 0)
			{
				argumentsWithoutDb.CopyTo(array, 1);
			}
			return array;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00038FAC File Offset: 0x000371AC
		private void SetDisconnectedInternal(FailureTag failureTag, ExEventLog.EventTuple setDisconnectedEventTuple, params string[] setDisconnectedArgs)
		{
			string[] argumentsWithDb = ReplicaInstanceContext.GetArgumentsWithDb(setDisconnectedArgs, this.m_displayName);
			int num;
			string text = setDisconnectedEventTuple.EventLogToString(out num, argumentsWithDb);
			lock (this.m_instance)
			{
				if (!this.FailureInfo.IsFailed)
				{
					this.LogCrimsonEventOnStateChange<bool>("Disconnected", this.FailureInfo.IsDisconnected, true);
					this.FailureInfo.SetDisconnected(setDisconnectedEventTuple, new LocalizedString(text));
					if (this.m_perfmonCounters != null)
					{
						this.m_perfmonCounters.Disconnected = 1L;
					}
				}
				else
				{
					ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} SetDisconnected ignored because RI is already marked Failed.", this.m_displayName);
				}
				this.ExternalStatus.Refresh();
			}
			bool flag2;
			setDisconnectedEventTuple.LogEvent(this.m_identity, out flag2, argumentsWithDb);
			ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} SetDisconnected because {1}", this.m_displayName, text);
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x000390A8 File Offset: 0x000372A8
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x000390B0 File Offset: 0x000372B0
		private ExternalReplicaInstanceStatus ExternalStatus { get; set; }

		// Token: 0x06000CF5 RID: 3317 RVA: 0x000390BC File Offset: 0x000372BC
		private void RestartInstanceSoonInternal(bool fAdminVisible, bool fPrepareToStop)
		{
			lock (this.m_instance)
			{
				this.RestartSoon = true;
				if (!this.AdminVisibleRestart && fAdminVisible)
				{
					this.AdminVisibleRestart = fAdminVisible;
					this.ClearCurrentFailedState();
					this.ClearPreviousFailedOrDisconnectedState();
				}
				this.ExternalStatus.Refresh();
			}
			if (fPrepareToStop)
			{
				this.m_instance.PrepareToStop();
			}
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00039134 File Offset: 0x00037334
		private void ClearCurrentFailedState()
		{
			this.FailureInfo.Reset();
			if (this.m_perfmonCounters != null)
			{
				this.m_perfmonCounters.Failed = 0L;
				this.m_perfmonCounters.FailedSuspended = 0L;
				this.m_perfmonCounters.Disconnected = 0L;
			}
			if (this.m_replayState.ConfigBroken)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} Failed persisted state cleared.", this.m_displayName);
				this.m_replayState.ConfigBroken = false;
				this.m_replayState.ConfigBrokenMessage = null;
				this.m_replayState.ConfigBrokenEventId = 0L;
				this.m_replayState.ConfigBrokenExtendedErrorInfo = null;
			}
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x000391D5 File Offset: 0x000373D5
		private void ClearPreviousFailedState()
		{
			if (this.m_instance.PreviousContext != null && this.m_instance.PreviousContext.FailureInfo.IsFailed)
			{
				this.m_instance.PreviousContext.FailureInfo.Reset();
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00039210 File Offset: 0x00037410
		private void ClearPreviousDisconnectedState()
		{
			if (this.m_instance.PreviousContext != null && this.m_instance.PreviousContext.FailureInfo.IsDisconnected)
			{
				this.m_instance.PreviousContext.FailureInfo.Reset();
			}
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0003924B File Offset: 0x0003744B
		private void ClearPreviousFailedOrDisconnectedState()
		{
			if (this.m_instance.PreviousContext != null)
			{
				this.m_instance.PreviousContext.FailureInfo.Reset();
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00039270 File Offset: 0x00037470
		private void SetSuspendedInternal()
		{
			this.LogCrimsonEventOnStateChange<bool>("Suspended", this.Suspended, true);
			this.Suspended = true;
			if (this.m_perfmonCounters != null)
			{
				this.m_perfmonCounters.Initializing = 0L;
				this.m_perfmonCounters.Resynchronizing = 0L;
				this.m_perfmonCounters.Suspended = 1L;
				if (this.m_perfmonCounters.FailedSuspended == 1L)
				{
					this.m_perfmonCounters.SuspendedAndNotSeeding = 0L;
					return;
				}
				this.m_perfmonCounters.SuspendedAndNotSeeding = 1L;
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000392F0 File Offset: 0x000374F0
		private void SetBrokenInternal(FailureTag failureTag, ExEventLog.EventTuple setBrokenEventTuple, ExtendedErrorInfo extendedErrorInfo, params string[] setBrokenArgsPlusDb)
		{
			int num;
			string text = setBrokenEventTuple.EventLogToString(out num, setBrokenArgsPlusDb);
			Exception failureException = extendedErrorInfo.FailureException;
			int num2 = 0;
			if (failureException != null)
			{
				num2 = failureException.HResult;
			}
			ReplayCrimsonEvents.SetBroken.LogPeriodic<Guid, string, string, string, Exception, int>(this.m_databaseName, DiagCore.DefaultEventSuppressionInterval, this.m_guid, this.m_databaseName, text, Environment.StackTrace, failureException, num2);
			bool flag = false;
			lock (this.m_instance)
			{
				flag = this.IsBroken;
				this.FailureInfo.SetBroken(setBrokenEventTuple, new LocalizedString(text), extendedErrorInfo);
				if (this.m_perfmonCounters != null)
				{
					this.m_perfmonCounters.Failed = 1L;
					this.m_perfmonCounters.Disconnected = 0L;
					if (this.Suspended)
					{
						this.m_perfmonCounters.FailedSuspended = 1L;
					}
				}
				bool flag3;
				setBrokenEventTuple.LogEvent(this.m_identity, out flag3, setBrokenArgsPlusDb);
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, FailureTag, string>((long)this.GetHashCode(), "{0} SetBroken with tag {1} because {2}", this.m_displayName, failureTag, text);
				MonitoredDatabase monitoredDatabase = MonitoredDatabase.FindMonitoredDatabase(this.ReplicaInstance.Configuration.ServerName, this.m_guid);
				if (monitoredDatabase != null && this.PassiveSeedingSourceContext != PassiveSeedingSourceContextEnum.None)
				{
					ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Cancel seeding for mdb {0}", this.m_guid);
					SourceSeedTable.Instance.CancelSeedingIfAppropriate(SourceSeedTable.CancelReason.CopyFailed, monitoredDatabase.DatabaseGuid);
				}
				bool flag4 = false;
				if (flag3 && (!RegistryParameters.DisableSetBrokenFailureItemSuppression || this.IsSuppressableFailureTag(failureTag)) && !this.IsNonSuppressableFailureTag(failureTag))
				{
					flag4 = true;
				}
				if (!flag4 && failureTag != FailureTag.NoOp)
				{
					FailureItemPublisherHelper.PublishAction(failureTag, this.m_guid, this.m_databaseName);
				}
				if (!flag)
				{
					this.FailureInfo.PersistFailure(this.m_replayState);
				}
				this.ExternalStatus.Refresh();
			}
			if (!flag)
			{
				this.m_instance.PrepareToStop();
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000394C8 File Offset: 0x000376C8
		private void SetSeedingStartedErrorMessage(RpcSeederArgs rpcSeederArgs)
		{
			string[] argumentsWithDb = ReplicaInstanceContext.GetArgumentsWithDb(new string[]
			{
				rpcSeederArgs.ToString()
			}, this.m_displayName);
			ExEventLog.EventTuple tuple_SeedInstanceStartedSetBroken = ReplayEventLogConstants.Tuple_SeedInstanceStartedSetBroken;
			int num;
			string value = tuple_SeedInstanceStartedSetBroken.EventLogToString(out num, argumentsWithDb);
			this.FailureInfo.SetBroken(tuple_SeedInstanceStartedSetBroken, new LocalizedString(value), null);
			this.FailureInfo.PersistFailure(this.m_replayState);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00039529 File Offset: 0x00037729
		private bool IsSuppressableFailureTag(FailureTag tag)
		{
			return tag == FailureTag.AlertOnly || tag == FailureTag.NoOp;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00039536 File Offset: 0x00037736
		private bool IsNonSuppressableFailureTag(FailureTag tag)
		{
			return tag == FailureTag.Reseed || tag == FailureTag.Configuration;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00039544 File Offset: 0x00037744
		private void LogCrimsonEventOnStateChange<T>(string stateName, T oldValue, T newValue)
		{
			ReplayState.LogCrimsonEventOnStateChange<T>(this.m_databaseName, this.m_identity, Environment.MachineName, stateName, oldValue, newValue);
		}

		// Token: 0x04000563 RID: 1379
		private string m_identity;

		// Token: 0x04000564 RID: 1380
		private string m_databaseName;

		// Token: 0x04000565 RID: 1381
		private string m_displayName;

		// Token: 0x04000566 RID: 1382
		private Guid m_guid;

		// Token: 0x04000567 RID: 1383
		private IPerfmonCounters m_perfmonCounters;

		// Token: 0x04000568 RID: 1384
		private ReplicaInstance m_instance;

		// Token: 0x04000569 RID: 1385
		private ReplicaInstanceStage m_progressStage;

		// Token: 0x0400056A RID: 1386
		private ReplayState m_replayState;

		// Token: 0x0400056B RID: 1387
		private long m_greenEventLogged;

		// Token: 0x0400056C RID: 1388
		private long m_countLogsCopied;

		// Token: 0x0400056D RID: 1389
		private long m_countLogsReplayed;

		// Token: 0x0400056E RID: 1390
		private long m_countLogsThreshold;

		// Token: 0x0400056F RID: 1391
		private bool m_fRestartSoon;

		// Token: 0x04000570 RID: 1392
		private bool m_fDoNotRestart;

		// Token: 0x04000571 RID: 1393
		private bool m_fSuspended;

		// Token: 0x04000572 RID: 1394
		private bool m_fSeeding;

		// Token: 0x04000573 RID: 1395
		private PassiveSeedingSourceContextEnum m_PassiveSeedingSourceContext;

		// Token: 0x04000574 RID: 1396
		private bool m_fActiveSeedingSource;

		// Token: 0x04000575 RID: 1397
		private bool m_fViable;

		// Token: 0x04000576 RID: 1398
		private bool m_fAdminVisibleRestart;

		// Token: 0x04000577 RID: 1399
		private int m_numThreadsInAcll;
	}
}
