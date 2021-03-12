using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MapiHttp;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200003E RID: 62
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Environment : IEnvironment
	{
		// Token: 0x0600018B RID: 395 RVA: 0x00005E52 File Offset: 0x00004052
		public ISampleClient CreateSampleClient()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005E59 File Offset: 0x00004059
		public IVerifyRpcProxyClient CreateVerifyRpcProxyClient(RpcBindingInfo bindingInfo)
		{
			return new VerifyRpcProxyClient(bindingInfo);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005E61 File Offset: 0x00004061
		public IEmsmdbClient CreateEmsmdbClient(RpcBindingInfo bindingInfo)
		{
			bindingInfo.AllowImpersonation = true;
			return new EmsmdbClient(bindingInfo);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005E70 File Offset: 0x00004070
		public IRfriClient CreateRfriClient(RpcBindingInfo bindingInfo)
		{
			return new RfriClient(bindingInfo);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005E78 File Offset: 0x00004078
		public INspiClient CreateNspiClient(RpcBindingInfo bindingInfo)
		{
			return new NspiClient(bindingInfo);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005E80 File Offset: 0x00004080
		public IEmsmdbClient CreateEmsmdbClient(MapiHttpBindingInfo bindingInfo)
		{
			return new EmsmdbClient(bindingInfo);
		}
	}
}
