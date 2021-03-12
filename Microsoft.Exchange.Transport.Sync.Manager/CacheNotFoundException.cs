using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class CacheNotFoundException : CachePermanentException
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000244A File Offset: 0x0000064A
		public CacheNotFoundException(Guid databaseGuid, Guid mailboxGuid) : base(databaseGuid, mailboxGuid, Strings.CacheNotFoundExceptionInfo(databaseGuid, mailboxGuid), null)
		{
		}
	}
}
