using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000062 RID: 98
	public interface ILocalDataStore
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003E9 RID: 1001
		// (set) Token: 0x060003EA RID: 1002
		int LastInstanceExecuted { get; set; }

		// Token: 0x060003EB RID: 1003
		bool CreateKey(int? instanceNumber, string keyName);

		// Token: 0x060003EC RID: 1004
		bool DeleteKey(int? instanceNumber, string keyName);

		// Token: 0x060003ED RID: 1005
		void SetProperty(int? instanceNumber, string keyName, string propertyName, PropertyValue propertyValue);

		// Token: 0x060003EE RID: 1006
		bool DeleteProperty(int? instanceNumber, string keyName, string propertyName);

		// Token: 0x060003EF RID: 1007
		void ExecuteBatch(int? instanceNumber, string keyName, DxStoreBatchCommand[] commands);

		// Token: 0x060003F0 RID: 1008
		bool IsKeyExist(string keyName);

		// Token: 0x060003F1 RID: 1009
		DataStoreStats GetStoreStats();

		// Token: 0x060003F2 RID: 1010
		string[] EnumSubkeyNames(string keyName);

		// Token: 0x060003F3 RID: 1011
		bool IsPropertyExist(string keyName, string propertyName);

		// Token: 0x060003F4 RID: 1012
		PropertyValue GetProperty(string keyName, string propertyName);

		// Token: 0x060003F5 RID: 1013
		Tuple<string, PropertyValue>[] GetAllProperties(string keyName);

		// Token: 0x060003F6 RID: 1014
		PropertyNameInfo[] EnumPropertyNames(string keyName);

		// Token: 0x060003F7 RID: 1015
		InstanceSnapshotInfo GetSnapshot(string keyName = null, bool isCompress = true);

		// Token: 0x060003F8 RID: 1016
		XElement GetXElementSnapshot(string keyName, out int lastInstanceExecuted);

		// Token: 0x060003F9 RID: 1017
		void ApplySnapshot(InstanceSnapshotInfo snapshotInfo, int? instanceNumber = null);

		// Token: 0x060003FA RID: 1018
		void ApplySnapshotFromXElement(string keyName, int lastInstanceExecuted, XElement rootElement);
	}
}
