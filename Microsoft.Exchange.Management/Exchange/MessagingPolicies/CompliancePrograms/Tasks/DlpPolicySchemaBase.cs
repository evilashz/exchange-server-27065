using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x0200095A RID: 2394
	internal class DlpPolicySchemaBase : ObjectSchema
	{
		// Token: 0x040031AE RID: 12718
		public static readonly ADPropertyDefinition Name = ADObjectSchema.Name;

		// Token: 0x040031AF RID: 12719
		public static readonly ADPropertyDefinition Version = new ADPropertyDefinition("Version", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchMailflowPolicyVersion", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040031B0 RID: 12720
		public static readonly ADPropertyDefinition State = new ADPropertyDefinition("State", ExchangeObjectVersion.Exchange2012, typeof(RuleState), "state", ADPropertyDefinitionFlags.None, RuleState.Enabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040031B1 RID: 12721
		public static readonly ADPropertyDefinition Mode = new ADPropertyDefinition("Mode", ExchangeObjectVersion.Exchange2012, typeof(RuleMode), "mode", ADPropertyDefinitionFlags.None, RuleMode.Enforce, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040031B2 RID: 12722
		public static readonly ADPropertyDefinition ContentVersion = new ADPropertyDefinition("ContentVersion", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchMailflowPolicyVersion", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040031B3 RID: 12723
		public static readonly ADPropertyDefinition Description = new ADPropertyDefinition("Description", ExchangeObjectVersion.Exchange2012, typeof(string), "Description", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040031B4 RID: 12724
		public static readonly ADPropertyDefinition PublisherName = new ADPropertyDefinition("PublisherName", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchMailflowPolicyPublisherName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040031B5 RID: 12725
		public static readonly ADPropertyDefinition Keywords = new ADPropertyDefinition("Keywords", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchMailflowPolicyKeywords", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040031B6 RID: 12726
		public static readonly ADPropertyDefinition Guid = ADObjectSchema.Guid;

		// Token: 0x040031B7 RID: 12727
		public static readonly ADPropertyDefinition DistinguishedName = ADObjectSchema.DistinguishedName;

		// Token: 0x040031B8 RID: 12728
		public static readonly ADPropertyDefinition ImmutableId = TransportRuleSchema.ImmutableId;
	}
}
