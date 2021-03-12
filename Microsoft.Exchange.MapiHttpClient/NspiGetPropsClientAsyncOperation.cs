using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiGetPropsClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00005D92 File Offset: 0x00003F92
		public NspiGetPropsClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00005D9D File Offset: 0x00003F9D
		protected override string RequestType
		{
			get
			{
				return "GetProps";
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005DA4 File Offset: 0x00003FA4
		public void Begin(NspiGetPropsRequest getPropsRequest)
		{
			base.InternalBegin(new Action<Writer>(getPropsRequest.Serialize));
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005DDC File Offset: 0x00003FDC
		public ErrorCode End(out NspiGetPropsResponse getPropsResponse)
		{
			NspiGetPropsResponse localGetPropsResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localGetPropsResponse = new NspiGetPropsResponse(reader);
				return (int)localGetPropsResponse.ReturnCode;
			});
			getPropsResponse = localGetPropsResponse;
			return result;
		}
	}
}
