using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbConnectClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00004577 File Offset: 0x00002777
		public EmsmdbConnectClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00004582 File Offset: 0x00002782
		protected override string RequestType
		{
			get
			{
				return "Connect";
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004589 File Offset: 0x00002789
		public void Begin(EmsmdbConnectRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000045C0 File Offset: 0x000027C0
		public ErrorCode End(out EmsmdbConnectResponse response)
		{
			EmsmdbConnectResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new EmsmdbConnectResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
