using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiGetPropListClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00005D12 File Offset: 0x00003F12
		public NspiGetPropListClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00005D1D File Offset: 0x00003F1D
		protected override string RequestType
		{
			get
			{
				return "GetPropList";
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005D24 File Offset: 0x00003F24
		public void Begin(NspiGetPropListRequest getPropListRequest)
		{
			base.InternalBegin(new Action<Writer>(getPropListRequest.Serialize));
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005D5C File Offset: 0x00003F5C
		public ErrorCode End(out NspiGetPropListResponse getPropListResponse)
		{
			NspiGetPropListResponse localGetPropListResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localGetPropListResponse = new NspiGetPropListResponse(reader);
				return (int)localGetPropListResponse.ReturnCode;
			});
			getPropListResponse = localGetPropListResponse;
			return result;
		}
	}
}
