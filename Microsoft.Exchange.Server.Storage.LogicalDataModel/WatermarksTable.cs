using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000018 RID: 24
	public sealed class WatermarksTable
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x0002B424 File Offset: 0x00029624
		internal WatermarksTable()
		{
			this.consumerGuid = Factory.CreatePhysicalColumn("ConsumerGuid", "ConsumerGuid", typeof(Guid), false, false, false, false, false, Visibility.Public, 0, 16, 16);
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.eventCounter = Factory.CreatePhysicalColumn("EventCounter", "EventCounter", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			string name = "WatermarksPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			this.watermarksPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.ConsumerGuid,
				this.MailboxNumber
			});
			Index[] indexes = new Index[]
			{
				this.WatermarksPK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.ConsumerGuid,
				this.MailboxNumber,
				this.EventCounter,
				this.ExtensionBlob
			};
			this.table = Factory.CreateTable("Watermarks", TableClass.Watermarks, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0002B5AA File Offset: 0x000297AA
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0002B5B2 File Offset: 0x000297B2
		public PhysicalColumn ConsumerGuid
		{
			get
			{
				return this.consumerGuid;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0002B5BA File Offset: 0x000297BA
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0002B5C2 File Offset: 0x000297C2
		public PhysicalColumn EventCounter
		{
			get
			{
				return this.eventCounter;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0002B5CA File Offset: 0x000297CA
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0002B5D2 File Offset: 0x000297D2
		public Index WatermarksPK
		{
			get
			{
				return this.watermarksPK;
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0002B5DC File Offset: 0x000297DC
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.consumerGuid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.consumerGuid = null;
			}
			physicalColumn = this.mailboxNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxNumber = null;
			}
			physicalColumn = this.eventCounter;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.eventCounter = null;
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
			Index index = this.watermarksPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.watermarksPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x0400020A RID: 522
		public const string ConsumerGuidName = "ConsumerGuid";

		// Token: 0x0400020B RID: 523
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x0400020C RID: 524
		public const string EventCounterName = "EventCounter";

		// Token: 0x0400020D RID: 525
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x0400020E RID: 526
		public const string PhysicalTableName = "Watermarks";

		// Token: 0x0400020F RID: 527
		private PhysicalColumn consumerGuid;

		// Token: 0x04000210 RID: 528
		private PhysicalColumn mailboxNumber;

		// Token: 0x04000211 RID: 529
		private PhysicalColumn eventCounter;

		// Token: 0x04000212 RID: 530
		private PhysicalColumn extensionBlob;

		// Token: 0x04000213 RID: 531
		private Index watermarksPK;

		// Token: 0x04000214 RID: 532
		private Table table;
	}
}
