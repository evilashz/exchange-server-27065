using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders
{
	// Token: 0x0200004B RID: 75
	public sealed class EnableAddingSpecialFolders : SchemaUpgrader
	{
		// Token: 0x060002FD RID: 765 RVA: 0x000178FF File Offset: 0x00015AFF
		public static bool IsReady(Context context, StoreDatabase database)
		{
			return EnableAddingSpecialFolders.Instance.TestVersionIsReady(context, database);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0001790D File Offset: 0x00015B0D
		private EnableAddingSpecialFolders() : base(new ComponentVersion(0, 130), new ComponentVersion(0, 131))
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001792B File Offset: 0x00015B2B
		public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0001792D File Offset: 0x00015B2D
		public override void PerformUpgrade(Context context, ISchemaVersion container)
		{
		}

		// Token: 0x04000282 RID: 642
		public static EnableAddingSpecialFolders Instance = new EnableAddingSpecialFolders();
	}
}
