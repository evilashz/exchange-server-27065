using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200007B RID: 123
	public sealed class GlobalsTableHelper
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x0001CFA0 File Offset: 0x0001B1A0
		public static TableOperator GetGlobalsTableRow(Context context, IList<Column> columnsToFetch)
		{
			GlobalsTable globalsTable = DatabaseSchema.GlobalsTable(context.Database);
			return Factory.CreateTableOperator(CultureHelper.DefaultCultureInfo, context, globalsTable.Table, globalsTable.Table.PrimaryKeyIndex, columnsToFetch, null, null, 0, 1, KeyRange.AllRows, false, true);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001CFE4 File Offset: 0x0001B1E4
		public static void UpdateGlobalsTableRow(Context context, IList<Column> columnsToUpdate, IList<object> columnValues)
		{
			using (LockManager.Lock(GlobalsTableHelper.instance, LockManager.LockType.GlobalsTableRowUpdate, context.Diagnostics))
			{
				using (UpdateOperator updateOperator = Factory.CreateUpdateOperator(CultureHelper.DefaultCultureInfo, context, GlobalsTableHelper.GetGlobalsTableRow(context, null), columnsToUpdate, columnValues, true))
				{
					int num = (int)updateOperator.ExecuteScalar();
				}
				context.Commit();
			}
		}

		// Token: 0x040003B7 RID: 951
		private static GlobalsTableHelper instance = new GlobalsTableHelper();
	}
}
