using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000D0 RID: 208
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class DomainSettings : ADObject
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00015939 File Offset: 0x00013B39
		public override ObjectId Identity
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00015941 File Offset: 0x00013B41
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x00015953 File Offset: 0x00013B53
		public ADObjectId HygieneConfigurationLinkProp
		{
			get
			{
				return (ADObjectId)this[DomainSettingsSchema.HygieneConfigurationLinkProp];
			}
			set
			{
				this[DomainSettingsSchema.HygieneConfigurationLinkProp] = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00015961 File Offset: 0x00013B61
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x00015973 File Offset: 0x00013B73
		public EdgeBlockMode EdgeBlockMode
		{
			get
			{
				return (EdgeBlockMode)this[DomainSettingsSchema.EdgeBlockModeProp];
			}
			set
			{
				this[DomainSettingsSchema.EdgeBlockModeProp] = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00015986 File Offset: 0x00013B86
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x00015998 File Offset: 0x00013B98
		public string MailServer
		{
			get
			{
				return (string)this[DomainSettingsSchema.MailServerProp];
			}
			set
			{
				this[DomainSettingsSchema.MailServerProp] = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x000159A6 File Offset: 0x00013BA6
		internal override ADObjectSchema Schema
		{
			get
			{
				return DomainSettings.schema;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x000159AD File Offset: 0x00013BAD
		internal override string MostDerivedObjectClass
		{
			get
			{
				return DomainSettings.mostDerivedClass;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x000159B4 File Offset: 0x00013BB4
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000159BB File Offset: 0x00013BBB
		internal override bool ShouldValidatePropertyLinkInSameOrganization(ADPropertyDefinition property)
		{
			return false;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000159BE File Offset: 0x00013BBE
		protected override void ValidateWrite(List<ValidationError> errors)
		{
		}

		// Token: 0x04000434 RID: 1076
		private static readonly DomainSettingsSchema schema = ObjectSchema.GetInstance<DomainSettingsSchema>();

		// Token: 0x04000435 RID: 1077
		private static string mostDerivedClass = "DomainSettings";
	}
}
