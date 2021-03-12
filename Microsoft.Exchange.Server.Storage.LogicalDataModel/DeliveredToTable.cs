using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000006 RID: 6
	public sealed class DeliveredToTable
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00003154 File Offset: 0x00001354
		internal DeliveredToTable()
		{
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.submitTime = Factory.CreatePhysicalColumn("SubmitTime", "SubmitTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.messageIdHash = Factory.CreatePhysicalColumn("MessageIdHash", "MessageIdHash", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			string name = "DeliveredToPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[3];
			this.deliveredToPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true,
				true
			}, new PhysicalColumn[]
			{
				this.SubmitTime,
				this.MailboxNumber,
				this.MessageIdHash
			});
			Index[] indexes = new Index[]
			{
				this.DeliveredToPK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxNumber,
				this.SubmitTime,
				this.MessageIdHash,
				this.ExtensionBlob
			};
			this.table = Factory.CreateTable("DeliveredTo", TableClass.DeliveredTo, CultureHelper.DefaultCultureInfo, false, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000032DF File Offset: 0x000014DF
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000032E7 File Offset: 0x000014E7
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000032EF File Offset: 0x000014EF
		public PhysicalColumn SubmitTime
		{
			get
			{
				return this.submitTime;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000032F7 File Offset: 0x000014F7
		public PhysicalColumn MessageIdHash
		{
			get
			{
				return this.messageIdHash;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000032FF File Offset: 0x000014FF
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003307 File Offset: 0x00001507
		public Index DeliveredToPK
		{
			get
			{
				return this.deliveredToPK;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003310 File Offset: 0x00001510
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxNumber = null;
			}
			physicalColumn = this.submitTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.submitTime = null;
			}
			physicalColumn = this.messageIdHash;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.messageIdHash = null;
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
			Index index = this.deliveredToPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.deliveredToPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x04000026 RID: 38
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x04000027 RID: 39
		public const string SubmitTimeName = "SubmitTime";

		// Token: 0x04000028 RID: 40
		public const string MessageIdHashName = "MessageIdHash";

		// Token: 0x04000029 RID: 41
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x0400002A RID: 42
		public const string PhysicalTableName = "DeliveredTo";

		// Token: 0x0400002B RID: 43
		private PhysicalColumn mailboxNumber;

		// Token: 0x0400002C RID: 44
		private PhysicalColumn submitTime;

		// Token: 0x0400002D RID: 45
		private PhysicalColumn messageIdHash;

		// Token: 0x0400002E RID: 46
		private PhysicalColumn extensionBlob;

		// Token: 0x0400002F RID: 47
		private Index deliveredToPK;

		// Token: 0x04000030 RID: 48
		private Table table;
	}
}
