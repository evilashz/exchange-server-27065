using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200010F RID: 271
	internal class CoordinatedQuotaBasedRecoveryAction : CoordinatedRecoveryAction
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x00030A8C File Offset: 0x0002EC8C
		internal CoordinatedQuotaBasedRecoveryAction(RecoveryActionId actionId, ResponderDefinition responderDefinition, string resourceName, int minimumRequiredTobeInReadyState, TimeSpan durationToCheck, string[] servers) : base(actionId, responderDefinition.Name, minimumRequiredTobeInReadyState, 1, servers)
		{
			this.resourceName = resourceName;
			this.durationToCheck = durationToCheck;
			FixedThrottleEntry throttleDefinition = Dependencies.ThrottleHelper.Settings.GetThrottleDefinition(actionId, resourceName, responderDefinition);
			this.maximumAllowedLocalActionsInTheDuration = throttleDefinition.ThrottleParameters.LocalMaximumAllowedAttemptsInADay;
			this.minimumDurationBetweenActionsAcrossGroup = TimeSpan.FromMinutes((double)throttleDefinition.ThrottleParameters.GroupMinimumMinutesBetweenAttempts);
			this.maximumAllowedTotalActionsAcrossGroup = throttleDefinition.ThrottleParameters.GroupMaximumAllowedAttemptsInADay;
			this.TotalQuotaExhaustedAcrossGroup = 0;
			this.MaximumPossibleQuotaFromRemainingServers = servers.Length * this.maximumAllowedLocalActionsInTheDuration;
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00030B29 File Offset: 0x0002ED29
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x00030B31 File Offset: 0x0002ED31
		internal int TotalQuotaExhaustedAcrossGroup { get; private set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00030B3A File Offset: 0x0002ED3A
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x00030B42 File Offset: 0x0002ED42
		internal int MaximumPossibleQuotaFromRemainingServers { get; private set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00030B4B File Offset: 0x0002ED4B
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x00030B53 File Offset: 0x0002ED53
		internal bool IsQuotaExhausted { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00030B5C File Offset: 0x0002ED5C
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00030B64 File Offset: 0x0002ED64
		internal bool IsQuotaComputationConcluded { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00030B6D File Offset: 0x0002ED6D
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x00030B75 File Offset: 0x0002ED75
		internal bool IsActionAttemptedBeforeMinimumTime { get; set; }

		// Token: 0x06000831 RID: 2097 RVA: 0x00030B80 File Offset: 0x0002ED80
		public override void EnsureArbitrationSucceeeded(CoordinatedRecoveryAction.ResourceAvailabilityStatistics stats)
		{
			ManagedAvailabilityCrimsonEvents.ArbitrationQuotaInfo.Log<string, bool, bool, bool, int, int>(base.GetType().Name, this.IsQuotaComputationConcluded, this.IsQuotaExhausted, this.IsActionAttemptedBeforeMinimumTime, this.TotalQuotaExhaustedAcrossGroup, this.maximumAllowedTotalActionsAcrossGroup);
			base.EnsureArbitrationSucceeeded(stats);
			if (!this.IsQuotaComputationConcluded || this.IsQuotaExhausted || this.IsActionAttemptedBeforeMinimumTime)
			{
				throw new ArbitrationQuotaCalculationFailedException(this.TotalQuotaExhaustedAcrossGroup, this.maximumAllowedTotalActionsAcrossGroup, this.IsQuotaComputationConcluded, this.IsActionAttemptedBeforeMinimumTime);
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00030BFD File Offset: 0x0002EDFD
		internal string GetQuotaStatusAsString()
		{
			return string.Format("QuotaStatus: (TotalQuotaExhaustedAcrossGroup = {0}, MaximumAllowedTotalActionsAcrossGroup = {1})", this.TotalQuotaExhaustedAcrossGroup, this.maximumAllowedTotalActionsAcrossGroup);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00030C20 File Offset: 0x0002EE20
		protected void ProcessQuotaInfo(RecoveryActionHelper.RecoveryActionQuotaInfo quotaInfo, out bool isArbitrating)
		{
			isArbitrating = false;
			lock (this.locker)
			{
				if (!this.IsArbitrationCompleted())
				{
					DateTime dateTime = DateTime.UtcNow.ToLocalTime();
					int num;
					if (quotaInfo != null)
					{
						num = quotaInfo.MaxAllowedQuota - quotaInfo.RemainingQuota;
					}
					else
					{
						num = this.maximumAllowedLocalActionsInTheDuration;
					}
					this.TotalQuotaExhaustedAcrossGroup += num;
					this.MaximumPossibleQuotaFromRemainingServers -= this.maximumAllowedLocalActionsInTheDuration;
					if (this.TotalQuotaExhaustedAcrossGroup >= this.maximumAllowedTotalActionsAcrossGroup)
					{
						this.IsQuotaExhausted = true;
						this.IsQuotaComputationConcluded = true;
					}
					else
					{
						int num2 = this.maximumAllowedTotalActionsAcrossGroup - this.TotalQuotaExhaustedAcrossGroup;
						if (this.MaximumPossibleQuotaFromRemainingServers < num2)
						{
							this.IsQuotaExhausted = false;
							this.IsQuotaComputationConcluded = true;
						}
					}
					if (quotaInfo != null)
					{
						if (this.minimumDurationBetweenActionsAcrossGroup > TimeSpan.Zero && quotaInfo.LastSucceededEntry != null && quotaInfo.LastSucceededEntry.EndTime > dateTime - this.minimumDurationBetweenActionsAcrossGroup)
						{
							this.IsActionAttemptedBeforeMinimumTime = true;
							this.IsQuotaComputationConcluded = true;
						}
						if (quotaInfo.LastStartedEntry != null && quotaInfo.LastStartedEntry.StartTime > quotaInfo.LastSystemBootTime && dateTime < quotaInfo.LastStartedEntry.EndTime && (quotaInfo.LastSucceededEntry == null || quotaInfo.LastStartedEntry.StartTime > quotaInfo.LastSucceededEntry.StartTime))
						{
							isArbitrating = true;
						}
					}
				}
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00030DB0 File Offset: 0x0002EFB0
		protected override bool IsArbitrationCompleted()
		{
			return base.IsArbitrationCompleted() && this.IsQuotaComputationConcluded;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00030DC8 File Offset: 0x0002EFC8
		protected override ResourceAvailabilityStatus RunCheck(string serverName, out string debugInfo)
		{
			debugInfo = string.Empty;
			DateTime dateTime = DateTime.UtcNow.ToLocalTime();
			DateTime queryStartTime = dateTime - this.durationToCheck;
			RecoveryActionHelper.RecoveryActionQuotaInfo recoveryActionQuotaInfo = null;
			bool flag = false;
			try
			{
				recoveryActionQuotaInfo = RpcGetRecoveryActionQuotaInfoImpl.SendRequest(serverName, base.ActionId, this.resourceName, this.maximumAllowedLocalActionsInTheDuration, queryStartTime, dateTime, 30000);
				this.ProcessQuotaInfo(recoveryActionQuotaInfo, out flag);
			}
			catch (Exception)
			{
				this.ProcessQuotaInfo(null, out flag);
				throw;
			}
			ResourceAvailabilityStatus result;
			if (flag)
			{
				result = ResourceAvailabilityStatus.Arbitrating;
			}
			else
			{
				result = ResourceAvailabilityStatus.Ready;
			}
			debugInfo = string.Format("[ActionId={0}, MaxAllowedQuota={1}, RemainingQuota={2}, IsTimedout={3}, LastOperationStartTime={4}, LastOperationEndTime={5}]", new object[]
			{
				recoveryActionQuotaInfo.ActionId,
				recoveryActionQuotaInfo.MaxAllowedQuota,
				recoveryActionQuotaInfo.RemainingQuota,
				recoveryActionQuotaInfo.IsTimedout,
				(recoveryActionQuotaInfo.LastStartedEntry != null) ? recoveryActionQuotaInfo.LastStartedEntry.StartTime : DateTime.MinValue,
				(recoveryActionQuotaInfo.LastSucceededEntry != null) ? recoveryActionQuotaInfo.LastSucceededEntry.EndTime : DateTime.MinValue
			});
			return result;
		}

		// Token: 0x04000589 RID: 1417
		private readonly string resourceName;

		// Token: 0x0400058A RID: 1418
		private readonly TimeSpan durationToCheck;

		// Token: 0x0400058B RID: 1419
		private readonly int maximumAllowedLocalActionsInTheDuration;

		// Token: 0x0400058C RID: 1420
		private readonly TimeSpan minimumDurationBetweenActionsAcrossGroup;

		// Token: 0x0400058D RID: 1421
		private readonly int maximumAllowedTotalActionsAcrossGroup;

		// Token: 0x0400058E RID: 1422
		private object locker = new object();
	}
}
