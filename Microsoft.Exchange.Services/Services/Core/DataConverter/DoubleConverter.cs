using System;
using System.Globalization;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001E5 RID: 485
	internal class DoubleConverter : BaseConverter
	{
		// Token: 0x06000CE1 RID: 3297 RVA: 0x00042568 File Offset: 0x00040768
		public static double Parse(string propertyString)
		{
			double result;
			try
			{
				result = double.Parse(propertyString, CultureInfo.InvariantCulture);
			}
			catch (OverflowException ex)
			{
				ex.Data["NeverGenerateWatson"] = null;
				throw;
			}
			return result;
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000425A8 File Offset: 0x000407A8
		public static string ToString(double propertyValue)
		{
			return propertyValue.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000425B6 File Offset: 0x000407B6
		public override object ConvertToObject(string propertyString)
		{
			return DoubleConverter.Parse(propertyString);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x000425C3 File Offset: 0x000407C3
		public override string ConvertToString(object propertyValue)
		{
			return DoubleConverter.ToString((double)propertyValue);
		}
	}
}
