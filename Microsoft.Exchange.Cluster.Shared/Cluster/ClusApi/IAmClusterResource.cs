using System;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000028 RID: 40
	internal interface IAmClusterResource : IDisposable
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000180 RID: 384
		string Name { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000181 RID: 385
		AmClusterResourceHandle Handle { get; }

		// Token: 0x06000182 RID: 386
		void SetPrivateProperty<MyType>(string key, MyType value);

		// Token: 0x06000183 RID: 387
		void SetPrivatePropertyList(AmClusterPropList propList);

		// Token: 0x06000184 RID: 388
		MyType GetPrivateProperty<MyType>(string key);

		// Token: 0x06000185 RID: 389
		uint OnlineResource();

		// Token: 0x06000186 RID: 390
		uint OfflineResource();

		// Token: 0x06000187 RID: 391
		void DeleteResource();

		// Token: 0x06000188 RID: 392
		string GetTypeName();

		// Token: 0x06000189 RID: 393
		AmResourceState GetState();

		// Token: 0x0600018A RID: 394
		void SetAllPossibleOwnerNodes();
	}
}
