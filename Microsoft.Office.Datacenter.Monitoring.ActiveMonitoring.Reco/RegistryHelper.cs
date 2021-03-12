using System;
using Microsoft.Win32;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200000C RID: 12
	public static class RegistryHelper
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002DAC File Offset: 0x00000FAC
		public static RegistryKey OpenKey(string rootKeyName, string subKeyName, bool isCreateKey, bool writable = false)
		{
			string text = rootKeyName;
			if (text == null)
			{
				text = string.Format("SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\ActiveMonitoring\\Parameters", "v15");
			}
			if (!string.IsNullOrEmpty(subKeyName))
			{
				text = string.Format("{0}\\{1}", text, subKeyName);
			}
			RegistryKey result;
			if (isCreateKey)
			{
				result = Registry.LocalMachine.CreateSubKey(text);
			}
			else
			{
				result = Registry.LocalMachine.OpenSubKey(text, writable);
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002E5C File Offset: 0x0000105C
		public static bool DeleteSubkeyRecursive(string rootKeyName = null, string subkeyName = null, bool isThrowOnError = false)
		{
			return RegistryHelper.HandleException<bool>(false, isThrowOnError, delegate
			{
				using (RegistryKey registryKey = RegistryHelper.OpenKey(rootKeyName, null, false, true))
				{
					if (registryKey != null)
					{
						registryKey.DeleteSubKeyTree(subkeyName, false);
						return true;
					}
				}
				return false;
			});
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002EEC File Offset: 0x000010EC
		public static bool DeleteProperty(string propertyName, string subkeyName = null, string rootKeyName = null, bool isThrowOnError = false)
		{
			return RegistryHelper.HandleException<bool>(false, isThrowOnError, delegate
			{
				using (RegistryKey registryKey = RegistryHelper.OpenKey(rootKeyName, subkeyName, true, false))
				{
					if (registryKey != null)
					{
						registryKey.DeleteValue(propertyName);
						return true;
					}
				}
				return false;
			});
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F28 File Offset: 0x00001128
		public static DateTime GetPropertyDateTime(string propertyName, DateTime defaultValue, string subkeyName = null, string rootKeyName = null, bool isThrowOnError = false)
		{
			string property = RegistryHelper.GetProperty<string>(propertyName, string.Empty, subkeyName, rootKeyName, isThrowOnError);
			DateTime result = defaultValue;
			if (!string.IsNullOrWhiteSpace(property))
			{
				result = DateTime.Parse(property);
			}
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F58 File Offset: 0x00001158
		public static void SetPropertyDateTime(string propertyName, DateTime propertyValue, string subkeyName = null, string rootKeyName = null, bool isThrowOnError = false)
		{
			string propertValue = propertyValue.ToString("o");
			RegistryHelper.SetProperty<string>(propertyName, propertValue, subkeyName, rootKeyName, isThrowOnError);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F80 File Offset: 0x00001180
		public static bool GetPropertyIntBool(string propertyName, bool defaultValue, string subkeyName = null, string rootKeyName = null, bool isThrowOnError = false)
		{
			int property = RegistryHelper.GetProperty<int>(propertyName, defaultValue ? 1 : 0, subkeyName, rootKeyName, isThrowOnError);
			return property > 0;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002FA3 File Offset: 0x000011A3
		public static bool SetPropertyIntBool(string propertyName, bool propertyValue, string subkeyName = null, string rootKeyName = null, bool isThrowOnError = false)
		{
			return RegistryHelper.SetProperty<int>(propertyName, propertyValue ? 1 : 0, subkeyName, rootKeyName, isThrowOnError);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002FB8 File Offset: 0x000011B8
		public static long GetPropertyAsLong(string propertyName, long defaultValue, string subkeyName = null, string rootKeyName = null, bool isThrowOnError = false)
		{
			long result = defaultValue;
			string property = RegistryHelper.GetProperty<string>(propertyName, null, subkeyName, rootKeyName, isThrowOnError);
			if (property != null && !long.TryParse(property, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FE4 File Offset: 0x000011E4
		public static string GetPropertyAsString(string propertyName, string subkeyName = null, string rootKeyName = null, bool isThrowOnError = false)
		{
			object property = RegistryHelper.GetProperty<object>(propertyName, null, subkeyName, rootKeyName, isThrowOnError);
			if (property != null)
			{
				return property.ToString();
			}
			return null;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003078 File Offset: 0x00001278
		public static T GetProperty<T>(string propertyName, T defaultValue, string subkeyName = null, string rootKeyName = null, bool isThrowOnError = false)
		{
			return RegistryHelper.HandleException<T>(defaultValue, isThrowOnError, delegate
			{
				T result = defaultValue;
				using (RegistryKey registryKey = RegistryHelper.OpenKey(rootKeyName, subkeyName, false, false))
				{
					if (registryKey != null)
					{
						result = (T)((object)registryKey.GetValue(propertyName, defaultValue));
					}
				}
				return result;
			});
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003128 File Offset: 0x00001328
		public static bool SetProperty<T>(string propertyName, T propertValue, string subkeyName = null, string rootKeyName = null, bool isThrowOnError = false)
		{
			return RegistryHelper.HandleException<bool>(false, isThrowOnError, delegate
			{
				using (RegistryKey registryKey = RegistryHelper.OpenKey(rootKeyName, subkeyName, true, false))
				{
					if (registryKey != null)
					{
						registryKey.SetValue(propertyName, propertValue);
						return true;
					}
				}
				return false;
			});
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000316C File Offset: 0x0000136C
		internal static T HandleException<T>(T defaultValue, bool isThrowOnError, Func<T> action)
		{
			try
			{
				return action();
			}
			catch (Exception)
			{
				if (isThrowOnError)
				{
					throw;
				}
			}
			return defaultValue;
		}

		// Token: 0x0200000D RID: 13
		public static class WellKnownPropertyNames
		{
			// Token: 0x04000033 RID: 51
			public const string SystemStartTime = "SystemStartTime";

			// Token: 0x04000034 RID: 52
			public const string ActualBugCheckInitiatedTime = "ActualBugCheckInitiatedTime";
		}
	}
}
