using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders
{
	// Token: 0x02000049 RID: 73
	public sealed class AddUpgradeHistoryTable : SchemaUpgrader
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x00017832 File Offset: 0x00015A32
		public static bool IsReady(Context context, StoreDatabase database)
		{
			return AddUpgradeHistoryTable.Instance.TestVersionIsReady(context, database);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00017840 File Offset: 0x00015A40
		private AddUpgradeHistoryTable() : base(new ComponentVersion(0, 122), new ComponentVersion(0, 123))
		{
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00017858 File Offset: 0x00015A58
		public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
			UpgradeHistoryTable upgradeHistoryTable = DatabaseSchema.UpgradeHistoryTable(database);
			upgradeHistoryTable.Table.MinVersion = base.To.Value;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00017888 File Offset: 0x00015A88
		public override void PerformUpgrade(Context context, ISchemaVersion container)
		{
			StoreDatabase database = container as StoreDatabase;
			UpgradeHistoryTable upgradeHistoryTable = DatabaseSchema.UpgradeHistoryTable(database);
			upgradeHistoryTable.Table.CreateTable(context, base.To.Value);
		}

		// Token: 0x04000280 RID: 640
		public static AddUpgradeHistoryTable Instance = new AddUpgradeHistoryTable();
	}
}
