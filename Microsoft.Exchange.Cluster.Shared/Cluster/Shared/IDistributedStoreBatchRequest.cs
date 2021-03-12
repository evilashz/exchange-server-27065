using System;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000A1 RID: 161
	public interface IDistributedStoreBatchRequest : IDisposable
	{
		// Token: 0x060005F8 RID: 1528
		void CreateKey(string keyName);

		// Token: 0x060005F9 RID: 1529
		void DeleteKey(string keyName);

		// Token: 0x060005FA RID: 1530
		void SetValue(string propertyName, object propertyValue, RegistryValueKind valueKind = RegistryValueKind.Unknown);

		// Token: 0x060005FB RID: 1531
		void DeleteValue(string propertyName);

		// Token: 0x060005FC RID: 1532
		void Execute(ReadWriteConstraints constraints = null);
	}
}
