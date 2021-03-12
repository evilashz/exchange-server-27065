using System;
using System.Globalization;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000202 RID: 514
	internal class UIntConverter
	{
		// Token: 0x06000D62 RID: 3426 RVA: 0x000436AB File Offset: 0x000418AB
		public static uint Parse(string propertyString)
		{
			return uint.Parse(propertyString, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x000436B8 File Offset: 0x000418B8
		public static string ToString(uint propertyValue)
		{
			return propertyValue.ToString(CultureInfo.InvariantCulture);
		}
	}
}
