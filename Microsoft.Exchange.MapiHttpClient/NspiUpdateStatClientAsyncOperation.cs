using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiUpdateStatClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00007792 File Offset: 0x00005992
		public NspiUpdateStatClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000779D File Offset: 0x0000599D
		protected override string RequestType
		{
			get
			{
				return "UpdateStat";
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000077A4 File Offset: 0x000059A4
		public void Begin(NspiUpdateStatRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000077DC File Offset: 0x000059DC
		public ErrorCode End(out NspiUpdateStatResponse response)
		{
			NspiUpdateStatResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new NspiUpdateStatResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
