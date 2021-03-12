using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000EC RID: 236
	public sealed class SqlConversionColumn : ConversionColumn, ISqlColumn
	{
		// Token: 0x06000A34 RID: 2612 RVA: 0x00031AC0 File Offset: 0x0002FCC0
		internal SqlConversionColumn(string name, Type type, int size, int maxLength, Table table, Func<object, object> conversionFunction, string functionName, Column argumentColumn) : base(name, type, size, maxLength, table, conversionFunction, functionName, argumentColumn)
		{
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00031AE0 File Offset: 0x0002FCE0
		public void AppendExpressionToQuery(SqlQueryModel model, ColumnUse use, SqlCommand command)
		{
			command.Append(base.FunctionName);
			command.Append("(");
			model.AppendColumnToQuery(base.ArgumentColumn, use, command);
			command.Append(")");
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00031B12 File Offset: 0x0002FD12
		public void AppendNameToQuery(SqlCommand command)
		{
			command.Append(this.Name);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00031B20 File Offset: 0x0002FD20
		public void AppendQueryText(SqlQueryModel model, SqlCommand command)
		{
			command.AppendColumn(this, model, ColumnUse.Criteria);
		}
	}
}
