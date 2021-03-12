using System;
using System.Globalization;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000D6 RID: 214
	public interface ISqlSearchCriteria
	{
		// Token: 0x06000959 RID: 2393
		void AppendQueryText(CultureInfo culture, SqlQueryModel model, SqlCommand command);
	}
}
