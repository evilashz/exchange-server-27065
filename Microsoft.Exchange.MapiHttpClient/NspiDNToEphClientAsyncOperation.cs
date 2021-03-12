using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiDNToEphClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00005C12 File Offset: 0x00003E12
		public NspiDNToEphClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00005C1D File Offset: 0x00003E1D
		protected override string RequestType
		{
			get
			{
				return "DNToMId";
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005C24 File Offset: 0x00003E24
		public void Begin(NspiDnToEphRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005C5C File Offset: 0x00003E5C
		public ErrorCode End(out NspiDnToEphResponse response)
		{
			NspiDnToEphResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new NspiDnToEphResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
