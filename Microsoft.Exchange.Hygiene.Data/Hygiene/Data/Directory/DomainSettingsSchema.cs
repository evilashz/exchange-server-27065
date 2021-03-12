using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000D1 RID: 209
	internal class DomainSettingsSchema : ADObjectSchema
	{
		// Token: 0x04000436 RID: 1078
		public static readonly PropertyDefinition HygieneConfigurationLinkProp = new HygienePropertyDefinition("HygieneConfigurationLink", typeof(ADObjectId));

		// Token: 0x04000437 RID: 1079
		public static readonly HygienePropertyDefinition EdgeBlockModeProp = new HygienePropertyDefinition("edgeBlockMode", typeof(EdgeBlockMode), EdgeBlockMode.None, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000438 RID: 1080
		public static readonly PropertyDefinition MailServerProp = new HygienePropertyDefinition("MailServer", typeof(string));
	}
}
