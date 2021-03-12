using System;
using System.Linq;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000288 RID: 648
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CatalogAutoReseedWorkflow : AutoReseedWorkflow
	{
		// Token: 0x0600192B RID: 6443 RVA: 0x00067C1D File Offset: 0x00065E1D
		public CatalogAutoReseedWorkflow(AutoReseedContext context, CatalogAutoReseedWorkflow.CatalogAutoReseedReason catalogReseedReason, string workflowLaunchReason) : base(AutoReseedWorkflowType.CatalogAutoReseed, workflowLaunchReason, context)
		{
			this.catalogReseedReason = catalogReseedReason;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00067C2F File Offset: 0x00065E2F
		public static IDisposable SetTestHook(Func<CatalogAutoReseedWorkflow, Exception> reseedAction)
		{
			return CatalogAutoReseedWorkflow.hookableReseedAction.SetTestHook(reseedAction);
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00067C3C File Offset: 0x00065E3C
		protected override TimeSpan GetThrottlingInterval(AutoReseedWorkflowState state)
		{
			return TimeSpan.FromSeconds((double)RegistryParameters.AutoReseedCiThrottlingIntervalInSecs);
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x00067C49 File Offset: 0x00065E49
		internal string SourceName
		{
			get
			{
				return this.sourceName;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x00067C51 File Offset: 0x00065E51
		internal CatalogAutoReseedWorkflow.CatalogAutoReseedReason ReseedReason
		{
			get
			{
				return this.catalogReseedReason;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x00067C5C File Offset: 0x00065E5C
		protected override bool IsDisabled
		{
			get
			{
				switch (this.catalogReseedReason)
				{
				case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.None:
				case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.CatalogCorruptionWhenFeedingStarts:
				case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.CatalogCorruptionWhenFeedingCompletes:
				case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.EventsMissingWithNotificationsWatermark:
				case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.CrawlOnNonPreferredActiveWithNotificationsWatermark:
				case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.CrawlOnNonPreferredActiveWithTooManyNotificationEvents:
				case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.CrawlOnPassive:
				case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.Unknown:
					return RegistryParameters.AutoReseedCiFailedSuspendedDisabled;
				case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.BehindBacklog:
				case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.BehindRetry:
					return RegistryParameters.AutoReseedCiBehindDisabled;
				case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.Upgrade:
					return RegistryParameters.AutoReseedCiUpgradeDisabled;
				default:
					throw new ArgumentOutOfRangeException("catalogReseedReason");
				}
			}
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00067CDC File Offset: 0x00065EDC
		protected override LocalizedString RunPrereqs(AutoReseedWorkflowState state)
		{
			LocalizedString result = base.RunPrereqs(state);
			if (!result.IsEmpty)
			{
				return result;
			}
			int num = base.Context.CopyStatusesForTargetDatabase.Count((CopyStatusClientCachedEntry status) => status.Result == CopyStatusRpcResult.Success && status.CopyStatus.ContentIndexStatus == ContentIndexStatusType.Healthy);
			if (num == 0)
			{
				AutoReseedWorkflow.Tracer.TraceDebug<string, Guid, string>((long)this.GetHashCode(), "CatalogAutoReseedWorkflow detected all catalogs failed for database '{0}' [{1}]: {2}.", base.Context.Database.Name, base.Context.Database.Guid, base.Context.TargetCopyStatus.CopyStatus.ContentIndexErrorMessage);
				ReplayCrimsonEvents.AutoReseedWorkflowAllCatalogFailed.Log<string, Guid, string, string>(base.Context.Database.Name, base.Context.Database.Guid, base.WorkflowName, base.Context.TargetCopyStatus.CopyStatus.ContentIndexErrorMessage);
				return ReplayStrings.AutoReseedAllCatalogFailed(base.Context.Database.Name);
			}
			if (num == 1)
			{
				AutoReseedWorkflow.Tracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "CatalogAutoReseedWorkflow detected only one catalog copy is healthy for database '{0}' [{1}].", base.Context.Database.Name, base.Context.Database.Guid);
				ReplayCrimsonEvents.AutoReseedWorkflowSingleCatalogHealthy.Log<string, Guid, string, string>(base.Context.Database.Name, base.Context.Database.Guid, base.WorkflowName, base.Context.TargetCopyStatus.CopyStatus.ContentIndexErrorMessage);
			}
			int num2;
			if (!base.Context.ReseedLimiter.TryStartCiSeed(out num2))
			{
				base.TraceError("CatalogAutoReseedWorkflow is being skipped for now because maximum number of concurrent seeds has been reached: {0}", new object[]
				{
					num2
				});
				return ReplayStrings.AutoReseedTooManyConcurrentSeeds(num2);
			}
			return LocalizedString.Empty;
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00067E94 File Offset: 0x00066094
		protected override Exception ExecuteInternal(AutoReseedWorkflowState state)
		{
			int num = int.MaxValue;
			bool skipBehindCatalog = false;
			if (this.catalogReseedReason == CatalogAutoReseedWorkflow.CatalogAutoReseedReason.BehindBacklog || this.catalogReseedReason == CatalogAutoReseedWorkflow.CatalogAutoReseedReason.BehindRetry)
			{
				num = this.WeighCiCopyStatus(base.Context.TargetCopyStatus, false);
				skipBehindCatalog = true;
			}
			foreach (CopyStatusClientCachedEntry copyStatusClientCachedEntry in base.Context.CopyStatusesForTargetDatabase)
			{
				if (!copyStatusClientCachedEntry.ServerContacted.Equals(base.Context.TargetServerName) && copyStatusClientCachedEntry.Result == CopyStatusRpcResult.Success && copyStatusClientCachedEntry.CopyStatus.ContentIndexStatus == ContentIndexStatusType.Healthy && (copyStatusClientCachedEntry.CopyStatus.CopyStatus == CopyStatusEnum.Mounted || copyStatusClientCachedEntry.CopyStatus.CopyStatus == CopyStatusEnum.Healthy || copyStatusClientCachedEntry.CopyStatus.CopyStatus == CopyStatusEnum.DisconnectedAndHealthy))
				{
					if (this.catalogReseedReason == CatalogAutoReseedWorkflow.CatalogAutoReseedReason.Upgrade)
					{
						if (copyStatusClientCachedEntry.CopyStatus.ContentIndexVersion == null)
						{
							continue;
						}
						int value = copyStatusClientCachedEntry.CopyStatus.ContentIndexVersion.Value;
						VersionInfo latest = VersionInfo.Latest;
						if (value != latest.QueryVersion)
						{
							continue;
						}
					}
					int num2 = this.WeighCiCopyStatus(copyStatusClientCachedEntry, skipBehindCatalog);
					if (num2 < num)
					{
						this.sourceName = copyStatusClientCachedEntry.ServerContacted.Fqdn;
						num = num2;
					}
				}
			}
			AutoReseedWorkflow.Tracer.TraceDebug<string, string, AmServerName>((long)this.GetHashCode(), "CatalogAutoReseedWorkflow: Selected '{0}' as source server for content index of database copy '{1}\\{2}'.", this.sourceName, base.Context.Database.Name, base.Context.TargetServerName);
			if (string.IsNullOrEmpty(this.sourceName))
			{
				return new AutoReseedCatalogSourceException(base.Context.Database.Name, base.Context.TargetServerName.NetbiosName);
			}
			if (base.Context.TargetCopyStatus.IsActive)
			{
				AutoReseedWorkflow.Tracer.TraceDebug<string, AmServerName>((long)this.GetHashCode(), "CatalogAutoReseedWorkflow: Database copy '{0}\\{1}' is active. Fail over.", base.Context.Database.Name, base.Context.TargetServerName);
				new DatabaseFailureItem(FailureNameSpace.ContentIndex, FailureTag.CatalogReseed, base.Context.Database.Guid)
				{
					InstanceName = base.Context.Database.Name
				}.Publish();
				return new AutoReseedCatalogActiveException(base.Context.Database.Name, base.Context.TargetServerName.NetbiosName);
			}
			return CatalogAutoReseedWorkflow.hookableReseedAction.Value(this);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00068100 File Offset: 0x00066300
		private static Exception ReseedCatalog(CatalogAutoReseedWorkflow workflow)
		{
			Exception result = null;
			using (SeederClient seederClient = SeederClient.Create(workflow.Context.TargetServerName.Fqdn, workflow.Context.Database.Name, null, workflow.Context.TargetServer.AdminDisplayVersion))
			{
				bool flag = false;
				for (int i = 0; i <= 1; i++)
				{
					result = null;
					try
					{
						if (flag)
						{
							seederClient.EndDbSeed(workflow.Context.Database.Guid);
						}
						AutoReseedWorkflow.Tracer.TraceDebug((long)workflow.GetHashCode(), "CatalogAutoReseedWorkflow: Attempt({0}) to reseed catalog for database copy '{1}\\{2}' from {3}.", new object[]
						{
							i,
							workflow.Context.Database.Name,
							workflow.Context.TargetServerName,
							string.IsNullOrEmpty(workflow.sourceName) ? "Active" : workflow.sourceName
						});
						SeederRpcFlags reseedRPCReason = CatalogAutoReseedWorkflow.GetReseedRPCReason(workflow.catalogReseedReason);
						seederClient.PrepareDbSeedAndBegin(workflow.Context.Database.Guid, false, false, false, false, false, true, string.Empty, null, workflow.sourceName, null, null, reseedRPCReason);
						break;
					}
					catch (SeederInstanceAlreadyInProgressException ex)
					{
						result = ex;
						break;
					}
					catch (SeederInstanceAlreadyFailedException ex2)
					{
						result = ex2;
						flag = true;
					}
					catch (SeederServerException ex3)
					{
						result = ex3;
					}
					catch (SeederServerTransientException ex4)
					{
						result = ex4;
					}
					if (!string.IsNullOrEmpty(workflow.sourceName))
					{
						workflow.sourceName = string.Empty;
					}
				}
			}
			return result;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x000682EC File Offset: 0x000664EC
		private static SeederRpcFlags GetReseedRPCReason(CatalogAutoReseedWorkflow.CatalogAutoReseedReason reason)
		{
			switch (reason)
			{
			case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.BehindBacklog:
				return SeederRpcFlags.CIAutoReseedReasonBehindBacklog;
			case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.BehindRetry:
				return SeederRpcFlags.CIAutoReseedReasonBehindRetry;
			case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.Upgrade:
				return SeederRpcFlags.CIAutoReseedReasonUpgrade;
			case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.CatalogCorruptionWhenFeedingStarts:
				return SeederRpcFlags.CatalogCorruptionWhenFeedingStarts;
			case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.CatalogCorruptionWhenFeedingCompletes:
				return SeederRpcFlags.CatalogCorruptionWhenFeedingCompletes;
			case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.EventsMissingWithNotificationsWatermark:
				return SeederRpcFlags.EventsMissingWithNotificationsWatermark;
			case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.CrawlOnNonPreferredActiveWithNotificationsWatermark:
				return SeederRpcFlags.CrawlOnNonPreferredActiveWithNotificationsWatermark;
			case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.CrawlOnNonPreferredActiveWithTooManyNotificationEvents:
				return SeederRpcFlags.CrawlOnNonPreferredActiveWithTooManyNotificationEvents;
			case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.CrawlOnPassive:
				return SeederRpcFlags.CrawlOnPassive;
			case CatalogAutoReseedWorkflow.CatalogAutoReseedReason.Unknown:
				return SeederRpcFlags.Unknown;
			default:
				return SeederRpcFlags.None;
			}
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0006835C File Offset: 0x0006655C
		private int WeighCiCopyStatus(CopyStatusClientCachedEntry entry, bool skipBehindCatalog)
		{
			if (entry.CopyStatus.ContentIndexBacklog == null || entry.CopyStatus.ContentIndexRetryQueueSize == null)
			{
				return int.MaxValue;
			}
			if (skipBehindCatalog && (entry.CopyStatus.ContentIndexBacklog.Value > RegistryParameters.AutoReseedCiBehindBacklog || entry.CopyStatus.ContentIndexRetryQueueSize.Value > RegistryParameters.AutoReseedCiBehindRetryCount))
			{
				return int.MaxValue;
			}
			return entry.CopyStatus.ContentIndexBacklog.Value * 100 + entry.CopyStatus.ContentIndexRetryQueueSize.Value;
		}

		// Token: 0x04000A19 RID: 2585
		private readonly CatalogAutoReseedWorkflow.CatalogAutoReseedReason catalogReseedReason;

		// Token: 0x04000A1A RID: 2586
		private string sourceName;

		// Token: 0x04000A1B RID: 2587
		private static readonly Hookable<Func<CatalogAutoReseedWorkflow, Exception>> hookableReseedAction = Hookable<Func<CatalogAutoReseedWorkflow, Exception>>.Create(true, new Func<CatalogAutoReseedWorkflow, Exception>(CatalogAutoReseedWorkflow.ReseedCatalog));

		// Token: 0x02000289 RID: 649
		internal enum CatalogAutoReseedReason
		{
			// Token: 0x04000A1E RID: 2590
			None,
			// Token: 0x04000A1F RID: 2591
			BehindBacklog,
			// Token: 0x04000A20 RID: 2592
			BehindRetry,
			// Token: 0x04000A21 RID: 2593
			Upgrade,
			// Token: 0x04000A22 RID: 2594
			CatalogCorruptionWhenFeedingStarts,
			// Token: 0x04000A23 RID: 2595
			CatalogCorruptionWhenFeedingCompletes,
			// Token: 0x04000A24 RID: 2596
			EventsMissingWithNotificationsWatermark,
			// Token: 0x04000A25 RID: 2597
			CrawlOnNonPreferredActiveWithNotificationsWatermark,
			// Token: 0x04000A26 RID: 2598
			CrawlOnNonPreferredActiveWithTooManyNotificationEvents,
			// Token: 0x04000A27 RID: 2599
			CrawlOnPassive,
			// Token: 0x04000A28 RID: 2600
			Unknown
		}
	}
}
