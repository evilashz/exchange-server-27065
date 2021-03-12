using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200075F RID: 1887
	[ProvisioningObjectTag("DynamicDistributionGroup")]
	[Serializable]
	public class SyncDynamicDistributionGroup : DynamicDistributionGroup
	{
		// Token: 0x17001FD7 RID: 8151
		// (get) Token: 0x06005B93 RID: 23443 RVA: 0x0013FF77 File Offset: 0x0013E177
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return SyncDynamicDistributionGroup.schema;
			}
		}

		// Token: 0x06005B94 RID: 23444 RVA: 0x0013FF7E File Offset: 0x0013E17E
		public SyncDynamicDistributionGroup()
		{
			base.SetObjectClass("group");
		}

		// Token: 0x06005B95 RID: 23445 RVA: 0x0013FF91 File Offset: 0x0013E191
		public SyncDynamicDistributionGroup(ADDynamicGroup dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005B96 RID: 23446 RVA: 0x0013FF9A File Offset: 0x0013E19A
		internal new static SyncDynamicDistributionGroup FromDataObject(ADDynamicGroup dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new SyncDynamicDistributionGroup(dataObject);
		}

		// Token: 0x17001FD8 RID: 8152
		// (get) Token: 0x06005B97 RID: 23447 RVA: 0x0013FFA7 File Offset: 0x0013E1A7
		// (set) Token: 0x06005B98 RID: 23448 RVA: 0x0013FFB9 File Offset: 0x0013E1B9
		[Parameter(Mandatory = false)]
		public byte[] BlockedSendersHash
		{
			get
			{
				return (byte[])this[SyncDynamicDistributionGroupSchema.BlockedSendersHash];
			}
			set
			{
				this[SyncDynamicDistributionGroupSchema.BlockedSendersHash] = value;
			}
		}

		// Token: 0x17001FD9 RID: 8153
		// (get) Token: 0x06005B99 RID: 23449 RVA: 0x0013FFC7 File Offset: 0x0013E1C7
		// (set) Token: 0x06005B9A RID: 23450 RVA: 0x0013FFD9 File Offset: 0x0013E1D9
		[Parameter(Mandatory = false)]
		public RecipientDisplayType? RecipientDisplayType
		{
			get
			{
				return (RecipientDisplayType?)this[SyncDynamicDistributionGroupSchema.RecipientDisplayType];
			}
			set
			{
				this[SyncDynamicDistributionGroupSchema.RecipientDisplayType] = value;
			}
		}

		// Token: 0x17001FDA RID: 8154
		// (get) Token: 0x06005B9B RID: 23451 RVA: 0x0013FFEC File Offset: 0x0013E1EC
		// (set) Token: 0x06005B9C RID: 23452 RVA: 0x0013FFFE File Offset: 0x0013E1FE
		[Parameter(Mandatory = false)]
		public byte[] SafeRecipientsHash
		{
			get
			{
				return (byte[])this[SyncDynamicDistributionGroupSchema.SafeRecipientsHash];
			}
			set
			{
				this[SyncDynamicDistributionGroupSchema.SafeRecipientsHash] = value;
			}
		}

		// Token: 0x17001FDB RID: 8155
		// (get) Token: 0x06005B9D RID: 23453 RVA: 0x0014000C File Offset: 0x0013E20C
		// (set) Token: 0x06005B9E RID: 23454 RVA: 0x0014001E File Offset: 0x0013E21E
		[Parameter(Mandatory = false)]
		public byte[] SafeSendersHash
		{
			get
			{
				return (byte[])this[SyncDynamicDistributionGroupSchema.SafeSendersHash];
			}
			set
			{
				this[SyncDynamicDistributionGroupSchema.SafeSendersHash] = value;
			}
		}

		// Token: 0x17001FDC RID: 8156
		// (get) Token: 0x06005B9F RID: 23455 RVA: 0x0014002C File Offset: 0x0013E22C
		// (set) Token: 0x06005BA0 RID: 23456 RVA: 0x0014003E File Offset: 0x0013E23E
		public bool EndOfList
		{
			get
			{
				return (bool)this[SyncDynamicDistributionGroupSchema.EndOfList];
			}
			internal set
			{
				this[SyncDynamicDistributionGroupSchema.EndOfList] = value;
			}
		}

		// Token: 0x17001FDD RID: 8157
		// (get) Token: 0x06005BA1 RID: 23457 RVA: 0x00140051 File Offset: 0x0013E251
		// (set) Token: 0x06005BA2 RID: 23458 RVA: 0x00140063 File Offset: 0x0013E263
		public byte[] Cookie
		{
			get
			{
				return (byte[])this[SyncDynamicDistributionGroupSchema.Cookie];
			}
			internal set
			{
				this[SyncDynamicDistributionGroupSchema.Cookie] = value;
			}
		}

		// Token: 0x17001FDE RID: 8158
		// (get) Token: 0x06005BA3 RID: 23459 RVA: 0x00140071 File Offset: 0x0013E271
		// (set) Token: 0x06005BA4 RID: 23460 RVA: 0x00140083 File Offset: 0x0013E283
		[Parameter(Mandatory = false)]
		public string DirSyncId
		{
			get
			{
				return (string)this[SyncDynamicDistributionGroupSchema.DirSyncId];
			}
			set
			{
				this[SyncDynamicDistributionGroupSchema.DirSyncId] = value;
			}
		}

		// Token: 0x04003E4D RID: 15949
		private static SyncDynamicDistributionGroupSchema schema = ObjectSchema.GetInstance<SyncDynamicDistributionGroupSchema>();
	}
}
