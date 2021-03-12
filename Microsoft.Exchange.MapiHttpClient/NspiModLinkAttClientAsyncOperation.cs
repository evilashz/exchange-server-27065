using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiModLinkAttClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00007391 File Offset: 0x00005591
		public NspiModLinkAttClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000739C File Offset: 0x0000559C
		protected override string RequestType
		{
			get
			{
				return "ModLinkAtt";
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000073A3 File Offset: 0x000055A3
		public void Begin(NspiModLinkAttRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000073DC File Offset: 0x000055DC
		public ErrorCode End(out NspiModLinkAttResponse response)
		{
			NspiModLinkAttResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new NspiModLinkAttResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
