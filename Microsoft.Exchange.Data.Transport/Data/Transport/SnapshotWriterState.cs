using System;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x020000B0 RID: 176
	internal sealed class SnapshotWriterState
	{
		// Token: 0x060003DE RID: 990 RVA: 0x00008F49 File Offset: 0x00007149
		public SnapshotWriterState()
		{
			this.id = Guid.NewGuid();
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00008F5C File Offset: 0x0000715C
		public string Id
		{
			get
			{
				return this.id.ToString();
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00008F7D File Offset: 0x0000717D
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x00008F85 File Offset: 0x00007185
		public int Sequence
		{
			get
			{
				return this.sequence;
			}
			set
			{
				this.sequence = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00008F8E File Offset: 0x0000718E
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x00008F9B File Offset: 0x0000719B
		public bool IsOriginalWritten
		{
			get
			{
				return (this.events & SnapshotWriterEvents.OriginalWritten) > (SnapshotWriterEvents)0;
			}
			set
			{
				if (value)
				{
					this.events |= SnapshotWriterEvents.OriginalWritten;
				}
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00008FAE File Offset: 0x000071AE
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x00008FBB File Offset: 0x000071BB
		public bool IsFolderCreated
		{
			get
			{
				return (this.events & SnapshotWriterEvents.FolderCreated) > (SnapshotWriterEvents)0;
			}
			set
			{
				if (value)
				{
					this.events |= SnapshotWriterEvents.FolderCreated;
				}
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00008FCE File Offset: 0x000071CE
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x00008FD6 File Offset: 0x000071D6
		public int? OriginalWriterAgentId
		{
			get
			{
				return this.originalWriterAgentId;
			}
			set
			{
				this.originalWriterAgentId = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00008FDF File Offset: 0x000071DF
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00008FE7 File Offset: 0x000071E7
		public string LastPreProcessedSnapshotTopic
		{
			get
			{
				return this.lastPreProcessedSnapshotTopic;
			}
			set
			{
				this.lastPreProcessedSnapshotTopic = value;
			}
		}

		// Token: 0x04000213 RID: 531
		private readonly Guid id;

		// Token: 0x04000214 RID: 532
		private int sequence;

		// Token: 0x04000215 RID: 533
		private SnapshotWriterEvents events;

		// Token: 0x04000216 RID: 534
		private int? originalWriterAgentId;

		// Token: 0x04000217 RID: 535
		private string lastPreProcessedSnapshotTopic;
	}
}
