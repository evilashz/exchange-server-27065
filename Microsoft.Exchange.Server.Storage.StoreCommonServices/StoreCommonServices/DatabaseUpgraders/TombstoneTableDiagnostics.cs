using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders
{
	// Token: 0x0200004F RID: 79
	public sealed class TombstoneTableDiagnostics : SchemaUpgrader
	{
		// Token: 0x06000313 RID: 787 RVA: 0x00017E52 File Offset: 0x00016052
		public static bool IsReady(Context context, StoreDatabase database)
		{
			return TombstoneTableDiagnostics.Instance.TestVersionIsReady(context, database);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00017E60 File Offset: 0x00016060
		public static void InitializeUpgraderAction(Action<Context> upgraderAction, Action<StoreDatabase> inMemorySchemaInitializationAction)
		{
			TombstoneTableDiagnostics.upgraderAction = upgraderAction;
			TombstoneTableDiagnostics.inMemorySchemaInitializationAction = inMemorySchemaInitializationAction;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00017E6E File Offset: 0x0001606E
		private TombstoneTableDiagnostics() : base(new ComponentVersion(0, 10000), new ComponentVersion(0, 10001))
		{
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00017E8C File Offset: 0x0001608C
		public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
			if (TombstoneTableDiagnostics.inMemorySchemaInitializationAction != null)
			{
				TombstoneTableDiagnostics.inMemorySchemaInitializationAction(database);
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00017EA0 File Offset: 0x000160A0
		public override void PerformUpgrade(Context context, ISchemaVersion container)
		{
			if (TombstoneTableDiagnostics.upgraderAction != null)
			{
				TombstoneTableDiagnostics.upgraderAction(context);
			}
		}

		// Token: 0x04000288 RID: 648
		public static TombstoneTableDiagnostics Instance = new TombstoneTableDiagnostics();

		// Token: 0x04000289 RID: 649
		private static Action<Context> upgraderAction;

		// Token: 0x0400028A RID: 650
		private static Action<StoreDatabase> inMemorySchemaInitializationAction;
	}
}
