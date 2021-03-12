using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Search.Core;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Probes
{
	// Token: 0x02000462 RID: 1122
	public abstract class SearchProbeBase : ProbeWorkItem
	{
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x000A6D9B File Offset: 0x000A4F9B
		// (set) Token: 0x06001C76 RID: 7286 RVA: 0x000A6DA3 File Offset: 0x000A4FA3
		private protected AttributeHelper AttributeHelper { protected get; private set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x000A6DAC File Offset: 0x000A4FAC
		protected virtual bool SkipOnNonHealthyCatalog
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x000A6DAF File Offset: 0x000A4FAF
		protected virtual bool SkipOnNonActiveDatabase
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x000A6DB2 File Offset: 0x000A4FB2
		protected virtual bool SkipOnDisabledCatalog
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x000A6DB5 File Offset: 0x000A4FB5
		protected virtual bool SkipOnAutoDagExcludeFromMonitoring
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x000A6DB8 File Offset: 0x000A4FB8
		// (set) Token: 0x06001C7C RID: 7292 RVA: 0x000A6DC0 File Offset: 0x000A4FC0
		private protected bool IsFromInvokeMonitoringItem { protected get; private set; }

		// Token: 0x06001C7D RID: 7293
		protected abstract void InternalDoWork(CancellationToken cancellationToken);

		// Token: 0x06001C7E RID: 7294 RVA: 0x000A6DCC File Offset: 0x000A4FCC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				this.AttributeHelper = new AttributeHelper(base.Definition);
				this.IsFromInvokeMonitoringItem = this.AttributeHelper.GetBool("FromInvokeMonitoringItem", false, false);
				if (!this.IsFromInvokeMonitoringItem)
				{
					string targetResource = base.Definition.TargetResource;
					if ((this.SkipOnDisabledCatalog || this.SkipOnNonHealthyCatalog) && SearchMonitoringHelper.IsCatalogDisabled(targetResource))
					{
						base.Result.StateAttribute23 = "Skipped";
						base.Result.StateAttribute24 = "CatalogDisabled";
						return;
					}
					if (this.SkipOnNonHealthyCatalog)
					{
						IndexStatus indexStatus = null;
						try
						{
							indexStatus = SearchMonitoringHelper.GetCachedLocalDatabaseIndexStatus(targetResource, true);
						}
						catch (IndexStatusException ex)
						{
							base.Result.StateAttribute23 = "Skipped";
							base.Result.StateAttribute24 = "CatalogNotHealthy";
							base.Result.StateAttribute25 = ex.GetType().Name;
							return;
						}
						if (indexStatus == null)
						{
							base.Result.StateAttribute23 = "Skipped";
							base.Result.StateAttribute24 = "CatalogNotHealthy";
							base.Result.StateAttribute25 = "IndexStatusNull";
							return;
						}
						if (indexStatus.IndexingState != ContentIndexStatusType.Healthy && indexStatus.IndexingState != ContentIndexStatusType.HealthyAndUpgrading)
						{
							base.Result.StateAttribute23 = "Skipped";
							base.Result.StateAttribute24 = "CatalogNotHealthy";
							base.Result.StateAttribute25 = indexStatus.IndexingState.ToString();
							return;
						}
					}
					if (this.SkipOnNonActiveDatabase)
					{
						if (!SearchMonitoringHelper.IsDatabaseActive(targetResource))
						{
							base.Result.StateAttribute23 = "Skipped";
							base.Result.StateAttribute24 = "CatalogNotActive";
							return;
						}
						CopyStatusClientCachedEntry cachedLocalDatabaseCopyStatus = SearchMonitoringHelper.GetCachedLocalDatabaseCopyStatus(targetResource);
						if (cachedLocalDatabaseCopyStatus == null || cachedLocalDatabaseCopyStatus.CopyStatus == null)
						{
							base.Result.StateAttribute23 = "Skipped";
							base.Result.StateAttribute24 = "CopyStatusNull";
							return;
						}
						if (cachedLocalDatabaseCopyStatus.CopyStatus.CopyStatus != CopyStatusEnum.Mounted)
						{
							base.Result.StateAttribute23 = "Skipped";
							base.Result.StateAttribute24 = "DatabaseActiveButNotMounted";
							return;
						}
					}
					if (this.SkipOnAutoDagExcludeFromMonitoring)
					{
						Guid mailboxDatabaseGuid = SearchMonitoringHelper.GetDatabaseInfo(targetResource).MailboxDatabaseGuid;
						if (CachedAdReader.Instance.GetDatabaseOnLocalServer(mailboxDatabaseGuid).AutoDagExcludeFromMonitoring)
						{
							base.Result.StateAttribute23 = "Skipped";
							base.Result.StateAttribute24 = "AutoDagExcludeFromMonitoring";
							return;
						}
					}
				}
				this.InternalDoWork(cancellationToken);
			}
			catch (Exception ex2)
			{
				string text = ex2.ToString();
				if (ex2 is SearchProbeFailureException || this.IsFromInvokeMonitoringItem)
				{
					string @string = this.AttributeHelper.GetString("OverrideString", false, null);
					if (string.IsNullOrEmpty(@string) || (!Regex.IsMatch(text, @string, RegexOptions.IgnoreCase) && text.IndexOf(@string, StringComparison.OrdinalIgnoreCase) < 0))
					{
						throw;
					}
					base.Result.StateAttribute23 = "Override";
					base.Result.StateAttribute24 = text;
					base.Result.StateAttribute25 = @string;
					SearchMonitoringHelper.LogInfo(this, "Failed probe is overriden by: '{0}', {1}", new object[]
					{
						@string,
						text
					});
				}
				else
				{
					base.Result.StateAttribute23 = "SkippedDueToFailure";
					base.Result.StateAttribute24 = text;
					SearchMonitoringHelper.LogInfo(this, "Unexpected probe failure: '{0}'", new object[]
					{
						ex2
					});
				}
			}
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x000A7148 File Offset: 0x000A5348
		public override void PopulateDefinition<TDefinition>(TDefinition definition, Dictionary<string, string> propertyBag)
		{
		}
	}
}
