using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	public sealed class DatabaseEventWatermark : IConfigurable
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000A62E File Offset: 0x0000882E
		ObjectId IConfigurable.Identity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000A631 File Offset: 0x00008831
		ValidationError[] IConfigurable.Validate()
		{
			return new ValidationError[0];
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000A639 File Offset: 0x00008839
		bool IConfigurable.IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000A63C File Offset: 0x0000883C
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.New;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000A640 File Offset: 0x00008840
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
			DatabaseEventWatermark databaseEventWatermark = source as DatabaseEventWatermark;
			if (databaseEventWatermark == null)
			{
				throw new NotImplementedException(string.Format("Cannot copy changes from type {0}.", source.GetType()));
			}
			this.watermark = databaseEventWatermark.watermark;
			this.consumerGuid = databaseEventWatermark.consumerGuid;
			this.databaseId = databaseEventWatermark.databaseId;
			this.server = databaseEventWatermark.server;
			this.isDatabaseCopyActive = databaseEventWatermark.isDatabaseCopyActive;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000A6A9 File Offset: 0x000088A9
		void IConfigurable.ResetChangeTracking()
		{
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000A6AB File Offset: 0x000088AB
		public DatabaseEventWatermark()
		{
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000A6B3 File Offset: 0x000088B3
		internal DatabaseEventWatermark(Watermark watermark, DatabaseId databaseId, long lastDatabaseCounter, Server server, bool isDatabaseCopyActive)
		{
			this.Instantiate(watermark, databaseId, lastDatabaseCounter, server, isDatabaseCopyActive);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000A6C8 File Offset: 0x000088C8
		internal void Instantiate(Watermark watermark, DatabaseId databaseId, long lastDatabaseCounter, Server server, bool isDatabaseCopyActive)
		{
			if (watermark == null)
			{
				throw new ArgumentNullException("watermark");
			}
			if (null == databaseId)
			{
				throw new ArgumentNullException("databaseId");
			}
			this.watermark = watermark;
			this.databaseId = databaseId;
			this.lastDatabaseCounter = lastDatabaseCounter;
			this.server = server;
			this.isDatabaseCopyActive = isDatabaseCopyActive;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A71C File Offset: 0x0000891C
		public long Counter
		{
			get
			{
				return this.watermark.EventCounter;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A729 File Offset: 0x00008929
		public long LastDatabaseCounter
		{
			get
			{
				return this.lastDatabaseCounter;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A731 File Offset: 0x00008931
		public Guid ConsumerGuid
		{
			get
			{
				return this.watermark.ConsumerGuid;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A740 File Offset: 0x00008940
		public Guid? MailboxGuid
		{
			get
			{
				if (Guid.Empty != this.watermark.MailboxGuid)
				{
					return new Guid?(this.watermark.MailboxGuid);
				}
				return null;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000A77E File Offset: 0x0000897E
		public DatabaseId Database
		{
			get
			{
				return this.databaseId;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000A786 File Offset: 0x00008986
		public Server Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000A78E File Offset: 0x0000898E
		public bool IsDatabaseCopyActive
		{
			get
			{
				return this.isDatabaseCopyActive;
			}
		}

		// Token: 0x040000CC RID: 204
		private Watermark watermark;

		// Token: 0x040000CD RID: 205
		private Guid consumerGuid;

		// Token: 0x040000CE RID: 206
		private DatabaseId databaseId;

		// Token: 0x040000CF RID: 207
		private long lastDatabaseCounter;

		// Token: 0x040000D0 RID: 208
		private Server server;

		// Token: 0x040000D1 RID: 209
		private bool isDatabaseCopyActive;
	}
}
