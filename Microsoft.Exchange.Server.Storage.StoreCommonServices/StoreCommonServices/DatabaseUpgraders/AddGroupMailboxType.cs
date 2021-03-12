using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders
{
	// Token: 0x02000046 RID: 70
	public sealed class AddGroupMailboxType : SchemaUpgrader
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x000176F8 File Offset: 0x000158F8
		public static bool IsReady(Context context, StoreDatabase database)
		{
			return AddGroupMailboxType.Instance.TestVersionIsReady(context, database);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00017706 File Offset: 0x00015906
		private AddGroupMailboxType() : base(new ComponentVersion(0, 125), new ComponentVersion(0, 126))
		{
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0001771E File Offset: 0x0001591E
		public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00017720 File Offset: 0x00015920
		public override void PerformUpgrade(Context context, ISchemaVersion container)
		{
		}

		// Token: 0x0400027B RID: 635
		public static AddGroupMailboxType Instance = new AddGroupMailboxType();
	}
}
