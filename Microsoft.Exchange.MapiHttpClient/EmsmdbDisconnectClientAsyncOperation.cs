using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbDisconnectClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x06000051 RID: 81 RVA: 0x000045F6 File Offset: 0x000027F6
		public EmsmdbDisconnectClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00004601 File Offset: 0x00002801
		protected override string RequestType
		{
			get
			{
				return "Disconnect";
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004608 File Offset: 0x00002808
		public void Begin(EmsmdbDisconnectRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004640 File Offset: 0x00002840
		public ErrorCode End(out EmsmdbDisconnectResponse response)
		{
			EmsmdbDisconnectResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new EmsmdbDisconnectResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
