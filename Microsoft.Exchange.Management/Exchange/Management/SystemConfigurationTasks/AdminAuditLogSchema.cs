using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200004E RID: 78
	internal class AdminAuditLogSchema : SimpleProviderObjectSchema
	{
		// Token: 0x0400011F RID: 287
		public static readonly SimpleProviderPropertyDefinition ObjectModified = new SimpleProviderPropertyDefinition("ObjectModified", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000120 RID: 288
		public static readonly SimpleProviderPropertyDefinition ModifiedObjectResolvedName = new SimpleProviderPropertyDefinition("ModifiedObjectResolvedName", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000121 RID: 289
		public static readonly SimpleProviderPropertyDefinition CmdletName = new SimpleProviderPropertyDefinition("CmdletName", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000122 RID: 290
		public static readonly SimpleProviderPropertyDefinition CmdletParameters = new SimpleProviderPropertyDefinition("CmdletParameters", ExchangeObjectVersion.Exchange2007, typeof(AdminAuditLogCmdletParameter), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000123 RID: 291
		public static readonly SimpleProviderPropertyDefinition ModifiedProperties = new SimpleProviderPropertyDefinition("ModifiedProperties", ExchangeObjectVersion.Exchange2007, typeof(AdminAuditLogModifiedProperty), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000124 RID: 292
		public static readonly SimpleProviderPropertyDefinition Caller = new SimpleProviderPropertyDefinition("Caller", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000125 RID: 293
		public static readonly SimpleProviderPropertyDefinition ExternalAccess = new SimpleProviderPropertyDefinition("ExternalAccess", ExchangeObjectVersion.Exchange2007, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000126 RID: 294
		public static readonly SimpleProviderPropertyDefinition Succeeded = new SimpleProviderPropertyDefinition("Succeeded", ExchangeObjectVersion.Exchange2007, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000127 RID: 295
		public static readonly SimpleProviderPropertyDefinition Error = new SimpleProviderPropertyDefinition("Error", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000128 RID: 296
		public static readonly SimpleProviderPropertyDefinition RunDate = new SimpleProviderPropertyDefinition("RunDate", ExchangeObjectVersion.Exchange2007, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000129 RID: 297
		public static readonly SimpleProviderPropertyDefinition OriginatingServer = new SimpleProviderPropertyDefinition("OriginatingServer", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
