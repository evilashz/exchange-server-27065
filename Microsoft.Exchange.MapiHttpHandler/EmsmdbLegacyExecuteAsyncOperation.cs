using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbLegacyExecuteAsyncOperation : EmsmdbAsyncOperation
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00005462 File Offset: 0x00003662
		public EmsmdbLegacyExecuteAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000546D File Offset: 0x0000366D
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "EcDoRpcExt2";
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000547A File Offset: 0x0000367A
		public override void ParseRequest(WorkBuffer requestBuffer)
		{
			base.CheckDisposed();
			this.parameters = new ExecuteParams(requestBuffer);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005734 File Offset: 0x00003934
		public override async Task ExecuteAsync()
		{
			base.CheckDisposed();
			ArraySegment<byte> segmentExtendedRopOut = Array<byte>.EmptySegment;
			ArraySegment<byte> segmentExtendedAuxOut = Array<byte>.EmptySegment;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr emsmdbContextHandle = base.EmsmdbContextHandle;
				return EmsmdbHttpHandler.ExchangeAsyncDispatch.BeginExecute(base.ProtocolRequestInfo, emsmdbContextHandle, this.parameters.Flags, this.parameters.SegmentExtendedRopIn, this.parameters.SegmentExtendedRopOut, this.parameters.SegmentExtendedAuxIn, this.parameters.SegmentExtendedAuxOut, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				IntPtr emsmdbContextHandle = this.EmsmdbContextHandle;
				this.ErrorCode = EmsmdbHttpHandler.ExchangeAsyncDispatch.EndExecute((ICancelableAsyncResult)asyncResult, out emsmdbContextHandle, out segmentExtendedRopOut, out segmentExtendedAuxOut);
				this.EmsmdbContextHandle = emsmdbContextHandle;
			});
			base.StatusCode = callResult.StatusCode;
			if (base.StatusCode != 0U)
			{
				this.parameters.SetFailedResponse(base.StatusCode);
			}
			else
			{
				this.parameters.SetSuccessResponse(base.ErrorCode, segmentExtendedRopOut, segmentExtendedAuxOut);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000577A File Offset: 0x0000397A
		public override void SerializeResponse(out WorkBuffer[] responseBuffers)
		{
			base.CheckDisposed();
			responseBuffers = this.parameters.Serialize();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000578F File Offset: 0x0000398F
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.parameters);
			base.InternalDispose();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000057A2 File Offset: 0x000039A2
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EmsmdbLegacyExecuteAsyncOperation>(this);
		}

		// Token: 0x04000064 RID: 100
		private ExecuteParams parameters;
	}
}
