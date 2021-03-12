using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000073 RID: 115
	internal class TagChange
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x0001DAB8 File Offset: 0x0001BCB8
		internal TagChange()
		{
			this.deletedTags = new List<Guid>();
			this.tagsWithContentSettingsChange = new List<Guid>();
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001DAD6 File Offset: 0x0001BCD6
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x0001DADE File Offset: 0x0001BCDE
		internal ChangeType ChangeType
		{
			get
			{
				return this.changeType;
			}
			set
			{
				this.changeType = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001DAE7 File Offset: 0x0001BCE7
		internal List<Guid> DeletedTags
		{
			get
			{
				return this.deletedTags;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0001DAEF File Offset: 0x0001BCEF
		internal List<Guid> TagsWithContentSettingsChange
		{
			get
			{
				return this.tagsWithContentSettingsChange;
			}
		}

		// Token: 0x0400034C RID: 844
		private ChangeType changeType;

		// Token: 0x0400034D RID: 845
		private List<Guid> deletedTags;

		// Token: 0x0400034E RID: 846
		private List<Guid> tagsWithContentSettingsChange;
	}
}
