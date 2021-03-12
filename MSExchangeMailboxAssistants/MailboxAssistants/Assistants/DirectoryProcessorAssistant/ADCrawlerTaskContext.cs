using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x02000199 RID: 409
	internal class ADCrawlerTaskContext : DirectoryProcessorBaseTaskContext
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x0005E065 File Offset: 0x0005C265
		public ADCrawlerTaskContext(MailboxData mailboxData, TimeBasedDatabaseJob job, Queue<TaskQueueItem> taskQueue, AssistantStep step, TaskStatus taskStatus, RunData runData, IList<DirectoryProcessorBaseTask> deferredFinalizeTasks) : base(mailboxData, job, taskQueue, step, taskStatus, runData, deferredFinalizeTasks)
		{
		}
	}
}
