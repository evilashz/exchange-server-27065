using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Office.CompliancePolicy.ComplianceData;

namespace Microsoft.Exchange.Data.ApplicationLogic.Compliance
{
	// Token: 0x020000C5 RID: 197
	internal abstract class ExComplianceItemContainer : ComplianceItemContainer
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000855 RID: 2133
		internal abstract MailboxSession Session { get; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000856 RID: 2134
		internal abstract ComplianceItemPagedReader ComplianceItemPagedReader { get; }
	}
}
