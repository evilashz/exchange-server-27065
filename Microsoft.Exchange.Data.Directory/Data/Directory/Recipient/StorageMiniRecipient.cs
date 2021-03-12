using System;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001DD RID: 477
	[Serializable]
	public class StorageMiniRecipient : MiniRecipient, IFederatedIdentityParameters
	{
		// Token: 0x06001405 RID: 5125 RVA: 0x00060029 File Offset: 0x0005E229
		internal StorageMiniRecipient(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00060033 File Offset: 0x0005E233
		public StorageMiniRecipient()
		{
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0006003B File Offset: 0x0005E23B
		public string Alias
		{
			get
			{
				return (string)this[StorageMiniRecipientSchema.Alias];
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x0006004D File Offset: 0x0005E24D
		public SmtpDomain ArchiveDomain
		{
			get
			{
				return (SmtpDomain)this[StorageMiniRecipientSchema.ArchiveDomain];
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0006005F File Offset: 0x0005E25F
		public ArchiveStatusFlags ArchiveStatus
		{
			get
			{
				return (ArchiveStatusFlags)this[StorageMiniRecipientSchema.ArchiveStatus];
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00060071 File Offset: 0x0005E271
		public ADObjectId ObjectId
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x00060079 File Offset: 0x0005E279
		public string ImmutableId
		{
			get
			{
				return (string)this[StorageMiniRecipientSchema.ImmutableId];
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x0006008B File Offset: 0x0005E28B
		public string ImmutableIdPartial
		{
			get
			{
				return ADRecipient.ConvertOnPremisesObjectIdToString(this[StorageMiniRecipientSchema.RawOnPremisesObjectId]);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0006009D File Offset: 0x0005E29D
		internal override ADObjectSchema Schema
		{
			get
			{
				return StorageMiniRecipient.schema;
			}
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x000600A4 File Offset: 0x0005E2A4
		internal FederatedIdentity GetFederatedIdentity()
		{
			return FederatedIdentityHelper.GetFederatedIdentity(this);
		}

		// Token: 0x04000AE8 RID: 2792
		private static readonly StorageMiniRecipientSchema schema = ObjectSchema.GetInstance<StorageMiniRecipientSchema>();
	}
}
