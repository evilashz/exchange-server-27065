using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200038E RID: 910
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class ADSite : ADConfigurationObject
	{
		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06002997 RID: 10647 RVA: 0x000AEBA1 File Offset: 0x000ACDA1
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADSite.schema;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06002998 RID: 10648 RVA: 0x000AEBA8 File Offset: 0x000ACDA8
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADSite.mostDerivedClass;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06002999 RID: 10649 RVA: 0x000AEBAF File Offset: 0x000ACDAF
		internal override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600299B RID: 10651 RVA: 0x000AEBBA File Offset: 0x000ACDBA
		// (set) Token: 0x0600299C RID: 10652 RVA: 0x000AEBCC File Offset: 0x000ACDCC
		[Parameter(Mandatory = false)]
		public bool HubSiteEnabled
		{
			get
			{
				return (bool)this[ADSiteSchema.HubSiteEnabled];
			}
			set
			{
				this[ADSiteSchema.HubSiteEnabled] = value;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x0600299D RID: 10653 RVA: 0x000AEBDF File Offset: 0x000ACDDF
		// (set) Token: 0x0600299E RID: 10654 RVA: 0x000AEBF4 File Offset: 0x000ACDF4
		[Parameter(Mandatory = false)]
		public bool InboundMailEnabled
		{
			get
			{
				return !(bool)this[ADSiteSchema.InboundMailDisabled];
			}
			set
			{
				this[ADSiteSchema.InboundMailDisabled] = !value;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x0600299F RID: 10655 RVA: 0x000AEC0A File Offset: 0x000ACE0A
		// (set) Token: 0x060029A0 RID: 10656 RVA: 0x000AEC1C File Offset: 0x000ACE1C
		[Parameter(Mandatory = false)]
		public int PartnerId
		{
			get
			{
				return (int)this[ADSiteSchema.PartnerId];
			}
			set
			{
				this[ADSiteSchema.PartnerId] = value;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060029A1 RID: 10657 RVA: 0x000AEC2F File Offset: 0x000ACE2F
		// (set) Token: 0x060029A2 RID: 10658 RVA: 0x000AEC41 File Offset: 0x000ACE41
		[Parameter(Mandatory = false)]
		public int MinorPartnerId
		{
			get
			{
				return (int)this[ADSiteSchema.MinorPartnerId];
			}
			set
			{
				this[ADSiteSchema.MinorPartnerId] = value;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x000AEC54 File Offset: 0x000ACE54
		// (set) Token: 0x060029A4 RID: 10660 RVA: 0x000AEC66 File Offset: 0x000ACE66
		public MultiValuedProperty<ADObjectId> ResponsibleForSites
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADSiteSchema.ResponsibleForSites];
			}
			set
			{
				this[ADSiteSchema.ResponsibleForSites] = value;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x000AEC74 File Offset: 0x000ACE74
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x04001972 RID: 6514
		public const int InvalidPartnerId = -1;

		// Token: 0x04001973 RID: 6515
		private static ADSiteSchema schema = ObjectSchema.GetInstance<ADSiteSchema>();

		// Token: 0x04001974 RID: 6516
		private static string mostDerivedClass = "site";
	}
}
