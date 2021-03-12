using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000E3 RID: 227
	internal interface IReportHelper
	{
		// Token: 0x060008BD RID: 2237
		void Load(ReportData report, MapiStore storeToUse);

		// Token: 0x060008BE RID: 2238
		void Flush(ReportData report, MapiStore storeToUse);

		// Token: 0x060008BF RID: 2239
		void Delete(ReportData report, MapiStore storeToUse);
	}
}
