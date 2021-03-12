using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MapiHttp;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200003D RID: 61
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IEnvironment
	{
		// Token: 0x06000185 RID: 389
		ISampleClient CreateSampleClient();

		// Token: 0x06000186 RID: 390
		IVerifyRpcProxyClient CreateVerifyRpcProxyClient(RpcBindingInfo bindingInfo);

		// Token: 0x06000187 RID: 391
		IEmsmdbClient CreateEmsmdbClient(RpcBindingInfo bindingInfo);

		// Token: 0x06000188 RID: 392
		IRfriClient CreateRfriClient(RpcBindingInfo bindingInfo);

		// Token: 0x06000189 RID: 393
		INspiClient CreateNspiClient(RpcBindingInfo bindingInfo);

		// Token: 0x0600018A RID: 394
		IEmsmdbClient CreateEmsmdbClient(MapiHttpBindingInfo bindingInfo);
	}
}
