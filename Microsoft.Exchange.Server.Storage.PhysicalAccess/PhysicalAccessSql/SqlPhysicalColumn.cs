using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000F7 RID: 247
	public class SqlPhysicalColumn : PhysicalColumn, ISqlColumn
	{
		// Token: 0x06000A9F RID: 2719 RVA: 0x00033B64 File Offset: 0x00031D64
		internal SqlPhysicalColumn(string name, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, bool schemaExtension, Visibility visibility, int maxLength, int size, Table table, int index, int maxInlineLength) : base(name, name, type, nullable, identity, streamSupport, notFetchedByDefault, schemaExtension, visibility, maxLength, size, table, index, maxInlineLength)
		{
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00033B90 File Offset: 0x00031D90
		internal SqlPhysicalColumn(string name, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, Visibility visibility, int maxLength, int size, Table table, int index, int maxInlineLength) : base(name, name, type, nullable, identity, streamSupport, notFetchedByDefault, visibility, maxLength, size, table, index, maxInlineLength)
		{
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00033BB9 File Offset: 0x00031DB9
		public virtual void AppendExpressionToQuery(SqlQueryModel model, ColumnUse use, SqlCommand command)
		{
			model.AppendSimpleColumnToQuery(this, use, command);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00033BC4 File Offset: 0x00031DC4
		public virtual void AppendNameToQuery(SqlCommand command)
		{
			command.Append(this.name);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00033BD2 File Offset: 0x00031DD2
		public virtual void AppendQueryText(SqlQueryModel model, SqlCommand command)
		{
			command.AppendColumn(this, model, ColumnUse.Criteria);
		}
	}
}
