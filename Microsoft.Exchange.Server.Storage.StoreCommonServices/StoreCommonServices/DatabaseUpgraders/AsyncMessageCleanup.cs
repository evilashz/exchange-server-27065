using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders
{
	// Token: 0x0200004A RID: 74
	public sealed class AsyncMessageCleanup : SchemaUpgrader
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x000178C9 File Offset: 0x00015AC9
		public static bool IsReady(Context context, StoreDatabase database)
		{
			return AsyncMessageCleanup.Instance.TestVersionIsReady(context, database);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000178D7 File Offset: 0x00015AD7
		private AsyncMessageCleanup() : base(new ComponentVersion(0, 123), new ComponentVersion(0, 124))
		{
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000178EF File Offset: 0x00015AEF
		public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000178F1 File Offset: 0x00015AF1
		public override void PerformUpgrade(Context context, ISchemaVersion container)
		{
		}

		// Token: 0x04000281 RID: 641
		public static AsyncMessageCleanup Instance = new AsyncMessageCleanup();
	}
}
