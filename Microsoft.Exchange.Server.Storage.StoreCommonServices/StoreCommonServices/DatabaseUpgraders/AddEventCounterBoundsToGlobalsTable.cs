using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders
{
	// Token: 0x02000045 RID: 69
	public sealed class AddEventCounterBoundsToGlobalsTable : SchemaUpgrader
	{
		// Token: 0x060002DE RID: 734 RVA: 0x00017584 File Offset: 0x00015784
		public static bool IsReady(Context context, StoreDatabase database)
		{
			return AddEventCounterBoundsToGlobalsTable.Instance.TestVersionIsReady(context, database);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00017592 File Offset: 0x00015792
		private AddEventCounterBoundsToGlobalsTable() : base(new ComponentVersion(0, 10000), new ComponentVersion(0, 10001))
		{
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000175B0 File Offset: 0x000157B0
		public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
			GlobalsTable globalsTable = DatabaseSchema.GlobalsTable(database);
			globalsTable.EventCounterLowerBound.MinVersion = base.To.Value;
			globalsTable.EventCounterUpperBound.MinVersion = base.To.Value;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000175F8 File Offset: 0x000157F8
		public override void PerformUpgrade(Context context, ISchemaVersion container)
		{
			StoreDatabase storeDatabase = container as StoreDatabase;
			if (storeDatabase.PhysicalDatabase.DatabaseType == DatabaseType.Jet)
			{
				GlobalsTable globalsTable = DatabaseSchema.GlobalsTable(storeDatabase);
				globalsTable.Table.AddColumn(context, globalsTable.EventCounterLowerBound);
				globalsTable.Table.AddColumn(context, globalsTable.EventCounterUpperBound);
				Column[] columnsToUpdate = new Column[]
				{
					globalsTable.EventCounterLowerBound,
					globalsTable.EventCounterUpperBound
				};
				object[] valuesToUpdate = new object[]
				{
					0L,
					1L
				};
				using (UpdateOperator updateOperator = Factory.CreateUpdateOperator(CultureHelper.DefaultCultureInfo, context, Factory.CreateTableOperator(CultureHelper.DefaultCultureInfo, context, globalsTable.Table, globalsTable.Table.PrimaryKeyIndex, null, null, null, 0, 1, KeyRange.AllRows, false, true), columnsToUpdate, valuesToUpdate, true))
				{
					int num = (int)updateOperator.ExecuteScalar();
				}
			}
		}

		// Token: 0x0400027A RID: 634
		public static AddEventCounterBoundsToGlobalsTable Instance = new AddEventCounterBoundsToGlobalsTable();
	}
}
