using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000C2 RID: 194
	internal static class AppSettings
	{
		// Token: 0x06000715 RID: 1813 RVA: 0x0003777C File Offset: 0x0003597C
		internal static T GetConfiguredValue<T>(string fieldName, T defaultValue)
		{
			if (string.IsNullOrEmpty(fieldName))
			{
				throw new ArgumentNullException("fieldName");
			}
			T result = defaultValue;
			string text = ConfigurationManager.AppSettings[fieldName];
			if (!string.IsNullOrEmpty(text))
			{
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
				if (converter != null)
				{
					try
					{
						result = (T)((object)converter.ConvertFromInvariantString(text));
					}
					catch (Exception)
					{
					}
				}
			}
			return result;
		}
	}
}
