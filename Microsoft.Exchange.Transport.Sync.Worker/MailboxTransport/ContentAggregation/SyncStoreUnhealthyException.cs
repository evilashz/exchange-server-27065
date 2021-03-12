using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SyncStoreUnhealthyException : NonPromotableTransientException
	{
		// Token: 0x06000212 RID: 530 RVA: 0x000097F9 File Offset: 0x000079F9
		public SyncStoreUnhealthyException(Guid databaseGuid, int backoff) : base(Strings.SyncStoreUnhealthyExceptionInfo(databaseGuid, backoff))
		{
			this.backoff = backoff;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000980F File Offset: 0x00007A0F
		internal int Backoff
		{
			get
			{
				return this.backoff;
			}
		}

		// Token: 0x04000118 RID: 280
		private int backoff;
	}
}
