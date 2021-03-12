using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200023C RID: 572
	public static class ConfigurableHelper
	{
		// Token: 0x06001A2A RID: 6698 RVA: 0x00072890 File Offset: 0x00070A90
		public static DataColumn[] GetCommonDataColumns()
		{
			DataColumn dataColumn = new DataColumn("Identity", typeof(object));
			dataColumn.ExtendedProperties["LambdaExpression"] = "DistinguishedName,Guid=>WinformsHelper.GenerateADObjectId(Guid(@0[\"Guid\"]),@0[DistinguishedName].ToString())";
			return new DataColumn[]
			{
				dataColumn,
				new DataColumn("Name", typeof(string)),
				new DataColumn("ObjectClass", typeof(string)),
				new DataColumn("Guid", typeof(Guid)),
				new DataColumn("DistinguishedName", typeof(string))
			};
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00072930 File Offset: 0x00070B30
		public static DataTable GetCommonDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.AddRange(ConfigurableHelper.GetCommonDataColumns());
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["Guid"]
			};
			return dataTable;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00072978 File Offset: 0x00070B78
		public static DataColumn GetDataColumnWithExpectedType(string columnName, Type expectedType)
		{
			DataColumn dataColumn = new DataColumn(columnName, typeof(object));
			dataColumn.ExtendedProperties["ExpectedType"] = expectedType;
			return dataColumn;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000729A8 File Offset: 0x00070BA8
		public static void AddColumnWithExpectedType(this DataColumnCollection columns, string columnName, Type expectedType)
		{
			columns.Add(ConfigurableHelper.GetDataColumnWithExpectedType(columnName, expectedType));
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000729B8 File Offset: 0x00070BB8
		public static void AddColumnWithLambdaExpression(this DataColumnCollection columns, string columnName, Type columnType, string lambdaExpression)
		{
			DataColumn dataColumn = new DataColumn(columnName, columnType);
			dataColumn.ExtendedProperties["LambdaExpression"] = lambdaExpression;
			columns.Add(dataColumn);
		}
	}
}
