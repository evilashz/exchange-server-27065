using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.StoreIntegrityCheck
{
	// Token: 0x020001D0 RID: 464
	internal sealed class StoreIntegrityCheckAssistant : StoreIntegrityCheckAssistantBase
	{
		// Token: 0x060011DD RID: 4573 RVA: 0x00067F44 File Offset: 0x00066144
		public StoreIntegrityCheckAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x00067F4F File Offset: 0x0006614F
		protected override IntegrityCheckExecutionFlags ExecutionFlags
		{
			get
			{
				return IntegrityCheckExecutionFlags.OnlineIntegrityCheckAssistant;
			}
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00067F52 File Offset: 0x00066152
		public override List<MailboxData> GetMailboxesToProcess()
		{
			return base.GetReadyToExecuteJobs(IntegrityCheckQueryFlags.OnlineIntegrityCheckAssistant);
		}
	}
}
