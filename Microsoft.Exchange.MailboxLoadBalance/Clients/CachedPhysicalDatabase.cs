using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Clients
{
	// Token: 0x02000030 RID: 48
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CachedPhysicalDatabase : CachedClient, IPhysicalDatabase, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000186 RID: 390 RVA: 0x00006FCD File Offset: 0x000051CD
		public CachedPhysicalDatabase(IPhysicalDatabase database) : base(null)
		{
			this.database = database;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00006FDD File Offset: 0x000051DD
		Guid IPhysicalDatabase.DatabaseGuid
		{
			get
			{
				return this.database.DatabaseGuid;
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006FEA File Offset: 0x000051EA
		public void LoadMailboxes()
		{
			this.database.LoadMailboxes();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006FF7 File Offset: 0x000051F7
		IEnumerable<IPhysicalMailbox> IPhysicalDatabase.GetConsumerMailboxes()
		{
			return this.database.GetConsumerMailboxes();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007004 File Offset: 0x00005204
		DatabaseSizeInfo IPhysicalDatabase.GetDatabaseSpaceData()
		{
			return this.database.GetDatabaseSpaceData();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007011 File Offset: 0x00005211
		IPhysicalMailbox IPhysicalDatabase.GetMailbox(Guid mailboxGuid)
		{
			return this.database.GetMailbox(mailboxGuid);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000701F File Offset: 0x0000521F
		IEnumerable<IPhysicalMailbox> IPhysicalDatabase.GetNonConnectedMailboxes()
		{
			return this.database.GetNonConnectedMailboxes();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000702C File Offset: 0x0000522C
		internal override void Cleanup()
		{
			this.database.Dispose();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007039 File Offset: 0x00005239
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CachedPhysicalDatabase>(this);
		}

		// Token: 0x04000092 RID: 146
		private readonly IPhysicalDatabase database;
	}
}
