using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000089 RID: 137
	public class RegUtils
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x00011F74 File Offset: 0x00010174
		internal static T GetProperty<T>(RegistryKey key, string propertyName, T defaultValue)
		{
			T result = defaultValue;
			if (key != null)
			{
				object value = key.GetValue(propertyName);
				if (value != null && value is T)
				{
					result = (T)((object)value);
				}
			}
			return result;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00011FA4 File Offset: 0x000101A4
		internal static TimeSpan GetLongPropertyAsTimeSpan(RegistryKey key, string propertyName, TimeSpan defaultValue)
		{
			TimeSpan result = defaultValue;
			if (key != null)
			{
				object value = key.GetValue(propertyName);
				if (value != null)
				{
					if (value is int)
					{
						result = TimeSpan.FromMilliseconds((double)((int)value));
					}
					else if (value is long)
					{
						result = TimeSpan.FromMilliseconds((double)((long)value));
					}
				}
			}
			return result;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00011FEE File Offset: 0x000101EE
		internal static DateTimeOffset GetTimeProperty(RegistryKey key, string propertyName)
		{
			return RegUtils.GetTimeProperty(key, propertyName, DateTimeOffset.MinValue);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00011FFC File Offset: 0x000101FC
		internal static DateTimeOffset GetTimeProperty(RegistryKey key, string propertyName, DateTimeOffset defaultValue)
		{
			string property = RegUtils.GetProperty<string>(key, propertyName, string.Empty);
			DateTimeOffset result;
			if (!DateTimeOffset.TryParse(property, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00012024 File Offset: 0x00010224
		internal static WcfTimeout GetWcfTimeoutProperty(RegistryKey key, string propertyName, WcfTimeout defaultTimeout)
		{
			string property = RegUtils.GetProperty<string>(key, propertyName, string.Empty);
			return WcfTimeout.Parse(property, defaultTimeout);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00012048 File Offset: 0x00010248
		internal static bool GetBoolProperty(RegistryKey key, string propertyName, bool defaultValue = false)
		{
			bool result = defaultValue;
			object property = RegUtils.GetProperty<object>(key, propertyName, null);
			if (property is int)
			{
				result = ((int)property > 0);
			}
			else if (property is string && !bool.TryParse(property as string, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00012090 File Offset: 0x00010290
		internal static void SetProperty<T>(RegistryKey key, string propertyName, T value)
		{
			if (value is bool)
			{
				key.SetValue(propertyName, ((bool)((object)value)) ? 1 : 0);
				return;
			}
			key.SetValue(propertyName, value);
		}
	}
}
