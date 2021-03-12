using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x02000102 RID: 258
	public class SqlSearchCriteriaNot : SearchCriteriaNot, ISqlSearchCriteria
	{
		// Token: 0x06000AEF RID: 2799 RVA: 0x00035408 File Offset: 0x00033608
		public SqlSearchCriteriaNot(SearchCriteria nestedCriteria) : base(nestedCriteria)
		{
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00035411 File Offset: 0x00033611
		public void AppendQueryText(CultureInfo culture, SqlQueryModel model, SqlCommand command)
		{
			command.Append("NOT(");
			((ISqlSearchCriteria)base.Criteria).AppendQueryText(culture, model, command);
			command.Append(")");
		}
	}
}
