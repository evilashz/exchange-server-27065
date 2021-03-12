using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class MailboxStatistics : MailboxEntry
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x000064C8 File Offset: 0x000046C8
		private static string FormatShortTermId(long? id)
		{
			if (id != null)
			{
				ulong value = (ulong)id.Value;
				ushort num = (ushort)(value & 65535UL);
				ulong num2 = ((value & 16711680UL) << 24) + ((value & (ulong)-16777216) << 8) + ((value & 1095216660480UL) >> 8) + ((value & 280375465082880UL) >> 24) + ((value & 71776119061217280UL) >> 40) + ((value & 18374686479671623680UL) >> 56);
				return string.Format("{0:X}-{1:X}", num, num2);
			}
			return null;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000655A File Offset: 0x0000475A
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxStatistics.schema;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00006561 File Offset: 0x00004761
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00006569 File Offset: 0x00004769
		internal bool IncludeQuarantineDetails
		{
			get
			{
				return this.includeQuarantineDetails;
			}
			set
			{
				this.includeQuarantineDetails = value;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006572 File Offset: 0x00004772
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			this.ValidateMailboxGuid(errors);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006582 File Offset: 0x00004782
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			this.ValidateMailboxGuid(errors);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006592 File Offset: 0x00004792
		private void ValidateMailboxGuid(List<ValidationError> errors)
		{
			if (Guid.Empty == this.MailboxGuid)
			{
				errors.Add(new PropertyValidationError(Strings.ErrorMailboxStatisticsMailboxGuidEmpty, null, this));
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000065B8 File Offset: 0x000047B8
		public string CurrentSchemaVersion
		{
			get
			{
				int? num = (int?)this[MailboxStatisticsSchema.CurrentSchemaVersion];
				if (num != null)
				{
					return string.Format("{0}.{1}", num.Value >> 16, num.Value & 65535);
				}
				return null;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000660C File Offset: 0x0000480C
		public uint? AssociatedItemCount
		{
			get
			{
				return (uint?)this[MailboxStatisticsSchema.AssociatedItemCount];
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000661E File Offset: 0x0000481E
		public uint? DeletedItemCount
		{
			get
			{
				return (uint?)this[MailboxStatisticsSchema.DeletedItemCount];
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00006630 File Offset: 0x00004830
		public DateTime? DisconnectDate
		{
			get
			{
				return (DateTime?)this[MailboxStatisticsSchema.DisconnectDate];
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00006644 File Offset: 0x00004844
		public MailboxState? DisconnectReason
		{
			get
			{
				object obj = this[MailboxStatisticsSchema.DisconnectDate];
				object obj2 = this[MailboxStatisticsSchema.MailboxMiscFlags];
				MailboxState? result = new MailboxState?(MailboxState.Unknown);
				if (obj != null && obj2 != null)
				{
					if (((MailboxMiscFlags)obj2 & MailboxMiscFlags.DisabledMailbox) == MailboxMiscFlags.DisabledMailbox)
					{
						result = new MailboxState?(MailboxState.Disabled);
					}
					else if (((MailboxMiscFlags)obj2 & MailboxMiscFlags.SoftDeletedMailbox) == MailboxMiscFlags.SoftDeletedMailbox)
					{
						result = new MailboxState?(MailboxState.SoftDeleted);
					}
					else
					{
						result = new MailboxState?(MailboxState.Unknown);
					}
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000066BE File Offset: 0x000048BE
		public string DisplayName
		{
			get
			{
				return (string)this[MailboxStatisticsSchema.DisplayName];
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000066D0 File Offset: 0x000048D0
		public uint? ItemCount
		{
			get
			{
				return (uint?)this[MailboxStatisticsSchema.ItemCount];
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000066E2 File Offset: 0x000048E2
		public string LastLoggedOnUserAccount
		{
			get
			{
				return (string)this[MailboxStatisticsSchema.LastLoggedOnUserAccount];
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000066F4 File Offset: 0x000048F4
		public DateTime? LastLogoffTime
		{
			get
			{
				return (DateTime?)this[MailboxStatisticsSchema.LastLogoffTime];
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00006706 File Offset: 0x00004906
		public DateTime? LastLogonTime
		{
			get
			{
				return (DateTime?)this[MailboxStatisticsSchema.LastLogonTime];
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00006718 File Offset: 0x00004918
		public string LegacyDN
		{
			get
			{
				return (string)this[MailboxStatisticsSchema.LegacyDN];
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000672A File Offset: 0x0000492A
		public Guid MailboxGuid
		{
			get
			{
				return (Guid)this[MailboxStatisticsSchema.MailboxGuid];
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000673C File Offset: 0x0000493C
		public StoreMailboxType MailboxType
		{
			get
			{
				object obj = this[MailboxStatisticsSchema.StoreMailboxType];
				if (obj != null)
				{
					return (StoreMailboxType)obj;
				}
				return StoreMailboxType.Private;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00006760 File Offset: 0x00004960
		public ObjectClass ObjectClass
		{
			get
			{
				return (ObjectClass)this[MailboxStatisticsSchema.ObjectClass];
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00006772 File Offset: 0x00004972
		public StorageLimitStatus? StorageLimitStatus
		{
			get
			{
				return (StorageLimitStatus?)this[MailboxStatisticsSchema.StorageLimitStatus];
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00006784 File Offset: 0x00004984
		public Unlimited<ByteQuantifiedSize> TotalDeletedItemSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxStatisticsSchema.TotalDeletedItemSize];
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00006796 File Offset: 0x00004996
		public Unlimited<ByteQuantifiedSize> TotalItemSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxStatisticsSchema.TotalItemSize];
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000067A8 File Offset: 0x000049A8
		public string MailboxTableIdentifier
		{
			get
			{
				return MailboxStatistics.FormatShortTermId((long?)this[MailboxStatisticsSchema.MailboxRootEntryId]);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000067BF File Offset: 0x000049BF
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000067C7 File Offset: 0x000049C7
		public ObjectId Database { get; internal set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000067D0 File Offset: 0x000049D0
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000067D8 File Offset: 0x000049D8
		public string ServerName { get; internal set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000067E1 File Offset: 0x000049E1
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x000067E9 File Offset: 0x000049E9
		public string DatabaseName { get; internal set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000067F2 File Offset: 0x000049F2
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000067FA File Offset: 0x000049FA
		public bool IsDatabaseCopyActive { get; internal set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00006803 File Offset: 0x00004A03
		public bool IsQuarantined
		{
			get
			{
				return (bool)this[MailboxStatisticsSchema.IsQuarantined];
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00006815 File Offset: 0x00004A15
		public string QuarantineDescription
		{
			get
			{
				if (!this.IncludeQuarantineDetails)
				{
					return string.Empty;
				}
				return (string)this[MailboxStatisticsSchema.QuarantineDescription];
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00006838 File Offset: 0x00004A38
		public DateTime? QuarantineLastCrash
		{
			get
			{
				if (!this.IncludeQuarantineDetails)
				{
					return null;
				}
				return (DateTime?)this[MailboxStatisticsSchema.QuarantineLastCrash];
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00006867 File Offset: 0x00004A67
		public DateTime? QuarantineEnd
		{
			get
			{
				return (DateTime?)this[MailboxStatisticsSchema.QuarantineEnd];
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000687C File Offset: 0x00004A7C
		public Guid ExternalDirectoryOrganizationId
		{
			get
			{
				if (this[MailboxStatisticsSchema.PersistableTenantPartitionHint] == null)
				{
					return Guid.Empty;
				}
				return TenantPartitionHint.FromPersistablePartitionHint((byte[])this[MailboxStatisticsSchema.PersistableTenantPartitionHint]).GetExternalDirectoryOrganizationId();
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000068B8 File Offset: 0x00004AB8
		public bool? IsArchiveMailbox
		{
			get
			{
				return this.CheckMailboxMiscFlags(MailboxMiscFlags.ArchiveMailbox);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000068C2 File Offset: 0x00004AC2
		public bool? IsMoveDestination
		{
			get
			{
				return this.CheckMailboxMiscFlags(MailboxMiscFlags.CreatedByMove);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000068CC File Offset: 0x00004ACC
		public int? MailboxMessagesPerFolderCountWarningQuota
		{
			get
			{
				return (int?)this[MailboxStatisticsSchema.MailboxMessagesPerFolderCountWarningQuota];
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000068DE File Offset: 0x00004ADE
		public int? MailboxMessagesPerFolderCountReceiveQuota
		{
			get
			{
				return (int?)this[MailboxStatisticsSchema.MailboxMessagesPerFolderCountReceiveQuota];
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000068F0 File Offset: 0x00004AF0
		public int? DumpsterMessagesPerFolderCountWarningQuota
		{
			get
			{
				return (int?)this[MailboxStatisticsSchema.DumpsterMessagesPerFolderCountWarningQuota];
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00006902 File Offset: 0x00004B02
		public int? DumpsterMessagesPerFolderCountReceiveQuota
		{
			get
			{
				return (int?)this[MailboxStatisticsSchema.DumpsterMessagesPerFolderCountReceiveQuota];
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00006914 File Offset: 0x00004B14
		public int? FolderHierarchyChildrenCountWarningQuota
		{
			get
			{
				return (int?)this[MailboxStatisticsSchema.FolderHierarchyChildrenCountWarningQuota];
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00006926 File Offset: 0x00004B26
		public int? FolderHierarchyChildrenCountReceiveQuota
		{
			get
			{
				return (int?)this[MailboxStatisticsSchema.FolderHierarchyChildrenCountReceiveQuota];
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00006938 File Offset: 0x00004B38
		public int? FolderHierarchyDepthWarningQuota
		{
			get
			{
				return (int?)this[MailboxStatisticsSchema.FolderHierarchyDepthWarningQuota];
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000694A File Offset: 0x00004B4A
		public int? FolderHierarchyDepthReceiveQuota
		{
			get
			{
				return (int?)this[MailboxStatisticsSchema.FolderHierarchyDepthReceiveQuota];
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000695C File Offset: 0x00004B5C
		public int? FoldersCountWarningQuota
		{
			get
			{
				return (int?)this[MailboxStatisticsSchema.FoldersCountWarningQuota];
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000696E File Offset: 0x00004B6E
		public int? FoldersCountReceiveQuota
		{
			get
			{
				return (int?)this[MailboxStatisticsSchema.FoldersCountReceiveQuota];
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006980 File Offset: 0x00004B80
		public int? NamedPropertiesCountQuota
		{
			get
			{
				return (int?)this[MailboxStatisticsSchema.NamedPropertiesCountQuota];
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00006992 File Offset: 0x00004B92
		public Unlimited<ByteQuantifiedSize> MessageTableTotalSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxStatisticsSchema.MessageTableTotalSize];
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000069A4 File Offset: 0x00004BA4
		public Unlimited<ByteQuantifiedSize> MessageTableAvailableSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxStatisticsSchema.MessageTableAvailableSize];
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000069B6 File Offset: 0x00004BB6
		public Unlimited<ByteQuantifiedSize> AttachmentTableTotalSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxStatisticsSchema.AttachmentTableTotalSize];
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000069C8 File Offset: 0x00004BC8
		public Unlimited<ByteQuantifiedSize> AttachmentTableAvailableSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxStatisticsSchema.AttachmentTableAvailableSize];
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000069DA File Offset: 0x00004BDA
		public Unlimited<ByteQuantifiedSize> OtherTablesTotalSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxStatisticsSchema.OtherTablesTotalSize];
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000069EC File Offset: 0x00004BEC
		public Unlimited<ByteQuantifiedSize> OtherTablesAvailableSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxStatisticsSchema.OtherTablesAvailableSize];
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000069FE File Offset: 0x00004BFE
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00006A06 File Offset: 0x00004C06
		public Unlimited<ByteQuantifiedSize> DatabaseIssueWarningQuota { get; internal set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00006A0F File Offset: 0x00004C0F
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00006A17 File Offset: 0x00004C17
		public Unlimited<ByteQuantifiedSize> DatabaseProhibitSendQuota { get; internal set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00006A20 File Offset: 0x00004C20
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00006A28 File Offset: 0x00004C28
		public Unlimited<ByteQuantifiedSize> DatabaseProhibitSendReceiveQuota { get; internal set; }

		// Token: 0x06000104 RID: 260 RVA: 0x00006A31 File Offset: 0x00004C31
		public MailboxStatistics()
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006A39 File Offset: 0x00004C39
		internal MailboxStatistics(MailboxId mapiObjectId, MapiSession mapiSession) : base(mapiObjectId, mapiSession)
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006A44 File Offset: 0x00004C44
		private bool? CheckMailboxMiscFlags(MailboxMiscFlags flags)
		{
			bool? result = null;
			object obj = this[MailboxStatisticsSchema.MailboxMiscFlags];
			if (obj != null)
			{
				if (((MailboxMiscFlags)obj & flags) == flags)
				{
					result = new bool?(true);
				}
				else
				{
					result = new bool?(false);
				}
			}
			return result;
		}

		// Token: 0x040000D3 RID: 211
		private static MapiObjectSchema schema = ObjectSchema.GetInstance<MailboxStatisticsSchema>();

		// Token: 0x040000D4 RID: 212
		private bool includeQuarantineDetails;
	}
}
