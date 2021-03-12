using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.Servicelets.RPCHTTP
{
	// Token: 0x02000004 RID: 4
	internal class RegistryUtilities
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002204 File Offset: 0x00000404
		internal static RegistryUtilities.RegistryValueAction UpdateRegValue(string keyName, string valueName, object newValue, out object oldValue)
		{
			RegistryUtilities.RegistryValueAction registryValueAction = RegistryUtilities.RegistryValueAction.None;
			oldValue = null;
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(keyName))
			{
				oldValue = registryKey.GetValue(valueName, null);
				if ((oldValue == null && newValue != null) || (object.Equals(oldValue, string.Empty) && !object.Equals(newValue, string.Empty)))
				{
					registryValueAction = RegistryUtilities.RegistryValueAction.Enabled;
				}
				else if ((oldValue != null && newValue == null) || (!object.Equals(oldValue, string.Empty) && object.Equals(newValue, string.Empty)))
				{
					registryValueAction = RegistryUtilities.RegistryValueAction.Disabled;
				}
				else if (!object.Equals(oldValue, newValue))
				{
					registryValueAction = RegistryUtilities.RegistryValueAction.Updated;
				}
				if (registryValueAction != RegistryUtilities.RegistryValueAction.None)
				{
					if (newValue != null)
					{
						registryKey.SetValue(valueName, newValue);
					}
					else
					{
						registryKey.DeleteValue(valueName, false);
					}
				}
			}
			return registryValueAction;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022BC File Offset: 0x000004BC
		internal static void SetRegkeyDWord(string keyName, string valueName, int newValue)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(keyName))
			{
				object value = registryKey.GetValue(valueName, null);
				if (value is int)
				{
					int num = (int)value;
					if (num != newValue)
					{
						registryKey.SetValue(valueName, newValue, RegistryValueKind.DWord);
					}
				}
				else
				{
					registryKey.SetValue(valueName, newValue, RegistryValueKind.DWord);
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000232C File Offset: 0x0000052C
		internal static void SetRegkeyString(string keyName, string valueName, string newValue)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(keyName))
			{
				object value = registryKey.GetValue(valueName, string.Empty);
				if (value is string)
				{
					string a = (string)value;
					if (!string.Equals(a, newValue))
					{
						registryKey.SetValue(valueName, newValue, RegistryValueKind.String);
					}
				}
				else
				{
					registryKey.SetValue(valueName, newValue, RegistryValueKind.String);
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000239C File Offset: 0x0000059C
		internal static void RemoveRegkey(string keyName)
		{
			RegistryKey registryKey = null;
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey(keyName, false);
				if (registryKey != null)
				{
					try
					{
						Registry.LocalMachine.DeleteSubKeyTree(keyName);
					}
					catch (ArgumentException)
					{
					}
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023F4 File Offset: 0x000005F4
		internal static void RemoveRegValue(string keyName, string valueName)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(keyName, true))
			{
				if (registryKey != null)
				{
					registryKey.DeleteValue(valueName, false);
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002438 File Offset: 0x00000638
		internal static T GetRegistryValueOrUseDefault<T>(string registryKeyName, string registryValueName, RegistryValueKind expectedValueKind, T defaultValue)
		{
			T registryValueOrUseDefault;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryKeyName, false))
			{
				registryValueOrUseDefault = RegistryUtilities.GetRegistryValueOrUseDefault<T>(registryKey, registryValueName, expectedValueKind, defaultValue);
			}
			return registryValueOrUseDefault;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000247C File Offset: 0x0000067C
		internal static T GetRegistryValueOrUseDefault<T>(RegistryKey registryKey, string registryValueName, RegistryValueKind expectedValueKind, T defaultValue)
		{
			T result;
			if (registryKey != null && RegistryUtilities.TryGetRegistryValue<T>(registryKey, registryValueName, expectedValueKind, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000249C File Offset: 0x0000069C
		internal static bool TryGetRegistryValue<T>(RegistryKey registryKey, string registryValueName, RegistryValueKind expectedValueKind, out T result)
		{
			result = default(T);
			object value = registryKey.GetValue(registryValueName);
			if (value != null)
			{
				RegistryValueKind valueKind = registryKey.GetValueKind(registryValueName);
				if (valueKind == expectedValueKind)
				{
					result = (T)((object)value);
					return true;
				}
			}
			return false;
		}

		// Token: 0x02000005 RID: 5
		internal enum RegistryValueAction
		{
			// Token: 0x04000008 RID: 8
			None,
			// Token: 0x04000009 RID: 9
			Enabled,
			// Token: 0x0400000A RID: 10
			Disabled,
			// Token: 0x0400000B RID: 11
			Updated
		}
	}
}
