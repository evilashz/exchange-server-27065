using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders
{
	// Token: 0x0200004E RID: 78
	public sealed class RemoveFolderIdsetIn : SchemaUpgrader
	{
		// Token: 0x0600030E RID: 782 RVA: 0x00017E19 File Offset: 0x00016019
		public static bool IsReady(Context context, StoreDatabase database)
		{
			return RemoveFolderIdsetIn.Instance.TestVersionIsReady(context, database);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00017E27 File Offset: 0x00016027
		private RemoveFolderIdsetIn() : base(new ComponentVersion(0, 127), new ComponentVersion(0, 128))
		{
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00017E42 File Offset: 0x00016042
		public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00017E44 File Offset: 0x00016044
		public override void PerformUpgrade(Context context, ISchemaVersion container)
		{
		}

		// Token: 0x04000287 RID: 647
		public static RemoveFolderIdsetIn Instance = new RemoveFolderIdsetIn();
	}
}
