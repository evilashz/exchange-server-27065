using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000710 RID: 1808
	[Serializable]
	public class FederatedOrganizationIdWithDomainStatus : ADPresentationObject
	{
		// Token: 0x06005502 RID: 21762 RVA: 0x001334D9 File Offset: 0x001316D9
		public FederatedOrganizationIdWithDomainStatus()
		{
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x001334E1 File Offset: 0x001316E1
		public FederatedOrganizationIdWithDomainStatus(FederatedOrganizationId dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001C59 RID: 7257
		// (get) Token: 0x06005504 RID: 21764 RVA: 0x001334EA File Offset: 0x001316EA
		// (set) Token: 0x06005505 RID: 21765 RVA: 0x001334FC File Offset: 0x001316FC
		public SmtpDomain AccountNamespace
		{
			get
			{
				return (SmtpDomain)this[FederatedOrganizationIdWithDomainStatusSchema.AccountNamespace];
			}
			internal set
			{
				this[FederatedOrganizationIdWithDomainStatusSchema.AccountNamespace] = value;
			}
		}

		// Token: 0x17001C5A RID: 7258
		// (get) Token: 0x06005506 RID: 21766 RVA: 0x0013350A File Offset: 0x0013170A
		// (set) Token: 0x06005507 RID: 21767 RVA: 0x00133512 File Offset: 0x00131712
		public MultiValuedProperty<FederatedDomain> Domains
		{
			get
			{
				return this.domains;
			}
			internal set
			{
				this.domains = value;
			}
		}

		// Token: 0x17001C5B RID: 7259
		// (get) Token: 0x06005508 RID: 21768 RVA: 0x0013351B File Offset: 0x0013171B
		// (set) Token: 0x06005509 RID: 21769 RVA: 0x00133523 File Offset: 0x00131723
		public SmtpDomain DefaultDomain
		{
			get
			{
				return this.defaultDomain;
			}
			internal set
			{
				this.defaultDomain = value;
			}
		}

		// Token: 0x17001C5C RID: 7260
		// (get) Token: 0x0600550A RID: 21770 RVA: 0x0013352C File Offset: 0x0013172C
		// (set) Token: 0x0600550B RID: 21771 RVA: 0x0013353E File Offset: 0x0013173E
		public bool Enabled
		{
			get
			{
				return (bool)this[FederatedOrganizationIdWithDomainStatusSchema.Enabled];
			}
			internal set
			{
				this[FederatedOrganizationIdWithDomainStatusSchema.Enabled] = value;
			}
		}

		// Token: 0x17001C5D RID: 7261
		// (get) Token: 0x0600550C RID: 21772 RVA: 0x00133551 File Offset: 0x00131751
		// (set) Token: 0x0600550D RID: 21773 RVA: 0x00133563 File Offset: 0x00131763
		public SmtpAddress OrganizationContact
		{
			get
			{
				return (SmtpAddress)this[FederatedOrganizationIdWithDomainStatusSchema.OrganizationContact];
			}
			internal set
			{
				this[FederatedOrganizationIdWithDomainStatusSchema.OrganizationContact] = value;
			}
		}

		// Token: 0x17001C5E RID: 7262
		// (get) Token: 0x0600550E RID: 21774 RVA: 0x00133576 File Offset: 0x00131776
		// (set) Token: 0x0600550F RID: 21775 RVA: 0x00133588 File Offset: 0x00131788
		public ADObjectId DelegationTrustLink
		{
			get
			{
				return this[FederatedOrganizationIdWithDomainStatusSchema.DelegationTrustLink] as ADObjectId;
			}
			internal set
			{
				this[FederatedOrganizationIdWithDomainStatusSchema.DelegationTrustLink] = value;
			}
		}

		// Token: 0x17001C5F RID: 7263
		// (get) Token: 0x06005510 RID: 21776 RVA: 0x00133596 File Offset: 0x00131796
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return FederatedOrganizationIdWithDomainStatus.schema;
			}
		}

		// Token: 0x0400390F RID: 14607
		private static FederatedOrganizationIdWithDomainStatusSchema schema = ObjectSchema.GetInstance<FederatedOrganizationIdWithDomainStatusSchema>();

		// Token: 0x04003910 RID: 14608
		private MultiValuedProperty<FederatedDomain> domains;

		// Token: 0x04003911 RID: 14609
		private SmtpDomain defaultDomain;
	}
}
