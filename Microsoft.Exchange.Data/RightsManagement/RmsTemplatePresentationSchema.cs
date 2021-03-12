using System;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.RightsManagement
{
	// Token: 0x0200029D RID: 669
	internal sealed class RmsTemplatePresentationSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000E44 RID: 3652
		public static readonly SimpleProviderPropertyDefinition Name = new SimpleProviderPropertyDefinition("Name", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000E45 RID: 3653
		public static readonly SimpleProviderPropertyDefinition Description = new SimpleProviderPropertyDefinition("Description", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000E46 RID: 3654
		public static readonly SimpleProviderPropertyDefinition Type = new SimpleProviderPropertyDefinition("Type", ExchangeObjectVersion.Exchange2007, typeof(RmsTemplateType), PropertyDefinitionFlags.None, RmsTemplateType.Distributed, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000E47 RID: 3655
		public static readonly SimpleProviderPropertyDefinition TemplateGuid = new SimpleProviderPropertyDefinition("TemplateGuid", ExchangeObjectVersion.Exchange2010, typeof(Guid), PropertyDefinitionFlags.None, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
