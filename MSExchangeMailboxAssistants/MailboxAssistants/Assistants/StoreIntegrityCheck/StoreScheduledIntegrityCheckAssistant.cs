using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.StoreIntegrityCheck
{
	// Token: 0x020001D6 RID: 470
	internal sealed class StoreScheduledIntegrityCheckAssistant : StoreIntegrityCheckAssistantBase
	{
		// Token: 0x060011FA RID: 4602 RVA: 0x00068012 File Offset: 0x00066212
		public StoreScheduledIntegrityCheckAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x0006801D File Offset: 0x0006621D
		protected override IntegrityCheckExecutionFlags ExecutionFlags
		{
			get
			{
				return IntegrityCheckExecutionFlags.ScheduledIntegrityCheckAssistant;
			}
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00068020 File Offset: 0x00066220
		public override List<MailboxData> GetMailboxesToProcess()
		{
			return base.GetReadyToExecuteJobs(IntegrityCheckQueryFlags.ScheduledIntegrityCheckAssistant);
		}
	}
}
