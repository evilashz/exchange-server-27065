using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000018 RID: 24
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiGetTemplateInfoClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00005E92 File Offset: 0x00004092
		public NspiGetTemplateInfoClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005E9D File Offset: 0x0000409D
		protected override string RequestType
		{
			get
			{
				return "GetTemplateInfo";
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005EA4 File Offset: 0x000040A4
		public void Begin(NspiGetTemplateInfoRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005EDC File Offset: 0x000040DC
		public ErrorCode End(out NspiGetTemplateInfoResponse response)
		{
			NspiGetTemplateInfoResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new NspiGetTemplateInfoResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
