using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000085 RID: 133
	internal class FolderTagData
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00025964 File Offset: 0x00023B64
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x0002596C File Offset: 0x00023B6C
		public Guid ArchiveGuid
		{
			get
			{
				return this.archiveGuid;
			}
			set
			{
				this.archiveGuid = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00025975 File Offset: 0x00023B75
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0002597D File Offset: 0x00023B7D
		public Guid RetentionGuid
		{
			get
			{
				return this.retentionGuid;
			}
			set
			{
				this.retentionGuid = value;
			}
		}

		// Token: 0x040003C5 RID: 965
		private Guid archiveGuid = Guid.Empty;

		// Token: 0x040003C6 RID: 966
		private Guid retentionGuid = Guid.Empty;
	}
}
