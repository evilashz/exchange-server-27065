using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PingClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00008110 File Offset: 0x00006310
		public PingClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000811B File Offset: 0x0000631B
		protected override string RequestType
		{
			get
			{
				return "PING";
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00008122 File Offset: 0x00006322
		public void Begin()
		{
			base.InternalBegin(null);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000812B File Offset: 0x0000632B
		public ErrorCode End()
		{
			return base.InternalEnd(null);
		}
	}
}
