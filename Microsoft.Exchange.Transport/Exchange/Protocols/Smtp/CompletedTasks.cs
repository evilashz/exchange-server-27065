using System;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000506 RID: 1286
	internal static class CompletedTasks
	{
		// Token: 0x04001DE4 RID: 7652
		public static readonly Task<SmtpResponse> SmtpResponseEmpty = Task.FromResult<SmtpResponse>(SmtpResponse.Empty);

		// Token: 0x04001DE5 RID: 7653
		public static readonly Task<SmtpResponse> SmtpResponseUnrecognizedCommand = Task.FromResult<SmtpResponse>(SmtpResponse.UnrecognizedCommand);

		// Token: 0x04001DE6 RID: 7654
		public static readonly Task<SmtpResponse> SmtpResponseBadCommandSequence = Task.FromResult<SmtpResponse>(SmtpResponse.BadCommandSequence);
	}
}
