using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.RightsManagementServices.Online
{
	// Token: 0x02000735 RID: 1845
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "TenantInfo", Namespace = "http://microsoft.com/RightsManagementServiceOnline/2011/04")]
	public class TenantInfo : TenantEnrollmentInfo
	{
		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x06004176 RID: 16758 RVA: 0x0010C714 File Offset: 0x0010A914
		// (set) Token: 0x06004177 RID: 16759 RVA: 0x0010C71C File Offset: 0x0010A91C
		[DataMember]
		public TenantStatus Status
		{
			get
			{
				return this.StatusField;
			}
			set
			{
				this.StatusField = value;
			}
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x06004178 RID: 16760 RVA: 0x0010C725 File Offset: 0x0010A925
		// (set) Token: 0x06004179 RID: 16761 RVA: 0x0010C72D File Offset: 0x0010A92D
		[DataMember(Order = 1)]
		public TrustedDocDomain ActivePublishingDomain
		{
			get
			{
				return this.ActivePublishingDomainField;
			}
			set
			{
				this.ActivePublishingDomainField = value;
			}
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x0600417A RID: 16762 RVA: 0x0010C736 File Offset: 0x0010A936
		// (set) Token: 0x0600417B RID: 16763 RVA: 0x0010C73E File Offset: 0x0010A93E
		[DataMember(Order = 2)]
		public TrustedDocDomain[] ArchivedPublishingDomains
		{
			get
			{
				return this.ArchivedPublishingDomainsField;
			}
			set
			{
				this.ArchivedPublishingDomainsField = value;
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x0600417C RID: 16764 RVA: 0x0010C747 File Offset: 0x0010A947
		// (set) Token: 0x0600417D RID: 16765 RVA: 0x0010C74F File Offset: 0x0010A94F
		[DataMember(Order = 3)]
		public CommonFault ErrorInfo
		{
			get
			{
				return this.ErrorInfoField;
			}
			set
			{
				this.ErrorInfoField = value;
			}
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x0600417E RID: 16766 RVA: 0x0010C758 File Offset: 0x0010A958
		// (set) Token: 0x0600417F RID: 16767 RVA: 0x0010C760 File Offset: 0x0010A960
		[DataMember(Order = 4)]
		public Uri LicensingIntranetDistributionPointUrl
		{
			get
			{
				return this.LicensingIntranetDistributionPointUrlField;
			}
			set
			{
				this.LicensingIntranetDistributionPointUrlField = value;
			}
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x06004180 RID: 16768 RVA: 0x0010C769 File Offset: 0x0010A969
		// (set) Token: 0x06004181 RID: 16769 RVA: 0x0010C771 File Offset: 0x0010A971
		[DataMember(Order = 5)]
		public Uri LicensingExtranetDistributionPointUrl
		{
			get
			{
				return this.LicensingExtranetDistributionPointUrlField;
			}
			set
			{
				this.LicensingExtranetDistributionPointUrlField = value;
			}
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x06004182 RID: 16770 RVA: 0x0010C77A File Offset: 0x0010A97A
		// (set) Token: 0x06004183 RID: 16771 RVA: 0x0010C782 File Offset: 0x0010A982
		[DataMember(Order = 6)]
		public Uri CertificationIntranetDistributionPointUrl
		{
			get
			{
				return this.CertificationIntranetDistributionPointUrlField;
			}
			set
			{
				this.CertificationIntranetDistributionPointUrlField = value;
			}
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x06004184 RID: 16772 RVA: 0x0010C78B File Offset: 0x0010A98B
		// (set) Token: 0x06004185 RID: 16773 RVA: 0x0010C793 File Offset: 0x0010A993
		[DataMember(Order = 7)]
		public Uri CertificationExtranetDistributionPointUrl
		{
			get
			{
				return this.CertificationExtranetDistributionPointUrlField;
			}
			set
			{
				this.CertificationExtranetDistributionPointUrlField = value;
			}
		}

		// Token: 0x04002948 RID: 10568
		private TenantStatus StatusField;

		// Token: 0x04002949 RID: 10569
		private TrustedDocDomain ActivePublishingDomainField;

		// Token: 0x0400294A RID: 10570
		private TrustedDocDomain[] ArchivedPublishingDomainsField;

		// Token: 0x0400294B RID: 10571
		private CommonFault ErrorInfoField;

		// Token: 0x0400294C RID: 10572
		private Uri LicensingIntranetDistributionPointUrlField;

		// Token: 0x0400294D RID: 10573
		private Uri LicensingExtranetDistributionPointUrlField;

		// Token: 0x0400294E RID: 10574
		private Uri CertificationIntranetDistributionPointUrlField;

		// Token: 0x0400294F RID: 10575
		private Uri CertificationExtranetDistributionPointUrlField;
	}
}
