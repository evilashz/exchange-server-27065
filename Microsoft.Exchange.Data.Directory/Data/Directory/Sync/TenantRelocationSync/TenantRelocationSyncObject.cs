using System;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x02000807 RID: 2055
	[Serializable]
	public class TenantRelocationSyncObject : ADRawEntry
	{
		// Token: 0x0600654D RID: 25933 RVA: 0x00162C0A File Offset: 0x00160E0A
		public TenantRelocationSyncObject()
		{
		}

		// Token: 0x0600654E RID: 25934 RVA: 0x00162C12 File Offset: 0x00160E12
		internal TenantRelocationSyncObject(ADPropertyBag propertyBag, DirectoryAttribute[] directoryAttributes) : base(propertyBag)
		{
			this.RawLdapSearchResult = directoryAttributes;
		}

		// Token: 0x170023D7 RID: 9175
		// (get) Token: 0x0600654F RID: 25935 RVA: 0x00162C22 File Offset: 0x00160E22
		// (set) Token: 0x06006550 RID: 25936 RVA: 0x00162C2A File Offset: 0x00160E2A
		internal DirectoryAttribute[] RawLdapSearchResult { get; private set; }

		// Token: 0x170023D8 RID: 9176
		// (get) Token: 0x06006551 RID: 25937 RVA: 0x00162C33 File Offset: 0x00160E33
		internal bool IsDeleted
		{
			get
			{
				return (bool)this[SyncObjectSchema.Deleted];
			}
		}

		// Token: 0x170023D9 RID: 9177
		// (get) Token: 0x06006552 RID: 25938 RVA: 0x00162C45 File Offset: 0x00160E45
		internal Guid CorrelationId
		{
			get
			{
				return (Guid)this[ADObjectSchema.CorrelationId];
			}
		}

		// Token: 0x170023DA RID: 9178
		// (get) Token: 0x06006553 RID: 25939 RVA: 0x00162C57 File Offset: 0x00160E57
		internal Guid Guid
		{
			get
			{
				return (Guid)this[ADObjectSchema.Guid];
			}
		}

		// Token: 0x170023DB RID: 9179
		// (get) Token: 0x06006554 RID: 25940 RVA: 0x00162C69 File Offset: 0x00160E69
		internal Guid ExchangeObjectId
		{
			get
			{
				return (Guid)this[ADObjectSchema.ExchangeObjectId];
			}
		}

		// Token: 0x170023DC RID: 9180
		// (get) Token: 0x06006555 RID: 25941 RVA: 0x00162C7B File Offset: 0x00160E7B
		internal string ExternalDirectoryObjectId
		{
			get
			{
				return (string)this[ADRecipientSchema.ExternalDirectoryObjectId];
			}
		}

		// Token: 0x170023DD RID: 9181
		// (get) Token: 0x06006556 RID: 25942 RVA: 0x00162C8D File Offset: 0x00160E8D
		internal string ConfigurationXMLRaw
		{
			get
			{
				return (string)this[ADRecipientSchema.ConfigurationXMLRaw];
			}
		}

		// Token: 0x170023DE RID: 9182
		// (get) Token: 0x06006557 RID: 25943 RVA: 0x00162C9F File Offset: 0x00160E9F
		internal MultiValuedProperty<LinkMetadata> LinkValueMetadata
		{
			get
			{
				return (MultiValuedProperty<LinkMetadata>)this[ADRecipientSchema.LinkMetadata];
			}
		}
	}
}
