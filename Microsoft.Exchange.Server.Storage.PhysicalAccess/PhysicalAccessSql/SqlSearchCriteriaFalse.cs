using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000FF RID: 255
	internal class SqlSearchCriteriaFalse : SearchCriteriaFalse, ISqlSearchCriteria
	{
		// Token: 0x06000AE9 RID: 2793 RVA: 0x000352F8 File Offset: 0x000334F8
		public void AppendQueryText(CultureInfo culture, SqlQueryModel model, SqlCommand command)
		{
			command.Append("1 = 0");
		}

		// Token: 0x04000377 RID: 887
		public static readonly SqlSearchCriteriaFalse Instance = new SqlSearchCriteriaFalse();
	}
}
