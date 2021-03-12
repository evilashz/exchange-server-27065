using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200075D RID: 1885
	[ProvisioningObjectTag("SyncDistributionGroup")]
	[Serializable]
	public class SyncDistributionGroup : DistributionGroup
	{
		// Token: 0x17001FC2 RID: 8130
		// (get) Token: 0x06005B65 RID: 23397 RVA: 0x0013FC4D File Offset: 0x0013DE4D
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return SyncDistributionGroup.schema;
			}
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x0013FC54 File Offset: 0x0013DE54
		public SyncDistributionGroup()
		{
			base.SetObjectClass("group");
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x0013FC67 File Offset: 0x0013DE67
		public SyncDistributionGroup(ADGroup dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x0013FC70 File Offset: 0x0013DE70
		internal new static SyncDistributionGroup FromDataObject(ADGroup dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new SyncDistributionGroup(dataObject);
		}

		// Token: 0x17001FC3 RID: 8131
		// (get) Token: 0x06005B69 RID: 23401 RVA: 0x0013FC7D File Offset: 0x0013DE7D
		// (set) Token: 0x06005B6A RID: 23402 RVA: 0x0013FC8F File Offset: 0x0013DE8F
		[Parameter(Mandatory = false)]
		public byte[] BlockedSendersHash
		{
			get
			{
				return (byte[])this[SyncDistributionGroupSchema.BlockedSendersHash];
			}
			set
			{
				this[SyncDistributionGroupSchema.BlockedSendersHash] = value;
			}
		}

		// Token: 0x17001FC4 RID: 8132
		// (get) Token: 0x06005B6B RID: 23403 RVA: 0x0013FC9D File Offset: 0x0013DE9D
		// (set) Token: 0x06005B6C RID: 23404 RVA: 0x0013FCAF File Offset: 0x0013DEAF
		public new MultiValuedProperty<ADObjectId> BypassModerationFrom
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailEnabledRecipientSchema.BypassModerationFrom];
			}
			set
			{
				this[MailEnabledRecipientSchema.BypassModerationFrom] = value;
			}
		}

		// Token: 0x17001FC5 RID: 8133
		// (get) Token: 0x06005B6D RID: 23405 RVA: 0x0013FCBD File Offset: 0x0013DEBD
		// (set) Token: 0x06005B6E RID: 23406 RVA: 0x0013FCCF File Offset: 0x0013DECF
		public new MultiValuedProperty<ADObjectId> BypassModerationFromDLMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailEnabledRecipientSchema.BypassModerationFromDLMembers];
			}
			set
			{
				this[MailEnabledRecipientSchema.BypassModerationFromDLMembers] = value;
			}
		}

		// Token: 0x17001FC6 RID: 8134
		// (get) Token: 0x06005B6F RID: 23407 RVA: 0x0013FCDD File Offset: 0x0013DEDD
		public MultiValuedProperty<ADObjectId> Members
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[SyncDistributionGroupSchema.Members];
			}
		}

		// Token: 0x17001FC7 RID: 8135
		// (get) Token: 0x06005B70 RID: 23408 RVA: 0x0013FCEF File Offset: 0x0013DEEF
		// (set) Token: 0x06005B71 RID: 23409 RVA: 0x0013FD01 File Offset: 0x0013DF01
		[Parameter(Mandatory = false)]
		public string Notes
		{
			get
			{
				return (string)this[SyncDistributionGroupSchema.Notes];
			}
			set
			{
				this[SyncDistributionGroupSchema.Notes] = value;
			}
		}

		// Token: 0x17001FC8 RID: 8136
		// (get) Token: 0x06005B72 RID: 23410 RVA: 0x0013FD0F File Offset: 0x0013DF0F
		// (set) Token: 0x06005B73 RID: 23411 RVA: 0x0013FD21 File Offset: 0x0013DF21
		[Parameter(Mandatory = false)]
		public RecipientDisplayType? RecipientDisplayType
		{
			get
			{
				return (RecipientDisplayType?)this[SyncDistributionGroupSchema.RecipientDisplayType];
			}
			set
			{
				this[SyncDistributionGroupSchema.RecipientDisplayType] = value;
			}
		}

		// Token: 0x17001FC9 RID: 8137
		// (get) Token: 0x06005B74 RID: 23412 RVA: 0x0013FD34 File Offset: 0x0013DF34
		// (set) Token: 0x06005B75 RID: 23413 RVA: 0x0013FD46 File Offset: 0x0013DF46
		[Parameter(Mandatory = false)]
		public byte[] SafeRecipientsHash
		{
			get
			{
				return (byte[])this[SyncDistributionGroupSchema.SafeRecipientsHash];
			}
			set
			{
				this[SyncDistributionGroupSchema.SafeRecipientsHash] = value;
			}
		}

		// Token: 0x17001FCA RID: 8138
		// (get) Token: 0x06005B76 RID: 23414 RVA: 0x0013FD54 File Offset: 0x0013DF54
		// (set) Token: 0x06005B77 RID: 23415 RVA: 0x0013FD66 File Offset: 0x0013DF66
		[Parameter(Mandatory = false)]
		public byte[] SafeSendersHash
		{
			get
			{
				return (byte[])this[SyncDistributionGroupSchema.SafeSendersHash];
			}
			set
			{
				this[SyncDistributionGroupSchema.SafeSendersHash] = value;
			}
		}

		// Token: 0x17001FCB RID: 8139
		// (get) Token: 0x06005B78 RID: 23416 RVA: 0x0013FD74 File Offset: 0x0013DF74
		// (set) Token: 0x06005B79 RID: 23417 RVA: 0x0013FD86 File Offset: 0x0013DF86
		public bool EndOfList
		{
			get
			{
				return (bool)this[SyncDistributionGroupSchema.EndOfList];
			}
			internal set
			{
				this[SyncDistributionGroupSchema.EndOfList] = value;
			}
		}

		// Token: 0x17001FCC RID: 8140
		// (get) Token: 0x06005B7A RID: 23418 RVA: 0x0013FD99 File Offset: 0x0013DF99
		// (set) Token: 0x06005B7B RID: 23419 RVA: 0x0013FDAB File Offset: 0x0013DFAB
		public byte[] Cookie
		{
			get
			{
				return (byte[])this[SyncDistributionGroupSchema.Cookie];
			}
			internal set
			{
				this[SyncDistributionGroupSchema.Cookie] = value;
			}
		}

		// Token: 0x17001FCD RID: 8141
		// (get) Token: 0x06005B7C RID: 23420 RVA: 0x0013FDB9 File Offset: 0x0013DFB9
		// (set) Token: 0x06005B7D RID: 23421 RVA: 0x0013FDCB File Offset: 0x0013DFCB
		[Parameter(Mandatory = false)]
		public string DirSyncId
		{
			get
			{
				return (string)this[SyncDistributionGroupSchema.DirSyncId];
			}
			set
			{
				this[SyncDistributionGroupSchema.DirSyncId] = value;
			}
		}

		// Token: 0x17001FCE RID: 8142
		// (get) Token: 0x06005B7E RID: 23422 RVA: 0x0013FDD9 File Offset: 0x0013DFD9
		// (set) Token: 0x06005B7F RID: 23423 RVA: 0x0013FDEB File Offset: 0x0013DFEB
		[Parameter(Mandatory = false)]
		public int? SeniorityIndex
		{
			get
			{
				return (int?)this[SyncDistributionGroupSchema.SeniorityIndex];
			}
			set
			{
				this[SyncDistributionGroupSchema.SeniorityIndex] = value;
			}
		}

		// Token: 0x17001FCF RID: 8143
		// (get) Token: 0x06005B80 RID: 23424 RVA: 0x0013FDFE File Offset: 0x0013DFFE
		// (set) Token: 0x06005B81 RID: 23425 RVA: 0x0013FE10 File Offset: 0x0013E010
		[Parameter(Mandatory = false)]
		public string PhoneticDisplayName
		{
			get
			{
				return (string)this[SyncDistributionGroupSchema.PhoneticDisplayName];
			}
			set
			{
				this[SyncDistributionGroupSchema.PhoneticDisplayName] = value;
			}
		}

		// Token: 0x17001FD0 RID: 8144
		// (get) Token: 0x06005B82 RID: 23426 RVA: 0x0013FE1E File Offset: 0x0013E01E
		// (set) Token: 0x06005B83 RID: 23427 RVA: 0x0013FE30 File Offset: 0x0013E030
		[Parameter(Mandatory = false)]
		public bool IsHierarchicalGroup
		{
			get
			{
				return (bool)this[SyncDistributionGroupSchema.IsHierarchicalGroup];
			}
			set
			{
				this[SyncDistributionGroupSchema.IsHierarchicalGroup] = value;
			}
		}

		// Token: 0x17001FD1 RID: 8145
		// (get) Token: 0x06005B84 RID: 23428 RVA: 0x0013FE43 File Offset: 0x0013E043
		// (set) Token: 0x06005B85 RID: 23429 RVA: 0x0013FE55 File Offset: 0x0013E055
		public ADObjectId RawManagedBy
		{
			get
			{
				return (ADObjectId)this[SyncDistributionGroupSchema.RawManagedBy];
			}
			internal set
			{
				this[SyncDistributionGroupSchema.RawManagedBy] = value;
			}
		}

		// Token: 0x17001FD2 RID: 8146
		// (get) Token: 0x06005B86 RID: 23430 RVA: 0x0013FE63 File Offset: 0x0013E063
		// (set) Token: 0x06005B87 RID: 23431 RVA: 0x0013FE75 File Offset: 0x0013E075
		public MultiValuedProperty<ADObjectId> CoManagedBy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[SyncDistributionGroupSchema.CoManagedBy];
			}
			internal set
			{
				this[SyncDistributionGroupSchema.CoManagedBy] = value;
			}
		}

		// Token: 0x17001FD3 RID: 8147
		// (get) Token: 0x06005B88 RID: 23432 RVA: 0x0013FE83 File Offset: 0x0013E083
		// (set) Token: 0x06005B89 RID: 23433 RVA: 0x0013FE95 File Offset: 0x0013E095
		[Parameter(Mandatory = false)]
		public string OnPremisesObjectId
		{
			get
			{
				return (string)this[SyncDistributionGroupSchema.OnPremisesObjectId];
			}
			set
			{
				this[SyncDistributionGroupSchema.OnPremisesObjectId] = value;
			}
		}

		// Token: 0x17001FD4 RID: 8148
		// (get) Token: 0x06005B8A RID: 23434 RVA: 0x0013FEA3 File Offset: 0x0013E0A3
		// (set) Token: 0x06005B8B RID: 23435 RVA: 0x0013FEB5 File Offset: 0x0013E0B5
		[Parameter(Mandatory = false)]
		public bool IsDirSynced
		{
			get
			{
				return (bool)this[SyncDistributionGroupSchema.IsDirSynced];
			}
			set
			{
				this[SyncDistributionGroupSchema.IsDirSynced] = value;
			}
		}

		// Token: 0x17001FD5 RID: 8149
		// (get) Token: 0x06005B8C RID: 23436 RVA: 0x0013FEC8 File Offset: 0x0013E0C8
		// (set) Token: 0x06005B8D RID: 23437 RVA: 0x0013FEDA File Offset: 0x0013E0DA
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> DirSyncAuthorityMetadata
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncDistributionGroupSchema.DirSyncAuthorityMetadata];
			}
			set
			{
				this[SyncDistributionGroupSchema.DirSyncAuthorityMetadata] = value;
			}
		}

		// Token: 0x17001FD6 RID: 8150
		// (get) Token: 0x06005B8E RID: 23438 RVA: 0x0013FEE8 File Offset: 0x0013E0E8
		// (set) Token: 0x06005B8F RID: 23439 RVA: 0x0013FEFA File Offset: 0x0013E0FA
		[Parameter(Mandatory = false)]
		public bool ExcludedFromBackSync
		{
			get
			{
				return (bool)this[SyncDistributionGroupSchema.ExcludedFromBackSync];
			}
			set
			{
				this[SyncDistributionGroupSchema.ExcludedFromBackSync] = value;
			}
		}

		// Token: 0x04003E45 RID: 15941
		private static SyncDistributionGroupSchema schema = ObjectSchema.GetInstance<SyncDistributionGroupSchema>();
	}
}
