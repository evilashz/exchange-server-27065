using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200000E RID: 14
	public sealed class TimedEventsTable
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x0000B928 File Offset: 0x00009B28
		internal TimedEventsTable()
		{
			this.eventTime = Factory.CreatePhysicalColumn("EventTime", "EventTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.uniqueId = Factory.CreatePhysicalColumn("UniqueId", "UniqueId", typeof(long), false, true, false, false, false, Visibility.Public, 0, 8, 8);
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.eventSource = Factory.CreatePhysicalColumn("EventSource", "EventSource", typeof(Guid), false, false, false, false, false, Visibility.Public, 0, 16, 16);
			this.eventType = Factory.CreatePhysicalColumn("EventType", "EventType", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.qoS = Factory.CreatePhysicalColumn("QoS", "QoS", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.eventData = Factory.CreatePhysicalColumn("EventData", "EventData", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			string name = "TimedEventsPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			this.timedEventsPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.EventTime,
				this.UniqueId
			});
			Index[] indexes = new Index[]
			{
				this.TimedEventsPK
			};
			SpecialColumns specialCols = new SpecialColumns(null, null, null, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.EventTime,
				this.UniqueId,
				this.MailboxNumber,
				this.EventSource,
				this.EventType,
				this.QoS,
				this.EventData,
				this.ExtensionBlob
			};
			this.table = Factory.CreateTable("TimedEvents", TableClass.TimedEvents, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000BB7E File Offset: 0x00009D7E
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000BB86 File Offset: 0x00009D86
		public PhysicalColumn EventTime
		{
			get
			{
				return this.eventTime;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000BB8E File Offset: 0x00009D8E
		public PhysicalColumn UniqueId
		{
			get
			{
				return this.uniqueId;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000BB96 File Offset: 0x00009D96
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000BB9E File Offset: 0x00009D9E
		public PhysicalColumn EventSource
		{
			get
			{
				return this.eventSource;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000BBA6 File Offset: 0x00009DA6
		public PhysicalColumn EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000BBAE File Offset: 0x00009DAE
		public PhysicalColumn QoS
		{
			get
			{
				return this.qoS;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000BBB6 File Offset: 0x00009DB6
		public PhysicalColumn EventData
		{
			get
			{
				return this.eventData;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000BBBE File Offset: 0x00009DBE
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000BBC6 File Offset: 0x00009DC6
		public Index TimedEventsPK
		{
			get
			{
				return this.timedEventsPK;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.eventTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.eventTime = null;
			}
			physicalColumn = this.uniqueId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.uniqueId = null;
			}
			physicalColumn = this.mailboxNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxNumber = null;
			}
			physicalColumn = this.eventSource;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.eventSource = null;
			}
			physicalColumn = this.eventType;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.eventType = null;
			}
			physicalColumn = this.qoS;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.qoS = null;
			}
			physicalColumn = this.eventData;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.eventData = null;
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
			Index index = this.timedEventsPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.timedEventsPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x0400015A RID: 346
		public const string EventTimeName = "EventTime";

		// Token: 0x0400015B RID: 347
		public const string UniqueIdName = "UniqueId";

		// Token: 0x0400015C RID: 348
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x0400015D RID: 349
		public const string EventSourceName = "EventSource";

		// Token: 0x0400015E RID: 350
		public const string EventTypeName = "EventType";

		// Token: 0x0400015F RID: 351
		public const string QoSName = "QoS";

		// Token: 0x04000160 RID: 352
		public const string EventDataName = "EventData";

		// Token: 0x04000161 RID: 353
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x04000162 RID: 354
		public const string PhysicalTableName = "TimedEvents";

		// Token: 0x04000163 RID: 355
		private PhysicalColumn eventTime;

		// Token: 0x04000164 RID: 356
		private PhysicalColumn uniqueId;

		// Token: 0x04000165 RID: 357
		private PhysicalColumn mailboxNumber;

		// Token: 0x04000166 RID: 358
		private PhysicalColumn eventSource;

		// Token: 0x04000167 RID: 359
		private PhysicalColumn eventType;

		// Token: 0x04000168 RID: 360
		private PhysicalColumn qoS;

		// Token: 0x04000169 RID: 361
		private PhysicalColumn eventData;

		// Token: 0x0400016A RID: 362
		private PhysicalColumn extensionBlob;

		// Token: 0x0400016B RID: 363
		private Index timedEventsPK;

		// Token: 0x0400016C RID: 364
		private Table table;
	}
}
