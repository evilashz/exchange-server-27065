using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000055 RID: 85
	internal abstract class EnforcerBase
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x00012B74 File Offset: 0x00010D74
		internal EnforcerBase(MailboxDataForFolders mailboxData, ElcFolderSubAssistant elcAssistant)
		{
			this.mailboxData = mailboxData;
			this.elcAssistant = elcAssistant;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00012B8A File Offset: 0x00010D8A
		internal MailboxDataForFolders MailboxData
		{
			get
			{
				return this.mailboxData;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00012B92 File Offset: 0x00010D92
		internal ElcFolderSubAssistant Assistant
		{
			get
			{
				return this.elcAssistant;
			}
		}

		// Token: 0x060002FB RID: 763
		internal abstract bool IsEnabled(ProvisionedFolder provisionedFolder);

		// Token: 0x060002FC RID: 764
		internal abstract void SetItemQueryFlags(ProvisionedFolder provisionedFolder, ItemFinder itemFinder);

		// Token: 0x060002FD RID: 765
		internal abstract void Invoke(ProvisionedFolder provisionedFolder, List<object[]> items, PropertyIndexHolder propertyIndexHolder);

		// Token: 0x04000278 RID: 632
		private MailboxDataForFolders mailboxData;

		// Token: 0x04000279 RID: 633
		private ElcFolderSubAssistant elcAssistant;
	}
}
