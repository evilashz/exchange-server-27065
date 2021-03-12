using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000014 RID: 20
	public sealed class SearchQueueTable
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x0002A198 File Offset: 0x00028398
		internal SearchQueueTable()
		{
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.searchFolderId = Factory.CreatePhysicalColumn("SearchFolderId", "SearchFolderId", typeof(byte[]), false, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.userSid = Factory.CreatePhysicalColumn("UserSid", "UserSid", typeof(byte[]), false, false, false, false, false, Visibility.Public, 0, 512, 512);
			this.clientType = Factory.CreatePhysicalColumn("ClientType", "ClientType", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.lCID = Factory.CreatePhysicalColumn("LCID", "LCID", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.incrementalScopeFolderId = Factory.CreatePhysicalColumn("IncrementalScopeFolderId", "IncrementalScopeFolderId", typeof(byte[]), true, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.creationTime = Factory.CreatePhysicalColumn("CreationTime", "CreationTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.populationAttempts = Factory.CreatePhysicalColumn("PopulationAttempts", "PopulationAttempts", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			string name = "SearchQueuePK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			this.searchQueuePK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxNumber,
				this.SearchFolderId
			});
			Index[] indexes = new Index[]
			{
				this.SearchQueuePK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxNumber,
				this.SearchFolderId,
				this.UserSid,
				this.ClientType,
				this.LCID,
				this.IncrementalScopeFolderId,
				this.CreationTime,
				this.PopulationAttempts,
				this.ExtensionBlob
			};
			this.table = Factory.CreateTable("SearchQueue", TableClass.SearchQueue, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0002A423 File Offset: 0x00028623
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0002A42B File Offset: 0x0002862B
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0002A433 File Offset: 0x00028633
		public PhysicalColumn SearchFolderId
		{
			get
			{
				return this.searchFolderId;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0002A43B File Offset: 0x0002863B
		public PhysicalColumn UserSid
		{
			get
			{
				return this.userSid;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0002A443 File Offset: 0x00028643
		public PhysicalColumn ClientType
		{
			get
			{
				return this.clientType;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0002A44B File Offset: 0x0002864B
		public PhysicalColumn LCID
		{
			get
			{
				return this.lCID;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0002A453 File Offset: 0x00028653
		public PhysicalColumn IncrementalScopeFolderId
		{
			get
			{
				return this.incrementalScopeFolderId;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0002A45B File Offset: 0x0002865B
		public PhysicalColumn CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0002A463 File Offset: 0x00028663
		public PhysicalColumn PopulationAttempts
		{
			get
			{
				return this.populationAttempts;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0002A46B File Offset: 0x0002866B
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0002A473 File Offset: 0x00028673
		public Index SearchQueuePK
		{
			get
			{
				return this.searchQueuePK;
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0002A47C File Offset: 0x0002867C
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxNumber = null;
			}
			physicalColumn = this.searchFolderId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.searchFolderId = null;
			}
			physicalColumn = this.userSid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.userSid = null;
			}
			physicalColumn = this.clientType;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.clientType = null;
			}
			physicalColumn = this.lCID;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lCID = null;
			}
			physicalColumn = this.incrementalScopeFolderId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.incrementalScopeFolderId = null;
			}
			physicalColumn = this.creationTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.creationTime = null;
			}
			physicalColumn = this.populationAttempts;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.populationAttempts = null;
			}
			physicalColumn = this.extensionBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.extensionBlob = null;
			}
			for (int i = this.Table.Columns.Count - 1; i >= 0; i--)
			{
				this.Table.Columns[i].Index = i;
			}
			Index index = this.searchQueuePK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.searchQueuePK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x040001C9 RID: 457
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x040001CA RID: 458
		public const string SearchFolderIdName = "SearchFolderId";

		// Token: 0x040001CB RID: 459
		public const string UserSidName = "UserSid";

		// Token: 0x040001CC RID: 460
		public const string ClientTypeName = "ClientType";

		// Token: 0x040001CD RID: 461
		public const string LCIDName = "LCID";

		// Token: 0x040001CE RID: 462
		public const string IncrementalScopeFolderIdName = "IncrementalScopeFolderId";

		// Token: 0x040001CF RID: 463
		public const string CreationTimeName = "CreationTime";

		// Token: 0x040001D0 RID: 464
		public const string PopulationAttemptsName = "PopulationAttempts";

		// Token: 0x040001D1 RID: 465
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x040001D2 RID: 466
		public const string PhysicalTableName = "SearchQueue";

		// Token: 0x040001D3 RID: 467
		private PhysicalColumn mailboxNumber;

		// Token: 0x040001D4 RID: 468
		private PhysicalColumn searchFolderId;

		// Token: 0x040001D5 RID: 469
		private PhysicalColumn userSid;

		// Token: 0x040001D6 RID: 470
		private PhysicalColumn clientType;

		// Token: 0x040001D7 RID: 471
		private PhysicalColumn lCID;

		// Token: 0x040001D8 RID: 472
		private PhysicalColumn incrementalScopeFolderId;

		// Token: 0x040001D9 RID: 473
		private PhysicalColumn creationTime;

		// Token: 0x040001DA RID: 474
		private PhysicalColumn populationAttempts;

		// Token: 0x040001DB RID: 475
		private PhysicalColumn extensionBlob;

		// Token: 0x040001DC RID: 476
		private Index searchQueuePK;

		// Token: 0x040001DD RID: 477
		private Table table;
	}
}
