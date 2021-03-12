using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200000F RID: 15
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbLegacyDisconnectAsyncOperation : EmsmdbAsyncOperation
	{
		// Token: 0x06000093 RID: 147 RVA: 0x000051CE File Offset: 0x000033CE
		public EmsmdbLegacyDisconnectAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence | AsyncOperationCookieFlags.DestroySession)
		{
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000051D9 File Offset: 0x000033D9
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "EcDoDisconnect";
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000051E6 File Offset: 0x000033E6
		public override void ParseRequest(WorkBuffer requestBuffer)
		{
			base.CheckDisposed();
			this.parameters = new DisconnectParams(requestBuffer);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000053EC File Offset: 0x000035EC
		public override async Task ExecuteAsync()
		{
			base.CheckDisposed();
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr emsmdbContextHandle = base.EmsmdbContextHandle;
				return EmsmdbHttpHandler.ExchangeAsyncDispatch.BeginDisconnect(base.ProtocolRequestInfo, emsmdbContextHandle, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				IntPtr emsmdbContextHandle = base.EmsmdbContextHandle;
				base.ErrorCode = EmsmdbHttpHandler.ExchangeAsyncDispatch.EndDisconnect((ICancelableAsyncResult)asyncResult, out emsmdbContextHandle);
				base.EmsmdbContextHandle = emsmdbContextHandle;
			});
			base.StatusCode = callResult.StatusCode;
			if (base.StatusCode != 0U)
			{
				this.parameters.SetFailedResponse(base.StatusCode);
			}
			else
			{
				this.parameters.SetSuccessResponse(base.ErrorCode);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005432 File Offset: 0x00003632
		public override void SerializeResponse(out WorkBuffer[] responseBuffers)
		{
			base.CheckDisposed();
			responseBuffers = this.parameters.Serialize();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005447 File Offset: 0x00003647
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.parameters);
			base.InternalDispose();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000545A File Offset: 0x0000365A
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EmsmdbLegacyDisconnectAsyncOperation>(this);
		}

		// Token: 0x04000063 RID: 99
		private DisconnectParams parameters;
	}
}
