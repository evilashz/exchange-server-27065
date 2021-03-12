using System;
using System.Collections.Specialized;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000DF4 RID: 3572
	internal static class NameValueCollectionExtensions
	{
		// Token: 0x06005C8B RID: 23691 RVA: 0x001207AC File Offset: 0x0011E9AC
		public static TEnum? GetQueryEnumValue<TEnum>(this NameValueCollection queryString, string paramName) where TEnum : struct
		{
			ArgumentValidator.ThrowIfNull("queryString", queryString);
			ArgumentValidator.ThrowIfNullOrEmpty("paramName", paramName);
			TEnum? result = null;
			string value = queryString[paramName];
			TEnum value2;
			if (!string.IsNullOrEmpty(value) && Enum.TryParse<TEnum>(value, out value2))
			{
				result = new TEnum?(value2);
			}
			return result;
		}

		// Token: 0x06005C8C RID: 23692 RVA: 0x001207FC File Offset: 0x0011E9FC
		public static bool GetCountQueryString(this NameValueCollection queryString)
		{
			ArgumentValidator.ThrowIfNull("queryString", queryString);
			string text = queryString["$count"];
			if (text == null)
			{
				return false;
			}
			bool result;
			if (bool.TryParse(text, out result))
			{
				return result;
			}
			throw new InvalidValueForCountSystemQueryOptionException();
		}
	}
}
