using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000007 RID: 7
	public sealed class MailboxTable
	{
		// Token: 0x06000036 RID: 54 RVA: 0x0000373C File Offset: 0x0000193C
		internal MailboxTable()
		{
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.mailboxGuid = Factory.CreatePhysicalColumn("MailboxGuid", "MailboxGuid", typeof(Guid), true, false, false, false, false, Visibility.Public, 0, 16, 16);
			this.ownerADGuid = Factory.CreatePhysicalColumn("OwnerADGuid", "OwnerADGuid", typeof(Guid), false, false, false, false, false, Visibility.Public, 0, 16, 16);
			this.ownerLegacyDN = Factory.CreatePhysicalColumn("OwnerLegacyDN", "OwnerLegacyDN", typeof(string), false, false, false, false, false, Visibility.Redacted, 1024, 0, 1024);
			this.mailboxInstanceGuid = Factory.CreatePhysicalColumn("MailboxInstanceGuid", "MailboxInstanceGuid", typeof(Guid), true, false, false, false, false, Visibility.Public, 0, 16, 16);
			this.mappingSignatureGuid = Factory.CreatePhysicalColumn("MappingSignatureGuid", "MappingSignatureGuid", typeof(Guid), false, false, false, false, false, Visibility.Public, 0, 16, 16);
			this.mailboxOwnerDisplayName = Factory.CreatePhysicalColumn("MailboxOwnerDisplayName", "MailboxOwnerDisplayName", typeof(string), true, false, false, false, false, Visibility.Redacted, 1024, 0, 1024);
			this.displayName = Factory.CreatePhysicalColumn("DisplayName", "DisplayName", typeof(string), true, false, false, false, false, Visibility.Redacted, 1024, 0, 1024);
			this.simpleDisplayName = Factory.CreatePhysicalColumn("SimpleDisplayName", "SimpleDisplayName", typeof(string), true, false, false, false, false, Visibility.Redacted, 128, 0, 128);
			this.comment = Factory.CreatePhysicalColumn("Comment", "Comment", typeof(string), true, false, false, false, false, Visibility.Redacted, 1024, 0, 1024);
			this.oofState = Factory.CreatePhysicalColumn("OofState", "OofState", typeof(bool), false, false, false, false, false, Visibility.Public, 0, 1, 1);
			this.deletedOn = Factory.CreatePhysicalColumn("DeletedOn", "DeletedOn", typeof(DateTime), true, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.status = Factory.CreatePhysicalColumn("Status", "Status", typeof(short), false, false, false, false, false, Visibility.Public, 0, 2, 2);
			this.lcid = Factory.CreatePhysicalColumn("Lcid", "Lcid", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.propertyBlob = Factory.CreatePhysicalColumn("PropertyBlob", "PropertyBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.propertyBag = Factory.CreatePhysicalColumn("PropertyBag", "PropertyBag", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 4, 0, 4);
			this.largePropertyValueBlob = Factory.CreatePhysicalColumn("LargePropertyValueBlob", "LargePropertyValueBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.messageCount = Factory.CreatePhysicalColumn("MessageCount", "MessageCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.messageSize = Factory.CreatePhysicalColumn("MessageSize", "MessageSize", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.hiddenMessageCount = Factory.CreatePhysicalColumn("HiddenMessageCount", "HiddenMessageCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.hiddenMessageSize = Factory.CreatePhysicalColumn("HiddenMessageSize", "HiddenMessageSize", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.messageDeletedCount = Factory.CreatePhysicalColumn("MessageDeletedCount", "MessageDeletedCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.messageDeletedSize = Factory.CreatePhysicalColumn("MessageDeletedSize", "MessageDeletedSize", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.hiddenMessageDeletedCount = Factory.CreatePhysicalColumn("HiddenMessageDeletedCount", "HiddenMessageDeletedCount", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.hiddenMessageDeletedSize = Factory.CreatePhysicalColumn("HiddenMessageDeletedSize", "HiddenMessageDeletedSize", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.lastLogonTime = Factory.CreatePhysicalColumn("LastLogonTime", "LastLogonTime", typeof(DateTime), true, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.lastLogoffTime = Factory.CreatePhysicalColumn("LastLogoffTime", "LastLogoffTime", typeof(DateTime), true, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.conversationEnabled = Factory.CreatePhysicalColumn("ConversationEnabled", "ConversationEnabled", typeof(bool), false, false, false, false, false, Visibility.Public, 0, 1, 1);
			this.mailboxDatabaseVersion = Factory.CreatePhysicalColumn("MailboxDatabaseVersion", "MailboxDatabaseVersion", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.lastQuotaNotificationTime = Factory.CreatePhysicalColumn("LastQuotaNotificationTime", "LastQuotaNotificationTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.preservingMailboxSignature = Factory.CreatePhysicalColumn("PreservingMailboxSignature", "PreservingMailboxSignature", typeof(bool), false, false, false, false, false, Visibility.Public, 0, 1, 1);
			this.tenantHint = Factory.CreatePhysicalColumn("TenantHint", "TenantHint", typeof(byte[]), true, false, false, false, false, Visibility.Public, 128, 0, 128);
			this.nextMessageDocumentId = Factory.CreatePhysicalColumn("NextMessageDocumentId", "NextMessageDocumentId", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.defaultPromotedMessagePropertyIds = Factory.CreatePhysicalColumn("DefaultPromotedMessagePropertyIds", "DefaultPromotedMessagePropertyIds", typeof(short[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.alwaysPromotedMessagePropertyIds = Factory.CreatePhysicalColumn("AlwaysPromotedMessagePropertyIds", "AlwaysPromotedMessagePropertyIds", typeof(short[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Redacted, 1073741824, 0, 1073741824);
			this.lastMailboxMaintenanceTime = Factory.CreatePhysicalColumn("LastMailboxMaintenanceTime", "LastMailboxMaintenanceTime", typeof(DateTime), true, false, false, false, true, Visibility.Public, 0, 8, 8);
			this.mailboxPartitionNumber = Factory.CreatePhysicalColumn("MailboxPartitionNumber", "MailboxPartitionNumber", typeof(int), true, false, false, false, true, Visibility.Public, 0, 4, 4);
			this.unifiedMailboxGuid = Factory.CreatePhysicalColumn("UnifiedMailboxGuid", "UnifiedMailboxGuid", typeof(Guid), true, false, false, false, true, Visibility.Public, 0, 16, 16);
			string name = "MailboxTablePK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[1];
			this.mailboxTablePK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true
			}, new PhysicalColumn[]
			{
				this.MailboxNumber
			});
			this.mailboxGuidIndex = new Index("MailboxGuidIndex", false, true, false, new bool[]
			{
				true
			}, new bool[]
			{
				true
			}, new PhysicalColumn[]
			{
				this.MailboxGuid
			});
			this.unifiedMailboxGuidIndex = new Index("UnifiedMailboxGuidIndex", false, false, true, new bool[]
			{
				true
			}, new bool[]
			{
				true
			}, new PhysicalColumn[]
			{
				this.UnifiedMailboxGuid
			});
			Index[] indexes = new Index[]
			{
				this.MailboxTablePK,
				this.MailboxGuidIndex
			};
			SpecialColumns specialCols = new SpecialColumns(this.PropertyBlob, null, this.PropertyBag, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[]
			{
				this.PropertyBag
			};
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.MailboxNumber,
				this.MailboxGuid,
				this.OwnerADGuid,
				this.OwnerLegacyDN,
				this.MailboxInstanceGuid,
				this.MappingSignatureGuid,
				this.MailboxOwnerDisplayName,
				this.DisplayName,
				this.SimpleDisplayName,
				this.Comment,
				this.OofState,
				this.DeletedOn,
				this.Status,
				this.Lcid,
				this.PropertyBlob,
				this.LargePropertyValueBlob,
				this.MessageCount,
				this.MessageSize,
				this.HiddenMessageCount,
				this.HiddenMessageSize,
				this.MessageDeletedCount,
				this.MessageDeletedSize,
				this.HiddenMessageDeletedCount,
				this.HiddenMessageDeletedSize,
				this.LastLogonTime,
				this.LastLogoffTime,
				this.ConversationEnabled,
				this.MailboxDatabaseVersion,
				this.LastQuotaNotificationTime,
				this.PreservingMailboxSignature,
				this.TenantHint,
				this.NextMessageDocumentId,
				this.DefaultPromotedMessagePropertyIds,
				this.AlwaysPromotedMessagePropertyIds,
				this.ExtensionBlob,
				this.LastMailboxMaintenanceTime,
				this.MailboxPartitionNumber,
				this.UnifiedMailboxGuid
			};
			this.table = Factory.CreateTable("Mailbox", TableClass.Mailbox, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Public, false, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000040A1 File Offset: 0x000022A1
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000040A9 File Offset: 0x000022A9
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000040B1 File Offset: 0x000022B1
		public PhysicalColumn MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000040B9 File Offset: 0x000022B9
		public PhysicalColumn OwnerADGuid
		{
			get
			{
				return this.ownerADGuid;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000040C1 File Offset: 0x000022C1
		public PhysicalColumn OwnerLegacyDN
		{
			get
			{
				return this.ownerLegacyDN;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000040C9 File Offset: 0x000022C9
		public PhysicalColumn MailboxInstanceGuid
		{
			get
			{
				return this.mailboxInstanceGuid;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000040D1 File Offset: 0x000022D1
		public PhysicalColumn MappingSignatureGuid
		{
			get
			{
				return this.mappingSignatureGuid;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000040D9 File Offset: 0x000022D9
		public PhysicalColumn MailboxOwnerDisplayName
		{
			get
			{
				return this.mailboxOwnerDisplayName;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000040E1 File Offset: 0x000022E1
		public PhysicalColumn DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000040E9 File Offset: 0x000022E9
		public PhysicalColumn SimpleDisplayName
		{
			get
			{
				return this.simpleDisplayName;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000040F1 File Offset: 0x000022F1
		public PhysicalColumn Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000040F9 File Offset: 0x000022F9
		public PhysicalColumn OofState
		{
			get
			{
				return this.oofState;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00004101 File Offset: 0x00002301
		public PhysicalColumn DeletedOn
		{
			get
			{
				return this.deletedOn;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00004109 File Offset: 0x00002309
		public PhysicalColumn Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00004111 File Offset: 0x00002311
		public PhysicalColumn Lcid
		{
			get
			{
				return this.lcid;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00004119 File Offset: 0x00002319
		public PhysicalColumn PropertyBlob
		{
			get
			{
				return this.propertyBlob;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00004121 File Offset: 0x00002321
		public PhysicalColumn PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00004129 File Offset: 0x00002329
		public PhysicalColumn LargePropertyValueBlob
		{
			get
			{
				return this.largePropertyValueBlob;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00004131 File Offset: 0x00002331
		public PhysicalColumn MessageCount
		{
			get
			{
				return this.messageCount;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00004139 File Offset: 0x00002339
		public PhysicalColumn MessageSize
		{
			get
			{
				return this.messageSize;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00004141 File Offset: 0x00002341
		public PhysicalColumn HiddenMessageCount
		{
			get
			{
				return this.hiddenMessageCount;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00004149 File Offset: 0x00002349
		public PhysicalColumn HiddenMessageSize
		{
			get
			{
				return this.hiddenMessageSize;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00004151 File Offset: 0x00002351
		public PhysicalColumn MessageDeletedCount
		{
			get
			{
				return this.messageDeletedCount;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00004159 File Offset: 0x00002359
		public PhysicalColumn MessageDeletedSize
		{
			get
			{
				return this.messageDeletedSize;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00004161 File Offset: 0x00002361
		public PhysicalColumn HiddenMessageDeletedCount
		{
			get
			{
				return this.hiddenMessageDeletedCount;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00004169 File Offset: 0x00002369
		public PhysicalColumn HiddenMessageDeletedSize
		{
			get
			{
				return this.hiddenMessageDeletedSize;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00004171 File Offset: 0x00002371
		public PhysicalColumn LastLogonTime
		{
			get
			{
				return this.lastLogonTime;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00004179 File Offset: 0x00002379
		public PhysicalColumn LastLogoffTime
		{
			get
			{
				return this.lastLogoffTime;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00004181 File Offset: 0x00002381
		public PhysicalColumn ConversationEnabled
		{
			get
			{
				return this.conversationEnabled;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00004189 File Offset: 0x00002389
		public PhysicalColumn MailboxDatabaseVersion
		{
			get
			{
				return this.mailboxDatabaseVersion;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00004191 File Offset: 0x00002391
		public PhysicalColumn LastQuotaNotificationTime
		{
			get
			{
				return this.lastQuotaNotificationTime;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00004199 File Offset: 0x00002399
		public PhysicalColumn PreservingMailboxSignature
		{
			get
			{
				return this.preservingMailboxSignature;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000041A1 File Offset: 0x000023A1
		public PhysicalColumn TenantHint
		{
			get
			{
				return this.tenantHint;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000041A9 File Offset: 0x000023A9
		public PhysicalColumn NextMessageDocumentId
		{
			get
			{
				return this.nextMessageDocumentId;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000041B1 File Offset: 0x000023B1
		public PhysicalColumn DefaultPromotedMessagePropertyIds
		{
			get
			{
				return this.defaultPromotedMessagePropertyIds;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000041B9 File Offset: 0x000023B9
		public PhysicalColumn AlwaysPromotedMessagePropertyIds
		{
			get
			{
				return this.alwaysPromotedMessagePropertyIds;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000041C1 File Offset: 0x000023C1
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000041C9 File Offset: 0x000023C9
		public PhysicalColumn LastMailboxMaintenanceTime
		{
			get
			{
				return this.lastMailboxMaintenanceTime;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000041D1 File Offset: 0x000023D1
		public PhysicalColumn MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000041D9 File Offset: 0x000023D9
		public PhysicalColumn UnifiedMailboxGuid
		{
			get
			{
				return this.unifiedMailboxGuid;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000041E1 File Offset: 0x000023E1
		public Index MailboxTablePK
		{
			get
			{
				return this.mailboxTablePK;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000041E9 File Offset: 0x000023E9
		public Index MailboxGuidIndex
		{
			get
			{
				return this.mailboxGuidIndex;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000041F1 File Offset: 0x000023F1
		public Index UnifiedMailboxGuidIndex
		{
			get
			{
				return this.unifiedMailboxGuidIndex;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000041FC File Offset: 0x000023FC
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.mailboxNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxNumber = null;
			}
			physicalColumn = this.mailboxGuid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxGuid = null;
			}
			physicalColumn = this.ownerADGuid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.ownerADGuid = null;
			}
			physicalColumn = this.ownerLegacyDN;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.ownerLegacyDN = null;
			}
			physicalColumn = this.mailboxInstanceGuid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxInstanceGuid = null;
			}
			physicalColumn = this.mappingSignatureGuid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mappingSignatureGuid = null;
			}
			physicalColumn = this.mailboxOwnerDisplayName;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxOwnerDisplayName = null;
			}
			physicalColumn = this.displayName;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.displayName = null;
			}
			physicalColumn = this.simpleDisplayName;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.simpleDisplayName = null;
			}
			physicalColumn = this.comment;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.comment = null;
			}
			physicalColumn = this.oofState;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.oofState = null;
			}
			physicalColumn = this.deletedOn;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.deletedOn = null;
			}
			physicalColumn = this.status;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.status = null;
			}
			physicalColumn = this.lcid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lcid = null;
			}
			physicalColumn = this.propertyBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.propertyBlob = null;
			}
			physicalColumn = this.propertyBag;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.propertyBag = null;
			}
			physicalColumn = this.largePropertyValueBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.largePropertyValueBlob = null;
			}
			physicalColumn = this.messageCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.messageCount = null;
			}
			physicalColumn = this.messageSize;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.messageSize = null;
			}
			physicalColumn = this.hiddenMessageCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.hiddenMessageCount = null;
			}
			physicalColumn = this.hiddenMessageSize;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.hiddenMessageSize = null;
			}
			physicalColumn = this.messageDeletedCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.messageDeletedCount = null;
			}
			physicalColumn = this.messageDeletedSize;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.messageDeletedSize = null;
			}
			physicalColumn = this.hiddenMessageDeletedCount;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.hiddenMessageDeletedCount = null;
			}
			physicalColumn = this.hiddenMessageDeletedSize;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.hiddenMessageDeletedSize = null;
			}
			physicalColumn = this.lastLogonTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lastLogonTime = null;
			}
			physicalColumn = this.lastLogoffTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lastLogoffTime = null;
			}
			physicalColumn = this.conversationEnabled;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.conversationEnabled = null;
			}
			physicalColumn = this.mailboxDatabaseVersion;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxDatabaseVersion = null;
			}
			physicalColumn = this.lastQuotaNotificationTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lastQuotaNotificationTime = null;
			}
			physicalColumn = this.preservingMailboxSignature;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.preservingMailboxSignature = null;
			}
			physicalColumn = this.tenantHint;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.tenantHint = null;
			}
			physicalColumn = this.nextMessageDocumentId;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.nextMessageDocumentId = null;
			}
			physicalColumn = this.defaultPromotedMessagePropertyIds;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.defaultPromotedMessagePropertyIds = null;
			}
			physicalColumn = this.alwaysPromotedMessagePropertyIds;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.alwaysPromotedMessagePropertyIds = null;
			}
			physicalColumn = this.extensionBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.extensionBlob = null;
			}
			physicalColumn = this.lastMailboxMaintenanceTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lastMailboxMaintenanceTime = null;
			}
			physicalColumn = this.mailboxPartitionNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.mailboxPartitionNumber = null;
			}
			physicalColumn = this.unifiedMailboxGuid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.unifiedMailboxGuid = null;
			}
			for (int i = this.Table.Columns.Count - 1; i >= 0; i--)
			{
				this.Table.Columns[i].Index = i;
			}
			Index index = this.mailboxTablePK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.mailboxTablePK = null;
				this.Table.Indexes.Remove(index);
			}
			index = this.mailboxGuidIndex;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.mailboxGuidIndex = null;
				this.Table.Indexes.Remove(index);
			}
			index = this.unifiedMailboxGuidIndex;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.unifiedMailboxGuidIndex = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x04000044 RID: 68
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x04000045 RID: 69
		public const string MailboxGuidName = "MailboxGuid";

		// Token: 0x04000046 RID: 70
		public const string OwnerADGuidName = "OwnerADGuid";

		// Token: 0x04000047 RID: 71
		public const string OwnerLegacyDNName = "OwnerLegacyDN";

		// Token: 0x04000048 RID: 72
		public const string MailboxInstanceGuidName = "MailboxInstanceGuid";

		// Token: 0x04000049 RID: 73
		public const string MappingSignatureGuidName = "MappingSignatureGuid";

		// Token: 0x0400004A RID: 74
		public const string MailboxOwnerDisplayNameName = "MailboxOwnerDisplayName";

		// Token: 0x0400004B RID: 75
		public const string DisplayNameName = "DisplayName";

		// Token: 0x0400004C RID: 76
		public const string SimpleDisplayNameName = "SimpleDisplayName";

		// Token: 0x0400004D RID: 77
		public const string CommentName = "Comment";

		// Token: 0x0400004E RID: 78
		public const string OofStateName = "OofState";

		// Token: 0x0400004F RID: 79
		public const string DeletedOnName = "DeletedOn";

		// Token: 0x04000050 RID: 80
		public const string StatusName = "Status";

		// Token: 0x04000051 RID: 81
		public const string LcidName = "Lcid";

		// Token: 0x04000052 RID: 82
		public const string PropertyBlobName = "PropertyBlob";

		// Token: 0x04000053 RID: 83
		public const string PropertyBagName = "PropertyBag";

		// Token: 0x04000054 RID: 84
		public const string LargePropertyValueBlobName = "LargePropertyValueBlob";

		// Token: 0x04000055 RID: 85
		public const string MessageCountName = "MessageCount";

		// Token: 0x04000056 RID: 86
		public const string MessageSizeName = "MessageSize";

		// Token: 0x04000057 RID: 87
		public const string HiddenMessageCountName = "HiddenMessageCount";

		// Token: 0x04000058 RID: 88
		public const string HiddenMessageSizeName = "HiddenMessageSize";

		// Token: 0x04000059 RID: 89
		public const string MessageDeletedCountName = "MessageDeletedCount";

		// Token: 0x0400005A RID: 90
		public const string MessageDeletedSizeName = "MessageDeletedSize";

		// Token: 0x0400005B RID: 91
		public const string HiddenMessageDeletedCountName = "HiddenMessageDeletedCount";

		// Token: 0x0400005C RID: 92
		public const string HiddenMessageDeletedSizeName = "HiddenMessageDeletedSize";

		// Token: 0x0400005D RID: 93
		public const string LastLogonTimeName = "LastLogonTime";

		// Token: 0x0400005E RID: 94
		public const string LastLogoffTimeName = "LastLogoffTime";

		// Token: 0x0400005F RID: 95
		public const string ConversationEnabledName = "ConversationEnabled";

		// Token: 0x04000060 RID: 96
		public const string MailboxDatabaseVersionName = "MailboxDatabaseVersion";

		// Token: 0x04000061 RID: 97
		public const string LastQuotaNotificationTimeName = "LastQuotaNotificationTime";

		// Token: 0x04000062 RID: 98
		public const string PreservingMailboxSignatureName = "PreservingMailboxSignature";

		// Token: 0x04000063 RID: 99
		public const string TenantHintName = "TenantHint";

		// Token: 0x04000064 RID: 100
		public const string NextMessageDocumentIdName = "NextMessageDocumentId";

		// Token: 0x04000065 RID: 101
		public const string DefaultPromotedMessagePropertyIdsName = "DefaultPromotedMessagePropertyIds";

		// Token: 0x04000066 RID: 102
		public const string AlwaysPromotedMessagePropertyIdsName = "AlwaysPromotedMessagePropertyIds";

		// Token: 0x04000067 RID: 103
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x04000068 RID: 104
		public const string LastMailboxMaintenanceTimeName = "LastMailboxMaintenanceTime";

		// Token: 0x04000069 RID: 105
		public const string MailboxPartitionNumberName = "MailboxPartitionNumber";

		// Token: 0x0400006A RID: 106
		public const string UnifiedMailboxGuidName = "UnifiedMailboxGuid";

		// Token: 0x0400006B RID: 107
		public const string PhysicalTableName = "Mailbox";

		// Token: 0x0400006C RID: 108
		private PhysicalColumn mailboxNumber;

		// Token: 0x0400006D RID: 109
		private PhysicalColumn mailboxGuid;

		// Token: 0x0400006E RID: 110
		private PhysicalColumn ownerADGuid;

		// Token: 0x0400006F RID: 111
		private PhysicalColumn ownerLegacyDN;

		// Token: 0x04000070 RID: 112
		private PhysicalColumn mailboxInstanceGuid;

		// Token: 0x04000071 RID: 113
		private PhysicalColumn mappingSignatureGuid;

		// Token: 0x04000072 RID: 114
		private PhysicalColumn mailboxOwnerDisplayName;

		// Token: 0x04000073 RID: 115
		private PhysicalColumn displayName;

		// Token: 0x04000074 RID: 116
		private PhysicalColumn simpleDisplayName;

		// Token: 0x04000075 RID: 117
		private PhysicalColumn comment;

		// Token: 0x04000076 RID: 118
		private PhysicalColumn oofState;

		// Token: 0x04000077 RID: 119
		private PhysicalColumn deletedOn;

		// Token: 0x04000078 RID: 120
		private PhysicalColumn status;

		// Token: 0x04000079 RID: 121
		private PhysicalColumn lcid;

		// Token: 0x0400007A RID: 122
		private PhysicalColumn propertyBlob;

		// Token: 0x0400007B RID: 123
		private PhysicalColumn propertyBag;

		// Token: 0x0400007C RID: 124
		private PhysicalColumn largePropertyValueBlob;

		// Token: 0x0400007D RID: 125
		private PhysicalColumn messageCount;

		// Token: 0x0400007E RID: 126
		private PhysicalColumn messageSize;

		// Token: 0x0400007F RID: 127
		private PhysicalColumn hiddenMessageCount;

		// Token: 0x04000080 RID: 128
		private PhysicalColumn hiddenMessageSize;

		// Token: 0x04000081 RID: 129
		private PhysicalColumn messageDeletedCount;

		// Token: 0x04000082 RID: 130
		private PhysicalColumn messageDeletedSize;

		// Token: 0x04000083 RID: 131
		private PhysicalColumn hiddenMessageDeletedCount;

		// Token: 0x04000084 RID: 132
		private PhysicalColumn hiddenMessageDeletedSize;

		// Token: 0x04000085 RID: 133
		private PhysicalColumn lastLogonTime;

		// Token: 0x04000086 RID: 134
		private PhysicalColumn lastLogoffTime;

		// Token: 0x04000087 RID: 135
		private PhysicalColumn conversationEnabled;

		// Token: 0x04000088 RID: 136
		private PhysicalColumn mailboxDatabaseVersion;

		// Token: 0x04000089 RID: 137
		private PhysicalColumn lastQuotaNotificationTime;

		// Token: 0x0400008A RID: 138
		private PhysicalColumn preservingMailboxSignature;

		// Token: 0x0400008B RID: 139
		private PhysicalColumn tenantHint;

		// Token: 0x0400008C RID: 140
		private PhysicalColumn nextMessageDocumentId;

		// Token: 0x0400008D RID: 141
		private PhysicalColumn defaultPromotedMessagePropertyIds;

		// Token: 0x0400008E RID: 142
		private PhysicalColumn alwaysPromotedMessagePropertyIds;

		// Token: 0x0400008F RID: 143
		private PhysicalColumn extensionBlob;

		// Token: 0x04000090 RID: 144
		private PhysicalColumn lastMailboxMaintenanceTime;

		// Token: 0x04000091 RID: 145
		private PhysicalColumn mailboxPartitionNumber;

		// Token: 0x04000092 RID: 146
		private PhysicalColumn unifiedMailboxGuid;

		// Token: 0x04000093 RID: 147
		private Index mailboxTablePK;

		// Token: 0x04000094 RID: 148
		private Index mailboxGuidIndex;

		// Token: 0x04000095 RID: 149
		private Index unifiedMailboxGuidIndex;

		// Token: 0x04000096 RID: 150
		private Table table;
	}
}
