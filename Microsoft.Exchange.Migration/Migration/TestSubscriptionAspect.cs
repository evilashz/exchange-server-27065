using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.DataAccessLayer;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200017F RID: 383
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class TestSubscriptionAspect
	{
		// Token: 0x060011D9 RID: 4569 RVA: 0x0004B130 File Offset: 0x00049330
		internal TestSubscriptionAspect(MigrationEndpointBase endpoint, MigrationJobItem jobItem, bool update)
		{
			this.Identifier = jobItem.Identifier;
			MRSSubscriptionId mrssubscriptionId = (MRSSubscriptionId)jobItem.SubscriptionId;
			if (mrssubscriptionId != null)
			{
				this.SubscriptionId = mrssubscriptionId.Id;
			}
			if (jobItem.MigrationType == MigrationType.ExchangeOutlookAnywhere)
			{
				ExchangeOutlookAnywhereEndpoint exchangeOutlookAnywhereEndpoint = (ExchangeOutlookAnywhereEndpoint)endpoint;
				ExchangeJobItemSubscriptionSettings settings = MRSMergeRequestAccessor.GetSettings(jobItem);
				this.RemoteServer = exchangeOutlookAnywhereEndpoint.RpcProxyServer;
				this.RemoteServerDN = settings.ExchangeServerDN;
				this.RemoteMailboxDN = settings.MailboxDN;
				this.UserName = exchangeOutlookAnywhereEndpoint.Username;
				this.HasAdminPrivilege = true;
				return;
			}
			if (jobItem.MigrationType == MigrationType.ExchangeRemoteMove || jobItem.MigrationType == MigrationType.ExchangeLocalMove)
			{
				ExchangeRemoteMoveEndpoint exchangeRemoteMoveEndpoint = (ExchangeRemoteMoveEndpoint)endpoint;
				MoveJobSubscriptionSettings moveJobSubscriptionSettings = (MoveJobSubscriptionSettings)jobItem.MigrationJob.SubscriptionSettings;
				if (exchangeRemoteMoveEndpoint != null)
				{
					this.RemoteServer = exchangeRemoteMoveEndpoint.RemoteServer;
					this.UserName = exchangeRemoteMoveEndpoint.Username;
				}
				Unlimited<int>? unlimited;
				Unlimited<int>? unlimited2;
				string targetDatabase;
				string targetArchiveDatabase;
				bool flag;
				bool flag2;
				MrsMoveRequestAccessor.RetrieveDuplicatedSettings(moveJobSubscriptionSettings, (MoveJobItemSubscriptionSettings)jobItem.SubscriptionSettings, !update, out unlimited, out unlimited2, out targetDatabase, out targetArchiveDatabase, out flag, out flag2);
				this.BadItemLimit = ((unlimited == null) ? string.Empty : unlimited.ToString());
				this.LargeItemLimit = ((unlimited2 == null) ? string.Empty : unlimited2.ToString());
				this.StartAfter = (DateTime?)moveJobSubscriptionSettings.StartAfter;
				this.CompleteAfter = (DateTime?)moveJobSubscriptionSettings.CompleteAfter;
				if (!update)
				{
					this.TargetDatabase = targetDatabase;
					this.TargetArchiveDatabase = targetArchiveDatabase;
					return;
				}
			}
			else if (jobItem.MigrationType == MigrationType.PSTImport)
			{
				PSTImportEndpoint pstimportEndpoint = (PSTImportEndpoint)endpoint;
				PSTJobSubscriptionSettings jobSettings = (PSTJobSubscriptionSettings)jobItem.MigrationJob.SubscriptionSettings;
				if (pstimportEndpoint != null)
				{
					this.RemoteServer = pstimportEndpoint.RemoteServer;
					this.UserName = pstimportEndpoint.Username;
				}
				Unlimited<int>? unlimited3;
				Unlimited<int>? unlimited4;
				bool flag3;
				bool flag4;
				PSTImportAccessor.RetrieveDuplicatedSettings(jobSettings, (PSTJobItemSubscriptionSettings)jobItem.SubscriptionSettings, !update, out unlimited3, out unlimited4, out flag3, out flag4);
				this.BadItemLimit = ((unlimited3 == null) ? string.Empty : unlimited3.ToString());
				this.LargeItemLimit = ((unlimited4 == null) ? string.Empty : unlimited4.ToString());
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x0004B35F File Offset: 0x0004955F
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x0004B367 File Offset: 0x00049567
		public string RemoteServer { get; private set; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0004B370 File Offset: 0x00049570
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x0004B378 File Offset: 0x00049578
		public string UserName { get; private set; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0004B381 File Offset: 0x00049581
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x0004B389 File Offset: 0x00049589
		public string Identifier { get; private set; }

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0004B392 File Offset: 0x00049592
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x0004B39A File Offset: 0x0004959A
		public string RemoteServerDN { get; private set; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0004B3A3 File Offset: 0x000495A3
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x0004B3AB File Offset: 0x000495AB
		public string RemoteMailboxDN { get; private set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x0004B3B4 File Offset: 0x000495B4
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x0004B3BC File Offset: 0x000495BC
		public TimeSpan TimeoutForFailingSubscription { get; private set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0004B3C5 File Offset: 0x000495C5
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x0004B3CD File Offset: 0x000495CD
		public Guid SubscriptionId { get; private set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0004B3D6 File Offset: 0x000495D6
		// (set) Token: 0x060011E9 RID: 4585 RVA: 0x0004B3DE File Offset: 0x000495DE
		public bool HasAdminPrivilege { get; private set; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0004B3E7 File Offset: 0x000495E7
		// (set) Token: 0x060011EB RID: 4587 RVA: 0x0004B3EF File Offset: 0x000495EF
		public string BadItemLimit { get; private set; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x0004B3F8 File Offset: 0x000495F8
		// (set) Token: 0x060011ED RID: 4589 RVA: 0x0004B400 File Offset: 0x00049600
		public string LargeItemLimit { get; private set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0004B409 File Offset: 0x00049609
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x0004B411 File Offset: 0x00049611
		public string TargetDatabase { get; private set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0004B41A File Offset: 0x0004961A
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x0004B422 File Offset: 0x00049622
		public string TargetArchiveDatabase { get; private set; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0004B42B File Offset: 0x0004962B
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x0004B433 File Offset: 0x00049633
		public DateTime? StartAfter { get; private set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0004B43C File Offset: 0x0004963C
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x0004B444 File Offset: 0x00049644
		public DateTime? CompleteAfter { get; private set; }

		// Token: 0x060011F6 RID: 4598 RVA: 0x0004B450 File Offset: 0x00049650
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: [JobItemIdentifier = {1}, SubscriptionID: {2}]", new object[]
			{
				base.GetType(),
				this.Identifier,
				this.SubscriptionId
			});
		}
	}
}
