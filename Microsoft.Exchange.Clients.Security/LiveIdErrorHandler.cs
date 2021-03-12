using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000028 RID: 40
	internal static class LiveIdErrorHandler
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00009CC8 File Offset: 0x00007EC8
		public static void ThrowRPSException(COMException e)
		{
			switch (RPSErrorHandler.CategorizeRPSException(e))
			{
			case RPSErrorCategory.ConfigurationError:
				throw new LiveConfigurationHRESULTException(e, (uint)e.ErrorCode);
			case RPSErrorCategory.TransientError:
				throw new LiveTransientHRESULTException(e, (uint)e.ErrorCode);
			case RPSErrorCategory.ExternalError:
				throw new LiveExternalHRESULTException(e, (uint)e.ErrorCode);
			case RPSErrorCategory.ClientError:
				throw new LiveClientHRESULTException(e, (uint)e.ErrorCode);
			}
			throw new LiveOperationException(e, (uint)e.ErrorCode);
		}
	}
}
