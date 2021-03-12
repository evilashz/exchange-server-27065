using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiQueryRowsClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00007512 File Offset: 0x00005712
		public NspiQueryRowsClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000751D File Offset: 0x0000571D
		protected override string RequestType
		{
			get
			{
				return "QueryRows";
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00007524 File Offset: 0x00005724
		public void Begin(NspiQueryRowsRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000755C File Offset: 0x0000575C
		public ErrorCode End(out NspiQueryRowsResponse response)
		{
			NspiQueryRowsResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new NspiQueryRowsResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
