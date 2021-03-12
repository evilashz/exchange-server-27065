using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiGetMatchesClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00005C92 File Offset: 0x00003E92
		public NspiGetMatchesClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005C9D File Offset: 0x00003E9D
		protected override string RequestType
		{
			get
			{
				return "GetMatches";
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public void Begin(NspiGetMatchesRequest getMatchesRequest)
		{
			base.InternalBegin(new Action<Writer>(getMatchesRequest.Serialize));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005CDC File Offset: 0x00003EDC
		public ErrorCode End(out NspiGetMatchesResponse getMatchesResponse)
		{
			NspiGetMatchesResponse localGetMatchesResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localGetMatchesResponse = new NspiGetMatchesResponse(reader);
				return (int)localGetMatchesResponse.ReturnCode;
			});
			getMatchesResponse = localGetMatchesResponse;
			return result;
		}
	}
}
