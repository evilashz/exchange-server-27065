using System;
using System.Data;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000077 RID: 119
	public class MailboxStatisticsCreator : IDataObjectCreator
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x0000F31C File Offset: 0x0000D51C
		public object Create(DataTable table)
		{
			MailboxStatistics mailboxStatistics = new MailboxStatistics();
			mailboxStatistics.Dispose();
			return mailboxStatistics;
		}
	}
}
