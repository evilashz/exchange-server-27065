using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200000A RID: 10
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbConnectAsyncOperation : EmsmdbSecurityContextAsyncOperation
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00004150 File Offset: 0x00002350
		public EmsmdbConnectAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.CreateSession)
		{
			context.SetTunnelExpirationTime(MapiHttpHandler.ClientTunnelExpirationTime);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004165 File Offset: 0x00002365
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "Connect";
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004172 File Offset: 0x00002372
		public override string GetTraceBeginParameters(MapiHttpRequest mapiHttpRequest)
		{
			base.CheckDisposed();
			return AsyncOperation.CombineTraceParameters(base.GetTraceBeginParameters(mapiHttpRequest), string.Format("UserDn={0}", ((EmsmdbConnectRequest)mapiHttpRequest).UserDn));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000419B File Offset: 0x0000239B
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new EmsmdbConnectRequest(reader);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000455C File Offset: 0x0000275C
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			EmsmdbConnectRequest request = (EmsmdbConnectRequest)mapiHttpRequest;
			TimeSpan pollsMax = TimeSpan.MinValue;
			int retryCount = 0;
			TimeSpan retryDelay = TimeSpan.MinValue;
			string dnPrefix = string.Empty;
			string displayName = string.Empty;
			short[] serverVersion = null;
			ArraySegment<byte> outputAuxiliaryBuffer = base.AllocateBuffer(4104, 4104);
			MapiHttpVersion clientVersion = null;
			if (!base.Context.TryGetClientVersion(out clientVersion))
			{
				throw ProtocolException.FromResponseCode((LID)40220, "Client version header not found", ResponseCode.MissingHeader, null);
			}
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync((AsyncCallback asyncCallback, object asyncState) => EmsmdbHttpHandler.ExchangeAsyncDispatch.BeginConnect(this.ProtocolRequestInfo, this.ClientBinding, request.UserDn, (int)request.Flags, 0, (int)request.DefaultCodePage, (int)request.StringLocaleId, (int)request.SortLocaleId, (from i in clientVersion.ToQuartet()
			select (short)i).ToArray<short>(), request.AuxiliaryBuffer, outputAuxiliaryBuffer, delegate(ICancelableAsyncResult cancelableAsyncResult)
			{
				asyncCallback(cancelableAsyncResult);
			}, asyncState), delegate(IAsyncResult asyncResult)
			{
				IntPtr zero = IntPtr.Zero;
				returnCode = (uint)EmsmdbHttpHandler.ExchangeAsyncDispatch.EndConnect((ICancelableAsyncResult)asyncResult, out zero, out pollsMax, out retryCount, out retryDelay, out dnPrefix, out displayName, out serverVersion, out outputAuxiliaryBuffer);
				this.EmsmdbContextHandle = zero;
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new EmsmdbConnectResponse(returnCode, (uint)pollsMax.TotalMilliseconds, (uint)retryCount, (uint)retryDelay.TotalMilliseconds, dnPrefix, displayName, outputAuxiliaryBuffer));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000045AA File Offset: 0x000027AA
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EmsmdbConnectAsyncOperation>(this);
		}
	}
}
