using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.GroupMetricsGenerator
{
	// Token: 0x020001A5 RID: 421
	internal class GroupMetricsGeneratorTaskContext : DirectoryProcessorBaseTaskContext
	{
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x00061635 File Offset: 0x0005F835
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x0006163D File Offset: 0x0005F83D
		public string LastProcessedGroupDistinguishedName { get; set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x00061646 File Offset: 0x0005F846
		// (set) Token: 0x0600109D RID: 4253 RVA: 0x0006164E File Offset: 0x0005F84E
		public bool AllChunksFinished { get; set; }

		// Token: 0x0600109E RID: 4254 RVA: 0x00061657 File Offset: 0x0005F857
		public GroupMetricsGeneratorTaskContext(MailboxData mailboxData, TimeBasedDatabaseJob job, Queue<TaskQueueItem> taskQueue, AssistantStep step, TaskStatus taskStatus, RunData runData, IList<DirectoryProcessorBaseTask> deferredFinalizeTasks) : base(mailboxData, job, taskQueue, step, taskStatus, runData, deferredFinalizeTasks)
		{
		}
	}
}
