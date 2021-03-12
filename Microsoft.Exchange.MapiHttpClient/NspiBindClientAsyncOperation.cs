using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiBindClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00005B13 File Offset: 0x00003D13
		public NspiBindClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00005B1E File Offset: 0x00003D1E
		protected override string RequestType
		{
			get
			{
				return "Bind";
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005B25 File Offset: 0x00003D25
		public void Begin(NspiBindRequest bindRequest)
		{
			base.InternalBegin(new Action<Writer>(bindRequest.Serialize));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005B5C File Offset: 0x00003D5C
		public ErrorCode End(out NspiBindResponse bindResponse)
		{
			NspiBindResponse localBindResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localBindResponse = new NspiBindResponse(reader);
				return (int)localBindResponse.ReturnCode;
			});
			bindResponse = localBindResponse;
			return result;
		}
	}
}
