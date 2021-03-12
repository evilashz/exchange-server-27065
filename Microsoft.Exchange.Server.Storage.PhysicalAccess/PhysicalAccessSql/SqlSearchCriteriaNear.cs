using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x02000103 RID: 259
	public class SqlSearchCriteriaNear : SearchCriteriaNear, ISqlSearchCriteria
	{
		// Token: 0x06000AF1 RID: 2801 RVA: 0x0003543C File Offset: 0x0003363C
		public SqlSearchCriteriaNear(int distance, bool ordered, SearchCriteriaAnd criteria) : base(distance, ordered, criteria)
		{
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00035447 File Offset: 0x00033647
		public void AppendQueryText(CultureInfo culture, SqlQueryModel model, SqlCommand command)
		{
			((ISqlSearchCriteria)base.Criteria).AppendQueryText(culture, model, command);
		}
	}
}
