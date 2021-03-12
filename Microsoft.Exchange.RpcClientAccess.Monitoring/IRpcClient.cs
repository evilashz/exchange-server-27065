using System;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000002 RID: 2
	internal interface IRpcClient : IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		string BindingString { get; }
	}
}
