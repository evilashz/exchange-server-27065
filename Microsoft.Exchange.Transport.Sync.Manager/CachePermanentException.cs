using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class CachePermanentException : LocalizedException
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public CachePermanentException(Guid databaseGuid, Guid mailboxGuid, Exception innerException) : base(Strings.CachePermanentExceptionInfo(databaseGuid, mailboxGuid, innerException.Message), innerException)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			this.databaseGuid = databaseGuid;
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020FF File Offset: 0x000002FF
		protected CachePermanentException(Guid databaseGuid, Guid mailboxGuid, LocalizedString exceptionInfo, Exception innerException) : base(exceptionInfo, innerException)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			this.databaseGuid = databaseGuid;
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002123 File Offset: 0x00000323
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000212B File Offset: 0x0000032B
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x04000001 RID: 1
		private Guid databaseGuid;

		// Token: 0x04000002 RID: 2
		private Guid mailboxGuid;
	}
}
