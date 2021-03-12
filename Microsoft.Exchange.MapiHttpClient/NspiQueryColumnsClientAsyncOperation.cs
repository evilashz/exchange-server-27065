using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200001C RID: 28
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiQueryColumnsClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00007492 File Offset: 0x00005692
		public NspiQueryColumnsClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000749D File Offset: 0x0000569D
		protected override string RequestType
		{
			get
			{
				return "QueryColumns";
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000074A4 File Offset: 0x000056A4
		public void Begin(NspiQueryColumnsRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000074DC File Offset: 0x000056DC
		public ErrorCode End(out NspiQueryColumnsResponse response)
		{
			NspiQueryColumnsResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new NspiQueryColumnsResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
