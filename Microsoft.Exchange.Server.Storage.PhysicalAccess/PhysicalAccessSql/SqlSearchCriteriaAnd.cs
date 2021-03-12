using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x02000100 RID: 256
	public class SqlSearchCriteriaAnd : SearchCriteriaAnd, ISqlSearchCriteria
	{
		// Token: 0x06000AEB RID: 2795 RVA: 0x00035311 File Offset: 0x00033511
		public SqlSearchCriteriaAnd(params SearchCriteria[] nestedCriteria) : base(nestedCriteria)
		{
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0003531C File Offset: 0x0003351C
		public void AppendQueryText(CultureInfo culture, SqlQueryModel model, SqlCommand command)
		{
			if (base.NestedCriteria.Length == 0)
			{
				command.Append("1 = 1");
				return;
			}
			command.Append("(");
			for (int i = 0; i < base.NestedCriteria.Length; i++)
			{
				if (i > 0)
				{
					command.Append(" AND ");
				}
				((ISqlSearchCriteria)base.NestedCriteria[i]).AppendQueryText(culture, model, command);
			}
			command.Append(")");
		}
	}
}
