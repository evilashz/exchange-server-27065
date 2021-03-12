using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000C0 RID: 192
	internal class ADMiniDomainSchema
	{
		// Token: 0x040003DC RID: 988
		public static readonly ADPropertyDefinition TenantIdProp = ADObjectSchema.OrganizationalUnitRoot;

		// Token: 0x040003DD RID: 989
		public static readonly ADPropertyDefinition DomainIdProp = ADObjectSchema.Id;

		// Token: 0x040003DE RID: 990
		public static readonly HygienePropertyDefinition ConfigurationIdProp = new HygienePropertyDefinition("configurationId", typeof(ADObjectId));

		// Token: 0x040003DF RID: 991
		public static readonly HygienePropertyDefinition ParentDomainIdProp = new HygienePropertyDefinition("parentDomainId", typeof(ADObjectId));

		// Token: 0x040003E0 RID: 992
		public static readonly HygienePropertyDefinition DomainNameProp = new HygienePropertyDefinition("DomainName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003E1 RID: 993
		public static readonly HygienePropertyDefinition UsingMicrosoftMxProp = new HygienePropertyDefinition("usingMicrosoftMxRecords", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003E2 RID: 994
		public static readonly HygienePropertyDefinition IsCatchAllProp = new HygienePropertyDefinition("isCatchAll", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003E3 RID: 995
		public static readonly HygienePropertyDefinition IsInitialDomainProp = new HygienePropertyDefinition("isInitial", typeof(bool), false, ExchangeObjectVersion.Exchange2007, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003E4 RID: 996
		public static readonly HygienePropertyDefinition IsDefaultDomainProp = new HygienePropertyDefinition("isDefaultOutbound", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003E5 RID: 997
		public static readonly HygienePropertyDefinition EdgeBlockModeProp = new HygienePropertyDefinition("edgeBlockMode", typeof(EdgeBlockMode), EdgeBlockMode.None, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003E6 RID: 998
		public static readonly HygienePropertyDefinition HygieneConfigurationLink = new HygienePropertyDefinition("hygieneConfigurationLink", typeof(ADObjectId));

		// Token: 0x040003E7 RID: 999
		public static readonly HygienePropertyDefinition MailServerProp = new HygienePropertyDefinition("MailServer", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003E8 RID: 1000
		public static readonly HygienePropertyDefinition LiveTypeProp = new HygienePropertyDefinition("liveType", typeof(string));

		// Token: 0x040003E9 RID: 1001
		public static readonly HygienePropertyDefinition LiveNetIdProp = new HygienePropertyDefinition("liveNetId", typeof(string));

		// Token: 0x040003EA RID: 1002
		public static readonly HygienePropertyDefinition DomainFlagsProperty = new HygienePropertyDefinition("Flags", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003EB RID: 1003
		public static readonly HygienePropertyDefinition ObjectStateProp = DalHelper.ObjectStateProp;
	}
}
