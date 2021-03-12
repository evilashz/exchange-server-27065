using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004D6 RID: 1238
	internal interface ISmtpInCommand<TEvent> where TEvent : struct
	{
		// Token: 0x06003924 RID: 14628
		Task<ParseAndProcessResult<TEvent>> ParseAndProcessAsync(CommandContext commandContext, CancellationToken cancellationToken);

		// Token: 0x06003925 RID: 14629
		void LogSmtpResponse(SmtpResponse smtpResponse);
	}
}
