using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000079 RID: 121
	internal class MigrationMailboxNotFoundOnServerException : Exception
	{
		// Token: 0x060006D2 RID: 1746 RVA: 0x0001F028 File Offset: 0x0001D228
		public MigrationMailboxNotFoundOnServerException(string hostServer, string expectedServer, string mailboxName) : base(ServerStrings.MigrationMailboxNotFoundOnServerError(mailboxName, expectedServer, hostServer))
		{
			this.hostServer = hostServer;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001F044 File Offset: 0x0001D244
		public string HostServer
		{
			get
			{
				return this.hostServer;
			}
		}

		// Token: 0x040002DA RID: 730
		private string hostServer;
	}
}
