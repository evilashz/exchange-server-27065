using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x020001B1 RID: 433
	internal class TaskQueueItem
	{
		// Token: 0x060010FD RID: 4349 RVA: 0x000630D3 File Offset: 0x000612D3
		public TaskQueueItem(DirectoryProcessorBaseTask task, RecipientType recipientType)
		{
			this.Task = task;
			this.TaskRecipientType = recipientType;
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x000630E9 File Offset: 0x000612E9
		// (set) Token: 0x060010FF RID: 4351 RVA: 0x000630F1 File Offset: 0x000612F1
		public DirectoryProcessorBaseTask Task { get; set; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x000630FA File Offset: 0x000612FA
		// (set) Token: 0x06001101 RID: 4353 RVA: 0x00063102 File Offset: 0x00061302
		public RecipientType TaskRecipientType { get; set; }
	}
}
