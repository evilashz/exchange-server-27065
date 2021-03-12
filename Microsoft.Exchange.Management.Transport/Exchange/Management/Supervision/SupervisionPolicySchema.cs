using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Supervision
{
	// Token: 0x02000090 RID: 144
	internal class SupervisionPolicySchema : ObjectSchema
	{
		// Token: 0x040001B4 RID: 436
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(SupervisionPolicyId), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001B5 RID: 437
		public static readonly SimpleProviderPropertyDefinition ObjectState = new SimpleProviderPropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2010, typeof(ObjectState), PropertyDefinitionFlags.ReadOnly, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001B6 RID: 438
		public static readonly SimpleProviderPropertyDefinition ExchangeVersion = new SimpleProviderPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2010, typeof(ExchangeObjectVersion), PropertyDefinitionFlags.ReadOnly, ExchangeObjectVersion.Exchange2010, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001B7 RID: 439
		public static readonly SimpleProviderPropertyDefinition ClosedCampusInboundPolicyEnabled = new SimpleProviderPropertyDefinition("ClosedCampusInboundPolicyEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001B8 RID: 440
		public static readonly SimpleProviderPropertyDefinition ClosedCampusInboundDomainExceptions = new SimpleProviderPropertyDefinition("ClosedCampusInboundDomainExceptions", ExchangeObjectVersion.Exchange2010, typeof(SmtpDomain), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001B9 RID: 441
		public static readonly SimpleProviderPropertyDefinition ClosedCampusInboundGroupExceptions = new SimpleProviderPropertyDefinition("ClosedCampusInboundGroupExceptions", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001BA RID: 442
		public static readonly SimpleProviderPropertyDefinition ClosedCampusOutboundPolicyEnabled = new SimpleProviderPropertyDefinition("ClosedCampusOutboundPolicyEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001BB RID: 443
		public static readonly SimpleProviderPropertyDefinition ClosedCampusOutboundDomainExceptions = new SimpleProviderPropertyDefinition("ClosedCampusOutboundDomainExceptions", ExchangeObjectVersion.Exchange2010, typeof(SmtpDomain), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001BC RID: 444
		public static readonly SimpleProviderPropertyDefinition ClosedCampusOutboundGroupExceptions = new SimpleProviderPropertyDefinition("ClosedCampusOutboundGroupExceptions", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001BD RID: 445
		public static readonly SimpleProviderPropertyDefinition BadWordsPolicyEnabled = new SimpleProviderPropertyDefinition("BadWordsPolicyEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001BE RID: 446
		public static readonly SimpleProviderPropertyDefinition BadWordsList = new SimpleProviderPropertyDefinition("BadWordsList", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001BF RID: 447
		public static readonly SimpleProviderPropertyDefinition AntiBullyingPolicyEnabled = new SimpleProviderPropertyDefinition("AntiBullyingPolicyEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
