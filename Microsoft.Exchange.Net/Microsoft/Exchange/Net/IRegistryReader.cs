using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C65 RID: 3173
	public interface IRegistryReader
	{
		// Token: 0x0600464C RID: 17996
		T GetValue<T>(RegistryKey baseKey, string subkeyName, string valueName, T defaultValue);

		// Token: 0x0600464D RID: 17997
		string[] GetSubKeyNames(RegistryKey baseKey, string subkeyName);

		// Token: 0x0600464E RID: 17998
		bool DoesValueExist(RegistryKey baseKey, string subkeyName, string valueName);
	}
}
