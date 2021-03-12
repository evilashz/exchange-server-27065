using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001DB RID: 475
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchMailboxNotFound : DiscoverySearchPermanentException
	{
		// Token: 0x06000C64 RID: 3172 RVA: 0x00035764 File Offset: 0x00033964
		public SearchMailboxNotFound(string server, string databaseName, string mailboxGuid) : base(Strings.SearchMailboxNotFound(mailboxGuid, databaseName, server))
		{
		}
	}
}
