using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x02000107 RID: 263
	public sealed class SqlSizeOfColumn : SizeOfColumn, ISqlColumn
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x00035F89 File Offset: 0x00034189
		internal SqlSizeOfColumn(string name, Column termColumn, bool compressedSize) : base(name, termColumn, compressedSize)
		{
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00035F94 File Offset: 0x00034194
		public void AppendExpressionToQuery(SqlQueryModel model, ColumnUse use, SqlCommand command)
		{
			command.Append("ISNULL(CAST(DATALENGTH(");
			((ISqlColumn)base.TermColumn).AppendExpressionToQuery(model, use, command);
			command.Append(") AS int), 0)");
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00035FBF File Offset: 0x000341BF
		public void AppendNameToQuery(SqlCommand command)
		{
			command.Append(this.Name);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00035FCD File Offset: 0x000341CD
		public void AppendQueryText(SqlQueryModel model, SqlCommand command)
		{
			command.Append("ISNULL(CAST(DATALENGTH(");
			((ISqlColumn)base.TermColumn).AppendQueryText(model, command);
			command.Append(") AS int), 0)");
		}

		// Token: 0x04000378 RID: 888
		private const string DatalengthExpressionPrefix = "ISNULL(CAST(DATALENGTH(";

		// Token: 0x04000379 RID: 889
		private const string DatalengthExpressionSuffix = ") AS int), 0)";
	}
}
