using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001DE RID: 478
	[Serializable]
	public class ActiveSyncMiniRecipient : StorageMiniRecipient
	{
		// Token: 0x06001410 RID: 5136 RVA: 0x000600B8 File Offset: 0x0005E2B8
		public ActiveSyncMiniRecipient()
		{
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x000600C0 File Offset: 0x0005E2C0
		internal ActiveSyncMiniRecipient(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x000600CA File Offset: 0x0005E2CA
		public bool ActiveSyncEnabled
		{
			get
			{
				return (bool)(this[ActiveSyncMiniRecipientSchema.ActiveSyncEnabled] ?? false);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x000600E6 File Offset: 0x0005E2E6
		public ADObjectId ActiveSyncMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[ActiveSyncMiniRecipientSchema.ActiveSyncMailboxPolicy];
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x000600F8 File Offset: 0x0005E2F8
		public MultiValuedProperty<string> ActiveSyncAllowedDeviceIDs
		{
			get
			{
				return (MultiValuedProperty<string>)this[ActiveSyncMiniRecipientSchema.ActiveSyncAllowedDeviceIDs];
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0006010A File Offset: 0x0005E30A
		public MultiValuedProperty<string> ActiveSyncBlockedDeviceIDs
		{
			get
			{
				return (MultiValuedProperty<string>)this[ActiveSyncMiniRecipientSchema.ActiveSyncBlockedDeviceIDs];
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0006011C File Offset: 0x0005E31C
		internal override ADObjectSchema Schema
		{
			get
			{
				return ActiveSyncMiniRecipient.schema;
			}
		}

		// Token: 0x04000AE9 RID: 2793
		private static readonly ActiveSyncMiniRecipientSchema schema = ObjectSchema.GetInstance<ActiveSyncMiniRecipientSchema>();
	}
}
