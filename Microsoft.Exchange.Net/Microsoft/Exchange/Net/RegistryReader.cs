using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C66 RID: 3174
	public class RegistryReader : IRegistryReader
	{
		// Token: 0x0600464F RID: 17999 RVA: 0x000BC362 File Offset: 0x000BA562
		internal RegistryReader()
		{
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06004650 RID: 18000 RVA: 0x000BC36A File Offset: 0x000BA56A
		public static IRegistryReader Instance
		{
			get
			{
				return RegistryReader.hookableInstance.Value;
			}
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x000BC378 File Offset: 0x000BA578
		public T GetValue<T>(RegistryKey baseKey, string subkeyName, string valueName, T defaultValue)
		{
			if (baseKey == null)
			{
				throw new ArgumentNullException("baseKey");
			}
			if (string.IsNullOrEmpty(valueName))
			{
				throw new ArgumentNullException("valueName");
			}
			if (string.IsNullOrEmpty(subkeyName))
			{
				return this.GetValue<T>(baseKey, valueName, defaultValue);
			}
			using (RegistryKey registryKey = baseKey.OpenSubKey(subkeyName, false))
			{
				if (registryKey != null)
				{
					return this.GetValue<T>(registryKey, valueName, defaultValue);
				}
			}
			return defaultValue;
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x000BC3F4 File Offset: 0x000BA5F4
		public string[] GetSubKeyNames(RegistryKey baseKey, string subkeyName)
		{
			if (baseKey == null)
			{
				throw new ArgumentNullException("baseKey");
			}
			using (RegistryKey registryKey = baseKey.OpenSubKey(subkeyName, false))
			{
				if (registryKey != null)
				{
					return registryKey.GetSubKeyNames();
				}
			}
			return Array<string>.Empty;
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x000BC448 File Offset: 0x000BA648
		public bool DoesValueExist(RegistryKey baseKey, string subkeyName, string valueName)
		{
			if (baseKey == null)
			{
				throw new ArgumentNullException("baseKey");
			}
			bool result;
			using (RegistryKey registryKey = baseKey.OpenSubKey(subkeyName, false))
			{
				result = (registryKey != null && registryKey.GetValue(valueName) != null);
			}
			return result;
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x000BC4A0 File Offset: 0x000BA6A0
		internal static IDisposable SetTestHook(IRegistryReader testHook)
		{
			return RegistryReader.hookableInstance.SetTestHook(testHook);
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x000BC4B0 File Offset: 0x000BA6B0
		internal static T GetValueOrDefault<T>(object value, T defaultValue)
		{
			if (value is T)
			{
				return (T)((object)value);
			}
			if (defaultValue is IConvertible)
			{
				try
				{
					IConvertible convertible = (IConvertible)((object)defaultValue);
					switch (convertible.GetTypeCode())
					{
					case TypeCode.Boolean:
						value = Convert.ToBoolean(value);
						break;
					case TypeCode.Int16:
						value = Convert.ToInt16(value);
						break;
					case TypeCode.UInt16:
						value = Convert.ToUInt16(value);
						break;
					case TypeCode.Int32:
						value = Convert.ToInt32(value);
						break;
					case TypeCode.UInt32:
						value = Convert.ToUInt32(value);
						break;
					case TypeCode.Int64:
						value = Convert.ToInt64(value);
						break;
					case TypeCode.UInt64:
						value = Convert.ToUInt64(value);
						break;
					}
				}
				catch (FormatException)
				{
					return defaultValue;
				}
				if (value is T)
				{
					return (T)((object)value);
				}
				return defaultValue;
			}
			return defaultValue;
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x000BC5B4 File Offset: 0x000BA7B4
		private T GetValue<T>(RegistryKey key, string valueName, T defaultValue)
		{
			object value = key.GetValue(valueName);
			if (value == null)
			{
				return defaultValue;
			}
			return RegistryReader.GetValueOrDefault<T>(value, defaultValue);
		}

		// Token: 0x04003AAF RID: 15023
		private static Hookable<IRegistryReader> hookableInstance = Hookable<IRegistryReader>.Create(false, new RegistryReader());
	}
}
