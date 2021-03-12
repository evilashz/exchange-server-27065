using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200007B RID: 123
	public interface IDxStoreAccessClient
	{
		// Token: 0x060004DA RID: 1242
		DxStoreAccessReply.CheckKey CheckKey(DxStoreAccessRequest.CheckKey request, TimeSpan? timeout = null);

		// Token: 0x060004DB RID: 1243
		DxStoreAccessReply.DeleteKey DeleteKey(DxStoreAccessRequest.DeleteKey request, TimeSpan? timeout = null);

		// Token: 0x060004DC RID: 1244
		DxStoreAccessReply.SetProperty SetProperty(DxStoreAccessRequest.SetProperty request, TimeSpan? timeout = null);

		// Token: 0x060004DD RID: 1245
		DxStoreAccessReply.DeleteProperty DeleteProperty(DxStoreAccessRequest.DeleteProperty request, TimeSpan? timeout = null);

		// Token: 0x060004DE RID: 1246
		DxStoreAccessReply.ExecuteBatch ExecuteBatch(DxStoreAccessRequest.ExecuteBatch request, TimeSpan? timeout = null);

		// Token: 0x060004DF RID: 1247
		DxStoreAccessReply.GetProperty GetProperty(DxStoreAccessRequest.GetProperty request, TimeSpan? timeout = null);

		// Token: 0x060004E0 RID: 1248
		DxStoreAccessReply.GetAllProperties GetAllProperties(DxStoreAccessRequest.GetAllProperties request, TimeSpan? timeout = null);

		// Token: 0x060004E1 RID: 1249
		DxStoreAccessReply.GetPropertyNames GetPropertyNames(DxStoreAccessRequest.GetPropertyNames request, TimeSpan? timeout = null);

		// Token: 0x060004E2 RID: 1250
		DxStoreAccessReply.GetSubkeyNames GetSubkeyNames(DxStoreAccessRequest.GetSubkeyNames request, TimeSpan? timeout = null);
	}
}
