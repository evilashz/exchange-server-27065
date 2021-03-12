using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000FE RID: 254
	public class SqlSearchCriteriaTrue : SearchCriteriaTrue, ISqlSearchCriteria
	{
		// Token: 0x06000AE5 RID: 2789 RVA: 0x000352CF File Offset: 0x000334CF
		internal SqlSearchCriteriaTrue()
		{
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000352D7 File Offset: 0x000334D7
		public void AppendQueryText(CultureInfo culture, SqlQueryModel model, SqlCommand command)
		{
			command.Append("1 = 1");
		}

		// Token: 0x04000376 RID: 886
		public static readonly SqlSearchCriteriaTrue Instance = new SqlSearchCriteriaTrue();
	}
}
