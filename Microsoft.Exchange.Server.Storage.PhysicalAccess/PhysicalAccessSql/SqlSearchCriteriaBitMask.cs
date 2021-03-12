using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x02000105 RID: 261
	public class SqlSearchCriteriaBitMask : SearchCriteriaBitMask, ISqlSearchCriteria
	{
		// Token: 0x06000AF5 RID: 2805 RVA: 0x000359DE File Offset: 0x00033BDE
		public SqlSearchCriteriaBitMask(Column lhs, Column rhs, SearchCriteriaBitMask.SearchBitMaskOp op) : base(lhs, rhs, op)
		{
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000359EC File Offset: 0x00033BEC
		public void AppendQueryText(CultureInfo culture, SqlQueryModel model, SqlCommand command)
		{
			if (base.Lhs.IsNullable || base.Rhs.IsNullable)
			{
				command.Append("(");
				switch (base.Op)
				{
				case SearchCriteriaBitMask.SearchBitMaskOp.EqualToZero:
					if (base.Lhs.IsNullable && base.Rhs.IsNullable)
					{
						((ISqlColumn)base.Lhs).AppendQueryText(model, command);
						command.Append(" IS NULL OR ");
						((ISqlColumn)base.Rhs).AppendQueryText(model, command);
						command.Append(" IS NULL OR ");
					}
					else if (base.Rhs.IsNullable)
					{
						((ISqlColumn)base.Rhs).AppendQueryText(model, command);
						command.Append(" IS NULL OR ");
					}
					else
					{
						((ISqlColumn)base.Lhs).AppendQueryText(model, command);
						command.Append(" IS NULL OR ");
					}
					break;
				case SearchCriteriaBitMask.SearchBitMaskOp.NotEqualToZero:
					if (base.Lhs.IsNullable && base.Rhs.IsNullable)
					{
						((ISqlColumn)base.Lhs).AppendQueryText(model, command);
						command.Append(" IS NOT NULL AND ");
						((ISqlColumn)base.Rhs).AppendQueryText(model, command);
						command.Append(" IS NOT NULL AND ");
					}
					else if (base.Rhs.IsNullable)
					{
						((ISqlColumn)base.Rhs).AppendQueryText(model, command);
						command.Append(" IS NOT NULL AND ");
					}
					else
					{
						((ISqlColumn)base.Lhs).AppendQueryText(model, command);
						command.Append(" IS NOT NULL AND ");
					}
					break;
				}
			}
			command.Append("(");
			((ISqlColumn)base.Lhs).AppendQueryText(model, command);
			command.Append(" & ");
			((ISqlColumn)base.Rhs).AppendQueryText(model, command);
			command.Append(" )");
			SearchCriteriaBitMask.BitMaskOpAsString(base.Op, command.Sb);
			if (base.Lhs.IsNullable || base.Rhs.IsNullable)
			{
				command.Append(")");
			}
		}
	}
}
