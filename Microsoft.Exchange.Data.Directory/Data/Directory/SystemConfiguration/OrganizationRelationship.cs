using System;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200052A RID: 1322
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class OrganizationRelationship : ADConfigurationObject
	{
		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x000E14AD File Offset: 0x000DF6AD
		// (set) Token: 0x06003AF6 RID: 15094 RVA: 0x000E14BF File Offset: 0x000DF6BF
		public MultiValuedProperty<SmtpDomain> DomainNames
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)this[OrganizationRelationshipSchema.DomainNames];
			}
			set
			{
				this[OrganizationRelationshipSchema.DomainNames] = value;
			}
		}

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x000E14CD File Offset: 0x000DF6CD
		// (set) Token: 0x06003AF8 RID: 15096 RVA: 0x000E14DF File Offset: 0x000DF6DF
		public bool FreeBusyAccessEnabled
		{
			get
			{
				return (bool)this[OrganizationRelationshipSchema.FreeBusyAccessEnabled];
			}
			set
			{
				this[OrganizationRelationshipSchema.FreeBusyAccessEnabled] = value;
			}
		}

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x000E14F2 File Offset: 0x000DF6F2
		// (set) Token: 0x06003AFA RID: 15098 RVA: 0x000E1504 File Offset: 0x000DF704
		public FreeBusyAccessLevel FreeBusyAccessLevel
		{
			get
			{
				return (FreeBusyAccessLevel)this[OrganizationRelationshipSchema.FreeBusyAccessLevel];
			}
			set
			{
				this[OrganizationRelationshipSchema.FreeBusyAccessLevel] = value;
			}
		}

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x000E1517 File Offset: 0x000DF717
		// (set) Token: 0x06003AFC RID: 15100 RVA: 0x000E1529 File Offset: 0x000DF729
		public ADObjectId FreeBusyAccessScope
		{
			get
			{
				return (ADObjectId)this[OrganizationRelationshipSchema.FreeBusyAccessScope];
			}
			set
			{
				this[OrganizationRelationshipSchema.FreeBusyAccessScope] = value;
			}
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06003AFD RID: 15101 RVA: 0x000E1537 File Offset: 0x000DF737
		// (set) Token: 0x06003AFE RID: 15102 RVA: 0x000E1549 File Offset: 0x000DF749
		public bool MailboxMoveEnabled
		{
			get
			{
				return (bool)this[OrganizationRelationshipSchema.MailboxMoveEnabled];
			}
			set
			{
				this[OrganizationRelationshipSchema.MailboxMoveEnabled] = value;
			}
		}

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06003AFF RID: 15103 RVA: 0x000E155C File Offset: 0x000DF75C
		// (set) Token: 0x06003B00 RID: 15104 RVA: 0x000E156E File Offset: 0x000DF76E
		public bool DeliveryReportEnabled
		{
			get
			{
				return (bool)this[OrganizationRelationshipSchema.DeliveryReportEnabled];
			}
			set
			{
				this[OrganizationRelationshipSchema.DeliveryReportEnabled] = value;
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06003B01 RID: 15105 RVA: 0x000E1581 File Offset: 0x000DF781
		// (set) Token: 0x06003B02 RID: 15106 RVA: 0x000E1593 File Offset: 0x000DF793
		public bool MailTipsAccessEnabled
		{
			get
			{
				return (bool)this[OrganizationRelationshipSchema.MailTipsAccessEnabled];
			}
			set
			{
				this[OrganizationRelationshipSchema.MailTipsAccessEnabled] = value;
			}
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x000E15A6 File Offset: 0x000DF7A6
		// (set) Token: 0x06003B04 RID: 15108 RVA: 0x000E15B8 File Offset: 0x000DF7B8
		public MailTipsAccessLevel MailTipsAccessLevel
		{
			get
			{
				return (MailTipsAccessLevel)this[OrganizationRelationshipSchema.MailTipsAccessLevel];
			}
			set
			{
				this[OrganizationRelationshipSchema.MailTipsAccessLevel] = value;
			}
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x000E15CB File Offset: 0x000DF7CB
		// (set) Token: 0x06003B06 RID: 15110 RVA: 0x000E15DD File Offset: 0x000DF7DD
		public ADObjectId MailTipsAccessScope
		{
			get
			{
				return (ADObjectId)this[OrganizationRelationshipSchema.MailTipsAccessScope];
			}
			set
			{
				this[OrganizationRelationshipSchema.MailTipsAccessScope] = value;
			}
		}

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06003B07 RID: 15111 RVA: 0x000E15EB File Offset: 0x000DF7EB
		// (set) Token: 0x06003B08 RID: 15112 RVA: 0x000E15FD File Offset: 0x000DF7FD
		public bool PhotosEnabled
		{
			get
			{
				return (bool)this[OrganizationRelationshipSchema.PhotosEnabled];
			}
			set
			{
				this[OrganizationRelationshipSchema.PhotosEnabled] = value;
			}
		}

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06003B09 RID: 15113 RVA: 0x000E1610 File Offset: 0x000DF810
		// (set) Token: 0x06003B0A RID: 15114 RVA: 0x000E1622 File Offset: 0x000DF822
		public Uri TargetApplicationUri
		{
			get
			{
				return (Uri)this[OrganizationRelationshipSchema.TargetApplicationUri];
			}
			set
			{
				this[OrganizationRelationshipSchema.TargetApplicationUri] = value;
			}
		}

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x000E1630 File Offset: 0x000DF830
		// (set) Token: 0x06003B0C RID: 15116 RVA: 0x000E1642 File Offset: 0x000DF842
		public Uri TargetSharingEpr
		{
			get
			{
				return (Uri)this[OrganizationRelationshipSchema.TargetSharingEpr];
			}
			set
			{
				this[OrganizationRelationshipSchema.TargetSharingEpr] = value;
			}
		}

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06003B0D RID: 15117 RVA: 0x000E1650 File Offset: 0x000DF850
		// (set) Token: 0x06003B0E RID: 15118 RVA: 0x000E1662 File Offset: 0x000DF862
		public Uri TargetOwaURL
		{
			get
			{
				return (Uri)this[OrganizationRelationshipSchema.TargetOwaURL];
			}
			set
			{
				this[OrganizationRelationshipSchema.TargetOwaURL] = value;
			}
		}

		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06003B0F RID: 15119 RVA: 0x000E1670 File Offset: 0x000DF870
		// (set) Token: 0x06003B10 RID: 15120 RVA: 0x000E1682 File Offset: 0x000DF882
		public Uri TargetAutodiscoverEpr
		{
			get
			{
				return (Uri)this[OrganizationRelationshipSchema.TargetAutodiscoverEpr];
			}
			set
			{
				this[OrganizationRelationshipSchema.TargetAutodiscoverEpr] = value;
			}
		}

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06003B11 RID: 15121 RVA: 0x000E1690 File Offset: 0x000DF890
		// (set) Token: 0x06003B12 RID: 15122 RVA: 0x000E16A2 File Offset: 0x000DF8A2
		public SmtpAddress OrganizationContact
		{
			get
			{
				return (SmtpAddress)this[OrganizationRelationshipSchema.OrganizationContact];
			}
			set
			{
				this[OrganizationRelationshipSchema.OrganizationContact] = value;
			}
		}

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06003B13 RID: 15123 RVA: 0x000E16B5 File Offset: 0x000DF8B5
		// (set) Token: 0x06003B14 RID: 15124 RVA: 0x000E16C7 File Offset: 0x000DF8C7
		public bool Enabled
		{
			get
			{
				return (bool)this[OrganizationRelationshipSchema.Enabled];
			}
			set
			{
				this[OrganizationRelationshipSchema.Enabled] = value;
			}
		}

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06003B15 RID: 15125 RVA: 0x000E16DA File Offset: 0x000DF8DA
		// (set) Token: 0x06003B16 RID: 15126 RVA: 0x000E16EC File Offset: 0x000DF8EC
		public bool ArchiveAccessEnabled
		{
			get
			{
				return (bool)this[OrganizationRelationshipSchema.ArchiveAccessEnabled];
			}
			set
			{
				this[OrganizationRelationshipSchema.ArchiveAccessEnabled] = value;
			}
		}

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06003B17 RID: 15127 RVA: 0x000E16FF File Offset: 0x000DF8FF
		internal override ADObjectSchema Schema
		{
			get
			{
				return OrganizationRelationship.SchemaObject;
			}
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06003B18 RID: 15128 RVA: 0x000E1706 File Offset: 0x000DF906
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchFedSharingRelationship";
			}
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06003B19 RID: 15129 RVA: 0x000E170D File Offset: 0x000DF90D
		internal override ADObjectId ParentPath
		{
			get
			{
				return FederatedOrganizationId.Container;
			}
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06003B1A RID: 15130 RVA: 0x000E1714 File Offset: 0x000DF914
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x000E171B File Offset: 0x000DF91B
		internal bool IsValidForRequestDispatcher()
		{
			return !(this.TargetApplicationUri == null) && (!(this.TargetAutodiscoverEpr == null) || !(this.TargetSharingEpr == null));
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x000E174C File Offset: 0x000DF94C
		internal TokenTarget GetTokenTarget()
		{
			Uri targetApplicationUri = this.TargetApplicationUri;
			if (targetApplicationUri == null)
			{
				throw new OrganizationRelationshipMissingTargetApplicationUriException();
			}
			return new TokenTarget(TokenTarget.Fix(targetApplicationUri));
		}

		// Token: 0x040027E8 RID: 10216
		internal const string TaskNoun = "OrganizationRelationship";

		// Token: 0x040027E9 RID: 10217
		internal const string LdapName = "msExchFedSharingRelationship";

		// Token: 0x040027EA RID: 10218
		private static readonly OrganizationRelationshipSchema SchemaObject = ObjectSchema.GetInstance<OrganizationRelationshipSchema>();
	}
}
