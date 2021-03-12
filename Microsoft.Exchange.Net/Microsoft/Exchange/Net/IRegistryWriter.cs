using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C67 RID: 3175
	public interface IRegistryWriter
	{
		// Token: 0x06004658 RID: 18008
		void SetValue(RegistryKey baseKey, string subkeyName, string valueName, object value, RegistryValueKind valueKind);

		// Token: 0x06004659 RID: 18009
		void DeleteValue(RegistryKey baseKey, string subkeyName, string valueName);

		// Token: 0x0600465A RID: 18010
		void CreateSubKey(RegistryKey baseKey, string subkeyName);

		// Token: 0x0600465B RID: 18011
		void DeleteSubKeyTree(RegistryKey baseKey, string subkeyName);
	}
}
