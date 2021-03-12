using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200000E RID: 14
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbNotificationWaitClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00005816 File Offset: 0x00003A16
		public EmsmdbNotificationWaitClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00005821 File Offset: 0x00003A21
		protected override string RequestType
		{
			get
			{
				return "NotificationWait";
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005828 File Offset: 0x00003A28
		public void Begin(EmsmdbNotificationWaitRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005860 File Offset: 0x00003A60
		public ErrorCode End(out EmsmdbNotificationWaitResponse response)
		{
			EmsmdbNotificationWaitResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new EmsmdbNotificationWaitResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
