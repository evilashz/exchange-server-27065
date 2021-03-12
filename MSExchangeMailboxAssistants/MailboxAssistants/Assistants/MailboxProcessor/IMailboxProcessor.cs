using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor
{
	// Token: 0x02000234 RID: 564
	internal interface IMailboxProcessor
	{
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001549 RID: 5449
		// (set) Token: 0x0600154A RID: 5450
		bool IsEnabled { get; set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600154B RID: 5451
		PropTag[] RequiredMailboxTableProperties { get; }

		// Token: 0x0600154C RID: 5452
		void OnStartWorkcycle();

		// Token: 0x0600154D RID: 5453
		void OnStopWorkcycle();

		// Token: 0x0600154E RID: 5454
		void ProcessSingleMailbox(MailboxData mailboxData);
	}
}
