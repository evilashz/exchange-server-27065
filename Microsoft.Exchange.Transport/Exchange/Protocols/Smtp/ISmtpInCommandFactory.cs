using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000503 RID: 1283
	internal interface ISmtpInCommandFactory<TEvent> where TEvent : struct
	{
		// Token: 0x06003B3E RID: 15166
		ISmtpInCommand<TEvent> CreateCommand(SmtpInCommand commandType);
	}
}
