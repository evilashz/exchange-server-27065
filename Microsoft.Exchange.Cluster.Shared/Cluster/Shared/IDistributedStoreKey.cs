using System;
using System.Collections.Generic;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200008D RID: 141
	public interface IDistributedStoreKey : IDisposable
	{
		// Token: 0x06000513 RID: 1299
		IDistributedStoreKey OpenKey(string keyName, DxStoreKeyAccessMode mode = DxStoreKeyAccessMode.Read, bool isIgnoreIfNotExist = false, ReadWriteConstraints constraints = null);

		// Token: 0x06000514 RID: 1300
		void CloseKey();

		// Token: 0x06000515 RID: 1301
		bool DeleteKey(string keyName, bool isIgnoreIfNotExist = true, ReadWriteConstraints constraints = null);

		// Token: 0x06000516 RID: 1302
		bool SetValue(string propertyName, object propertyValue, RegistryValueKind valueKind, bool isBestEffort = false, ReadWriteConstraints constraints = null);

		// Token: 0x06000517 RID: 1303
		object GetValue(string propertyName, out bool isValueExist, out RegistryValueKind valueKind, ReadWriteConstraints constraints = null);

		// Token: 0x06000518 RID: 1304
		IEnumerable<Tuple<string, object>> GetAllValues(ReadWriteConstraints constraints = null);

		// Token: 0x06000519 RID: 1305
		bool DeleteValue(string propertyValue, bool isIgnoreIfNotExist = true, ReadWriteConstraints constraints = null);

		// Token: 0x0600051A RID: 1306
		IEnumerable<string> GetSubkeyNames(ReadWriteConstraints constraints = null);

		// Token: 0x0600051B RID: 1307
		IEnumerable<string> GetValueNames(ReadWriteConstraints constraints = null);

		// Token: 0x0600051C RID: 1308
		IEnumerable<Tuple<string, RegistryValueKind>> GetValueInfos(ReadWriteConstraints constraints = null);

		// Token: 0x0600051D RID: 1309
		IDistributedStoreBatchRequest CreateBatchUpdateRequest();

		// Token: 0x0600051E RID: 1310
		void ExecuteBatchRequest(List<DxStoreBatchCommand> commands, ReadWriteConstraints constraints);

		// Token: 0x0600051F RID: 1311
		IDistributedStoreChangeNotify CreateChangeNotify(ChangeNotificationFlags flags, object context, Action callback);
	}
}
