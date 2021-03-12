using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200001C RID: 28
	internal interface IRegistryDataProvider
	{
		// Token: 0x06000042 RID: 66
		object GetRegistryKeyValue(RegistryKey baseRegistryKey, string subRegKeyPath, string regKeyName);
	}
}
