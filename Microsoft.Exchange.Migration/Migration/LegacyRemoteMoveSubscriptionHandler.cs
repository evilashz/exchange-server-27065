using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200015F RID: 351
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LegacyRemoteMoveSubscriptionHandler : LegacyMoveSubscriptionHandlerBase
	{
		// Token: 0x06001127 RID: 4391 RVA: 0x00048129 File Offset: 0x00046329
		public LegacyRemoteMoveSubscriptionHandler(IMigrationDataProvider dataProvider, MigrationJob migrationJob) : base(dataProvider, migrationJob)
		{
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x00048133 File Offset: 0x00046333
		public override MigrationType SupportedMigrationType
		{
			get
			{
				return MigrationType.ExchangeRemoteMove;
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00048137 File Offset: 0x00046337
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LegacyRemoteMoveSubscriptionHandler>(this);
		}
	}
}
