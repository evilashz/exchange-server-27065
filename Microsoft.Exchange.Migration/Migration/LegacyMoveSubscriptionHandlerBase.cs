using System;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200015D RID: 349
	internal abstract class LegacyMoveSubscriptionHandlerBase : LegacyMrsSubscriptionHandlerBase
	{
		// Token: 0x0600111F RID: 4383 RVA: 0x000480EA File Offset: 0x000462EA
		protected LegacyMoveSubscriptionHandlerBase(IMigrationDataProvider dataProvider, MigrationJob migrationJob) : base(dataProvider, migrationJob, MoveSubscriptionArbiter.Instance)
		{
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x000480F9 File Offset: 0x000462F9
		public override bool SupportsAdvancedValidation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x000480FC File Offset: 0x000462FC
		protected override MigrationUserStatus PostTestStatus
		{
			get
			{
				return MigrationUserStatus.Queued;
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x000480FF File Offset: 0x000462FF
		public override bool CreateUnderlyingSubscriptions(MigrationJobItem jobItem)
		{
			return base.InternalCreate(jobItem, false);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00048109 File Offset: 0x00046309
		public override bool TestCreateUnderlyingSubscriptions(MigrationJobItem jobItem)
		{
			return base.InternalCreate(jobItem, true);
		}
	}
}
