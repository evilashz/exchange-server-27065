using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200002D RID: 45
	public static class DataRowExtension
	{
		// Token: 0x06000208 RID: 520 RVA: 0x0000848E File Offset: 0x0000668E
		public static T CastCellValue<T>(this DataRow dataRow, string columnName)
		{
			return (T)((object)dataRow[columnName]);
		}
	}
}
