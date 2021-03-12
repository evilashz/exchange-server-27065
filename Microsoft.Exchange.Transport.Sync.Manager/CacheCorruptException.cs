using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class CacheCorruptException : CachePermanentException
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002133 File Offset: 0x00000333
		public CacheCorruptException(Guid databaseGuid, Guid mailboxGuid, Exception innerException) : this(databaseGuid, mailboxGuid, Strings.CacheCorruptExceptionInfo(databaseGuid, mailboxGuid, innerException.Message), innerException)
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000214B File Offset: 0x0000034B
		protected CacheCorruptException(Guid databaseGuid, Guid mailboxGuid, LocalizedString exceptionInfo, Exception innerException) : base(databaseGuid, mailboxGuid, exceptionInfo, innerException)
		{
		}
	}
}
