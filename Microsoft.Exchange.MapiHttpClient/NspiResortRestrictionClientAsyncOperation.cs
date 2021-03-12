using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiResortRestrictionClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00007612 File Offset: 0x00005812
		public NspiResortRestrictionClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000761D File Offset: 0x0000581D
		protected override string RequestType
		{
			get
			{
				return "ResortRestriction";
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007624 File Offset: 0x00005824
		public void Begin(NspiResortRestrictionRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000765C File Offset: 0x0000585C
		public ErrorCode End(out NspiResortRestrictionResponse response)
		{
			NspiResortRestrictionResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new NspiResortRestrictionResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
