using System;
using System.Globalization;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001F0 RID: 496
	internal class LongConverter : BaseConverter
	{
		// Token: 0x06000D11 RID: 3345 RVA: 0x000429BC File Offset: 0x00040BBC
		public static long Parse(string propertyString)
		{
			long result;
			try
			{
				result = long.Parse(propertyString, CultureInfo.InvariantCulture);
			}
			catch (OverflowException ex)
			{
				ex.Data["NeverGenerateWatson"] = null;
				throw;
			}
			return result;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x000429FC File Offset: 0x00040BFC
		public static string ToString(long propertyValue)
		{
			return propertyValue.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00042A0A File Offset: 0x00040C0A
		public override object ConvertToObject(string propertyString)
		{
			return LongConverter.Parse(propertyString);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00042A17 File Offset: 0x00040C17
		public override string ConvertToString(object propertyValue)
		{
			return LongConverter.ToString((long)propertyValue);
		}
	}
}
