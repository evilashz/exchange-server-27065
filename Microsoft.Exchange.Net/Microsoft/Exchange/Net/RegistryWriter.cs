using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C68 RID: 3176
	public class RegistryWriter : IRegistryWriter
	{
		// Token: 0x0600465C RID: 18012 RVA: 0x000BC5E7 File Offset: 0x000BA7E7
		internal RegistryWriter()
		{
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x0600465D RID: 18013 RVA: 0x000BC5EF File Offset: 0x000BA7EF
		public static IRegistryWriter Instance
		{
			get
			{
				return RegistryWriter.hookableInstance.Value;
			}
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x000BC5FC File Offset: 0x000BA7FC
		public void SetValue(RegistryKey baseKey, string subkeyName, string valueName, object value, RegistryValueKind valueKind)
		{
			if (baseKey == null)
			{
				throw new ArgumentNullException("baseKey");
			}
			using (RegistryKey registryKey = baseKey.OpenSubKey(subkeyName, true))
			{
				if (registryKey != null)
				{
					registryKey.SetValue(valueName, value, valueKind);
				}
			}
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x000BC64C File Offset: 0x000BA84C
		public void DeleteValue(RegistryKey baseKey, string subkeyName, string valueName)
		{
			if (baseKey == null)
			{
				throw new ArgumentNullException("baseKey");
			}
			using (RegistryKey registryKey = baseKey.OpenSubKey(subkeyName, true))
			{
				if (registryKey != null)
				{
					registryKey.DeleteValue(valueName, false);
				}
			}
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x000BC698 File Offset: 0x000BA898
		public void CreateSubKey(RegistryKey baseKey, string subkeyName)
		{
			if (baseKey == null)
			{
				throw new ArgumentNullException("baseKey");
			}
			if (string.IsNullOrEmpty(subkeyName))
			{
				throw new ArgumentNullException("subKeyName");
			}
			using (RegistryKey registryKey = baseKey.CreateSubKey(subkeyName))
			{
				if (registryKey == null)
				{
					throw new ArgumentException(string.Empty, "subkeyName");
				}
			}
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x000BC700 File Offset: 0x000BA900
		public void DeleteSubKeyTree(RegistryKey baseKey, string subkeyName)
		{
			if (baseKey == null)
			{
				throw new ArgumentNullException("baseKey");
			}
			if (string.IsNullOrEmpty(subkeyName))
			{
				throw new ArgumentNullException("subKeyName");
			}
			baseKey.DeleteSubKeyTree(subkeyName);
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x000BC72A File Offset: 0x000BA92A
		internal static IDisposable SetTestHook(IRegistryWriter testHook)
		{
			return RegistryWriter.hookableInstance.SetTestHook(testHook);
		}

		// Token: 0x04003AB0 RID: 15024
		private static Hookable<IRegistryWriter> hookableInstance = Hookable<IRegistryWriter>.Create(false, new RegistryWriter());
	}
}
