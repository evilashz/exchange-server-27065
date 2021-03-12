using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class CacheTransientException : TransientException
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000245C File Offset: 0x0000065C
		public CacheTransientException(Guid databaseGuid, Guid mailboxGuid, LocalizedString transientConditionInfo) : base(Strings.CacheTransientExceptionInfo(databaseGuid, mailboxGuid, transientConditionInfo))
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			this.databaseGuid = databaseGuid;
			this.mailboxGuid = mailboxGuid;
			this.storeRequestedBackOff = false;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002494 File Offset: 0x00000694
		public CacheTransientException(Guid databaseGuid, Guid mailboxGuid, Exception innerException) : base(Strings.CacheTransientExceptionInfo(databaseGuid, mailboxGuid, innerException.Message), innerException)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			SyncUtilities.ThrowIfArgumentNull("innerException", innerException);
			this.databaseGuid = databaseGuid;
			this.mailboxGuid = mailboxGuid;
			Exception[] array = new Exception[]
			{
				innerException,
				innerException.InnerException
			};
			foreach (Exception ex in array)
			{
				if (ex is MapiExceptionMaxObjsExceeded || ex is MapiExceptionRpcServerTooBusy)
				{
					this.storeRequestedBackOff = true;
					return;
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002520 File Offset: 0x00000720
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002528 File Offset: 0x00000728
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002530 File Offset: 0x00000730
		public bool StoreRequestedBackOff
		{
			get
			{
				return this.storeRequestedBackOff;
			}
		}

		// Token: 0x04000006 RID: 6
		private Guid databaseGuid;

		// Token: 0x04000007 RID: 7
		private Guid mailboxGuid;

		// Token: 0x04000008 RID: 8
		private bool storeRequestedBackOff;
	}
}
