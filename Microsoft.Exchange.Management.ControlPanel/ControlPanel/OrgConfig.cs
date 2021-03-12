using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200052C RID: 1324
	[DataContract]
	public class OrgConfig : BaseRow
	{
		// Token: 0x06003EF1 RID: 16113 RVA: 0x000BD830 File Offset: 0x000BBA30
		public OrgConfig(OrganizationConfig organizationConfig) : base(organizationConfig)
		{
			if (organizationConfig == null)
			{
				throw new ArgumentNullException("organizationConfig");
			}
			this.OriginalOrganizationConfig = organizationConfig;
		}

		// Token: 0x17002499 RID: 9369
		// (get) Token: 0x06003EF2 RID: 16114 RVA: 0x000BD84E File Offset: 0x000BBA4E
		// (set) Token: 0x06003EF3 RID: 16115 RVA: 0x000BD856 File Offset: 0x000BBA56
		public OrganizationConfig OriginalOrganizationConfig { get; private set; }

		// Token: 0x1700249A RID: 9370
		// (get) Token: 0x06003EF4 RID: 16116 RVA: 0x000BD85F File Offset: 0x000BBA5F
		// (set) Token: 0x06003EF5 RID: 16117 RVA: 0x000BD871 File Offset: 0x000BBA71
		[DataMember]
		public string[] GroupNamingPolicyPrefixElements
		{
			get
			{
				return this.OriginalOrganizationConfig.DistributionGroupNamingPolicy.PrefixElements;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700249B RID: 9371
		// (get) Token: 0x06003EF6 RID: 16118 RVA: 0x000BD878 File Offset: 0x000BBA78
		// (set) Token: 0x06003EF7 RID: 16119 RVA: 0x000BD88A File Offset: 0x000BBA8A
		[DataMember]
		public string[] GroupNamingPolicySuffixElements
		{
			get
			{
				return this.OriginalOrganizationConfig.DistributionGroupNamingPolicy.SuffixElements;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700249C RID: 9372
		// (get) Token: 0x06003EF8 RID: 16120 RVA: 0x000BD891 File Offset: 0x000BBA91
		// (set) Token: 0x06003EF9 RID: 16121 RVA: 0x000BD8A3 File Offset: 0x000BBAA3
		[DataMember]
		public IEnumerable<string> DistributionGroupNameBlockedWordsList
		{
			get
			{
				return this.OriginalOrganizationConfig.DistributionGroupNameBlockedWordsList.ToStringArray<string>();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
