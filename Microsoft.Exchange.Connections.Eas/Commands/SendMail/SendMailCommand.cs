using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.SendMail
{
	// Token: 0x0200005F RID: 95
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SendMailCommand : EasServerCommand<SendMailRequest, SendMailResponse, SendMailStatus>
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x000053E6 File Offset: 0x000035E6
		internal SendMailCommand(EasConnectionSettings easConnectionSettings) : base(Command.SendMail, easConnectionSettings)
		{
		}
	}
}
