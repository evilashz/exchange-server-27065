using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200001D RID: 29
	public interface IClusterDB : IDisposable
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010C RID: 268
		bool IsInstalled { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600010D RID: 269
		bool IsInitialized { get; }

		// Token: 0x0600010E RID: 270
		IEnumerable<string> GetSubKeyNames(string registryKey);

		// Token: 0x0600010F RID: 271
		IEnumerable<Tuple<string, RegistryValueKind>> GetValueInfos(string registryKey);

		// Token: 0x06000110 RID: 272
		T GetValue<T>(string keyName, string valueName, T defaultValue);

		// Token: 0x06000111 RID: 273
		void SetValue<T>(string keyName, string propertyName, T propetyValue);

		// Token: 0x06000112 RID: 274
		void DeleteValue(string keyName, string propertyName);

		// Token: 0x06000113 RID: 275
		IClusterDBWriteBatch CreateWriteBatch(string registryKey);
	}
}
