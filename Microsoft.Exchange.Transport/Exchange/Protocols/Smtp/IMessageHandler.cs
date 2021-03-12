using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004EB RID: 1259
	internal interface IMessageHandler
	{
		// Token: 0x06003A46 RID: 14918
		MessageHandlerResult Process(CommandContext commandContext, out SmtpResponse smtpResponse);
	}
}
