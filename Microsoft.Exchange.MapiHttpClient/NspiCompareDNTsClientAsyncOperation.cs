using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiCompareDNTsClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00005B92 File Offset: 0x00003D92
		public NspiCompareDNTsClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00005B9D File Offset: 0x00003D9D
		protected override string RequestType
		{
			get
			{
				return "CompareDNTs";
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005BA4 File Offset: 0x00003DA4
		public void Begin(NspiCompareDntsRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005BDC File Offset: 0x00003DDC
		public ErrorCode End(out NspiCompareDntsResponse response)
		{
			NspiCompareDntsResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new NspiCompareDntsResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
