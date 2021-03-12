using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMDtmfMapGenerator
{
	// Token: 0x020001A0 RID: 416
	internal class DtmfMapGeneratorTaskContext : DirectoryProcessorBaseTaskContext, IDisposable
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0005FCC8 File Offset: 0x0005DEC8
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x0005FCD0 File Offset: 0x0005DED0
		public XmlReader AdEntriesReader { get; private set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0005FCD9 File Offset: 0x0005DED9
		// (set) Token: 0x06001067 RID: 4199 RVA: 0x0005FCE1 File Offset: 0x0005DEE1
		public DtmfMapGenerationMetadata Metadata { get; private set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x0005FCEA File Offset: 0x0005DEEA
		// (set) Token: 0x06001069 RID: 4201 RVA: 0x0005FCF2 File Offset: 0x0005DEF2
		public bool IsFullUpdate { get; private set; }

		// Token: 0x0600106A RID: 4202 RVA: 0x0005FCFC File Offset: 0x0005DEFC
		public DtmfMapGeneratorTaskContext(MailboxData mailboxData, TimeBasedDatabaseJob job, Queue<TaskQueueItem> taskQueue, AssistantStep step, TaskStatus taskStatus, XmlReader adEntriesReader, DtmfMapGenerationMetadata metadata, bool isFullUpdate, RunData runData, IList<DirectoryProcessorBaseTask> deferredFinalizeTasks) : base(mailboxData, job, taskQueue, step, taskStatus, runData, deferredFinalizeTasks)
		{
			ValidateArgument.NotNull(adEntriesReader, "adEntriesReader");
			ValidateArgument.NotNull(metadata, "metadata");
			this.AdEntriesReader = adEntriesReader;
			this.Metadata = metadata;
			this.IsFullUpdate = isFullUpdate;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0005FD4A File Offset: 0x0005DF4A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0005FD59 File Offset: 0x0005DF59
		protected virtual void Dispose(bool disposing)
		{
			if (this.AdEntriesReader != null)
			{
				if (disposing)
				{
					this.AdEntriesReader.Close();
				}
				this.AdEntriesReader = null;
			}
		}
	}
}
