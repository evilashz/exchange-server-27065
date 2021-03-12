using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiModPropsClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00007412 File Offset: 0x00005612
		public NspiModPropsClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000741D File Offset: 0x0000561D
		protected override string RequestType
		{
			get
			{
				return "ModProps";
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007424 File Offset: 0x00005624
		public void Begin(NspiModPropsRequest modPropsRequest)
		{
			base.InternalBegin(new Action<Writer>(modPropsRequest.Serialize));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000745C File Offset: 0x0000565C
		public ErrorCode End(out NspiModPropsResponse modPropsResponse)
		{
			NspiModPropsResponse localModPropsResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localModPropsResponse = new NspiModPropsResponse(reader);
				return (int)localModPropsResponse.ReturnCode;
			});
			modPropsResponse = localModPropsResponse;
			return result;
		}
	}
}
