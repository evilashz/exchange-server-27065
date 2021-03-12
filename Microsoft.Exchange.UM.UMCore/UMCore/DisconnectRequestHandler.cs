using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000123 RID: 291
	internal class DisconnectRequestHandler : RequestHandler
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x0002276C File Offset: 0x0002096C
		protected override ResponseBase Execute(RequestBase requestBase)
		{
			DisconnectRequest disconnectRequest = requestBase as DisconnectRequest;
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Processing a Disconnect request.", new object[0]);
			Guid guid;
			try
			{
				guid = new Guid(disconnectRequest.CallId);
			}
			catch (FormatException innerException)
			{
				throw new InvalidCallIdException(innerException);
			}
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DisconnectRequest, null, new object[]
			{
				disconnectRequest.ProxyAddress,
				disconnectRequest.CallId
			});
			Utils.ThreadPoolQueueUserWorkItem(new WaitCallback(this.DisconnectCallWorkItem), guid);
			return new DisconnectResponse();
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00022804 File Offset: 0x00020A04
		private void DisconnectCallWorkItem(object state)
		{
			Guid sessionGuid = (Guid)state;
			UmServiceGlobals.VoipPlatform.CloseSession(sessionGuid);
		}
	}
}
