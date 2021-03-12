using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000021 RID: 33
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiUnbindClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00007712 File Offset: 0x00005912
		public NspiUnbindClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000771D File Offset: 0x0000591D
		protected override string RequestType
		{
			get
			{
				return "Unbind";
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007724 File Offset: 0x00005924
		public void Begin(NspiUnbindRequest unbindRequest)
		{
			base.InternalBegin(new Action<Writer>(unbindRequest.Serialize));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000775C File Offset: 0x0000595C
		public ErrorCode End(out NspiUnbindResponse unbindResponse)
		{
			NspiUnbindResponse localUnbindResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localUnbindResponse = new NspiUnbindResponse(reader);
				return (int)localUnbindResponse.ReturnCode;
			});
			unbindResponse = localUnbindResponse;
			return result;
		}
	}
}
