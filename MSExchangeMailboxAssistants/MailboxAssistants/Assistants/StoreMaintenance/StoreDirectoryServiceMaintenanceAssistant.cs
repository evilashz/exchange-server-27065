using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.StoreMaintenance
{
	// Token: 0x020001CC RID: 460
	internal class StoreDirectoryServiceMaintenanceAssistant : StoreMaintenanceAssistant
	{
		// Token: 0x060011BF RID: 4543 RVA: 0x00067BAC File Offset: 0x00065DAC
		public StoreDirectoryServiceMaintenanceAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00067BB7 File Offset: 0x00065DB7
		public override List<MailboxData> GetMailboxesToProcess()
		{
			return base.GetMailboxesToProcess(MailboxTableFlags.MaintenanceItemsWithDS);
		}
	}
}
