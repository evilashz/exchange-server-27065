using System;
using System.Globalization;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001DE RID: 478
	internal class ByteConverter
	{
		// Token: 0x06000CC3 RID: 3267 RVA: 0x00041D5D File Offset: 0x0003FF5D
		public static byte Parse(string propertyString)
		{
			return byte.Parse(propertyString, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00041D6A File Offset: 0x0003FF6A
		public static string ToString(byte propertyValue)
		{
			return propertyValue.ToString(CultureInfo.InvariantCulture);
		}
	}
}
