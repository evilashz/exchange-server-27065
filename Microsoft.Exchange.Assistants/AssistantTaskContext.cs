using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200001F RID: 31
	internal class AssistantTaskContext
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x000058D0 File Offset: 0x00003AD0
		public AssistantTaskContext(MailboxData mailboxData, TimeBasedDatabaseJob job, List<KeyValuePair<string, object>> customDataToLog = null)
		{
			this.mailboxData = mailboxData;
			this.job = job;
			this.CustomDataToLog = (customDataToLog ?? new List<KeyValuePair<string, object>>());
			if (job != null)
			{
				this.step = new AssistantStep(job.Assistant.InitialStep);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000591C File Offset: 0x00003B1C
		public MailboxData MailboxData
		{
			get
			{
				return this.mailboxData;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005924 File Offset: 0x00003B24
		public TimeBasedDatabaseJob Job
		{
			get
			{
				return this.job;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000592C File Offset: 0x00003B2C
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00005934 File Offset: 0x00003B34
		public InvokeArgs Args
		{
			get
			{
				return this.args;
			}
			set
			{
				this.args = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000593D File Offset: 0x00003B3D
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00005945 File Offset: 0x00003B45
		public AssistantStep Step
		{
			get
			{
				return this.step;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("step");
				}
				this.step = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000595C File Offset: 0x00003B5C
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00005964 File Offset: 0x00003B64
		public List<KeyValuePair<string, object>> CustomDataToLog { get; set; }

		// Token: 0x040000F1 RID: 241
		private MailboxData mailboxData;

		// Token: 0x040000F2 RID: 242
		private TimeBasedDatabaseJob job;

		// Token: 0x040000F3 RID: 243
		private InvokeArgs args;

		// Token: 0x040000F4 RID: 244
		private AssistantStep step;
	}
}
