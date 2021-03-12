using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200016A RID: 362
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LegacyPublicFolderSubscriptionHandler : LegacyMrsSubscriptionHandlerBase
	{
		// Token: 0x0600116B RID: 4459 RVA: 0x000497BD File Offset: 0x000479BD
		public LegacyPublicFolderSubscriptionHandler(IMigrationDataProvider dataProvider, MigrationJob migrationJob) : base(dataProvider, migrationJob, MoveSubscriptionArbiter.Instance)
		{
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x000497CC File Offset: 0x000479CC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LegacyPublicFolderSubscriptionHandler>(this);
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x000497D4 File Offset: 0x000479D4
		public override bool SupportsAdvancedValidation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x000497D7 File Offset: 0x000479D7
		public override MigrationType SupportedMigrationType
		{
			get
			{
				return MigrationType.PublicFolder;
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000497DE File Offset: 0x000479DE
		public override bool CreateUnderlyingSubscriptions(MigrationJobItem jobItem)
		{
			base.InternalCreate(jobItem, false);
			return true;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000497EA File Offset: 0x000479EA
		public override bool TestCreateUnderlyingSubscriptions(MigrationJobItem jobItem)
		{
			base.InternalCreate(jobItem, true);
			return true;
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x000497F6 File Offset: 0x000479F6
		protected override MigrationUserStatus PostTestStatus
		{
			get
			{
				return MigrationUserStatus.Queued;
			}
		}
	}
}
