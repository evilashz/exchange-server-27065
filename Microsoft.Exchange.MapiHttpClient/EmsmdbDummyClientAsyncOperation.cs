using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbDummyClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00004676 File Offset: 0x00002876
		public EmsmdbDummyClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00004681 File Offset: 0x00002881
		protected override string RequestType
		{
			get
			{
				return "EcDoDummy";
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004688 File Offset: 0x00002888
		public void Begin(EmsmdbDummyRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000046C0 File Offset: 0x000028C0
		public ErrorCode End(out EmsmdbDummyResponse response)
		{
			EmsmdbDummyResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new EmsmdbDummyResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
