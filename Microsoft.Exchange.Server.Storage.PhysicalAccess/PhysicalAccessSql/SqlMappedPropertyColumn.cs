using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000F5 RID: 245
	public sealed class SqlMappedPropertyColumn : MappedPropertyColumn, ISqlColumn
	{
		// Token: 0x06000A92 RID: 2706 RVA: 0x00033835 File Offset: 0x00031A35
		internal SqlMappedPropertyColumn(Column actualColumn, StorePropTag propTag) : base(actualColumn, propTag)
		{
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0003383F File Offset: 0x00031A3F
		public void AppendExpressionToQuery(SqlQueryModel model, ColumnUse use, SqlCommand command)
		{
			((ISqlColumn)this.ActualColumn).AppendExpressionToQuery(model, use, command);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00033854 File Offset: 0x00031A54
		public void AppendNameToQuery(SqlCommand command)
		{
			command.Append(this.Name);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00033862 File Offset: 0x00031A62
		public void AppendQueryText(SqlQueryModel model, SqlCommand command)
		{
			((ISqlColumn)this.ActualColumn).AppendQueryText(model, command);
		}
	}
}
