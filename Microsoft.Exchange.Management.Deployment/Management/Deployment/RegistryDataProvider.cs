using System;
using Microsoft.Exchange.Management.Analysis;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000027 RID: 39
	internal class RegistryDataProvider : IRegistryDataProvider
	{
		// Token: 0x06000080 RID: 128 RVA: 0x000033E8 File Offset: 0x000015E8
		public object GetRegistryKeyValue(RegistryKey baseRegistryKey, string subRegKeyPath, string regKeyName)
		{
			if (baseRegistryKey == null || string.IsNullOrWhiteSpace(subRegKeyPath))
			{
				throw new ArgumentNullException();
			}
			RegistryKey registryKey = baseRegistryKey.OpenSubKey(subRegKeyPath, false);
			if (registryKey == null)
			{
				throw new FailureException(Strings.RegistryKeyNotFound(baseRegistryKey.Name + "\\" + subRegKeyPath));
			}
			object result;
			if (regKeyName == null)
			{
				result = registryKey.GetSubKeyNames();
			}
			else
			{
				result = registryKey.GetValue(regKeyName);
			}
			registryKey.Dispose();
			return result;
		}
	}
}
