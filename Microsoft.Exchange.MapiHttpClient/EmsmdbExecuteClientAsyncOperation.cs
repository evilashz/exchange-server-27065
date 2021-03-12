using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200000A RID: 10
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbExecuteClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x06000059 RID: 89 RVA: 0x000046F6 File Offset: 0x000028F6
		public EmsmdbExecuteClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004701 File Offset: 0x00002901
		protected override string RequestType
		{
			get
			{
				return "Execute";
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004708 File Offset: 0x00002908
		public void Begin(EmsmdbExecuteRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004740 File Offset: 0x00002940
		public ErrorCode End(out EmsmdbExecuteResponse response)
		{
			EmsmdbExecuteResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new EmsmdbExecuteResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
