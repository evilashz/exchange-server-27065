using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000027 RID: 39
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RfriGetAddressBookUrlClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x06000136 RID: 310 RVA: 0x00008134 File Offset: 0x00006334
		public RfriGetAddressBookUrlClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000813F File Offset: 0x0000633F
		protected override string RequestType
		{
			get
			{
				return "GetNspiUrl";
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00008146 File Offset: 0x00006346
		public void Begin(RfriGetAddressBookUrlRequest getAddressBookUrlRequest)
		{
			base.InternalBegin(new Action<Writer>(getAddressBookUrlRequest.Serialize));
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000817C File Offset: 0x0000637C
		public ErrorCode End(out RfriGetAddressBookUrlResponse getAddressBookUrlResponse)
		{
			RfriGetAddressBookUrlResponse localGetAddressBookUrlResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localGetAddressBookUrlResponse = new RfriGetAddressBookUrlResponse(reader);
				return (int)localGetAddressBookUrlResponse.ReturnCode;
			});
			getAddressBookUrlResponse = localGetAddressBookUrlResponse;
			return result;
		}
	}
}
