using System;
using System.Globalization;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000203 RID: 515
	internal class ULongConverter
	{
		// Token: 0x06000D65 RID: 3429 RVA: 0x000436CE File Offset: 0x000418CE
		public static ulong Parse(string propertyString)
		{
			return ulong.Parse(propertyString, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x000436DB File Offset: 0x000418DB
		public static string ToString(ulong propertyValue)
		{
			return propertyValue.ToString(CultureInfo.InvariantCulture);
		}
	}
}
