using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000258 RID: 600
	[Serializable]
	public class OWAMiniRecipient : StorageMiniRecipient
	{
		// Token: 0x06001D2F RID: 7471 RVA: 0x000790D3 File Offset: 0x000772D3
		internal OWAMiniRecipient(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x000790DD File Offset: 0x000772DD
		public OWAMiniRecipient()
		{
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x000790E5 File Offset: 0x000772E5
		public string PhoneticDisplayName
		{
			get
			{
				return (string)this[OWAMiniRecipientSchema.PhoneticDisplayName];
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x000790F7 File Offset: 0x000772F7
		public bool ActiveSyncEnabled
		{
			get
			{
				return (bool)(this[OWAMiniRecipientSchema.ActiveSyncEnabled] ?? false);
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x00079113 File Offset: 0x00077313
		public ExternalOofOptions ExternalOofOptions
		{
			get
			{
				return (ExternalOofOptions)this[OWAMiniRecipientSchema.ExternalOofOptions];
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x00079125 File Offset: 0x00077325
		public string MobilePhoneNumber
		{
			get
			{
				return (string)this[OWAMiniRecipientSchema.MobilePhoneNumber];
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x00079137 File Offset: 0x00077337
		internal override ADObjectSchema Schema
		{
			get
			{
				return OWAMiniRecipient.schema;
			}
		}

		// Token: 0x04000DDE RID: 3550
		private static OWAMiniRecipientSchema schema = ObjectSchema.GetInstance<OWAMiniRecipientSchema>();
	}
}
