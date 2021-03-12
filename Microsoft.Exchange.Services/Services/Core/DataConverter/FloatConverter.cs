using System;
using System.Globalization;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001EB RID: 491
	internal class FloatConverter : BaseConverter
	{
		// Token: 0x06000CFA RID: 3322 RVA: 0x000427AC File Offset: 0x000409AC
		public static float Parse(string propertyString)
		{
			float result;
			try
			{
				result = float.Parse(propertyString, CultureInfo.InvariantCulture);
			}
			catch (OverflowException ex)
			{
				ex.Data["NeverGenerateWatson"] = null;
				throw;
			}
			return result;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000427EC File Offset: 0x000409EC
		public static string ToString(float propertyValue)
		{
			return propertyValue.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000427FA File Offset: 0x000409FA
		public override object ConvertToObject(string propertyString)
		{
			return FloatConverter.Parse(propertyString);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00042807 File Offset: 0x00040A07
		public override string ConvertToString(object propertyValue)
		{
			return FloatConverter.ToString((float)propertyValue);
		}
	}
}
