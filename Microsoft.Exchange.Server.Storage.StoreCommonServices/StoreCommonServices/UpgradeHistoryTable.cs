using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200000F RID: 15
	public sealed class UpgradeHistoryTable
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		internal UpgradeHistoryTable()
		{
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.upgradeTime = Factory.CreatePhysicalColumn("UpgradeTime", "UpgradeTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.fromVersion = Factory.CreatePhysicalColumn("FromVersion", "FromVersion", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.toVersion = Factory.CreatePhysicalColumn("ToVersion", "ToVersion", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			string name = "UpgradeHistoryPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[3];
			this.upgradeHistoryPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true,
				true
			}, new PhysicalColumn[]
			{
				this.UpgradeTime,
				this.FromVersion,
				this.MailboxNumber
			});
			Index[] indexes = new Index[]
			{
				this.UpgradeHistoryPK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxNumber,
				this.UpgradeTime,
				this.FromVersion,
				this.ToVersion,
				this.ExtensionBlob
			};
			this.table = Factory.CreateTable("UpgradeHistory", TableClass.UpgradeHistory, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, true, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000C091 File Offset: 0x0000A291
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000C099 File Offset: 0x0000A299
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000C0A1 File Offset: 0x0000A2A1
		public PhysicalColumn UpgradeTime
		{
			get
			{
				return this.upgradeTime;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000C0A9 File Offset: 0x0000A2A9
		public PhysicalColumn FromVersion
		{
			get
			{
				return this.fromVersion;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000C0B1 File Offset: 0x0000A2B1
		public PhysicalColumn ToVersion
		{
			get
			{
				return this.toVersion;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000C0B9 File Offset: 0x0000A2B9
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000C0C1 File Offset: 0x0000A2C1
		public Index UpgradeHistoryPK
		{
			get
			{
				return this.upgradeHistoryPK;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000C0CC File Offset: 0x0000A2CC
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxNumber = null;
			}
			physicalColumn = this.upgradeTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.upgradeTime = null;
			}
			physicalColumn = this.fromVersion;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.fromVersion = null;
			}
			physicalColumn = this.toVersion;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.toVersion = null;
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
			Index index = this.upgradeHistoryPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.upgradeHistoryPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x0400016D RID: 365
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x0400016E RID: 366
		public const string UpgradeTimeName = "UpgradeTime";

		// Token: 0x0400016F RID: 367
		public const string FromVersionName = "FromVersion";

		// Token: 0x04000170 RID: 368
		public const string ToVersionName = "ToVersion";

		// Token: 0x04000171 RID: 369
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x04000172 RID: 370
		public const string PhysicalTableName = "UpgradeHistory";

		// Token: 0x04000173 RID: 371
		private PhysicalColumn mailboxNumber;

		// Token: 0x04000174 RID: 372
		private PhysicalColumn upgradeTime;

		// Token: 0x04000175 RID: 373
		private PhysicalColumn fromVersion;

		// Token: 0x04000176 RID: 374
		private PhysicalColumn toVersion;

		// Token: 0x04000177 RID: 375
		private PhysicalColumn extensionBlob;

		// Token: 0x04000178 RID: 376
		private Index upgradeHistoryPK;

		// Token: 0x04000179 RID: 377
		private Table table;
	}
}
