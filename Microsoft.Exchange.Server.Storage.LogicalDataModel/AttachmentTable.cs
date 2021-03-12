using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000008 RID: 8
	public sealed class AttachmentTable
	{
		// Token: 0x0600004E RID: 78 RVA: 0x000040F0 File Offset: 0x000022F0
		internal AttachmentTable()
		{
			this.mailboxPartitionNumber = Factory.CreatePhysicalColumn("MailboxPartitionNumber", "MailboxPartitionNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.inid = Factory.CreatePhysicalColumn("Inid", "Inid", typeof(long), false, true, false, false, false, Visibility.Public, 0, 8, 8);
			this.attachmentId = Factory.CreatePhysicalColumn("AttachmentId", "AttachmentId", typeof(byte[]), true, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.attachmentMethod = Factory.CreatePhysicalColumn("AttachmentMethod", "AttachmentMethod", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.renderingPosition = Factory.CreatePhysicalColumn("RenderingPosition", "RenderingPosition", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.creationTime = Factory.CreatePhysicalColumn("CreationTime", "CreationTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.lastModificationTime = Factory.CreatePhysicalColumn("LastModificationTime", "LastModificationTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.size = Factory.CreatePhysicalColumn("Size", "Size", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.name = Factory.CreatePhysicalColumn("Name", "Name", typeof(string), true, false, false, false, false, Visibility.Public, 512, 0, 512);
			this.contentId = Factory.CreatePhysicalColumn("ContentId", "ContentId", typeof(string), true, false, false, false, false, Visibility.Public, 128, 0, 128);
			this.contentType = Factory.CreatePhysicalColumn("ContentType", "ContentType", typeof(string), true, false, false, false, false, Visibility.Public, 128, 0, 128);
			this.content = Factory.CreatePhysicalColumn("Content", "Content", typeof(byte[]), true, false, true, true, false, Visibility.Private, 1073741824, 0, 256);
			this.messageFlagsActual = Factory.CreatePhysicalColumn("MessageFlagsActual", "MessageFlagsActual", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.mailFlags = Factory.CreatePhysicalColumn("MailFlags", "MailFlags", typeof(short), false, false, false, false, false, Visibility.Public, 0, 2, 2);
			this.recipientList = Factory.CreatePhysicalColumn("RecipientList", "RecipientList", typeof(byte[][]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.subobjectsBlob = Factory.CreatePhysicalColumn("SubobjectsBlob", "SubobjectsBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.propertyBlob = Factory.CreatePhysicalColumn("PropertyBlob", "PropertyBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.largePropertyValueBlob = Factory.CreatePhysicalColumn("LargePropertyValueBlob", "LargePropertyValueBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.isEmbeddedMessage = Factory.CreatePhysicalColumn("IsEmbeddedMessage", "IsEmbeddedMessage", typeof(bool), true, false, false, false, false, Visibility.Public, 0, 1, 1);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.fullTextType = Factory.CreatePhysicalColumn("FullTextType", "FullTextType", typeof(short), true, false, false, false, false, Visibility.Public, 0, 2, 2);
			this.language = Factory.CreatePhysicalColumn("Language", "Language", typeof(string), true, false, false, false, false, Visibility.Public, 4, 0, 4);
			this.propertyBag = Factory.CreatePhysicalColumn("PropertyBag", "PropertyBag", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 4, 0, 4);
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), true, false, false, false, true, Visibility.Public, 0, 4, 4);
			string text = "AttachmentPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			this.attachmentPK = new Index(text, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.Inid
			});
			Index[] indexes = new Index[]
			{
				this.AttachmentPK
			};
			SpecialColumns specialCols = new SpecialColumns(this.PropertyBlob, null, this.PropertyBag, 1);
			PhysicalColumn[] computedColumns = new PhysicalColumn[]
			{
				this.PropertyBag
			};
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.Inid,
				this.AttachmentId,
				this.AttachmentMethod,
				this.RenderingPosition,
				this.CreationTime,
				this.LastModificationTime,
				this.Size,
				this.Name,
				this.ContentId,
				this.ContentType,
				this.Content,
				this.MessageFlagsActual,
				this.MailFlags,
				this.RecipientList,
				this.SubobjectsBlob,
				this.PropertyBlob,
				this.LargePropertyValueBlob,
				this.IsEmbeddedMessage,
				this.ExtensionBlob,
				this.FullTextType,
				this.Language,
				this.MailboxNumber
			};
			this.table = Factory.CreateTable("Attachment", TableClass.Attachment, CultureHelper.DefaultCultureInfo, false, TableAccessHints.None, false, Visibility.Redacted, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000046BA File Offset: 0x000028BA
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000046C2 File Offset: 0x000028C2
		public PhysicalColumn MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000046CA File Offset: 0x000028CA
		public PhysicalColumn Inid
		{
			get
			{
				return this.inid;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000046D2 File Offset: 0x000028D2
		public PhysicalColumn AttachmentId
		{
			get
			{
				return this.attachmentId;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000046DA File Offset: 0x000028DA
		public PhysicalColumn AttachmentMethod
		{
			get
			{
				return this.attachmentMethod;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000046E2 File Offset: 0x000028E2
		public PhysicalColumn RenderingPosition
		{
			get
			{
				return this.renderingPosition;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000046EA File Offset: 0x000028EA
		public PhysicalColumn CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000046F2 File Offset: 0x000028F2
		public PhysicalColumn LastModificationTime
		{
			get
			{
				return this.lastModificationTime;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000046FA File Offset: 0x000028FA
		public PhysicalColumn Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004702 File Offset: 0x00002902
		public PhysicalColumn Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000470A File Offset: 0x0000290A
		public PhysicalColumn ContentId
		{
			get
			{
				return this.contentId;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004712 File Offset: 0x00002912
		public PhysicalColumn ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000471A File Offset: 0x0000291A
		public PhysicalColumn Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00004722 File Offset: 0x00002922
		public PhysicalColumn MessageFlagsActual
		{
			get
			{
				return this.messageFlagsActual;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000472A File Offset: 0x0000292A
		public PhysicalColumn MailFlags
		{
			get
			{
				return this.mailFlags;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00004732 File Offset: 0x00002932
		public PhysicalColumn RecipientList
		{
			get
			{
				return this.recipientList;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000473A File Offset: 0x0000293A
		public PhysicalColumn SubobjectsBlob
		{
			get
			{
				return this.subobjectsBlob;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00004742 File Offset: 0x00002942
		public PhysicalColumn PropertyBlob
		{
			get
			{
				return this.propertyBlob;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000474A File Offset: 0x0000294A
		public PhysicalColumn LargePropertyValueBlob
		{
			get
			{
				return this.largePropertyValueBlob;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00004752 File Offset: 0x00002952
		public PhysicalColumn IsEmbeddedMessage
		{
			get
			{
				return this.isEmbeddedMessage;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000475A File Offset: 0x0000295A
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00004762 File Offset: 0x00002962
		public PhysicalColumn FullTextType
		{
			get
			{
				return this.fullTextType;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000476A File Offset: 0x0000296A
		public PhysicalColumn Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00004772 File Offset: 0x00002972
		public PhysicalColumn PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000477A File Offset: 0x0000297A
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00004782 File Offset: 0x00002982
		public Index AttachmentPK
		{
			get
			{
				return this.attachmentPK;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000478C File Offset: 0x0000298C
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxPartitionNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxPartitionNumber = null;
			}
			physicalColumn = this.inid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.inid = null;
			}
			physicalColumn = this.attachmentId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.attachmentId = null;
			}
			physicalColumn = this.attachmentMethod;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.attachmentMethod = null;
			}
			physicalColumn = this.renderingPosition;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.renderingPosition = null;
			}
			physicalColumn = this.creationTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.creationTime = null;
			}
			physicalColumn = this.lastModificationTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lastModificationTime = null;
			}
			physicalColumn = this.size;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.size = null;
			}
			physicalColumn = this.name;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.name = null;
			}
			physicalColumn = this.contentId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.contentId = null;
			}
			physicalColumn = this.contentType;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.contentType = null;
			}
			physicalColumn = this.content;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.content = null;
			}
			physicalColumn = this.messageFlagsActual;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.messageFlagsActual = null;
			}
			physicalColumn = this.mailFlags;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailFlags = null;
			}
			physicalColumn = this.recipientList;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.recipientList = null;
			}
			physicalColumn = this.subobjectsBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.subobjectsBlob = null;
			}
			physicalColumn = this.propertyBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.propertyBlob = null;
			}
			physicalColumn = this.largePropertyValueBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.largePropertyValueBlob = null;
			}
			physicalColumn = this.isEmbeddedMessage;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.isEmbeddedMessage = null;
			}
			physicalColumn = this.extensionBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.extensionBlob = null;
			}
			physicalColumn = this.fullTextType;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.fullTextType = null;
			}
			physicalColumn = this.language;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.language = null;
			}
			physicalColumn = this.propertyBag;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.propertyBag = null;
			}
			physicalColumn = this.mailboxNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxNumber = null;
			}
			for (int i = this.Table.Columns.Count - 1; i >= 0; i--)
			{
				this.Table.Columns[i].Index = i;
			}
			Index index = this.attachmentPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.attachmentPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x0400005C RID: 92
		public const string MailboxPartitionNumberName = "MailboxPartitionNumber";

		// Token: 0x0400005D RID: 93
		public const string InidName = "Inid";

		// Token: 0x0400005E RID: 94
		public const string AttachmentIdName = "AttachmentId";

		// Token: 0x0400005F RID: 95
		public const string AttachmentMethodName = "AttachmentMethod";

		// Token: 0x04000060 RID: 96
		public const string RenderingPositionName = "RenderingPosition";

		// Token: 0x04000061 RID: 97
		public const string CreationTimeName = "CreationTime";

		// Token: 0x04000062 RID: 98
		public const string LastModificationTimeName = "LastModificationTime";

		// Token: 0x04000063 RID: 99
		public const string SizeName = "Size";

		// Token: 0x04000064 RID: 100
		public const string NameName = "Name";

		// Token: 0x04000065 RID: 101
		public const string ContentIdName = "ContentId";

		// Token: 0x04000066 RID: 102
		public const string ContentTypeName = "ContentType";

		// Token: 0x04000067 RID: 103
		public const string ContentName = "Content";

		// Token: 0x04000068 RID: 104
		public const string MessageFlagsActualName = "MessageFlagsActual";

		// Token: 0x04000069 RID: 105
		public const string MailFlagsName = "MailFlags";

		// Token: 0x0400006A RID: 106
		public const string RecipientListName = "RecipientList";

		// Token: 0x0400006B RID: 107
		public const string SubobjectsBlobName = "SubobjectsBlob";

		// Token: 0x0400006C RID: 108
		public const string PropertyBlobName = "PropertyBlob";

		// Token: 0x0400006D RID: 109
		public const string LargePropertyValueBlobName = "LargePropertyValueBlob";

		// Token: 0x0400006E RID: 110
		public const string IsEmbeddedMessageName = "IsEmbeddedMessage";

		// Token: 0x0400006F RID: 111
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x04000070 RID: 112
		public const string FullTextTypeName = "FullTextType";

		// Token: 0x04000071 RID: 113
		public const string LanguageName = "Language";

		// Token: 0x04000072 RID: 114
		public const string PropertyBagName = "PropertyBag";

		// Token: 0x04000073 RID: 115
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x04000074 RID: 116
		public const string PhysicalTableName = "Attachment";

		// Token: 0x04000075 RID: 117
		private PhysicalColumn mailboxPartitionNumber;

		// Token: 0x04000076 RID: 118
		private PhysicalColumn inid;

		// Token: 0x04000077 RID: 119
		private PhysicalColumn attachmentId;

		// Token: 0x04000078 RID: 120
		private PhysicalColumn attachmentMethod;

		// Token: 0x04000079 RID: 121
		private PhysicalColumn renderingPosition;

		// Token: 0x0400007A RID: 122
		private PhysicalColumn creationTime;

		// Token: 0x0400007B RID: 123
		private PhysicalColumn lastModificationTime;

		// Token: 0x0400007C RID: 124
		private PhysicalColumn size;

		// Token: 0x0400007D RID: 125
		private PhysicalColumn name;

		// Token: 0x0400007E RID: 126
		private PhysicalColumn contentId;

		// Token: 0x0400007F RID: 127
		private PhysicalColumn contentType;

		// Token: 0x04000080 RID: 128
		private PhysicalColumn content;

		// Token: 0x04000081 RID: 129
		private PhysicalColumn messageFlagsActual;

		// Token: 0x04000082 RID: 130
		private PhysicalColumn mailFlags;

		// Token: 0x04000083 RID: 131
		private PhysicalColumn recipientList;

		// Token: 0x04000084 RID: 132
		private PhysicalColumn subobjectsBlob;

		// Token: 0x04000085 RID: 133
		private PhysicalColumn propertyBlob;

		// Token: 0x04000086 RID: 134
		private PhysicalColumn largePropertyValueBlob;

		// Token: 0x04000087 RID: 135
		private PhysicalColumn isEmbeddedMessage;

		// Token: 0x04000088 RID: 136
		private PhysicalColumn extensionBlob;

		// Token: 0x04000089 RID: 137
		private PhysicalColumn fullTextType;

		// Token: 0x0400008A RID: 138
		private PhysicalColumn language;

		// Token: 0x0400008B RID: 139
		private PhysicalColumn propertyBag;

		// Token: 0x0400008C RID: 140
		private PhysicalColumn mailboxNumber;

		// Token: 0x0400008D RID: 141
		private Index attachmentPK;

		// Token: 0x0400008E RID: 142
		private Table table;
	}
}
