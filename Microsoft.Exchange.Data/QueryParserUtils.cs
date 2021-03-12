using System;
using System.Management.Automation;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000253 RID: 595
	internal class QueryParserUtils
	{
		// Token: 0x06001429 RID: 5161 RVA: 0x0003F8C4 File Offset: 0x0003DAC4
		internal static object ConvertValueFromString(object valueToConvert, Type resultType)
		{
			string text = valueToConvert as string;
			bool flag;
			if (resultType == typeof(bool) && bool.TryParse(text, out flag))
			{
				return flag;
			}
			object result;
			if (resultType.IsEnum && EnumValidator.TryParse(resultType, text, EnumParseOptions.Default, out result))
			{
				return result;
			}
			if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				bool flag2 = text == null || "null".Equals(text, StringComparison.OrdinalIgnoreCase) || "$null".Equals(text, StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					return null;
				}
			}
			return LanguagePrimitives.ConvertTo(text, resultType);
		}
	}
}
