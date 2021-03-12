using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders
{
	// Token: 0x0200004D RID: 77
	public sealed class FixReceiveFolderPK : SchemaUpgrader
	{
		// Token: 0x06000308 RID: 776 RVA: 0x00017DA4 File Offset: 0x00015FA4
		public static bool IsReady(Context context, StoreDatabase database)
		{
			return FixReceiveFolderPK.Instance.TestVersionIsReady(context, database);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00017DB2 File Offset: 0x00015FB2
		public static void InitializeUpgraderAction(Action<Context, StoreDatabase> upgraderAction, Action<Context, StoreDatabase> inMemorySchemaInitializationAction)
		{
			FixReceiveFolderPK.upgraderAction = upgraderAction;
			FixReceiveFolderPK.inMemorySchemaInitializationAction = inMemorySchemaInitializationAction;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00017DC0 File Offset: 0x00015FC0
		private FixReceiveFolderPK() : base(new ComponentVersion(0, 10000), new ComponentVersion(0, 10001))
		{
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00017DDE File Offset: 0x00015FDE
		public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
			if (FixReceiveFolderPK.inMemorySchemaInitializationAction != null)
			{
				FixReceiveFolderPK.inMemorySchemaInitializationAction(context, database);
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00017DF3 File Offset: 0x00015FF3
		public override void PerformUpgrade(Context context, ISchemaVersion container)
		{
			if (FixReceiveFolderPK.upgraderAction != null)
			{
				FixReceiveFolderPK.upgraderAction(context, (StoreDatabase)container);
			}
		}

		// Token: 0x04000284 RID: 644
		public static FixReceiveFolderPK Instance = new FixReceiveFolderPK();

		// Token: 0x04000285 RID: 645
		private static Action<Context, StoreDatabase> upgraderAction;

		// Token: 0x04000286 RID: 646
		private static Action<Context, StoreDatabase> inMemorySchemaInitializationAction;
	}
}
