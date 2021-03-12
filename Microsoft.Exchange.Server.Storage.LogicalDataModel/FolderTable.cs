using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200000B RID: 11
	public sealed class FolderTable
	{
		// Token: 0x06000081 RID: 129 RVA: 0x0000572C File Offset: 0x0000392C
		internal FolderTable()
		{
			this.mailboxPartitionNumber = Factory.CreatePhysicalColumn("MailboxPartitionNumber", "MailboxPartitionNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.folderId = Factory.CreatePhysicalColumn("FolderId", "FolderId", typeof(byte[]), false, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.lcnCurrent = Factory.CreatePhysicalColumn("LcnCurrent", "LcnCurrent", typeof(byte[]), false, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.versionHistory = Factory.CreatePhysicalColumn("VersionHistory", "VersionHistory", typeof(byte[]), false, false, false, false, false, Visibility.Public, 1048576, 0, 1048576);
			this.parentFolderId = Factory.CreatePhysicalColumn("ParentFolderId", "ParentFolderId", typeof(byte[]), true, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.creationTime = Factory.CreatePhysicalColumn("CreationTime", "CreationTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.creatorSid = Factory.CreatePhysicalColumn("CreatorSid", "CreatorSid", typeof(byte[]), false, false, false, false, false, Visibility.Public, 512, 0, 512);
			this.lastModificationTime = Factory.CreatePhysicalColumn("LastModificationTime", "LastModificationTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.sourceKey = Factory.CreatePhysicalColumn("SourceKey", "SourceKey", typeof(byte[]), true, false, false, false, false, Visibility.Public, 256, 0, 256);
			this.changeKey = Factory.CreatePhysicalColumn("ChangeKey", "ChangeKey", typeof(byte[]), true, false, false, false, false, Visibility.Public, 256, 0, 256);
			this.localCommitTimeMax = Factory.CreatePhysicalColumn("LocalCommitTimeMax", "LocalCommitTimeMax", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.lastModifierSid = Factory.CreatePhysicalColumn("LastModifierSid", "LastModifierSid", typeof(byte[]), false, false, false, false, false, Visibility.Public, 512, 0, 512);
			this.displayName = Factory.CreatePhysicalColumn("DisplayName", "DisplayName", typeof(string), true, false, false, false, false, Visibility.Public, 512, 0, 512);
			this.comment = Factory.CreatePhysicalColumn("Comment", "Comment", typeof(string), true, false, false, false, false, Visibility.Public, 512, 0, 512);
			this.containerClass = Factory.CreatePhysicalColumn("ContainerClass", "ContainerClass", typeof(string), true, false, false, false, false, Visibility.Public, 128, 0, 128);
			this.messageCount = Factory.CreatePhysicalColumn("MessageCount", "MessageCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.totalDeletedCount = Factory.CreatePhysicalColumn("TotalDeletedCount", "TotalDeletedCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.unreadMessageCount = Factory.CreatePhysicalColumn("UnreadMessageCount", "UnreadMessageCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.virtualUnreadMessageCount = Factory.CreatePhysicalColumn("VirtualUnreadMessageCount", "VirtualUnreadMessageCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.messageSize = Factory.CreatePhysicalColumn("MessageSize", "MessageSize", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.messageAttachCount = Factory.CreatePhysicalColumn("MessageAttachCount", "MessageAttachCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.messageHasAttachCount = Factory.CreatePhysicalColumn("MessageHasAttachCount", "MessageHasAttachCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.hiddenItemCount = Factory.CreatePhysicalColumn("HiddenItemCount", "HiddenItemCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.unreadHiddenItemCount = Factory.CreatePhysicalColumn("UnreadHiddenItemCount", "UnreadHiddenItemCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.hiddenItemSize = Factory.CreatePhysicalColumn("HiddenItemSize", "HiddenItemSize", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.hiddenItemHasAttachCount = Factory.CreatePhysicalColumn("HiddenItemHasAttachCount", "HiddenItemHasAttachCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.hiddenItemAttachCount = Factory.CreatePhysicalColumn("HiddenItemAttachCount", "HiddenItemAttachCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.displayType = Factory.CreatePhysicalColumn("DisplayType", "DisplayType", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.conversationCount = Factory.CreatePhysicalColumn("ConversationCount", "ConversationCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.folderCount = Factory.CreatePhysicalColumn("FolderCount", "FolderCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.specialFolderNumber = Factory.CreatePhysicalColumn("SpecialFolderNumber", "SpecialFolderNumber", typeof(short), false, false, false, false, false, Visibility.Public, 0, 2, 2);
			this.reservedMessageIdGlobCntCurrent = Factory.CreatePhysicalColumn("ReservedMessageIdGlobCntCurrent", "ReservedMessageIdGlobCntCurrent", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.reservedMessageIdGlobCntMax = Factory.CreatePhysicalColumn("ReservedMessageIdGlobCntMax", "ReservedMessageIdGlobCntMax", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.reservedMessageCnGlobCntCurrent = Factory.CreatePhysicalColumn("ReservedMessageCnGlobCntCurrent", "ReservedMessageCnGlobCntCurrent", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.reservedMessageCnGlobCntMax = Factory.CreatePhysicalColumn("ReservedMessageCnGlobCntMax", "ReservedMessageCnGlobCntMax", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.nextArticleNumber = Factory.CreatePhysicalColumn("NextArticleNumber", "NextArticleNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.midsetDeleted = Factory.CreatePhysicalColumn("MidsetDeleted", "MidsetDeleted", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.normalItemPromotedColumns = Factory.CreatePhysicalColumn("NormalItemPromotedColumns", "NormalItemPromotedColumns", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.hiddenItemPromotedColumns = Factory.CreatePhysicalColumn("HiddenItemPromotedColumns", "HiddenItemPromotedColumns", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.promotionTimestamp = Factory.CreatePhysicalColumn("PromotionTimestamp", "PromotionTimestamp", typeof(DateTime), true, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.promotionUseHistory = Factory.CreatePhysicalColumn("PromotionUseHistory", "PromotionUseHistory", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.queryCriteria = Factory.CreatePhysicalColumn("QueryCriteria", "QueryCriteria", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.searchState = Factory.CreatePhysicalColumn("SearchState", "SearchState", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.setSearchCriteriaFlags = Factory.CreatePhysicalColumn("SetSearchCriteriaFlags", "SetSearchCriteriaFlags", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.scopeFolders = Factory.CreatePhysicalColumn("ScopeFolders", "ScopeFolders", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.nonRecursiveSearchBacklinks = Factory.CreatePhysicalColumn("NonRecursiveSearchBacklinks", "NonRecursiveSearchBacklinks", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.recursiveSearchBacklinks = Factory.CreatePhysicalColumn("RecursiveSearchBacklinks", "RecursiveSearchBacklinks", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.logicalIndexNumber = Factory.CreatePhysicalColumn("LogicalIndexNumber", "LogicalIndexNumber", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.propertyBlob = Factory.CreatePhysicalColumn("PropertyBlob", "PropertyBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.largePropertyValueBlob = Factory.CreatePhysicalColumn("LargePropertyValueBlob", "LargePropertyValueBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.propertyBag = Factory.CreatePhysicalColumn("PropertyBag", "PropertyBag", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 4, 0, 4);
			this.aclTableAndSecurityDescriptor = Factory.CreatePhysicalColumn("AclTableAndSecurityDescriptor", "AclTableAndSecurityDescriptor", typeof(byte[]), false, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.midsetDeletedDelta = Factory.CreatePhysicalColumn("MidsetDeletedDelta", "MidsetDeletedDelta", typeof(byte[]), true, false, false, false, true, Visibility.Public, 512, 0, 512);
			this.reservedDocumentIdCurrent = Factory.CreatePhysicalColumn("ReservedDocumentIdCurrent", "ReservedDocumentIdCurrent", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.reservedDocumentIdMax = Factory.CreatePhysicalColumn("ReservedDocumentIdMax", "ReservedDocumentIdMax", typeof(int), true, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), true, false, false, false, true, Visibility.Public, 0, 4, 4);
			string name = "FolderPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			this.folderPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.FolderId
			});
			string name2 = "FolderChangeNumber";
			bool primaryKey2 = false;
			bool unique2 = true;
			bool schemaExtension2 = false;
			bool[] conditional2 = new bool[3];
			this.folderChangeNumberIndex = new Index(name2, primaryKey2, unique2, schemaExtension2, conditional2, new bool[]
			{
				true,
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.LcnCurrent,
				this.FolderId
			});
			string name3 = "FolderByParent";
			bool primaryKey3 = false;
			bool unique3 = true;
			bool schemaExtension3 = false;
			bool[] conditional3 = new bool[3];
			this.folderByParentIndex = new Index(name3, primaryKey3, unique3, schemaExtension3, conditional3, new bool[]
			{
				true,
				true,
				true
			}, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.ParentFolderId,
				this.FolderId
			});
			Index[] indexes = new Index[]
			{
				this.FolderPK,
				this.FolderChangeNumberIndex,
				this.FolderByParentIndex
			};
			SpecialColumns specialCols = new SpecialColumns(this.PropertyBlob, null, this.PropertyBag, 1);
			PhysicalColumn[] computedColumns = new PhysicalColumn[]
			{
				this.VirtualUnreadMessageCount,
				this.PropertyBag
			};
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.FolderId,
				this.LcnCurrent,
				this.VersionHistory,
				this.ParentFolderId,
				this.CreationTime,
				this.CreatorSid,
				this.LastModificationTime,
				this.SourceKey,
				this.ChangeKey,
				this.LocalCommitTimeMax,
				this.LastModifierSid,
				this.DisplayName,
				this.Comment,
				this.ContainerClass,
				this.MessageCount,
				this.TotalDeletedCount,
				this.UnreadMessageCount,
				this.MessageSize,
				this.MessageAttachCount,
				this.MessageHasAttachCount,
				this.HiddenItemCount,
				this.UnreadHiddenItemCount,
				this.HiddenItemSize,
				this.HiddenItemHasAttachCount,
				this.HiddenItemAttachCount,
				this.DisplayType,
				this.ConversationCount,
				this.FolderCount,
				this.SpecialFolderNumber,
				this.ReservedMessageIdGlobCntCurrent,
				this.ReservedMessageIdGlobCntMax,
				this.ReservedMessageCnGlobCntCurrent,
				this.ReservedMessageCnGlobCntMax,
				this.NextArticleNumber,
				this.MidsetDeleted,
				this.NormalItemPromotedColumns,
				this.HiddenItemPromotedColumns,
				this.PromotionTimestamp,
				this.PromotionUseHistory,
				this.QueryCriteria,
				this.SearchState,
				this.SetSearchCriteriaFlags,
				this.ScopeFolders,
				this.NonRecursiveSearchBacklinks,
				this.RecursiveSearchBacklinks,
				this.LogicalIndexNumber,
				this.PropertyBlob,
				this.LargePropertyValueBlob,
				this.ExtensionBlob,
				this.AclTableAndSecurityDescriptor,
				this.MidsetDeletedDelta,
				this.ReservedDocumentIdCurrent,
				this.ReservedDocumentIdMax,
				this.MailboxNumber
			};
			this.table = Factory.CreateTable("Folder", TableClass.Folder, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Redacted, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000064AC File Offset: 0x000046AC
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000064B4 File Offset: 0x000046B4
		public PhysicalColumn MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000064BC File Offset: 0x000046BC
		public PhysicalColumn FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000064C4 File Offset: 0x000046C4
		public PhysicalColumn LcnCurrent
		{
			get
			{
				return this.lcnCurrent;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000064CC File Offset: 0x000046CC
		public PhysicalColumn VersionHistory
		{
			get
			{
				return this.versionHistory;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000064D4 File Offset: 0x000046D4
		public PhysicalColumn ParentFolderId
		{
			get
			{
				return this.parentFolderId;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000064DC File Offset: 0x000046DC
		public PhysicalColumn CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000064E4 File Offset: 0x000046E4
		public PhysicalColumn CreatorSid
		{
			get
			{
				return this.creatorSid;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000064EC File Offset: 0x000046EC
		public PhysicalColumn LastModificationTime
		{
			get
			{
				return this.lastModificationTime;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000064F4 File Offset: 0x000046F4
		public PhysicalColumn SourceKey
		{
			get
			{
				return this.sourceKey;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000064FC File Offset: 0x000046FC
		public PhysicalColumn ChangeKey
		{
			get
			{
				return this.changeKey;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00006504 File Offset: 0x00004704
		public PhysicalColumn LocalCommitTimeMax
		{
			get
			{
				return this.localCommitTimeMax;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000650C File Offset: 0x0000470C
		public PhysicalColumn LastModifierSid
		{
			get
			{
				return this.lastModifierSid;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00006514 File Offset: 0x00004714
		public PhysicalColumn DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000651C File Offset: 0x0000471C
		public PhysicalColumn Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00006524 File Offset: 0x00004724
		public PhysicalColumn ContainerClass
		{
			get
			{
				return this.containerClass;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000652C File Offset: 0x0000472C
		public PhysicalColumn MessageCount
		{
			get
			{
				return this.messageCount;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00006534 File Offset: 0x00004734
		public PhysicalColumn TotalDeletedCount
		{
			get
			{
				return this.totalDeletedCount;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000653C File Offset: 0x0000473C
		public PhysicalColumn UnreadMessageCount
		{
			get
			{
				return this.unreadMessageCount;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00006544 File Offset: 0x00004744
		public PhysicalColumn VirtualUnreadMessageCount
		{
			get
			{
				return this.virtualUnreadMessageCount;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000654C File Offset: 0x0000474C
		public PhysicalColumn MessageSize
		{
			get
			{
				return this.messageSize;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00006554 File Offset: 0x00004754
		public PhysicalColumn MessageAttachCount
		{
			get
			{
				return this.messageAttachCount;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000655C File Offset: 0x0000475C
		public PhysicalColumn MessageHasAttachCount
		{
			get
			{
				return this.messageHasAttachCount;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00006564 File Offset: 0x00004764
		public PhysicalColumn HiddenItemCount
		{
			get
			{
				return this.hiddenItemCount;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000656C File Offset: 0x0000476C
		public PhysicalColumn UnreadHiddenItemCount
		{
			get
			{
				return this.unreadHiddenItemCount;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00006574 File Offset: 0x00004774
		public PhysicalColumn HiddenItemSize
		{
			get
			{
				return this.hiddenItemSize;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000657C File Offset: 0x0000477C
		public PhysicalColumn HiddenItemHasAttachCount
		{
			get
			{
				return this.hiddenItemHasAttachCount;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00006584 File Offset: 0x00004784
		public PhysicalColumn HiddenItemAttachCount
		{
			get
			{
				return this.hiddenItemAttachCount;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000658C File Offset: 0x0000478C
		public PhysicalColumn DisplayType
		{
			get
			{
				return this.displayType;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00006594 File Offset: 0x00004794
		public PhysicalColumn ConversationCount
		{
			get
			{
				return this.conversationCount;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000659C File Offset: 0x0000479C
		public PhysicalColumn FolderCount
		{
			get
			{
				return this.folderCount;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000065A4 File Offset: 0x000047A4
		public PhysicalColumn SpecialFolderNumber
		{
			get
			{
				return this.specialFolderNumber;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000065AC File Offset: 0x000047AC
		public PhysicalColumn ReservedMessageIdGlobCntCurrent
		{
			get
			{
				return this.reservedMessageIdGlobCntCurrent;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000065B4 File Offset: 0x000047B4
		public PhysicalColumn ReservedMessageIdGlobCntMax
		{
			get
			{
				return this.reservedMessageIdGlobCntMax;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000065BC File Offset: 0x000047BC
		public PhysicalColumn ReservedMessageCnGlobCntCurrent
		{
			get
			{
				return this.reservedMessageCnGlobCntCurrent;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000065C4 File Offset: 0x000047C4
		public PhysicalColumn ReservedMessageCnGlobCntMax
		{
			get
			{
				return this.reservedMessageCnGlobCntMax;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000065CC File Offset: 0x000047CC
		public PhysicalColumn NextArticleNumber
		{
			get
			{
				return this.nextArticleNumber;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000065D4 File Offset: 0x000047D4
		public PhysicalColumn MidsetDeleted
		{
			get
			{
				return this.midsetDeleted;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000065DC File Offset: 0x000047DC
		public PhysicalColumn NormalItemPromotedColumns
		{
			get
			{
				return this.normalItemPromotedColumns;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000065E4 File Offset: 0x000047E4
		public PhysicalColumn HiddenItemPromotedColumns
		{
			get
			{
				return this.hiddenItemPromotedColumns;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000065EC File Offset: 0x000047EC
		public PhysicalColumn PromotionTimestamp
		{
			get
			{
				return this.promotionTimestamp;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000065F4 File Offset: 0x000047F4
		public PhysicalColumn PromotionUseHistory
		{
			get
			{
				return this.promotionUseHistory;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000065FC File Offset: 0x000047FC
		public PhysicalColumn QueryCriteria
		{
			get
			{
				return this.queryCriteria;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00006604 File Offset: 0x00004804
		public PhysicalColumn SearchState
		{
			get
			{
				return this.searchState;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000660C File Offset: 0x0000480C
		public PhysicalColumn SetSearchCriteriaFlags
		{
			get
			{
				return this.setSearchCriteriaFlags;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00006614 File Offset: 0x00004814
		public PhysicalColumn ScopeFolders
		{
			get
			{
				return this.scopeFolders;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000661C File Offset: 0x0000481C
		public PhysicalColumn NonRecursiveSearchBacklinks
		{
			get
			{
				return this.nonRecursiveSearchBacklinks;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00006624 File Offset: 0x00004824
		public PhysicalColumn RecursiveSearchBacklinks
		{
			get
			{
				return this.recursiveSearchBacklinks;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000662C File Offset: 0x0000482C
		public PhysicalColumn LogicalIndexNumber
		{
			get
			{
				return this.logicalIndexNumber;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00006634 File Offset: 0x00004834
		public PhysicalColumn PropertyBlob
		{
			get
			{
				return this.propertyBlob;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000663C File Offset: 0x0000483C
		public PhysicalColumn LargePropertyValueBlob
		{
			get
			{
				return this.largePropertyValueBlob;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00006644 File Offset: 0x00004844
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000664C File Offset: 0x0000484C
		public PhysicalColumn PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00006654 File Offset: 0x00004854
		public PhysicalColumn AclTableAndSecurityDescriptor
		{
			get
			{
				return this.aclTableAndSecurityDescriptor;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000665C File Offset: 0x0000485C
		public PhysicalColumn MidsetDeletedDelta
		{
			get
			{
				return this.midsetDeletedDelta;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00006664 File Offset: 0x00004864
		public PhysicalColumn ReservedDocumentIdCurrent
		{
			get
			{
				return this.reservedDocumentIdCurrent;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000666C File Offset: 0x0000486C
		public PhysicalColumn ReservedDocumentIdMax
		{
			get
			{
				return this.reservedDocumentIdMax;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00006674 File Offset: 0x00004874
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000667C File Offset: 0x0000487C
		public Index FolderPK
		{
			get
			{
				return this.folderPK;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00006684 File Offset: 0x00004884
		public Index FolderChangeNumberIndex
		{
			get
			{
				return this.folderChangeNumberIndex;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000668C File Offset: 0x0000488C
		public Index FolderByParentIndex
		{
			get
			{
				return this.folderByParentIndex;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006694 File Offset: 0x00004894
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxPartitionNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxPartitionNumber = null;
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
			physicalColumn = this.parentFolderId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.parentFolderId = null;
			}
			physicalColumn = this.creationTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.creationTime = null;
			}
			physicalColumn = this.creatorSid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.creatorSid = null;
			}
			physicalColumn = this.lastModificationTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lastModificationTime = null;
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
			physicalColumn = this.localCommitTimeMax;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.localCommitTimeMax = null;
			}
			physicalColumn = this.lastModifierSid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lastModifierSid = null;
			}
			physicalColumn = this.displayName;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.displayName = null;
			}
			physicalColumn = this.comment;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.comment = null;
			}
			physicalColumn = this.containerClass;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.containerClass = null;
			}
			physicalColumn = this.messageCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.messageCount = null;
			}
			physicalColumn = this.totalDeletedCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.totalDeletedCount = null;
			}
			physicalColumn = this.unreadMessageCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.unreadMessageCount = null;
			}
			physicalColumn = this.virtualUnreadMessageCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.virtualUnreadMessageCount = null;
			}
			physicalColumn = this.messageSize;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.messageSize = null;
			}
			physicalColumn = this.messageAttachCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.messageAttachCount = null;
			}
			physicalColumn = this.messageHasAttachCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.messageHasAttachCount = null;
			}
			physicalColumn = this.hiddenItemCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.hiddenItemCount = null;
			}
			physicalColumn = this.unreadHiddenItemCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.unreadHiddenItemCount = null;
			}
			physicalColumn = this.hiddenItemSize;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.hiddenItemSize = null;
			}
			physicalColumn = this.hiddenItemHasAttachCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.hiddenItemHasAttachCount = null;
			}
			physicalColumn = this.hiddenItemAttachCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.hiddenItemAttachCount = null;
			}
			physicalColumn = this.displayType;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.displayType = null;
			}
			physicalColumn = this.conversationCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.conversationCount = null;
			}
			physicalColumn = this.folderCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.folderCount = null;
			}
			physicalColumn = this.specialFolderNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.specialFolderNumber = null;
			}
			physicalColumn = this.reservedMessageIdGlobCntCurrent;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.reservedMessageIdGlobCntCurrent = null;
			}
			physicalColumn = this.reservedMessageIdGlobCntMax;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.reservedMessageIdGlobCntMax = null;
			}
			physicalColumn = this.reservedMessageCnGlobCntCurrent;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.reservedMessageCnGlobCntCurrent = null;
			}
			physicalColumn = this.reservedMessageCnGlobCntMax;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.reservedMessageCnGlobCntMax = null;
			}
			physicalColumn = this.nextArticleNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.nextArticleNumber = null;
			}
			physicalColumn = this.midsetDeleted;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.midsetDeleted = null;
			}
			physicalColumn = this.normalItemPromotedColumns;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.normalItemPromotedColumns = null;
			}
			physicalColumn = this.hiddenItemPromotedColumns;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.hiddenItemPromotedColumns = null;
			}
			physicalColumn = this.promotionTimestamp;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.promotionTimestamp = null;
			}
			physicalColumn = this.promotionUseHistory;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.promotionUseHistory = null;
			}
			physicalColumn = this.queryCriteria;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.queryCriteria = null;
			}
			physicalColumn = this.searchState;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.searchState = null;
			}
			physicalColumn = this.setSearchCriteriaFlags;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.setSearchCriteriaFlags = null;
			}
			physicalColumn = this.scopeFolders;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.scopeFolders = null;
			}
			physicalColumn = this.nonRecursiveSearchBacklinks;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.nonRecursiveSearchBacklinks = null;
			}
			physicalColumn = this.recursiveSearchBacklinks;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.recursiveSearchBacklinks = null;
			}
			physicalColumn = this.logicalIndexNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.logicalIndexNumber = null;
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
			physicalColumn = this.aclTableAndSecurityDescriptor;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.aclTableAndSecurityDescriptor = null;
			}
			physicalColumn = this.midsetDeletedDelta;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.midsetDeletedDelta = null;
			}
			physicalColumn = this.reservedDocumentIdCurrent;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.reservedDocumentIdCurrent = null;
			}
			physicalColumn = this.reservedDocumentIdMax;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.reservedDocumentIdMax = null;
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
			Index index = this.folderPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.folderPK = null;
				this.Table.Indexes.Remove(index);
			}
			index = this.folderChangeNumberIndex;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.folderChangeNumberIndex = null;
				this.Table.Indexes.Remove(index);
			}
			index = this.folderByParentIndex;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.folderByParentIndex = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x040000A7 RID: 167
		public const string MailboxPartitionNumberName = "MailboxPartitionNumber";

		// Token: 0x040000A8 RID: 168
		public const string FolderIdName = "FolderId";

		// Token: 0x040000A9 RID: 169
		public const string LcnCurrentName = "LcnCurrent";

		// Token: 0x040000AA RID: 170
		public const string VersionHistoryName = "VersionHistory";

		// Token: 0x040000AB RID: 171
		public const string ParentFolderIdName = "ParentFolderId";

		// Token: 0x040000AC RID: 172
		public const string CreationTimeName = "CreationTime";

		// Token: 0x040000AD RID: 173
		public const string CreatorSidName = "CreatorSid";

		// Token: 0x040000AE RID: 174
		public const string LastModificationTimeName = "LastModificationTime";

		// Token: 0x040000AF RID: 175
		public const string SourceKeyName = "SourceKey";

		// Token: 0x040000B0 RID: 176
		public const string ChangeKeyName = "ChangeKey";

		// Token: 0x040000B1 RID: 177
		public const string LocalCommitTimeMaxName = "LocalCommitTimeMax";

		// Token: 0x040000B2 RID: 178
		public const string LastModifierSidName = "LastModifierSid";

		// Token: 0x040000B3 RID: 179
		public const string DisplayNameName = "DisplayName";

		// Token: 0x040000B4 RID: 180
		public const string CommentName = "Comment";

		// Token: 0x040000B5 RID: 181
		public const string ContainerClassName = "ContainerClass";

		// Token: 0x040000B6 RID: 182
		public const string MessageCountName = "MessageCount";

		// Token: 0x040000B7 RID: 183
		public const string TotalDeletedCountName = "TotalDeletedCount";

		// Token: 0x040000B8 RID: 184
		public const string UnreadMessageCountName = "UnreadMessageCount";

		// Token: 0x040000B9 RID: 185
		public const string VirtualUnreadMessageCountName = "VirtualUnreadMessageCount";

		// Token: 0x040000BA RID: 186
		public const string MessageSizeName = "MessageSize";

		// Token: 0x040000BB RID: 187
		public const string MessageAttachCountName = "MessageAttachCount";

		// Token: 0x040000BC RID: 188
		public const string MessageHasAttachCountName = "MessageHasAttachCount";

		// Token: 0x040000BD RID: 189
		public const string HiddenItemCountName = "HiddenItemCount";

		// Token: 0x040000BE RID: 190
		public const string UnreadHiddenItemCountName = "UnreadHiddenItemCount";

		// Token: 0x040000BF RID: 191
		public const string HiddenItemSizeName = "HiddenItemSize";

		// Token: 0x040000C0 RID: 192
		public const string HiddenItemHasAttachCountName = "HiddenItemHasAttachCount";

		// Token: 0x040000C1 RID: 193
		public const string HiddenItemAttachCountName = "HiddenItemAttachCount";

		// Token: 0x040000C2 RID: 194
		public const string DisplayTypeName = "DisplayType";

		// Token: 0x040000C3 RID: 195
		public const string ConversationCountName = "ConversationCount";

		// Token: 0x040000C4 RID: 196
		public const string FolderCountName = "FolderCount";

		// Token: 0x040000C5 RID: 197
		public const string SpecialFolderNumberName = "SpecialFolderNumber";

		// Token: 0x040000C6 RID: 198
		public const string ReservedMessageIdGlobCntCurrentName = "ReservedMessageIdGlobCntCurrent";

		// Token: 0x040000C7 RID: 199
		public const string ReservedMessageIdGlobCntMaxName = "ReservedMessageIdGlobCntMax";

		// Token: 0x040000C8 RID: 200
		public const string ReservedMessageCnGlobCntCurrentName = "ReservedMessageCnGlobCntCurrent";

		// Token: 0x040000C9 RID: 201
		public const string ReservedMessageCnGlobCntMaxName = "ReservedMessageCnGlobCntMax";

		// Token: 0x040000CA RID: 202
		public const string NextArticleNumberName = "NextArticleNumber";

		// Token: 0x040000CB RID: 203
		public const string MidsetDeletedName = "MidsetDeleted";

		// Token: 0x040000CC RID: 204
		public const string NormalItemPromotedColumnsName = "NormalItemPromotedColumns";

		// Token: 0x040000CD RID: 205
		public const string HiddenItemPromotedColumnsName = "HiddenItemPromotedColumns";

		// Token: 0x040000CE RID: 206
		public const string PromotionTimestampName = "PromotionTimestamp";

		// Token: 0x040000CF RID: 207
		public const string PromotionUseHistoryName = "PromotionUseHistory";

		// Token: 0x040000D0 RID: 208
		public const string QueryCriteriaName = "QueryCriteria";

		// Token: 0x040000D1 RID: 209
		public const string SearchStateName = "SearchState";

		// Token: 0x040000D2 RID: 210
		public const string SetSearchCriteriaFlagsName = "SetSearchCriteriaFlags";

		// Token: 0x040000D3 RID: 211
		public const string ScopeFoldersName = "ScopeFolders";

		// Token: 0x040000D4 RID: 212
		public const string NonRecursiveSearchBacklinksName = "NonRecursiveSearchBacklinks";

		// Token: 0x040000D5 RID: 213
		public const string RecursiveSearchBacklinksName = "RecursiveSearchBacklinks";

		// Token: 0x040000D6 RID: 214
		public const string LogicalIndexNumberName = "LogicalIndexNumber";

		// Token: 0x040000D7 RID: 215
		public const string PropertyBlobName = "PropertyBlob";

		// Token: 0x040000D8 RID: 216
		public const string LargePropertyValueBlobName = "LargePropertyValueBlob";

		// Token: 0x040000D9 RID: 217
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x040000DA RID: 218
		public const string PropertyBagName = "PropertyBag";

		// Token: 0x040000DB RID: 219
		public const string AclTableAndSecurityDescriptorName = "AclTableAndSecurityDescriptor";

		// Token: 0x040000DC RID: 220
		public const string MidsetDeletedDeltaName = "MidsetDeletedDelta";

		// Token: 0x040000DD RID: 221
		public const string ReservedDocumentIdCurrentName = "ReservedDocumentIdCurrent";

		// Token: 0x040000DE RID: 222
		public const string ReservedDocumentIdMaxName = "ReservedDocumentIdMax";

		// Token: 0x040000DF RID: 223
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x040000E0 RID: 224
		public const string PhysicalTableName = "Folder";

		// Token: 0x040000E1 RID: 225
		private PhysicalColumn mailboxPartitionNumber;

		// Token: 0x040000E2 RID: 226
		private PhysicalColumn folderId;

		// Token: 0x040000E3 RID: 227
		private PhysicalColumn lcnCurrent;

		// Token: 0x040000E4 RID: 228
		private PhysicalColumn versionHistory;

		// Token: 0x040000E5 RID: 229
		private PhysicalColumn parentFolderId;

		// Token: 0x040000E6 RID: 230
		private PhysicalColumn creationTime;

		// Token: 0x040000E7 RID: 231
		private PhysicalColumn creatorSid;

		// Token: 0x040000E8 RID: 232
		private PhysicalColumn lastModificationTime;

		// Token: 0x040000E9 RID: 233
		private PhysicalColumn sourceKey;

		// Token: 0x040000EA RID: 234
		private PhysicalColumn changeKey;

		// Token: 0x040000EB RID: 235
		private PhysicalColumn localCommitTimeMax;

		// Token: 0x040000EC RID: 236
		private PhysicalColumn lastModifierSid;

		// Token: 0x040000ED RID: 237
		private PhysicalColumn displayName;

		// Token: 0x040000EE RID: 238
		private PhysicalColumn comment;

		// Token: 0x040000EF RID: 239
		private PhysicalColumn containerClass;

		// Token: 0x040000F0 RID: 240
		private PhysicalColumn messageCount;

		// Token: 0x040000F1 RID: 241
		private PhysicalColumn totalDeletedCount;

		// Token: 0x040000F2 RID: 242
		private PhysicalColumn unreadMessageCount;

		// Token: 0x040000F3 RID: 243
		private PhysicalColumn virtualUnreadMessageCount;

		// Token: 0x040000F4 RID: 244
		private PhysicalColumn messageSize;

		// Token: 0x040000F5 RID: 245
		private PhysicalColumn messageAttachCount;

		// Token: 0x040000F6 RID: 246
		private PhysicalColumn messageHasAttachCount;

		// Token: 0x040000F7 RID: 247
		private PhysicalColumn hiddenItemCount;

		// Token: 0x040000F8 RID: 248
		private PhysicalColumn unreadHiddenItemCount;

		// Token: 0x040000F9 RID: 249
		private PhysicalColumn hiddenItemSize;

		// Token: 0x040000FA RID: 250
		private PhysicalColumn hiddenItemHasAttachCount;

		// Token: 0x040000FB RID: 251
		private PhysicalColumn hiddenItemAttachCount;

		// Token: 0x040000FC RID: 252
		private PhysicalColumn displayType;

		// Token: 0x040000FD RID: 253
		private PhysicalColumn conversationCount;

		// Token: 0x040000FE RID: 254
		private PhysicalColumn folderCount;

		// Token: 0x040000FF RID: 255
		private PhysicalColumn specialFolderNumber;

		// Token: 0x04000100 RID: 256
		private PhysicalColumn reservedMessageIdGlobCntCurrent;

		// Token: 0x04000101 RID: 257
		private PhysicalColumn reservedMessageIdGlobCntMax;

		// Token: 0x04000102 RID: 258
		private PhysicalColumn reservedMessageCnGlobCntCurrent;

		// Token: 0x04000103 RID: 259
		private PhysicalColumn reservedMessageCnGlobCntMax;

		// Token: 0x04000104 RID: 260
		private PhysicalColumn nextArticleNumber;

		// Token: 0x04000105 RID: 261
		private PhysicalColumn midsetDeleted;

		// Token: 0x04000106 RID: 262
		private PhysicalColumn normalItemPromotedColumns;

		// Token: 0x04000107 RID: 263
		private PhysicalColumn hiddenItemPromotedColumns;

		// Token: 0x04000108 RID: 264
		private PhysicalColumn promotionTimestamp;

		// Token: 0x04000109 RID: 265
		private PhysicalColumn promotionUseHistory;

		// Token: 0x0400010A RID: 266
		private PhysicalColumn queryCriteria;

		// Token: 0x0400010B RID: 267
		private PhysicalColumn searchState;

		// Token: 0x0400010C RID: 268
		private PhysicalColumn setSearchCriteriaFlags;

		// Token: 0x0400010D RID: 269
		private PhysicalColumn scopeFolders;

		// Token: 0x0400010E RID: 270
		private PhysicalColumn nonRecursiveSearchBacklinks;

		// Token: 0x0400010F RID: 271
		private PhysicalColumn recursiveSearchBacklinks;

		// Token: 0x04000110 RID: 272
		private PhysicalColumn logicalIndexNumber;

		// Token: 0x04000111 RID: 273
		private PhysicalColumn propertyBlob;

		// Token: 0x04000112 RID: 274
		private PhysicalColumn largePropertyValueBlob;

		// Token: 0x04000113 RID: 275
		private PhysicalColumn extensionBlob;

		// Token: 0x04000114 RID: 276
		private PhysicalColumn propertyBag;

		// Token: 0x04000115 RID: 277
		private PhysicalColumn aclTableAndSecurityDescriptor;

		// Token: 0x04000116 RID: 278
		private PhysicalColumn midsetDeletedDelta;

		// Token: 0x04000117 RID: 279
		private PhysicalColumn reservedDocumentIdCurrent;

		// Token: 0x04000118 RID: 280
		private PhysicalColumn reservedDocumentIdMax;

		// Token: 0x04000119 RID: 281
		private PhysicalColumn mailboxNumber;

		// Token: 0x0400011A RID: 282
		private Index folderPK;

		// Token: 0x0400011B RID: 283
		private Index folderChangeNumberIndex;

		// Token: 0x0400011C RID: 284
		private Index folderByParentIndex;

		// Token: 0x0400011D RID: 285
		private Table table;
	}
}
