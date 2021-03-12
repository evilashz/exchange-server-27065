using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.StoreMaintenance
{
	// Token: 0x020001D8 RID: 472
	internal class StoreUrgentMaintenanceAssistant : StoreMaintenanceAssistant
	{
		// Token: 0x0600120A RID: 4618 RVA: 0x00068085 File Offset: 0x00066285
		public StoreUrgentMaintenanceAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00068090 File Offset: 0x00066290
		public override List<MailboxData> GetMailboxesToProcess()
		{
			return base.GetMailboxesToProcess(MailboxTableFlags.UrgentMaintenanceItems);
		}
	}
}
