using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200001E RID: 30
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiResolveNamesClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00007592 File Offset: 0x00005792
		public NspiResolveNamesClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000759D File Offset: 0x0000579D
		protected override string RequestType
		{
			get
			{
				return "ResolveNames";
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000075A4 File Offset: 0x000057A4
		public void Begin(NspiResolveNamesRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000075DC File Offset: 0x000057DC
		public ErrorCode End(out NspiResolveNamesResponse response)
		{
			NspiResolveNamesResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new NspiResolveNamesResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
