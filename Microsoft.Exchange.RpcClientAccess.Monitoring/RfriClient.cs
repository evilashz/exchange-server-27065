using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi.Rfri;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.RfriClient;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RfriClient : BaseRpcClient<RfriAsyncRpcClient>, IRfriClient, IRpcClient, IDisposable
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00004142 File Offset: 0x00002342
		public RfriClient(RpcBindingInfo bindingInfo) : base(new RfriAsyncRpcClient(bindingInfo))
		{
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004150 File Offset: 0x00002350
		public IAsyncResult BeginGetNewDsa(string userLegacyDN, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState)
		{
			return new RfriClient.GetNewDsaRpcCallContext(base.RpcClient, userLegacyDN, timeout, asyncCallback, asyncState).Begin();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004167 File Offset: 0x00002367
		public RfriCallResult EndGetNewDsa(IAsyncResult asyncResult, out string server)
		{
			return ((RfriClient.GetNewDsaRpcCallContext)asyncResult).End(asyncResult, out server);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004176 File Offset: 0x00002376
		public IAsyncResult BeginGetFqdnFromLegacyDn(string serverDn, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState)
		{
			return new RfriClient.GetFqdnRpcCallContext(base.RpcClient, serverDn, timeout, asyncCallback, asyncState).Begin();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000418D File Offset: 0x0000238D
		public RfriCallResult EndGetFqdnFromLegacyDn(IAsyncResult asyncResult, out string serverFqdn)
		{
			return ((RfriClient.GetFqdnRpcCallContext)asyncResult).End(asyncResult, out serverFqdn);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000419C File Offset: 0x0000239C
		protected sealed override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RfriClient>(this);
		}

		// Token: 0x02000027 RID: 39
		private class GetNewDsaRpcCallContext : RpcCallContext<RfriCallResult>
		{
			// Token: 0x060000FE RID: 254 RVA: 0x000041A4 File Offset: 0x000023A4
			public GetNewDsaRpcCallContext(RfriAsyncRpcClient rpcClient, string userDn, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(timeout, asyncCallback, asyncState)
			{
				Util.ThrowOnNullArgument(rpcClient, "rpcClient");
				Util.ThrowOnNullOrEmptyArgument(userDn, "userDn");
				this.rpcClient = rpcClient;
				this.userDn = userDn;
			}

			// Token: 0x060000FF RID: 255 RVA: 0x000041D8 File Offset: 0x000023D8
			public RfriCallResult End(IAsyncResult asyncResult, out string serverDn)
			{
				RfriCallResult result = base.GetResult();
				serverDn = this.serverDn;
				return result;
			}

			// Token: 0x06000100 RID: 256 RVA: 0x000041F8 File Offset: 0x000023F8
			protected override ICancelableAsyncResult OnBegin(CancelableAsyncCallback asyncCallback, object asyncState)
			{
				ICancelableAsyncResult result = this.rpcClient.BeginGetNewDSA(null, null, RfriGetNewDSAFlags.None, this.userDn, asyncCallback, asyncState);
				base.StartRpcTimer();
				return result;
			}

			// Token: 0x06000101 RID: 257 RVA: 0x00004224 File Offset: 0x00002424
			protected override RfriCallResult OnEnd(ICancelableAsyncResult asyncResult)
			{
				RfriCallResult result;
				try
				{
					RfriStatus rfriStatus = this.rpcClient.EndGetNewDSA(asyncResult, out this.serverDn);
					result = new RfriCallResult(rfriStatus);
				}
				finally
				{
					base.StopAndCleanupRpcTimer();
				}
				return result;
			}

			// Token: 0x06000102 RID: 258 RVA: 0x00004268 File Offset: 0x00002468
			protected override RfriCallResult OnRpcException(RpcException rpcException)
			{
				return new RfriCallResult(rpcException);
			}

			// Token: 0x0400004E RID: 78
			private readonly RfriAsyncRpcClient rpcClient;

			// Token: 0x0400004F RID: 79
			private readonly string userDn;

			// Token: 0x04000050 RID: 80
			private string serverDn;
		}

		// Token: 0x02000028 RID: 40
		private class GetFqdnRpcCallContext : RpcCallContext<RfriCallResult>
		{
			// Token: 0x06000103 RID: 259 RVA: 0x00004270 File Offset: 0x00002470
			public GetFqdnRpcCallContext(RfriAsyncRpcClient rpcClient, string serverDn, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(timeout, asyncCallback, asyncState)
			{
				Util.ThrowOnNullArgument(rpcClient, "rpcClient");
				Util.ThrowOnNullOrEmptyArgument(serverDn, "serverDn");
				this.rpcClient = rpcClient;
				this.serverDn = serverDn;
			}

			// Token: 0x06000104 RID: 260 RVA: 0x000042A4 File Offset: 0x000024A4
			public RfriCallResult End(IAsyncResult asyncResult, out string serverFqdn)
			{
				RfriCallResult result = base.GetResult();
				serverFqdn = this.serverFqdn;
				return result;
			}

			// Token: 0x06000105 RID: 261 RVA: 0x000042C4 File Offset: 0x000024C4
			protected override ICancelableAsyncResult OnBegin(CancelableAsyncCallback asyncCallback, object asyncState)
			{
				ICancelableAsyncResult result = this.rpcClient.BeginGetFQDNFromLegacyDN(null, null, RfriGetFQDNFromLegacyDNFlags.None, this.serverDn, asyncCallback, asyncState);
				base.StartRpcTimer();
				return result;
			}

			// Token: 0x06000106 RID: 262 RVA: 0x000042F0 File Offset: 0x000024F0
			protected override RfriCallResult OnEnd(ICancelableAsyncResult asyncResult)
			{
				RfriCallResult result;
				try
				{
					RfriStatus rfriStatus = this.rpcClient.EndGetFQDNFromLegacyDN(asyncResult, out this.serverFqdn);
					result = new RfriCallResult(rfriStatus);
				}
				finally
				{
					base.StopAndCleanupRpcTimer();
				}
				return result;
			}

			// Token: 0x06000107 RID: 263 RVA: 0x00004334 File Offset: 0x00002534
			protected override RfriCallResult OnRpcException(RpcException rpcException)
			{
				return new RfriCallResult(rpcException);
			}

			// Token: 0x04000051 RID: 81
			private readonly RfriAsyncRpcClient rpcClient;

			// Token: 0x04000052 RID: 82
			private readonly string serverDn;

			// Token: 0x04000053 RID: 83
			private string serverFqdn;
		}
	}
}
