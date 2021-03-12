using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000ED RID: 237
	public sealed class SqlFunctionColumn : FunctionColumn, ISqlColumn
	{
		// Token: 0x06000A38 RID: 2616 RVA: 0x00031B2C File Offset: 0x0002FD2C
		internal SqlFunctionColumn(string name, Type type, int size, int maxLength, Table table, Func<object[], object> function, string functionName, params Column[] argumentColumns) : base(name, type, size, maxLength, table, function, functionName, argumentColumns)
		{
			if (argumentColumns != null)
			{
				foreach (Column column in argumentColumns)
				{
				}
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00031B68 File Offset: 0x0002FD68
		public void AppendExpressionToQuery(SqlQueryModel model, ColumnUse use, SqlCommand command)
		{
			command.Append(base.FunctionName);
			command.Append("(");
			if (this.ArgumentColumns != null)
			{
				for (int i = 0; i < this.ArgumentColumns.Length; i++)
				{
					if (i != 0)
					{
						command.Append(", ");
					}
					if (this.ArgumentColumns[i] is ConstantColumn)
					{
						((ISqlColumn)this.ArgumentColumns[i]).AppendQueryText(model, command);
					}
					else
					{
						model.AppendColumnToQuery(this.ArgumentColumns[i], use, command);
					}
				}
			}
			command.Append(")");
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00031BF5 File Offset: 0x0002FDF5
		public void AppendNameToQuery(SqlCommand command)
		{
			command.Append(this.Name);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00031C03 File Offset: 0x0002FE03
		public void AppendQueryText(SqlQueryModel model, SqlCommand command)
		{
			command.AppendColumn(this, model, ColumnUse.Criteria);
		}
	}
}
