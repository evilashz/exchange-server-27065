using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000EB RID: 235
	public sealed class SqlConstantColumn : ConstantColumn, ISqlColumn
	{
		// Token: 0x06000A30 RID: 2608 RVA: 0x00031A84 File Offset: 0x0002FC84
		internal SqlConstantColumn(string name, Type type, Visibility visibility, int size, int maxLength, object value) : base(name, type, visibility, size, maxLength, value)
		{
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00031A95 File Offset: 0x0002FC95
		public void AppendExpressionToQuery(SqlQueryModel model, ColumnUse use, SqlCommand command)
		{
			command.AppendParameter(base.Value);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00031AA3 File Offset: 0x0002FCA3
		public void AppendNameToQuery(SqlCommand command)
		{
			command.Append(this.Name);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00031AB1 File Offset: 0x0002FCB1
		public void AppendQueryText(SqlQueryModel model, SqlCommand command)
		{
			command.AppendParameter(base.Value);
		}
	}
}
