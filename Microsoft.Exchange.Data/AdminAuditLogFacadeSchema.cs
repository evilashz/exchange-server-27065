using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001EB RID: 491
	internal class AdminAuditLogFacadeSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000A7D RID: 2685
		public static readonly SimpleProviderPropertyDefinition ObjectModified = new SimpleProviderPropertyDefinition("ObjectModified", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000A7E RID: 2686
		public static readonly SimpleProviderPropertyDefinition ModifiedObjectResolvedName = new SimpleProviderPropertyDefinition("ModifiedObjectResolvedName", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000A7F RID: 2687
		public static readonly SimpleProviderPropertyDefinition CmdletName = new SimpleProviderPropertyDefinition("CmdletName", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000A80 RID: 2688
		public static readonly SimpleProviderPropertyDefinition CmdletParameters = new SimpleProviderPropertyDefinition("CmdletParameters", ExchangeObjectVersion.Exchange2007, typeof(AdminAuditLogCmdletParameter), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000A81 RID: 2689
		public static readonly SimpleProviderPropertyDefinition ModifiedProperties = new SimpleProviderPropertyDefinition("ModifiedProperties", ExchangeObjectVersion.Exchange2007, typeof(AdminAuditLogModifiedProperty), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000A82 RID: 2690
		public static readonly SimpleProviderPropertyDefinition Caller = new SimpleProviderPropertyDefinition("Caller", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000A83 RID: 2691
		public static readonly SimpleProviderPropertyDefinition Succeeded = new SimpleProviderPropertyDefinition("Succeeded", ExchangeObjectVersion.Exchange2007, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000A84 RID: 2692
		public static readonly SimpleProviderPropertyDefinition Error = new SimpleProviderPropertyDefinition("Error", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000A85 RID: 2693
		public static readonly SimpleProviderPropertyDefinition RunDate = new SimpleProviderPropertyDefinition("RunDate", ExchangeObjectVersion.Exchange2007, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000A86 RID: 2694
		public static readonly SimpleProviderPropertyDefinition OriginatingServer = new SimpleProviderPropertyDefinition("OriginatingServer", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
