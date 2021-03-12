using System;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Common.Extensions
{
	// Token: 0x02000003 RID: 3
	internal class RegistryReader : IRegistryReader
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000213D File Offset: 0x0000033D
		private RegistryReader()
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002150 File Offset: 0x00000350
		public static RegistryReader Instance
		{
			get
			{
				return RegistryReader.instance;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002157 File Offset: 0x00000357
		public T GetValue<T>(RegistryKey baseKey, string subkeyName, string valueName, T defaultValue)
		{
			return this.registryReader.GetValue<T>(baseKey, subkeyName, valueName, defaultValue);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002169 File Offset: 0x00000369
		public string[] GetSubKeyNames(RegistryKey baseKey, string subkeyName)
		{
			return this.registryReader.GetSubKeyNames(baseKey, subkeyName);
		}

		// Token: 0x04000002 RID: 2
		private static RegistryReader instance = new RegistryReader();

		// Token: 0x04000003 RID: 3
		private IRegistryReader registryReader = RegistryReader.Instance;
	}
}
