using System;
using System.Globalization;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001FB RID: 507
	internal class ShortConverter : BaseConverter
	{
		// Token: 0x06000D44 RID: 3396 RVA: 0x000432F4 File Offset: 0x000414F4
		public static short Parse(string propertyString)
		{
			short result;
			try
			{
				result = short.Parse(propertyString, CultureInfo.InvariantCulture);
			}
			catch (OverflowException ex)
			{
				ex.Data["NeverGenerateWatson"] = null;
				throw;
			}
			return result;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00043334 File Offset: 0x00041534
		public static string ToString(short propertyValue)
		{
			return propertyValue.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00043342 File Offset: 0x00041542
		public override object ConvertToObject(string propertyString)
		{
			return ShortConverter.Parse(propertyString);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0004334F File Offset: 0x0004154F
		public override string ConvertToString(object propertyValue)
		{
			return ShortConverter.ToString((short)propertyValue);
		}
	}
}
