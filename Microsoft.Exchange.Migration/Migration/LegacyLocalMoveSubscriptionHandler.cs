using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200015E RID: 350
	internal class LegacyLocalMoveSubscriptionHandler : LegacyMoveSubscriptionHandlerBase
	{
		// Token: 0x06001124 RID: 4388 RVA: 0x00048113 File Offset: 0x00046313
		public LegacyLocalMoveSubscriptionHandler(IMigrationDataProvider dataProvider, MigrationJob migrationJob) : base(dataProvider, migrationJob)
		{
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x0004811D File Offset: 0x0004631D
		public override MigrationType SupportedMigrationType
		{
			get
			{
				return MigrationType.ExchangeLocalMove;
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00048121 File Offset: 0x00046321
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LegacyLocalMoveSubscriptionHandler>(this);
		}
	}
}
