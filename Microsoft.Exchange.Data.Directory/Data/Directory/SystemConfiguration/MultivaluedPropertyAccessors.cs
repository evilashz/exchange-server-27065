using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002F2 RID: 754
	internal class MultivaluedPropertyAccessors
	{
		// Token: 0x06002318 RID: 8984 RVA: 0x00098BB0 File Offset: 0x00096DB0
		internal static string GetStringValueFromMultivaluedProperty(string name, IEnumerable<string> property, string defaultValue)
		{
			string text = property.SingleOrDefault((string s) => s.StartsWith(name + ":"));
			if (text == null)
			{
				return defaultValue;
			}
			if (text.Length <= name.Length + ":".Length)
			{
				return string.Empty;
			}
			return text.Substring(name.Length + ":".Length);
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x00098C24 File Offset: 0x00096E24
		internal static int GetIntValueFromMultivaluedProperty(string name, IEnumerable<string> property, int defaultValue)
		{
			string stringValueFromMultivaluedProperty = MultivaluedPropertyAccessors.GetStringValueFromMultivaluedProperty(name, property, null);
			if (string.IsNullOrEmpty(stringValueFromMultivaluedProperty))
			{
				return defaultValue;
			}
			int result;
			if (int.TryParse(stringValueFromMultivaluedProperty, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x00098C54 File Offset: 0x00096E54
		internal static Version GetVersionValueFromMultivaluedProperty(string name, IEnumerable<string> property, Version defaultValue)
		{
			string stringValueFromMultivaluedProperty = MultivaluedPropertyAccessors.GetStringValueFromMultivaluedProperty(name, property, null);
			if (string.IsNullOrEmpty(stringValueFromMultivaluedProperty))
			{
				return defaultValue;
			}
			Version result;
			if (Version.TryParse(stringValueFromMultivaluedProperty, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x00098C84 File Offset: 0x00096E84
		internal static ByteQuantifiedSize GetByteQuantifiedValueFromMultivaluedProperty(string name, IEnumerable<string> property, ByteQuantifiedSize defaultValue)
		{
			string stringValueFromMultivaluedProperty = MultivaluedPropertyAccessors.GetStringValueFromMultivaluedProperty(name, property, null);
			if (string.IsNullOrEmpty(stringValueFromMultivaluedProperty))
			{
				return defaultValue;
			}
			ByteQuantifiedSize result;
			if (ByteQuantifiedSize.TryParse(stringValueFromMultivaluedProperty, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x00098CB4 File Offset: 0x00096EB4
		internal static EnhancedTimeSpan GetTimespanValueFromMultivaluedProperty(string name, IEnumerable<string> property, EnhancedTimeSpan defaultValue)
		{
			string stringValueFromMultivaluedProperty = MultivaluedPropertyAccessors.GetStringValueFromMultivaluedProperty(name, property, null);
			if (string.IsNullOrEmpty(stringValueFromMultivaluedProperty))
			{
				return defaultValue;
			}
			EnhancedTimeSpan result;
			if (EnhancedTimeSpan.TryParse(stringValueFromMultivaluedProperty, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x00098D04 File Offset: 0x00096F04
		internal static void UpdateMultivaluedProperty<T>(T value, string name, MultiValuedProperty<string> property)
		{
			string text = name + ":" + value.ToString();
			int num = property.ToList<string>().FindIndex((string s) => s.StartsWith(name + ":"));
			if (num < 0)
			{
				property.Add(text);
				return;
			}
			property[num] = text;
		}

		// Token: 0x040015C6 RID: 5574
		private const string NameValueSeparator = ":";
	}
}
