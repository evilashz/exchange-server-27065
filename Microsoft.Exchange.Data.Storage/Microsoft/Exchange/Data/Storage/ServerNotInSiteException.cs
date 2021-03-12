using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D5D RID: 3421
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ServerNotInSiteException : ServiceDiscoveryPermanentException
	{
		// Token: 0x0600765C RID: 30300 RVA: 0x0020A7AA File Offset: 0x002089AA
		public ServerNotInSiteException(string message, string serverName) : base(message)
		{
			this.ServerName = serverName;
		}

		// Token: 0x0600765D RID: 30301 RVA: 0x0020A7BA File Offset: 0x002089BA
		public ServerNotInSiteException(string message, string serverName, string mailboxDisplayName) : this(message, serverName)
		{
			this.MailboxDisplayName = mailboxDisplayName;
		}

		// Token: 0x040051EF RID: 20975
		public readonly string ServerName;

		// Token: 0x040051F0 RID: 20976
		public readonly string MailboxDisplayName;
	}
}
