using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbLegacyNotificationWaitAsyncOperation : EmsmdbAsyncOperation
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x000057AA File Offset: 0x000039AA
		public EmsmdbLegacyNotificationWaitAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.AllowInvalidSession)
		{
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000057B5 File Offset: 0x000039B5
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "EcDoAsyncWaitEx";
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000057C2 File Offset: 0x000039C2
		public override void ParseRequest(WorkBuffer requestBuffer)
		{
			base.CheckDisposed();
			this.parameters = new NotificationWaitParams(requestBuffer);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005A04 File Offset: 0x00003C04
		public override async Task ExecuteAsync()
		{
			base.CheckDisposed();
			int flags = 0;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr emsmdbContextHandle = base.EmsmdbContextHandle;
				return EmsmdbHttpHandler.ExchangeAsyncDispatch.BeginNotificationWait(base.ProtocolRequestInfo, emsmdbContextHandle, this.parameters.Flags, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				this.ErrorCode = EmsmdbHttpHandler.ExchangeAsyncDispatch.EndNotificationWait((ICancelableAsyncResult)asyncResult, out flags);
			});
			base.StatusCode = callResult.StatusCode;
			if (base.StatusCode != 0U)
			{
				this.parameters.SetFailedResponse(base.StatusCode);
			}
			else
			{
				this.parameters.SetSuccessResponse(base.ErrorCode, flags);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005A4A File Offset: 0x00003C4A
		public override void SerializeResponse(out WorkBuffer[] responseBuffers)
		{
			base.CheckDisposed();
			responseBuffers = this.parameters.Serialize();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005A5F File Offset: 0x00003C5F
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.parameters);
			base.InternalDispose();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005A72 File Offset: 0x00003C72
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EmsmdbLegacyNotificationWaitAsyncOperation>(this);
		}

		// Token: 0x04000065 RID: 101
		private NotificationWaitParams parameters;
	}
}
