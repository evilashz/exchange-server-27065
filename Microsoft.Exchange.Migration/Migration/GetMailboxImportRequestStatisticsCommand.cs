using System;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000147 RID: 327
	internal class GetMailboxImportRequestStatisticsCommand : MrsAccessorCommand
	{
		// Token: 0x06001086 RID: 4230 RVA: 0x00045729 File Offset: 0x00043929
		public GetMailboxImportRequestStatisticsCommand() : base("Get-MailboxImportRequestStatistics", null, null)
		{
		}

		// Token: 0x040005D3 RID: 1491
		internal const string CmdletName = "Get-MailboxImportRequestStatistics";
	}
}
