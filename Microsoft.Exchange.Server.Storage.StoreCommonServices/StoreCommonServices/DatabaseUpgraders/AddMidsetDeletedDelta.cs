using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders
{
	// Token: 0x02000048 RID: 72
	public sealed class AddMidsetDeletedDelta : SchemaUpgrader
	{
		// Token: 0x060002ED RID: 749 RVA: 0x000177CA File Offset: 0x000159CA
		public static bool IsReady(Context context, StoreDatabase database)
		{
			return AddMidsetDeletedDelta.Instance.TestVersionIsReady(context, database);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000177D8 File Offset: 0x000159D8
		public static void InitializeUpgraderAction(Action<Context> upgraderAction, Action<StoreDatabase> inMemorySchemaInitializationAction)
		{
			AddMidsetDeletedDelta.upgraderAction = upgraderAction;
			AddMidsetDeletedDelta.inMemorySchemaInitializationAction = inMemorySchemaInitializationAction;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000177E6 File Offset: 0x000159E6
		private AddMidsetDeletedDelta() : base(new ComponentVersion(0, 124), new ComponentVersion(0, 125))
		{
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000177FE File Offset: 0x000159FE
		public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
			if (AddMidsetDeletedDelta.inMemorySchemaInitializationAction != null)
			{
				AddMidsetDeletedDelta.inMemorySchemaInitializationAction(database);
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00017812 File Offset: 0x00015A12
		public override void PerformUpgrade(Context context, ISchemaVersion container)
		{
			if (AddMidsetDeletedDelta.upgraderAction != null)
			{
				AddMidsetDeletedDelta.upgraderAction(context);
			}
		}

		// Token: 0x0400027D RID: 637
		public static AddMidsetDeletedDelta Instance = new AddMidsetDeletedDelta();

		// Token: 0x0400027E RID: 638
		private static Action<Context> upgraderAction;

		// Token: 0x0400027F RID: 639
		private static Action<StoreDatabase> inMemorySchemaInitializationAction;
	}
}
