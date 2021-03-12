using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.XropService;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000051 RID: 81
	internal sealed class WebServiceCallStateDisconnect : WebServiceCallState<DisconnectRequest, DisconnectResponse>
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0000FD0B File Offset: 0x0000DF0B
		public WebServiceCallStateDisconnect(WebServiceUserInformation userInformation, IExchangeAsyncDispatch exchangeAsyncDispatch, AsyncCallback asyncCallback, object asyncState, IntPtr contextHandle) : base(userInformation, exchangeAsyncDispatch, asyncCallback, asyncState)
		{
			this.contextHandle = contextHandle;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000FD20 File Offset: 0x0000DF20
		internal IntPtr ContextHandle
		{
			get
			{
				return this.contextHandle;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000FD28 File Offset: 0x0000DF28
		protected override string Name
		{
			get
			{
				return "Disconnect";
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000FD2F File Offset: 0x0000DF2F
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ConnectXropTracer;
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000FD38 File Offset: 0x0000DF38
		protected override void InternalBegin(DisconnectRequest request)
		{
			if (request == null)
			{
				throw new ServerInvalidArgumentException("request", null);
			}
			if (this.contextHandle == IntPtr.Zero)
			{
				throw new ServerInvalidBindingException(string.Format("Disconnect being called when we don't have a valid stored context handle.", new object[0]), null);
			}
			if (request.Context != (uint)this.contextHandle.ToInt64())
			{
				throw new ServerInvalidBindingException(string.Format("Disconnect called with a context handle that doesn't match stored value; request={0}, stored={1}.", request.Context, (uint)this.contextHandle.ToInt64()), null);
			}
			base.ExchangeAsyncDispatch.BeginDisconnect(null, this.contextHandle, new CancelableAsyncCallback(base.Complete), this);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000FDDE File Offset: 0x0000DFDE
		protected override void InternalBeginCleanup(bool isSuccessful)
		{
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
		protected override DisconnectResponse InternalEnd(ICancelableAsyncResult asyncResult)
		{
			return new DisconnectResponse
			{
				ErrorCode = (uint)base.ExchangeAsyncDispatch.EndDisconnect(asyncResult, out this.contextHandle),
				Context = (uint)this.contextHandle.ToInt64()
			};
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000FE1E File Offset: 0x0000E01E
		protected override void InternalEndCleanup()
		{
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000FE20 File Offset: 0x0000E020
		protected override DisconnectResponse InternalFailure(uint serviceCode)
		{
			return new DisconnectResponse
			{
				ServiceCode = serviceCode
			};
		}

		// Token: 0x0400017D RID: 381
		private IntPtr contextHandle;
	}
}
