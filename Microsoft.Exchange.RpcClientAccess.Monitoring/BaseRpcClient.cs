using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class BaseRpcClient<TClient> : BaseObject, IRpcClient, IDisposable where TClient : RpcClientBase
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020D0 File Offset: 0x000002D0
		protected BaseRpcClient(TClient rpcClient)
		{
			this.RpcClient = rpcClient;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E0 File Offset: 0x000002E0
		public string BindingString
		{
			get
			{
				TClient rpcClient = this.RpcClient;
				return rpcClient.GetBindingString();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002101 File Offset: 0x00000301
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002109 File Offset: 0x00000309
		private protected TClient RpcClient { protected get; private set; }

		// Token: 0x06000006 RID: 6 RVA: 0x00002112 File Offset: 0x00000312
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.RpcClient);
			base.InternalDispose();
		}
	}
}
