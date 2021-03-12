using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x02000101 RID: 257
	public class SqlSearchCriteriaOr : SearchCriteriaOr, ISqlSearchCriteria
	{
		// Token: 0x06000AED RID: 2797 RVA: 0x0003538C File Offset: 0x0003358C
		public SqlSearchCriteriaOr(params SearchCriteria[] nestedCriteria) : base(nestedCriteria)
		{
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00035398 File Offset: 0x00033598
		public void AppendQueryText(CultureInfo culture, SqlQueryModel model, SqlCommand command)
		{
			if (base.NestedCriteria.Length == 0)
			{
				command.Append("1 = 0");
				return;
			}
			command.Append("(");
			for (int i = 0; i < base.NestedCriteria.Length; i++)
			{
				if (i > 0)
				{
					command.Append(" OR ");
				}
				((ISqlSearchCriteria)base.NestedCriteria[i]).AppendQueryText(culture, model, command);
			}
			command.Append(")");
		}
	}
}
