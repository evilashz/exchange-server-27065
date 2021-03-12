using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders
{
	// Token: 0x02000051 RID: 81
	public sealed class UserInfoUpgrader : SchemaUpgrader
	{
		// Token: 0x0600031F RID: 799 RVA: 0x0001801E File Offset: 0x0001621E
		public static bool IsReady(Context context, StoreDatabase database)
		{
			return UserInfoUpgrader.Instance.TestVersionIsReady(context, database);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001802C File Offset: 0x0001622C
		public static void InitializeUpgraderAction(Action<Context> upgraderAction, Action<StoreDatabase> inMemorySchemaInitializationAction)
		{
			UserInfoUpgrader.upgraderAction = upgraderAction;
			UserInfoUpgrader.inMemorySchemaInitializationAction = inMemorySchemaInitializationAction;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001803A File Offset: 0x0001623A
		private UserInfoUpgrader() : base(new ComponentVersion(0, 128), new ComponentVersion(0, 129))
		{
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00018058 File Offset: 0x00016258
		public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
			if (UserInfoUpgrader.inMemorySchemaInitializationAction != null)
			{
				UserInfoUpgrader.inMemorySchemaInitializationAction(database);
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001806C File Offset: 0x0001626C
		public override void PerformUpgrade(Context context, ISchemaVersion container)
		{
			if (UserInfoUpgrader.upgraderAction != null)
			{
				UserInfoUpgrader.upgraderAction(context);
			}
		}

		// Token: 0x0400028E RID: 654
		public static UserInfoUpgrader Instance = new UserInfoUpgrader();

		// Token: 0x0400028F RID: 655
		private static Action<Context> upgraderAction;

		// Token: 0x04000290 RID: 656
		private static Action<StoreDatabase> inMemorySchemaInitializationAction;
	}
}
