using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Providers;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x02000082 RID: 130
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PhysicalDatabase : DisposeTrackableBase, IPhysicalDatabase, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x0000BF04 File Offset: 0x0000A104
		public PhysicalDatabase(DirectoryDatabase database, IStorePort storePort, ILogger logger)
		{
			AnchorUtil.ThrowOnNullArgument(database, "database");
			AnchorUtil.ThrowOnNullArgument(storePort, "storePort");
			this.databaseLoadLock = new object();
			this.database = database;
			this.logger = logger;
			this.DatabaseGuid = database.Guid;
			this.physicalMailboxes = new Dictionary<Guid, IPhysicalMailbox>();
			this.nonConnectedMailboxes = new List<IPhysicalMailbox>();
			this.storePort = storePort;
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000BF6F File Offset: 0x0000A16F
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0000BF77 File Offset: 0x0000A177
		public Guid DatabaseGuid { get; private set; }

		// Token: 0x06000495 RID: 1173 RVA: 0x0000BF88 File Offset: 0x0000A188
		public IEnumerable<IPhysicalMailbox> GetConsumerMailboxes()
		{
			this.LoadAllMailboxes();
			return from m in this.physicalMailboxes.Values
			where m.IsConsumer
			select m;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000BFBD File Offset: 0x0000A1BD
		public DatabaseSizeInfo GetDatabaseSpaceData()
		{
			return this.storePort.GetDatabaseSize(this.database);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000BFD0 File Offset: 0x0000A1D0
		public IPhysicalMailbox GetMailbox(Guid mailboxGuid)
		{
			if (!this.mailboxesLoaded)
			{
				try
				{
					MailboxTableEntry mailboxTableEntry = this.storePort.GetMailboxTable(this.database, mailboxGuid, MailboxTablePropertyDefinitions.MailboxTablePropertiesToLoad).FirstOrDefault<MailboxTableEntry>();
					return (mailboxTableEntry == null) ? null : mailboxTableEntry.ToPhysicalMailbox();
				}
				catch (MapiExceptionNotFound mapiExceptionNotFound)
				{
					this.logger.LogVerbose("Could not find the mailbox {0} in database {1}. {2}", new object[]
					{
						mailboxGuid,
						this.database.Identity,
						mapiExceptionNotFound
					});
					return null;
				}
			}
			IPhysicalMailbox result;
			if (this.physicalMailboxes.TryGetValue(mailboxGuid, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000C074 File Offset: 0x0000A274
		public IEnumerable<IPhysicalMailbox> GetNonConnectedMailboxes()
		{
			this.LoadAllMailboxes();
			return this.nonConnectedMailboxes;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000C082 File Offset: 0x0000A282
		public void LoadMailboxes()
		{
			this.LoadAllMailboxes();
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000C08A File Offset: 0x0000A28A
		protected override void InternalDispose(bool disposing)
		{
			this.nonConnectedMailboxes.Clear();
			this.physicalMailboxes.Clear();
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000C0A2 File Offset: 0x0000A2A2
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PhysicalDatabase>(this);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000C0B4 File Offset: 0x0000A2B4
		private void LoadAllMailboxes()
		{
			lock (this.databaseLoadLock)
			{
				if (!this.mailboxesLoaded)
				{
					foreach (IPhysicalMailbox physicalMailbox in from tb in this.storePort.GetMailboxTable(this.database, Guid.Empty, null)
					select tb.ToPhysicalMailbox())
					{
						physicalMailbox.DatabaseName = this.database.Name;
						if (physicalMailbox.IsSoftDeleted || physicalMailbox.IsMoveDestination || physicalMailbox.IsDisabled)
						{
							this.nonConnectedMailboxes.Add(physicalMailbox);
						}
						else
						{
							this.physicalMailboxes[physicalMailbox.Guid] = physicalMailbox;
						}
					}
					this.mailboxesLoaded = true;
				}
			}
		}

		// Token: 0x0400016A RID: 362
		private readonly DirectoryDatabase database;

		// Token: 0x0400016B RID: 363
		private readonly object databaseLoadLock;

		// Token: 0x0400016C RID: 364
		private readonly ILogger logger;

		// Token: 0x0400016D RID: 365
		private readonly List<IPhysicalMailbox> nonConnectedMailboxes;

		// Token: 0x0400016E RID: 366
		private readonly Dictionary<Guid, IPhysicalMailbox> physicalMailboxes;

		// Token: 0x0400016F RID: 367
		private readonly IStorePort storePort;

		// Token: 0x04000170 RID: 368
		private bool mailboxesLoaded;
	}
}
