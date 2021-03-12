using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbNotificationWaitAsyncOperation : EmsmdbAsyncOperation
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00006B2F File Offset: 0x00004D2F
		public EmsmdbNotificationWaitAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.AllowInvalidSession)
		{
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00006B3A File Offset: 0x00004D3A
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "NotificationWait";
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00006B47 File Offset: 0x00004D47
		public override TimeSpan InitialFlushPeriod
		{
			get
			{
				return TimeSpan.Zero;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00006B4E File Offset: 0x00004D4E
		public override object DroppedConnectionContextHandle
		{
			get
			{
				base.CheckDisposed();
				return base.ContextHandle;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006B5C File Offset: 0x00004D5C
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new EmsmdbNotificationWaitRequest(reader);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006D68 File Offset: 0x00004F68
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			int flags = 0;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr emsmdbContextHandle = base.EmsmdbContextHandle;
				return EmsmdbHttpHandler.ExchangeAsyncDispatch.BeginNotificationWait(base.ProtocolRequestInfo, emsmdbContextHandle, 0, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)EmsmdbHttpHandler.ExchangeAsyncDispatch.EndNotificationWait((ICancelableAsyncResult)asyncResult, out flags);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new EmsmdbNotificationWaitResponse(returnCode, (uint)flags, Array<byte>.EmptySegment));
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006DAE File Offset: 0x00004FAE
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EmsmdbNotificationWaitAsyncOperation>(this);
		}
	}
}
