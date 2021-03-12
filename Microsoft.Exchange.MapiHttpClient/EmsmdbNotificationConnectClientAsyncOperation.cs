using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbNotificationConnectClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x06000093 RID: 147 RVA: 0x000057F9 File Offset: 0x000039F9
		public EmsmdbNotificationConnectClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00005804 File Offset: 0x00003A04
		protected override string RequestType
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000580B File Offset: 0x00003A0B
		public void Begin()
		{
			base.InvokeCallback();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005813 File Offset: 0x00003A13
		public ErrorCode End()
		{
			return ErrorCode.None;
		}
	}
}
