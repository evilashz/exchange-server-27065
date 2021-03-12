using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200000E RID: 14
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbLegacyConnectAsyncOperation : EmsmdbSecurityContextAsyncOperation
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00004DA2 File Offset: 0x00002FA2
		public EmsmdbLegacyConnectAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.CreateSession)
		{
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004DAC File Offset: 0x00002FAC
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "EcDoConnectEx";
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004DB9 File Offset: 0x00002FB9
		public override string GetTraceBeginParameters(MapiHttpRequest mapiHttpRequest)
		{
			base.CheckDisposed();
			return AsyncOperation.CombineTraceParameters(base.GetTraceBeginParameters(mapiHttpRequest), string.Format("UserDn={0}", this.parameters.UserDn));
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004DE2 File Offset: 0x00002FE2
		public override void ParseRequest(WorkBuffer requestBuffer)
		{
			base.CheckDisposed();
			this.parameters = new ConnectParams(requestBuffer);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005158 File Offset: 0x00003358
		public override async Task ExecuteAsync()
		{
			base.CheckDisposed();
			TimeSpan pollsMax = TimeSpan.MinValue;
			int retryCount = 0;
			TimeSpan retryDelay = TimeSpan.MinValue;
			string dnPrefix = string.Empty;
			string displayName = string.Empty;
			short[] serverVersion = null;
			ArraySegment<byte> segmentExtendedAuxOut = Array<byte>.EmptySegment;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync((AsyncCallback asyncCallback, object asyncState) => EmsmdbHttpHandler.ExchangeAsyncDispatch.BeginConnect(base.ProtocolRequestInfo, base.ClientBinding, this.parameters.UserDn, this.parameters.Flags, this.parameters.ConnectionModulus, this.parameters.CodePage, this.parameters.StringLcid, this.parameters.SortLcid, this.parameters.ClientVersion, this.parameters.SegmentExtendedAuxIn, this.parameters.SegmentExtendedAuxOut, delegate(ICancelableAsyncResult cancelableAsyncResult)
			{
				asyncCallback(cancelableAsyncResult);
			}, asyncState), delegate(IAsyncResult asyncResult)
			{
				IntPtr zero = IntPtr.Zero;
				this.ErrorCode = EmsmdbHttpHandler.ExchangeAsyncDispatch.EndConnect((ICancelableAsyncResult)asyncResult, out zero, out pollsMax, out retryCount, out retryDelay, out dnPrefix, out displayName, out serverVersion, out segmentExtendedAuxOut);
				this.EmsmdbContextHandle = zero;
			});
			base.StatusCode = callResult.StatusCode;
			if (base.StatusCode != 0U)
			{
				this.parameters.SetFailedResponse(base.StatusCode);
			}
			else
			{
				this.parameters.SetSuccessResponse(base.ErrorCode, pollsMax, retryCount, retryDelay, dnPrefix, displayName, serverVersion, segmentExtendedAuxOut);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000519E File Offset: 0x0000339E
		public override void SerializeResponse(out WorkBuffer[] responseBuffers)
		{
			base.CheckDisposed();
			responseBuffers = this.parameters.Serialize();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000051B3 File Offset: 0x000033B3
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.parameters);
			base.InternalDispose();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000051C6 File Offset: 0x000033C6
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EmsmdbLegacyConnectAsyncOperation>(this);
		}

		// Token: 0x04000062 RID: 98
		private ConnectParams parameters;
	}
}
