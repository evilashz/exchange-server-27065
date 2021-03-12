using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000148 RID: 328
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExternalReplicaInstanceStatus
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00037130 File Offset: 0x00035330
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x00037138 File Offset: 0x00035338
		private ReplayConfigType ConfigType { get; set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00037141 File Offset: 0x00035341
		// (set) Token: 0x06000C7A RID: 3194 RVA: 0x00037149 File Offset: 0x00035349
		private ReplicaInstanceContext CurrentContext { get; set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00037152 File Offset: 0x00035352
		// (set) Token: 0x06000C7C RID: 3196 RVA: 0x0003715A File Offset: 0x0003535A
		private ReplicaInstanceContextMinimal PreviousContext { get; set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x00037163 File Offset: 0x00035363
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x0003716B File Offset: 0x0003536B
		private IPerfmonCounters PerfmonCounters { get; set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00037174 File Offset: 0x00035374
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x0003717C File Offset: 0x0003537C
		private ReplayState ReplayState { get; set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00037185 File Offset: 0x00035385
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0003718D File Offset: 0x0003538D
		private string DatabaseName { get; set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00037198 File Offset: 0x00035398
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x000371D8 File Offset: 0x000353D8
		public CopyStatusEnum LastCopyStatus
		{
			get
			{
				CopyStatusEnum lastCopyStatus;
				lock (this)
				{
					lastCopyStatus = this.m_lastCopyStatus;
				}
				return lastCopyStatus;
			}
			private set
			{
				lock (this)
				{
					this.m_lastCopyStatus = value;
				}
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00037214 File Offset: 0x00035414
		public ExternalReplicaInstanceStatus(ReplicaInstanceContext currentContext, ReplicaInstanceContextMinimal previousContext, ReplayConfigType configurationType, IPerfmonCounters perfmonCounters, ReplayState replayState)
		{
			this.CurrentContext = currentContext;
			this.PreviousContext = previousContext;
			this.ConfigType = configurationType;
			this.PerfmonCounters = perfmonCounters;
			this.ReplayState = replayState;
			if (ThirdPartyManager.IsThirdPartyReplicationEnabled && this.ConfigType == ReplayConfigType.RemoteCopyTarget)
			{
				this.LastCopyStatus = CopyStatusEnum.NonExchangeReplication;
			}
			else
			{
				this.LastCopyStatus = CopyStatusEnum.Unknown;
			}
			this.m_displayName = currentContext.DisplayName;
			this.m_identity = currentContext.Identity;
			this.DatabaseName = currentContext.DatabaseName;
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00037290 File Offset: 0x00035490
		private bool ShouldTrackTransitions
		{
			get
			{
				return !ThirdPartyManager.IsThirdPartyReplicationEnabled && this.ConfigType == ReplayConfigType.RemoteCopyTarget;
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x000372A4 File Offset: 0x000354A4
		public void CarryOverPreviousStatus(CopyStatusEnum lastCopyStatusEnum)
		{
			this.LastCopyStatus = lastCopyStatusEnum;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x000372B0 File Offset: 0x000354B0
		public void Refresh()
		{
			lock (this)
			{
				if (ThirdPartyManager.IsThirdPartyReplicationEnabled && this.ConfigType == ReplayConfigType.RemoteCopyTarget)
				{
					this.LastCopyStatus = CopyStatusEnum.NonExchangeReplication;
				}
				else
				{
					CopyStatusEnum copyStatusEnum;
					if (this.CurrentContext.Seeding)
					{
						copyStatusEnum = CopyStatusEnum.Seeding;
					}
					else if (this.CurrentContext.PassiveSeedingSourceContext == PassiveSeedingSourceContextEnum.Database)
					{
						copyStatusEnum = CopyStatusEnum.SeedingSource;
					}
					else if (this.CurrentContext.Suspended)
					{
						if (this.CurrentContext.IsBroken || (this.PreviousContext != null && this.PreviousContext.FailureInfo.IsFailed))
						{
							copyStatusEnum = CopyStatusEnum.FailedAndSuspended;
						}
						else
						{
							copyStatusEnum = CopyStatusEnum.Suspended;
						}
					}
					else if (this.CurrentContext.IsBroken)
					{
						copyStatusEnum = CopyStatusEnum.Failed;
					}
					else if (!this.CurrentContext.IsDisconnected && this.PreviousContext != null && this.PreviousContext.FailureInfo.IsFailed)
					{
						copyStatusEnum = CopyStatusEnum.Failed;
					}
					else if (this.CurrentContext.IsDisconnected || (this.PreviousContext != null && this.PreviousContext.FailureInfo.IsDisconnected))
					{
						if (this.CurrentContext.Initializing || this.CurrentContext.Resynchronizing || !this.CurrentContext.Viable)
						{
							copyStatusEnum = CopyStatusEnum.DisconnectedAndResynchronizing;
						}
						else
						{
							copyStatusEnum = CopyStatusEnum.DisconnectedAndHealthy;
						}
					}
					else if (this.CurrentContext.Initializing || this.CurrentContext.AdminVisibleRestart)
					{
						copyStatusEnum = CopyStatusEnum.Initializing;
					}
					else if (this.CurrentContext.Resynchronizing || !this.CurrentContext.Viable)
					{
						copyStatusEnum = CopyStatusEnum.Resynchronizing;
					}
					else
					{
						copyStatusEnum = CopyStatusEnum.Healthy;
					}
					if (this.ShouldTrackTransitions && this.LastCopyStatus != copyStatusEnum)
					{
						ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, CopyStatusEnum, CopyStatusEnum>((long)this.GetHashCode(), "{0} CopyStatusEnum changing from '{1}' to '{2}'.", this.m_displayName, this.LastCopyStatus, copyStatusEnum);
						this.LogCrimsonEventOnStateChange<CopyStatusEnum>("CopyStatusEnum", this.LastCopyStatus, copyStatusEnum);
						this.UpdateLastStatusTransitionTime(copyStatusEnum);
						this.LastCopyStatus = copyStatusEnum;
					}
				}
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x000374A8 File Offset: 0x000356A8
		private void UpdateLastStatusTransitionTime(CopyStatusEnum copyStatus)
		{
			bool flag = false;
			if (copyStatus == CopyStatusEnum.Suspended || copyStatus == CopyStatusEnum.FailedAndSuspended)
			{
				if (this.LastCopyStatus != CopyStatusEnum.Unknown || this.ReplayState.LastStatusTransitionTime.Equals(ReplayState.ZeroFileTime))
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				this.ReplayState.LastStatusTransitionTime = DateTime.UtcNow;
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000374FA File Offset: 0x000356FA
		private void LogCrimsonEventOnStateChange<T>(string stateName, T oldValue, T newValue)
		{
			ReplayState.LogCrimsonEventOnStateChange<T>(this.DatabaseName, this.m_identity, Environment.MachineName, stateName, oldValue, newValue);
		}

		// Token: 0x04000557 RID: 1367
		private string m_displayName;

		// Token: 0x04000558 RID: 1368
		private string m_identity;

		// Token: 0x04000559 RID: 1369
		private CopyStatusEnum m_lastCopyStatus;
	}
}
