using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x020000F8 RID: 248
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SkippedItemUtilities
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x00023348 File Offset: 0x00021548
		public static ReportData GetReportData(Guid guid)
		{
			return new ReportData(guid, ReportVersion.ReportE14R6Compression, "TransportSync Reports", "IPM.MS-Exchange.TransportSyncReports");
		}

		// Token: 0x040003FD RID: 1021
		private const string ReportFolderName = "TransportSync Reports";

		// Token: 0x040003FE RID: 1022
		private const string ReportMessageClass = "IPM.MS-Exchange.TransportSyncReports";
	}
}
