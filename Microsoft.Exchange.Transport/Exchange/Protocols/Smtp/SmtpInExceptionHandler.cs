using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004BD RID: 1213
	internal static class SmtpInExceptionHandler
	{
		// Token: 0x060036B3 RID: 14003 RVA: 0x000E0A58 File Offset: 0x000DEC58
		public static bool ShouldHandleException(Exception exception, ITracer tracer, TransportMailItem transportMailItem, out SmtpResponse response)
		{
			ArgumentValidator.ThrowIfNull("exception", exception);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			tracer.TraceError<long, Exception>(0L, "Leaked exception detected (msgId={0}): {1}", (transportMailItem != null) ? transportMailItem.RecordId : -1L, exception);
			if (ExceptionHelper.HandleLeakedException)
			{
				if (ExceptionHelper.IsHandleablePermanentException(exception))
				{
					response = SmtpResponse.InvalidContent;
					return true;
				}
				if (ExceptionHelper.IsHandleableTransientException(exception))
				{
					response = SmtpResponse.ConnectionDroppedByAgentError;
					return true;
				}
			}
			response = SmtpResponse.Empty;
			return false;
		}
	}
}
