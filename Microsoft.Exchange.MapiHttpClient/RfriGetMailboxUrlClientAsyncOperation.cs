using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RfriGetMailboxUrlClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x0600013A RID: 314 RVA: 0x000081B2 File Offset: 0x000063B2
		public RfriGetMailboxUrlClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000081BD File Offset: 0x000063BD
		protected override string RequestType
		{
			get
			{
				return "GetMailboxUrl";
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000081C4 File Offset: 0x000063C4
		public void Begin(RfriGetMailboxUrlRequest getMailboxUrlRequest)
		{
			base.InternalBegin(new Action<Writer>(getMailboxUrlRequest.Serialize));
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000081FC File Offset: 0x000063FC
		public ErrorCode End(out RfriGetMailboxUrlResponse getGetMailboxUrlResponse)
		{
			RfriGetMailboxUrlResponse localGetMailboxUrlResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localGetMailboxUrlResponse = new RfriGetMailboxUrlResponse(reader);
				return (int)localGetMailboxUrlResponse.ReturnCode;
			});
			getGetMailboxUrlResponse = localGetMailboxUrlResponse;
			return result;
		}
	}
}
