using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000014 RID: 20
	internal class AnchorMailboxNotFoundOnServerException : LocalizedException
	{
		// Token: 0x060000FD RID: 253 RVA: 0x0000453E File Offset: 0x0000273E
		public AnchorMailboxNotFoundOnServerException(string hostServer, string expectedServer, string mailboxName) : base(ServerStrings.MigrationMailboxNotFoundOnServerError(mailboxName, expectedServer, hostServer))
		{
			this.hostServer = hostServer;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004555 File Offset: 0x00002755
		public string HostServer
		{
			get
			{
				return this.hostServer;
			}
		}

		// Token: 0x0400005B RID: 91
		private readonly string hostServer;
	}
}
