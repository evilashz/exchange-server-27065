using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x02000198 RID: 408
	internal class DirectoryProcessorBaseTaskContext : AssistantTaskContext
	{
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x0005DFCE File Offset: 0x0005C1CE
		internal string ClassName
		{
			get
			{
				if (this.className == null)
				{
					this.className = base.GetType().Name;
				}
				return this.className;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x0005DFEF File Offset: 0x0005C1EF
		// (set) Token: 0x06001010 RID: 4112 RVA: 0x0005DFF7 File Offset: 0x0005C1F7
		public TaskStatus TaskStatus { get; set; }

		// Token: 0x06001011 RID: 4113 RVA: 0x0005E000 File Offset: 0x0005C200
		public DirectoryProcessorBaseTaskContext(MailboxData mailboxData, TimeBasedDatabaseJob job, Queue<TaskQueueItem> taskQueue, AssistantStep step, TaskStatus taskStatus, RunData runData, IList<DirectoryProcessorBaseTask> deferredFinalizeTasks) : base(mailboxData, job, null)
		{
			base.Step = step;
			this.TaskQueue = taskQueue;
			this.TaskStatus = taskStatus;
			this.DeferredFinalizeTasks = deferredFinalizeTasks;
			this.RunData = runData;
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x0005E032 File Offset: 0x0005C232
		// (set) Token: 0x06001013 RID: 4115 RVA: 0x0005E03A File Offset: 0x0005C23A
		public Queue<TaskQueueItem> TaskQueue { get; private set; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0005E043 File Offset: 0x0005C243
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x0005E04B File Offset: 0x0005C24B
		public IList<DirectoryProcessorBaseTask> DeferredFinalizeTasks { get; private set; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0005E054 File Offset: 0x0005C254
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x0005E05C File Offset: 0x0005C25C
		public RunData RunData { get; private set; }

		// Token: 0x04000A2D RID: 2605
		private string className;
	}
}
