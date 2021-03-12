using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000287 RID: 647
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AutoReseedWorkflowSuppression
	{
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x000677DE File Offset: 0x000659DE
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AutoReseedTracer;
			}
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000677E8 File Offset: 0x000659E8
		public AutoReseedWorkflowSuppression()
		{
			this.m_healthyWorkflowSuppression = new TransientDatabaseErrorSuppression();
			TransientPeriodicDatabaseErrorSuppression value = new TransientPeriodicDatabaseErrorSuppression(TimeSpan.Zero, InvokeWithTimeout.InfiniteTimeSpan, AutoReseedWorkflowSuppression.s_dbFailedSuppresionInterval, AutoReseedWorkflowSuppression.s_dbFailedSuppresionInterval, TransientErrorInfo.ErrorType.Success);
			TransientPeriodicDatabaseErrorSuppression value2 = new TransientPeriodicDatabaseErrorSuppression(TimeSpan.Zero, InvokeWithTimeout.InfiniteTimeSpan, AutoReseedWorkflowSuppression.s_dbReseedSuppresionInterval, AutoReseedWorkflowSuppression.s_dbReseedRetryInterval, TransientErrorInfo.ErrorType.Success);
			TransientPeriodicDatabaseErrorSuppression value3 = new TransientPeriodicDatabaseErrorSuppression(TimeSpan.Zero, InvokeWithTimeout.InfiniteTimeSpan, AutoReseedWorkflowSuppression.s_ciReseedSuppresionInterval, AutoReseedWorkflowSuppression.s_ciReseedRetryInterval, TransientErrorInfo.ErrorType.Success);
			TransientPeriodicDatabaseErrorSuppression value4 = new TransientPeriodicDatabaseErrorSuppression(TimeSpan.Zero, InvokeWithTimeout.InfiniteTimeSpan, AutoReseedWorkflowSuppression.s_ciRebuildSuppresionInterval, AutoReseedWorkflowSuppression.s_ciRebuildRetryInterval, TransientErrorInfo.ErrorType.Success);
			this.m_wfSuppressionTable = new Dictionary<AutoReseedWorkflowType, TransientPeriodicDatabaseErrorSuppression>
			{
				{
					AutoReseedWorkflowType.FailedCopy,
					value
				},
				{
					AutoReseedWorkflowType.FailedSuspendedCopyAutoReseed,
					value2
				},
				{
					AutoReseedWorkflowType.CatalogAutoReseed,
					value3
				},
				{
					AutoReseedWorkflowType.FailedSuspendedCatalogRebuild,
					value4
				}
			};
			this.m_catalogAutoReseedElapsedTime = new Dictionary<Guid, DateTime>();
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x000678B0 File Offset: 0x00065AB0
		public bool ReportWorkflowLaunchConditionMet(AutoReseedWorkflowType workflowType, Guid dbGuid, CatalogAutoReseedWorkflow.CatalogAutoReseedReason reason = CatalogAutoReseedWorkflow.CatalogAutoReseedReason.None, int activationPreference = 1)
		{
			bool flag = false;
			AutoReseedWorkflowSuppression.Tracer.TraceDebug((long)this.GetHashCode(), "AutoReseedWorkflowSuppression.ReportWorkflowLaunchConditionMet: Database '{0}' running workflow {1} with ActivationPreference {2} and reason {3}", new object[]
			{
				dbGuid,
				workflowType,
				activationPreference,
				reason
			});
			foreach (KeyValuePair<AutoReseedWorkflowType, TransientPeriodicDatabaseErrorSuppression> keyValuePair in this.m_wfSuppressionTable)
			{
				if (keyValuePair.Key == workflowType)
				{
					TransientErrorInfo.ErrorType errorType;
					flag = (keyValuePair.Value.ReportFailurePeriodic(dbGuid, out errorType) && errorType == TransientErrorInfo.ErrorType.Failure);
					AutoReseedWorkflowSuppression.Tracer.TraceDebug((long)this.GetHashCode(), "AutoReseedWorkflowSuppression.ReportWorkflowLaunchConditionMet: Database '{0}' is reporting failure on periodic suppression fail table with ErrorType {1} for workflow {2}, shouldLaunch {3}.", new object[]
					{
						dbGuid,
						errorType,
						keyValuePair.Key,
						flag
					});
				}
				else
				{
					TransientErrorInfo.ErrorType errorType;
					keyValuePair.Value.ReportSuccessPeriodic(dbGuid, out errorType);
					AutoReseedWorkflowSuppression.Tracer.TraceDebug<Guid, TransientErrorInfo.ErrorType, AutoReseedWorkflowType>((long)this.GetHashCode(), "AutoReseedWorkflowSuppression.ReportWorkflowLaunchConditionMet: Database '{0}' is resetting periodic suppression fail table with ErrorType {1} for workflow {2}.", dbGuid, errorType, keyValuePair.Key);
				}
			}
			if (flag && workflowType == AutoReseedWorkflowType.CatalogAutoReseed && reason == CatalogAutoReseedWorkflow.CatalogAutoReseedReason.Upgrade)
			{
				DateTime utcNow = DateTime.UtcNow;
				activationPreference = ((activationPreference <= 1) ? 1 : activationPreference);
				TimeSpan timeSpan = TimeSpan.FromSeconds((double)(activationPreference * (int)AutoReseedWorkflowSuppression.s_ciCatalogAutoReseedOnUpgradeInterval.TotalSeconds));
				DateTime value;
				if (this.m_catalogAutoReseedElapsedTime.TryGetValue(dbGuid, out value))
				{
					TimeSpan timeSpan2 = utcNow.Subtract(value);
					if (timeSpan2 < timeSpan)
					{
						flag = false;
						AutoReseedWorkflowSuppression.Tracer.TraceDebug((long)this.GetHashCode(), "AutoReseedWorkflowSuppression.ReportWorkflowLaunchConditionMet: Database '{0}' blocking reseed elapsed time {1} < delay time {2}. ActivationPref is {3}", new object[]
						{
							dbGuid,
							timeSpan2,
							timeSpan,
							activationPreference
						});
					}
					else
					{
						this.m_catalogAutoReseedElapsedTime.Remove(dbGuid);
						AutoReseedWorkflowSuppression.Tracer.TraceDebug<Guid, TimeSpan, int>((long)this.GetHashCode(), "AutoReseedWorkflowSuppression.ReportWorkflowLaunchConditionMet: Database '{0}' allowing reseed at current time as a previous reseed was called and delayed {1}. ActivationPref is {2}", dbGuid, timeSpan, activationPreference);
					}
				}
				else
				{
					this.m_catalogAutoReseedElapsedTime[dbGuid] = utcNow;
					flag = false;
					AutoReseedWorkflowSuppression.Tracer.TraceDebug<Guid, TimeSpan, int>((long)this.GetHashCode(), "AutoReseedWorkflowSuppression.ReportWorkflowLaunchConditionMet: Database '{0}' blocking reseed for {1} as this is the first call. ActivationPref is {2}", dbGuid, timeSpan, activationPreference);
				}
			}
			return flag;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00067B00 File Offset: 0x00065D00
		public void ReportNoWorkflowsNeedToLaunch(Guid dbGuid)
		{
			foreach (TransientPeriodicDatabaseErrorSuppression transientPeriodicDatabaseErrorSuppression in this.m_wfSuppressionTable.Values)
			{
				TransientErrorInfo.ErrorType errorType;
				transientPeriodicDatabaseErrorSuppression.ReportSuccessPeriodic(dbGuid, out errorType);
			}
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00067B5C File Offset: 0x00065D5C
		public bool ReportHealthyWorkflowLaunchConditionMet(Guid dbGuid)
		{
			return this.m_healthyWorkflowSuppression.ReportFailure(dbGuid, AutoReseedWorkflowSuppression.s_dbHealthySuppressionInterval);
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00067B6F File Offset: 0x00065D6F
		public void ReportHealthyWorkflowNotNeeded(Guid dbGuid)
		{
			this.m_healthyWorkflowSuppression.ReportSuccess(dbGuid);
		}

		// Token: 0x04000A0D RID: 2573
		internal static readonly TimeSpan s_dbHealthySuppressionInterval = TimeSpan.FromSeconds((double)RegistryParameters.AutoReseedDbHealthySuppressionInSecs);

		// Token: 0x04000A0E RID: 2574
		internal static readonly TimeSpan s_dbFailedSuppresionInterval = TimeSpan.FromSeconds((double)RegistryParameters.AutoReseedDbFailedPeriodicIntervalInSecs);

		// Token: 0x04000A0F RID: 2575
		internal static readonly TimeSpan s_dbReseedSuppresionInterval = TimeSpan.FromSeconds((double)RegistryParameters.AutoReseedDbFailedSuspendedSuppressionInSecs);

		// Token: 0x04000A10 RID: 2576
		internal static readonly TimeSpan s_dbReseedRetryInterval = TimeSpan.FromSeconds((double)RegistryParameters.AutoReseedDbFailedSuspendedPeriodicIntervalInSecs);

		// Token: 0x04000A11 RID: 2577
		internal static readonly TimeSpan s_ciReseedSuppresionInterval = TimeSpan.FromSeconds((double)RegistryParameters.AutoReseedCiSuppressionInSecs);

		// Token: 0x04000A12 RID: 2578
		internal static readonly TimeSpan s_ciReseedRetryInterval = TimeSpan.FromSeconds((double)RegistryParameters.AutoReseedCiPeriodicIntervalInSecs);

		// Token: 0x04000A13 RID: 2579
		internal static readonly TimeSpan s_ciRebuildSuppresionInterval = TimeSpan.FromSeconds((double)RegistryParameters.AutoReseedCiRebuildFailedSuspendedSuppressionInSecs);

		// Token: 0x04000A14 RID: 2580
		internal static readonly TimeSpan s_ciRebuildRetryInterval = TimeSpan.FromSeconds((double)RegistryParameters.AutoReseedCiRebuildFailedSuspendedPeriodicIntervalInSecs);

		// Token: 0x04000A15 RID: 2581
		internal static readonly TimeSpan s_ciCatalogAutoReseedOnUpgradeInterval = TimeSpan.FromSeconds((double)RegistryParameters.AutoReseedCiCatalogOnUpgradeIntervalInSecs);

		// Token: 0x04000A16 RID: 2582
		private readonly TransientDatabaseErrorSuppression m_healthyWorkflowSuppression;

		// Token: 0x04000A17 RID: 2583
		private readonly Dictionary<AutoReseedWorkflowType, TransientPeriodicDatabaseErrorSuppression> m_wfSuppressionTable;

		// Token: 0x04000A18 RID: 2584
		private Dictionary<Guid, DateTime> m_catalogAutoReseedElapsedTime;
	}
}
