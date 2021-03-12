using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.XropService;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000050 RID: 80
	internal sealed class WebServiceCallStateExecute : WebServiceCallState<ExecuteRequest, ExecuteResponse>
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000FADA File Offset: 0x0000DCDA
		public WebServiceCallStateExecute(WebServiceUserInformation userInformation, IExchangeAsyncDispatch exchangeAsyncDispatch, AsyncCallback asyncCallback, object asyncState, IntPtr contextHandle) : base(userInformation, exchangeAsyncDispatch, asyncCallback, asyncState)
		{
			this.contextHandle = contextHandle;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000FAEF File Offset: 0x0000DCEF
		internal IntPtr ContextHandle
		{
			get
			{
				return this.contextHandle;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000FAF7 File Offset: 0x0000DCF7
		protected override string Name
		{
			get
			{
				return "Execute";
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000FAFE File Offset: 0x0000DCFE
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.FailedXropTracer;
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000FB08 File Offset: 0x0000DD08
		protected override void InternalBegin(ExecuteRequest request)
		{
			this.timeStart = ExDateTime.Now;
			if (request == null)
			{
				throw new ServerInvalidArgumentException("request", null);
			}
			if (this.contextHandle == IntPtr.Zero)
			{
				throw new ServerInvalidBindingException(string.Format("Execute being called when we don't have a valid stored context handle.", new object[0]), null);
			}
			if (request.Context != (uint)this.contextHandle.ToInt64())
			{
				throw new ServerInvalidBindingException(string.Format("Execute called with a context handle that doesn't match stored value; request={0}, stored={1}.", request.Context, (uint)this.contextHandle.ToInt64()), null);
			}
			ArraySegment<byte> segmentExtendedRopIn = WebServiceCall.BuildRequestSegment(request.In);
			ArraySegment<byte> responseRopSegment = WebServiceCall.GetResponseRopSegment((int)request.MaxSizeOut, out this.ropOut);
			ArraySegment<byte> segmentExtendedAuxIn = WebServiceCall.BuildRequestSegment(request.AuxIn);
			ArraySegment<byte> responseAuxSegment = WebServiceCall.GetResponseAuxSegment((int)request.MaxSizeAuxOut, out this.auxOut);
			base.ExchangeAsyncDispatch.BeginExecute(null, this.contextHandle, (int)request.Flags, segmentExtendedRopIn, responseRopSegment, segmentExtendedAuxIn, responseAuxSegment, new CancelableAsyncCallback(base.Complete), this);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000FBFF File Offset: 0x0000DDFF
		protected override void InternalBeginCleanup(bool isSuccessful)
		{
			if (!isSuccessful)
			{
				if (this.ropOut != null)
				{
					WebServiceCall.ReleaseBuffer(this.ropOut);
					this.ropOut = null;
				}
				if (this.auxOut != null)
				{
					WebServiceCall.ReleaseBuffer(this.auxOut);
					this.auxOut = null;
				}
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000FC38 File Offset: 0x0000DE38
		protected override ExecuteResponse InternalEnd(ICancelableAsyncResult asyncResult)
		{
			ArraySegment<byte> segment;
			ArraySegment<byte> segment2;
			return new ExecuteResponse
			{
				ErrorCode = (uint)base.ExchangeAsyncDispatch.EndExecute(asyncResult, out this.contextHandle, out segment, out segment2),
				Context = (uint)this.contextHandle.ToInt64(),
				Out = WebServiceCall.BuildResponseArray(segment),
				AuxOut = WebServiceCall.BuildResponseArray(segment2),
				Flags = 0U,
				TransTime = (uint)(ExDateTime.Now - this.timeStart).TotalMilliseconds
			};
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000FCB8 File Offset: 0x0000DEB8
		protected override void InternalEndCleanup()
		{
			if (this.ropOut != null)
			{
				WebServiceCall.ReleaseBuffer(this.ropOut);
				this.ropOut = null;
			}
			if (this.auxOut != null)
			{
				WebServiceCall.ReleaseBuffer(this.auxOut);
				this.auxOut = null;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		protected override ExecuteResponse InternalFailure(uint serviceCode)
		{
			return new ExecuteResponse
			{
				ServiceCode = serviceCode
			};
		}

		// Token: 0x04000179 RID: 377
		private IntPtr contextHandle;

		// Token: 0x0400017A RID: 378
		private byte[] ropOut;

		// Token: 0x0400017B RID: 379
		private byte[] auxOut;

		// Token: 0x0400017C RID: 380
		private ExDateTime timeStart;
	}
}
