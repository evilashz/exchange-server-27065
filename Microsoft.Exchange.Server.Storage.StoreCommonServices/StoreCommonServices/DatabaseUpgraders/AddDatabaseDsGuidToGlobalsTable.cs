using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders
{
	// Token: 0x02000044 RID: 68
	public sealed class AddDatabaseDsGuidToGlobalsTable : SchemaUpgrader
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x00017449 File Offset: 0x00015649
		public static bool IsReady(Context context, StoreDatabase database)
		{
			return AddDatabaseDsGuidToGlobalsTable.Instance.TestVersionIsReady(context, database);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00017457 File Offset: 0x00015657
		private AddDatabaseDsGuidToGlobalsTable() : base(new ComponentVersion(0, 129), new ComponentVersion(0, 130))
		{
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00017478 File Offset: 0x00015678
		public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
			GlobalsTable globalsTable = DatabaseSchema.GlobalsTable(database);
			globalsTable.DatabaseDsGuid.MinVersion = base.To.Value;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000174A8 File Offset: 0x000156A8
		public override void PerformUpgrade(Context context, ISchemaVersion container)
		{
			StoreDatabase storeDatabase = container as StoreDatabase;
			if (storeDatabase.PhysicalDatabase.DatabaseType == DatabaseType.Jet)
			{
				GlobalsTable globalsTable = DatabaseSchema.GlobalsTable(storeDatabase);
				globalsTable.Table.AddColumn(context, globalsTable.DatabaseDsGuid);
				Column[] columnsToUpdate = new Column[]
				{
					globalsTable.DatabaseDsGuid
				};
				List<object> list = new List<object>(1);
				list.Add(storeDatabase.MdbGuid);
				using (UpdateOperator updateOperator = Factory.CreateUpdateOperator(CultureHelper.DefaultCultureInfo, context, Factory.CreateTableOperator(CultureHelper.DefaultCultureInfo, context, globalsTable.Table, globalsTable.Table.PrimaryKeyIndex, null, null, null, 0, 1, KeyRange.AllRows, false, true), columnsToUpdate, list, true))
				{
					int num = (int)updateOperator.ExecuteScalar();
				}
			}
		}

		// Token: 0x04000279 RID: 633
		public static AddDatabaseDsGuidToGlobalsTable Instance = new AddDatabaseDsGuidToGlobalsTable();
	}
}
