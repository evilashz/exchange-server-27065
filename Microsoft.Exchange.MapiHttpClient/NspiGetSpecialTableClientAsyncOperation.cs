using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiGetSpecialTableClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00005E12 File Offset: 0x00004012
		public NspiGetSpecialTableClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005E1D File Offset: 0x0000401D
		protected override string RequestType
		{
			get
			{
				return "GetSpecialTable";
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005E24 File Offset: 0x00004024
		public void Begin(NspiGetSpecialTableRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005E5C File Offset: 0x0000405C
		public ErrorCode End(out NspiGetSpecialTableResponse response)
		{
			NspiGetSpecialTableResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new NspiGetSpecialTableResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
