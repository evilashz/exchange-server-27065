using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200000D RID: 13
	public sealed class MessageTable
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00007D7C File Offset: 0x00005F7C
		internal MessageTable()
		{
			this.mailboxPartitionNumber = Factory.CreatePhysicalColumn("MailboxPartitionNumber", "MailboxPartitionNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.messageDocumentId = Factory.CreatePhysicalColumn("MessageDocumentId", "MessageDocumentId", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.messageId = Factory.CreatePhysicalColumn("MessageId", "MessageId", typeof(byte[]), true, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.folderId = Factory.CreatePhysicalColumn("FolderId", "FolderId", typeof(byte[]), true, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.lcnCurrent = Factory.CreatePhysicalColumn("LcnCurrent", "LcnCurrent", typeof(byte[]), false, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.versionHistory = Factory.CreatePhysicalColumn("VersionHistory", "VersionHistory", typeof(byte[]), false, false, false, false, false, Visibility.Public, 1048576, 0, 1048576);
			this.groupCns = Factory.CreatePhysicalColumn("GroupCns", "GroupCns", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.lastModificationTime = Factory.CreatePhysicalColumn("LastModificationTime", "LastModificationTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.lcnReadUnread = Factory.CreatePhysicalColumn("LcnReadUnread", "LcnReadUnread", typeof(byte[]), true, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.sourceKey = Factory.CreatePhysicalColumn("SourceKey", "SourceKey", typeof(byte[]), true, false, false, false, false, Visibility.Public, 256, 0, 256);
			this.changeKey = Factory.CreatePhysicalColumn("ChangeKey", "ChangeKey", typeof(byte[]), true, false, false, false, false, Visibility.Public, 256, 0, 256);
			this.size = Factory.CreatePhysicalColumn("Size", "Size", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.recipientList = Factory.CreatePhysicalColumn("RecipientList", "RecipientList", typeof(byte[][]), true, false, false, true, false, Visibility.Redacted, 1073741824, 0, 50);
			this.propertyBlob = Factory.CreatePhysicalColumn("PropertyBlob", "PropertyBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 3110);
			this.offPagePropertyBlob = Factory.CreatePhysicalColumn("OffPagePropertyBlob", "OffPagePropertyBlob", typeof(byte[]), true, false, true, true, false, Visibility.Redacted, 1073741824, 0, 100);
			this.largePropertyValueBlob = Factory.CreatePhysicalColumn("LargePropertyValueBlob", "LargePropertyValueBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.subobjectsBlob = Factory.CreatePhysicalColumn("SubobjectsBlob", "SubobjectsBlob", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 50);
			this.isHidden = Factory.CreatePhysicalColumn("IsHidden", "IsHidden", typeof(bool), false, false, false, false, false, Visibility.Public, 0, 1, 1);
			this.isRead = Factory.CreatePhysicalColumn("IsRead", "IsRead", typeof(bool), false, false, false, false, false, Visibility.Public, 0, 1, 1);
			this.virtualIsRead = Factory.CreatePhysicalColumn("VirtualIsRead", "VirtualIsRead", typeof(bool), true, false, false, false, false, Visibility.Public, 0, 1, 1);
			this.virtualUnreadMessageCount = Factory.CreatePhysicalColumn("VirtualUnreadMessageCount", "VirtualUnreadMessageCount", typeof(long), true, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.hasAttachments = Factory.CreatePhysicalColumn("HasAttachments", "HasAttachments", typeof(bool), false, false, false, false, false, Visibility.Public, 0, 1, 1);
			this.conversationIndexTracking = Factory.CreatePhysicalColumn("ConversationIndexTracking", "ConversationIndexTracking", typeof(bool), true, false, false, false, false, Visibility.Public, 0, 1, 1);
			this.messageFlagsActual = Factory.CreatePhysicalColumn("MessageFlagsActual", "MessageFlagsActual", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.mailFlags = Factory.CreatePhysicalColumn("MailFlags", "MailFlags", typeof(short), false, false, false, false, false, Visibility.Public, 0, 2, 2);
			this.status = Factory.CreatePhysicalColumn("Status", "Status", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.messageClass = Factory.CreatePhysicalColumn("MessageClass", "MessageClass", typeof(string), true, false, false, false, false, Visibility.Public, 255, 0, 255);
			this.importance = Factory.CreatePhysicalColumn("Importance", "Importance", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.sensitivity = Factory.CreatePhysicalColumn("Sensitivity", "Sensitivity", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.priority = Factory.CreatePhysicalColumn("Priority", "Priority", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.conversationIndex = Factory.CreatePhysicalColumn("ConversationIndex", "ConversationIndex", typeof(byte[]), true, false, false, false, false, Visibility.Public, 4096, 0, 4096);
			this.conversationMembers = Factory.CreatePhysicalColumn("ConversationMembers", "ConversationMembers", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.subjectPrefix = Factory.CreatePhysicalColumn("SubjectPrefix", "SubjectPrefix", typeof(string), true, false, false, false, false, Visibility.Public, 1024, 0, 1024);
			this.articleNumber = Factory.CreatePhysicalColumn("ArticleNumber", "ArticleNumber", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.iMAPId = Factory.CreatePhysicalColumn("IMAPId", "IMAPId", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.displayTo = Factory.CreatePhysicalColumn("DisplayTo", "DisplayTo", typeof(string), true, false, false, false, false, Visibility.Public, 30720, 0, 256);
			this.conversationDocumentId = Factory.CreatePhysicalColumn("ConversationDocumentId", "ConversationDocumentId", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.bodyType = Factory.CreatePhysicalColumn("BodyType", "BodyType", typeof(short), true, false, false, false, false, Visibility.Public, 0, 2, 2);
			this.dateCreated = Factory.CreatePhysicalColumn("DateCreated", "DateCreated", typeof(DateTime), true, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.codePage = Factory.CreatePhysicalColumn("CodePage", "CodePage", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.dateReceived = Factory.CreatePhysicalColumn("DateReceived", "DateReceived", typeof(DateTime), true, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.dateSent = Factory.CreatePhysicalColumn("DateSent", "DateSent", typeof(DateTime), true, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.nativeBody = Factory.CreatePhysicalColumn("NativeBody", "NativeBody", typeof(byte[]), true, false, true, true, false, Visibility.Private, 1073741824, 0, 50);
			this.annotationToken = Factory.CreatePhysicalColumn("AnnotationToken", "AnnotationToken", typeof(byte[]), true, false, true, true, false, Visibility.Private, 1073741824, 0, 20);
			this.userConfigurationXmlStream = Factory.CreatePhysicalColumn("UserConfigurationXmlStream", "UserConfigurationXmlStream", typeof(byte[]), true, false, true, true, false, Visibility.Private, 1073741824, 0, 20);
			this.userConfigurationStream = Factory.CreatePhysicalColumn("UserConfigurationStream", "UserConfigurationStream", typeof(byte[]), true, false, true, true, false, Visibility.Private, 1073741824, 0, 20);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.propertyBag = Factory.CreatePhysicalColumn("PropertyBag", "PropertyBag", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 4, 0, 4);
			this.conversationId = Factory.CreatePhysicalColumn("ConversationId", "ConversationId", typeof(byte[]), true, false, false, false, false, Visibility.Public, 0, 16, 16);
			this.virtualParentDisplay = Factory.CreatePhysicalColumn("VirtualParentDisplay", "VirtualParentDisplay", typeof(string), true, false, false, false, false, Visibility.Public, 512, 0, 512);
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), true, false, false, false, true, Visibility.Public, 0, 4, 4);
			string name = "MessagePK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			this.messagePK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.MessageDocumentId
			});
			string name2 = "MessageUnique";
			bool primaryKey2 = false;
			bool unique2 = true;
			bool schemaExtension2 = false;
			bool[] conditional2 = new bool[4];
			this.messageUnique = new Index(name2, primaryKey2, unique2, schemaExtension2, conditional2, new bool[]
			{
				true,
				true,
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.FolderId,
				this.IsHidden,
				this.MessageId
			});
			string name3 = "MessageIdUnique";
			bool primaryKey3 = false;
			bool unique3 = true;
			bool schemaExtension3 = false;
			bool[] conditional3 = new bool[2];
			this.messageIdUnique = new Index(name3, primaryKey3, unique3, schemaExtension3, conditional3, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.MessageId
			});
			this.conversationIdUnique = new Index("ConversationIdUnique", false, true, false, new bool[]
			{
				default(bool),
				true
			}, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.ConversationId
			});
			this.iMAPIDUnique = new Index("IMAPIDUnique", false, true, false, new bool[]
			{
				default(bool),
				true,
				true
			}, new bool[]
			{
				true,
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.FolderId,
				this.IMAPId
			});
			this.articleNumberUnique = new Index("ArticleNumberUnique", false, true, false, new bool[]
			{
				default(bool),
				true,
				true
			}, new bool[]
			{
				true,
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.FolderId,
				this.ArticleNumber
			});
			Index[] indexes = new Index[]
			{
				this.MessagePK,
				this.MessageUnique,
				this.ConversationIdUnique
			};
			SpecialColumns specialCols = new SpecialColumns(this.PropertyBlob, this.OffPagePropertyBlob, this.PropertyBag, 1);
			PhysicalColumn[] computedColumns = new PhysicalColumn[]
			{
				this.VirtualIsRead,
				this.VirtualUnreadMessageCount,
				this.PropertyBag,
				this.VirtualParentDisplay
			};
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.MessageDocumentId,
				this.MessageId,
				this.FolderId,
				this.LcnCurrent,
				this.VersionHistory,
				this.GroupCns,
				this.LastModificationTime,
				this.LcnReadUnread,
				this.SourceKey,
				this.ChangeKey,
				this.Size,
				this.RecipientList,
				this.PropertyBlob,
				this.OffPagePropertyBlob,
				this.LargePropertyValueBlob,
				this.SubobjectsBlob,
				this.IsHidden,
				this.IsRead,
				this.HasAttachments,
				this.ConversationIndexTracking,
				this.MessageFlagsActual,
				this.MailFlags,
				this.Status,
				this.MessageClass,
				this.Importance,
				this.Sensitivity,
				this.Priority,
				this.ConversationIndex,
				this.ConversationMembers,
				this.SubjectPrefix,
				this.ArticleNumber,
				this.IMAPId,
				this.DisplayTo,
				this.ConversationDocumentId,
				this.BodyType,
				this.DateCreated,
				this.CodePage,
				this.DateReceived,
				this.DateSent,
				this.NativeBody,
				this.AnnotationToken,
				this.UserConfigurationXmlStream,
				this.UserConfigurationStream,
				this.ExtensionBlob,
				this.ConversationId,
				this.MailboxNumber
			};
			this.table = Factory.CreateTable("Message", TableClass.Message, CultureHelper.DefaultCultureInfo, false, TableAccessHints.None, false, Visibility.Redacted, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00008ACB File Offset: 0x00006CCB
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00008AD3 File Offset: 0x00006CD3
		public PhysicalColumn MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00008ADB File Offset: 0x00006CDB
		public PhysicalColumn MessageDocumentId
		{
			get
			{
				return this.messageDocumentId;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00008AE3 File Offset: 0x00006CE3
		public PhysicalColumn MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00008AEB File Offset: 0x00006CEB
		public PhysicalColumn FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00008AF3 File Offset: 0x00006CF3
		public PhysicalColumn LcnCurrent
		{
			get
			{
				return this.lcnCurrent;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00008AFB File Offset: 0x00006CFB
		public PhysicalColumn VersionHistory
		{
			get
			{
				return this.versionHistory;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00008B03 File Offset: 0x00006D03
		public PhysicalColumn GroupCns
		{
			get
			{
				return this.groupCns;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00008B0B File Offset: 0x00006D0B
		public PhysicalColumn LastModificationTime
		{
			get
			{
				return this.lastModificationTime;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00008B13 File Offset: 0x00006D13
		public PhysicalColumn LcnReadUnread
		{
			get
			{
				return this.lcnReadUnread;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00008B1B File Offset: 0x00006D1B
		public PhysicalColumn SourceKey
		{
			get
			{
				return this.sourceKey;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00008B23 File Offset: 0x00006D23
		public PhysicalColumn ChangeKey
		{
			get
			{
				return this.changeKey;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00008B2B File Offset: 0x00006D2B
		public PhysicalColumn Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00008B33 File Offset: 0x00006D33
		public PhysicalColumn RecipientList
		{
			get
			{
				return this.recipientList;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00008B3B File Offset: 0x00006D3B
		public PhysicalColumn PropertyBlob
		{
			get
			{
				return this.propertyBlob;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00008B43 File Offset: 0x00006D43
		public PhysicalColumn OffPagePropertyBlob
		{
			get
			{
				return this.offPagePropertyBlob;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00008B4B File Offset: 0x00006D4B
		public PhysicalColumn LargePropertyValueBlob
		{
			get
			{
				return this.largePropertyValueBlob;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00008B53 File Offset: 0x00006D53
		public PhysicalColumn SubobjectsBlob
		{
			get
			{
				return this.subobjectsBlob;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00008B5B File Offset: 0x00006D5B
		public PhysicalColumn IsHidden
		{
			get
			{
				return this.isHidden;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00008B63 File Offset: 0x00006D63
		public PhysicalColumn IsRead
		{
			get
			{
				return this.isRead;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00008B6B File Offset: 0x00006D6B
		public PhysicalColumn VirtualIsRead
		{
			get
			{
				return this.virtualIsRead;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00008B73 File Offset: 0x00006D73
		public PhysicalColumn VirtualUnreadMessageCount
		{
			get
			{
				return this.virtualUnreadMessageCount;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00008B7B File Offset: 0x00006D7B
		public PhysicalColumn HasAttachments
		{
			get
			{
				return this.hasAttachments;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00008B83 File Offset: 0x00006D83
		public PhysicalColumn ConversationIndexTracking
		{
			get
			{
				return this.conversationIndexTracking;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00008B8B File Offset: 0x00006D8B
		public PhysicalColumn MessageFlagsActual
		{
			get
			{
				return this.messageFlagsActual;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00008B93 File Offset: 0x00006D93
		public PhysicalColumn MailFlags
		{
			get
			{
				return this.mailFlags;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00008B9B File Offset: 0x00006D9B
		public PhysicalColumn Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00008BA3 File Offset: 0x00006DA3
		public PhysicalColumn MessageClass
		{
			get
			{
				return this.messageClass;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00008BAB File Offset: 0x00006DAB
		public PhysicalColumn Importance
		{
			get
			{
				return this.importance;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00008BB3 File Offset: 0x00006DB3
		public PhysicalColumn Sensitivity
		{
			get
			{
				return this.sensitivity;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00008BBB File Offset: 0x00006DBB
		public PhysicalColumn Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00008BC3 File Offset: 0x00006DC3
		public PhysicalColumn ConversationIndex
		{
			get
			{
				return this.conversationIndex;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00008BCB File Offset: 0x00006DCB
		public PhysicalColumn ConversationMembers
		{
			get
			{
				return this.conversationMembers;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00008BD3 File Offset: 0x00006DD3
		public PhysicalColumn SubjectPrefix
		{
			get
			{
				return this.subjectPrefix;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00008BDB File Offset: 0x00006DDB
		public PhysicalColumn ArticleNumber
		{
			get
			{
				return this.articleNumber;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00008BE3 File Offset: 0x00006DE3
		public PhysicalColumn IMAPId
		{
			get
			{
				return this.iMAPId;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00008BEB File Offset: 0x00006DEB
		public PhysicalColumn DisplayTo
		{
			get
			{
				return this.displayTo;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00008BF3 File Offset: 0x00006DF3
		public PhysicalColumn ConversationDocumentId
		{
			get
			{
				return this.conversationDocumentId;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00008BFB File Offset: 0x00006DFB
		public PhysicalColumn BodyType
		{
			get
			{
				return this.bodyType;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00008C03 File Offset: 0x00006E03
		public PhysicalColumn DateCreated
		{
			get
			{
				return this.dateCreated;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00008C0B File Offset: 0x00006E0B
		public PhysicalColumn CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00008C13 File Offset: 0x00006E13
		public PhysicalColumn DateReceived
		{
			get
			{
				return this.dateReceived;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00008C1B File Offset: 0x00006E1B
		public PhysicalColumn DateSent
		{
			get
			{
				return this.dateSent;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00008C23 File Offset: 0x00006E23
		public PhysicalColumn NativeBody
		{
			get
			{
				return this.nativeBody;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00008C2B File Offset: 0x00006E2B
		public PhysicalColumn AnnotationToken
		{
			get
			{
				return this.annotationToken;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00008C33 File Offset: 0x00006E33
		public PhysicalColumn UserConfigurationXmlStream
		{
			get
			{
				return this.userConfigurationXmlStream;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00008C3B File Offset: 0x00006E3B
		public PhysicalColumn UserConfigurationStream
		{
			get
			{
				return this.userConfigurationStream;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00008C43 File Offset: 0x00006E43
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00008C4B File Offset: 0x00006E4B
		public PhysicalColumn PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00008C53 File Offset: 0x00006E53
		public PhysicalColumn ConversationId
		{
			get
			{
				return this.conversationId;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00008C5B File Offset: 0x00006E5B
		public PhysicalColumn VirtualParentDisplay
		{
			get
			{
				return this.virtualParentDisplay;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00008C63 File Offset: 0x00006E63
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00008C6B File Offset: 0x00006E6B
		public Index MessagePK
		{
			get
			{
				return this.messagePK;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00008C73 File Offset: 0x00006E73
		public Index MessageUnique
		{
			get
			{
				return this.messageUnique;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00008C7B File Offset: 0x00006E7B
		public Index MessageIdUnique
		{
			get
			{
				return this.messageIdUnique;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00008C83 File Offset: 0x00006E83
		public Index ConversationIdUnique
		{
			get
			{
				return this.conversationIdUnique;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00008C8B File Offset: 0x00006E8B
		public Index IMAPIDUnique
		{
			get
			{
				return this.iMAPIDUnique;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00008C93 File Offset: 0x00006E93
		public Index ArticleNumberUnique
		{
			get
			{
				return this.articleNumberUnique;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00008C9C File Offset: 0x00006E9C
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxPartitionNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxPartitionNumber = null;
			}
			physicalColumn = this.messageDocumentId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.messageDocumentId = null;
			}
			physicalColumn = this.messageId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.messageId = null;
			}
			physicalColumn = this.folderId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.folderId = null;
			}
			physicalColumn = this.lcnCurrent;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lcnCurrent = null;
			}
			physicalColumn = this.versionHistory;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.versionHistory = null;
			}
			physicalColumn = this.groupCns;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.groupCns = null;
			}
			physicalColumn = this.lastModificationTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lastModificationTime = null;
			}
			physicalColumn = this.lcnReadUnread;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lcnReadUnread = null;
			}
			physicalColumn = this.sourceKey;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.sourceKey = null;
			}
			physicalColumn = this.changeKey;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.changeKey = null;
			}
			physicalColumn = this.size;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.size = null;
			}
			physicalColumn = this.recipientList;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.recipientList = null;
			}
			physicalColumn = this.propertyBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.propertyBlob = null;
			}
			physicalColumn = this.offPagePropertyBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.offPagePropertyBlob = null;
			}
			physicalColumn = this.largePropertyValueBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.largePropertyValueBlob = null;
			}
			physicalColumn = this.subobjectsBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.subobjectsBlob = null;
			}
			physicalColumn = this.isHidden;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.isHidden = null;
			}
			physicalColumn = this.isRead;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.isRead = null;
			}
			physicalColumn = this.virtualIsRead;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.virtualIsRead = null;
			}
			physicalColumn = this.virtualUnreadMessageCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.virtualUnreadMessageCount = null;
			}
			physicalColumn = this.hasAttachments;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.hasAttachments = null;
			}
			physicalColumn = this.conversationIndexTracking;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.conversationIndexTracking = null;
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
			physicalColumn = this.status;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.status = null;
			}
			physicalColumn = this.messageClass;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.messageClass = null;
			}
			physicalColumn = this.importance;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.importance = null;
			}
			physicalColumn = this.sensitivity;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.sensitivity = null;
			}
			physicalColumn = this.priority;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.priority = null;
			}
			physicalColumn = this.conversationIndex;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.conversationIndex = null;
			}
			physicalColumn = this.conversationMembers;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.conversationMembers = null;
			}
			physicalColumn = this.subjectPrefix;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.subjectPrefix = null;
			}
			physicalColumn = this.articleNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.articleNumber = null;
			}
			physicalColumn = this.iMAPId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.iMAPId = null;
			}
			physicalColumn = this.displayTo;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.displayTo = null;
			}
			physicalColumn = this.conversationDocumentId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.conversationDocumentId = null;
			}
			physicalColumn = this.bodyType;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.bodyType = null;
			}
			physicalColumn = this.dateCreated;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.dateCreated = null;
			}
			physicalColumn = this.codePage;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.codePage = null;
			}
			physicalColumn = this.dateReceived;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.dateReceived = null;
			}
			physicalColumn = this.dateSent;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.dateSent = null;
			}
			physicalColumn = this.nativeBody;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.nativeBody = null;
			}
			physicalColumn = this.annotationToken;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.annotationToken = null;
			}
			physicalColumn = this.userConfigurationXmlStream;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.userConfigurationXmlStream = null;
			}
			physicalColumn = this.userConfigurationStream;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.userConfigurationStream = null;
			}
			physicalColumn = this.extensionBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.extensionBlob = null;
			}
			physicalColumn = this.propertyBag;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.propertyBag = null;
			}
			physicalColumn = this.conversationId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.conversationId = null;
			}
			physicalColumn = this.virtualParentDisplay;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.virtualParentDisplay = null;
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
			Index index = this.messagePK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.messagePK = null;
				this.Table.Indexes.Remove(index);
			}
			index = this.messageUnique;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.messageUnique = null;
				this.Table.Indexes.Remove(index);
			}
			index = this.messageIdUnique;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.messageIdUnique = null;
				this.Table.Indexes.Remove(index);
			}
			index = this.conversationIdUnique;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.conversationIdUnique = null;
				this.Table.Indexes.Remove(index);
			}
			index = this.iMAPIDUnique;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.iMAPIDUnique = null;
				this.Table.Indexes.Remove(index);
			}
			index = this.articleNumberUnique;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.articleNumberUnique = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00009E44 File Offset: 0x00008044
		internal void SetupFullTextIndex(StoreDatabase database)
		{
			this.messageFullText = new FullTextIndex("MessageFullText", new Column[]
			{
				this.MailboxPartitionNumber,
				this.MessageDocumentId
			});
			this.table.FullTextIndex = this.messageFullText;
		}

		// Token: 0x0400012B RID: 299
		public const string MailboxPartitionNumberName = "MailboxPartitionNumber";

		// Token: 0x0400012C RID: 300
		public const string MessageDocumentIdName = "MessageDocumentId";

		// Token: 0x0400012D RID: 301
		public const string MessageIdName = "MessageId";

		// Token: 0x0400012E RID: 302
		public const string FolderIdName = "FolderId";

		// Token: 0x0400012F RID: 303
		public const string LcnCurrentName = "LcnCurrent";

		// Token: 0x04000130 RID: 304
		public const string VersionHistoryName = "VersionHistory";

		// Token: 0x04000131 RID: 305
		public const string GroupCnsName = "GroupCns";

		// Token: 0x04000132 RID: 306
		public const string LastModificationTimeName = "LastModificationTime";

		// Token: 0x04000133 RID: 307
		public const string LcnReadUnreadName = "LcnReadUnread";

		// Token: 0x04000134 RID: 308
		public const string SourceKeyName = "SourceKey";

		// Token: 0x04000135 RID: 309
		public const string ChangeKeyName = "ChangeKey";

		// Token: 0x04000136 RID: 310
		public const string SizeName = "Size";

		// Token: 0x04000137 RID: 311
		public const string RecipientListName = "RecipientList";

		// Token: 0x04000138 RID: 312
		public const string PropertyBlobName = "PropertyBlob";

		// Token: 0x04000139 RID: 313
		public const string OffPagePropertyBlobName = "OffPagePropertyBlob";

		// Token: 0x0400013A RID: 314
		public const string LargePropertyValueBlobName = "LargePropertyValueBlob";

		// Token: 0x0400013B RID: 315
		public const string SubobjectsBlobName = "SubobjectsBlob";

		// Token: 0x0400013C RID: 316
		public const string IsHiddenName = "IsHidden";

		// Token: 0x0400013D RID: 317
		public const string IsReadName = "IsRead";

		// Token: 0x0400013E RID: 318
		public const string VirtualIsReadName = "VirtualIsRead";

		// Token: 0x0400013F RID: 319
		public const string VirtualUnreadMessageCountName = "VirtualUnreadMessageCount";

		// Token: 0x04000140 RID: 320
		public const string HasAttachmentsName = "HasAttachments";

		// Token: 0x04000141 RID: 321
		public const string ConversationIndexTrackingName = "ConversationIndexTracking";

		// Token: 0x04000142 RID: 322
		public const string MessageFlagsActualName = "MessageFlagsActual";

		// Token: 0x04000143 RID: 323
		public const string MailFlagsName = "MailFlags";

		// Token: 0x04000144 RID: 324
		public const string StatusName = "Status";

		// Token: 0x04000145 RID: 325
		public const string MessageClassName = "MessageClass";

		// Token: 0x04000146 RID: 326
		public const string ImportanceName = "Importance";

		// Token: 0x04000147 RID: 327
		public const string SensitivityName = "Sensitivity";

		// Token: 0x04000148 RID: 328
		public const string PriorityName = "Priority";

		// Token: 0x04000149 RID: 329
		public const string ConversationIndexName = "ConversationIndex";

		// Token: 0x0400014A RID: 330
		public const string ConversationMembersName = "ConversationMembers";

		// Token: 0x0400014B RID: 331
		public const string SubjectPrefixName = "SubjectPrefix";

		// Token: 0x0400014C RID: 332
		public const string ArticleNumberName = "ArticleNumber";

		// Token: 0x0400014D RID: 333
		public const string IMAPIdName = "IMAPId";

		// Token: 0x0400014E RID: 334
		public const string DisplayToName = "DisplayTo";

		// Token: 0x0400014F RID: 335
		public const string ConversationDocumentIdName = "ConversationDocumentId";

		// Token: 0x04000150 RID: 336
		public const string BodyTypeName = "BodyType";

		// Token: 0x04000151 RID: 337
		public const string DateCreatedName = "DateCreated";

		// Token: 0x04000152 RID: 338
		public const string CodePageName = "CodePage";

		// Token: 0x04000153 RID: 339
		public const string DateReceivedName = "DateReceived";

		// Token: 0x04000154 RID: 340
		public const string DateSentName = "DateSent";

		// Token: 0x04000155 RID: 341
		public const string NativeBodyName = "NativeBody";

		// Token: 0x04000156 RID: 342
		public const string AnnotationTokenName = "AnnotationToken";

		// Token: 0x04000157 RID: 343
		public const string UserConfigurationXmlStreamName = "UserConfigurationXmlStream";

		// Token: 0x04000158 RID: 344
		public const string UserConfigurationStreamName = "UserConfigurationStream";

		// Token: 0x04000159 RID: 345
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x0400015A RID: 346
		public const string PropertyBagName = "PropertyBag";

		// Token: 0x0400015B RID: 347
		public const string ConversationIdName = "ConversationId";

		// Token: 0x0400015C RID: 348
		public const string VirtualParentDisplayName = "VirtualParentDisplay";

		// Token: 0x0400015D RID: 349
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x0400015E RID: 350
		public const string PhysicalTableName = "Message";

		// Token: 0x0400015F RID: 351
		private PhysicalColumn mailboxPartitionNumber;

		// Token: 0x04000160 RID: 352
		private PhysicalColumn messageDocumentId;

		// Token: 0x04000161 RID: 353
		private PhysicalColumn messageId;

		// Token: 0x04000162 RID: 354
		private PhysicalColumn folderId;

		// Token: 0x04000163 RID: 355
		private PhysicalColumn lcnCurrent;

		// Token: 0x04000164 RID: 356
		private PhysicalColumn versionHistory;

		// Token: 0x04000165 RID: 357
		private PhysicalColumn groupCns;

		// Token: 0x04000166 RID: 358
		private PhysicalColumn lastModificationTime;

		// Token: 0x04000167 RID: 359
		private PhysicalColumn lcnReadUnread;

		// Token: 0x04000168 RID: 360
		private PhysicalColumn sourceKey;

		// Token: 0x04000169 RID: 361
		private PhysicalColumn changeKey;

		// Token: 0x0400016A RID: 362
		private PhysicalColumn size;

		// Token: 0x0400016B RID: 363
		private PhysicalColumn recipientList;

		// Token: 0x0400016C RID: 364
		private PhysicalColumn propertyBlob;

		// Token: 0x0400016D RID: 365
		private PhysicalColumn offPagePropertyBlob;

		// Token: 0x0400016E RID: 366
		private PhysicalColumn largePropertyValueBlob;

		// Token: 0x0400016F RID: 367
		private PhysicalColumn subobjectsBlob;

		// Token: 0x04000170 RID: 368
		private PhysicalColumn isHidden;

		// Token: 0x04000171 RID: 369
		private PhysicalColumn isRead;

		// Token: 0x04000172 RID: 370
		private PhysicalColumn virtualIsRead;

		// Token: 0x04000173 RID: 371
		private PhysicalColumn virtualUnreadMessageCount;

		// Token: 0x04000174 RID: 372
		private PhysicalColumn hasAttachments;

		// Token: 0x04000175 RID: 373
		private PhysicalColumn conversationIndexTracking;

		// Token: 0x04000176 RID: 374
		private PhysicalColumn messageFlagsActual;

		// Token: 0x04000177 RID: 375
		private PhysicalColumn mailFlags;

		// Token: 0x04000178 RID: 376
		private PhysicalColumn status;

		// Token: 0x04000179 RID: 377
		private PhysicalColumn messageClass;

		// Token: 0x0400017A RID: 378
		private PhysicalColumn importance;

		// Token: 0x0400017B RID: 379
		private PhysicalColumn sensitivity;

		// Token: 0x0400017C RID: 380
		private PhysicalColumn priority;

		// Token: 0x0400017D RID: 381
		private PhysicalColumn conversationIndex;

		// Token: 0x0400017E RID: 382
		private PhysicalColumn conversationMembers;

		// Token: 0x0400017F RID: 383
		private PhysicalColumn subjectPrefix;

		// Token: 0x04000180 RID: 384
		private PhysicalColumn articleNumber;

		// Token: 0x04000181 RID: 385
		private PhysicalColumn iMAPId;

		// Token: 0x04000182 RID: 386
		private PhysicalColumn displayTo;

		// Token: 0x04000183 RID: 387
		private PhysicalColumn conversationDocumentId;

		// Token: 0x04000184 RID: 388
		private PhysicalColumn bodyType;

		// Token: 0x04000185 RID: 389
		private PhysicalColumn dateCreated;

		// Token: 0x04000186 RID: 390
		private PhysicalColumn codePage;

		// Token: 0x04000187 RID: 391
		private PhysicalColumn dateReceived;

		// Token: 0x04000188 RID: 392
		private PhysicalColumn dateSent;

		// Token: 0x04000189 RID: 393
		private PhysicalColumn nativeBody;

		// Token: 0x0400018A RID: 394
		private PhysicalColumn annotationToken;

		// Token: 0x0400018B RID: 395
		private PhysicalColumn userConfigurationXmlStream;

		// Token: 0x0400018C RID: 396
		private PhysicalColumn userConfigurationStream;

		// Token: 0x0400018D RID: 397
		private PhysicalColumn extensionBlob;

		// Token: 0x0400018E RID: 398
		private PhysicalColumn propertyBag;

		// Token: 0x0400018F RID: 399
		private PhysicalColumn conversationId;

		// Token: 0x04000190 RID: 400
		private PhysicalColumn virtualParentDisplay;

		// Token: 0x04000191 RID: 401
		private PhysicalColumn mailboxNumber;

		// Token: 0x04000192 RID: 402
		private Index messagePK;

		// Token: 0x04000193 RID: 403
		private Index messageUnique;

		// Token: 0x04000194 RID: 404
		private Index messageIdUnique;

		// Token: 0x04000195 RID: 405
		private Index conversationIdUnique;

		// Token: 0x04000196 RID: 406
		private Index iMAPIDUnique;

		// Token: 0x04000197 RID: 407
		private Index articleNumberUnique;

		// Token: 0x04000198 RID: 408
		private FullTextIndex messageFullText;

		// Token: 0x04000199 RID: 409
		private Table table;
	}
}
