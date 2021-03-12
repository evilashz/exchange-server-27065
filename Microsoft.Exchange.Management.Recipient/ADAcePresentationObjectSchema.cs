using System;
using System.DirectoryServices;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000094 RID: 148
	internal class ADAcePresentationObjectSchema : AcePresentationObjectSchema
	{
		// Token: 0x04000200 RID: 512
		public static readonly SimpleProviderPropertyDefinition AccessRights = new SimpleProviderPropertyDefinition("AccessRights", ExchangeObjectVersion.Exchange2003, typeof(ActiveDirectoryRights[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000201 RID: 513
		public static readonly SimpleProviderPropertyDefinition ExtendedRights = new SimpleProviderPropertyDefinition("ExtendedRights", ExchangeObjectVersion.Exchange2003, typeof(ExtendedRightIdParameter[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000202 RID: 514
		public static readonly SimpleProviderPropertyDefinition ChildObjectTypes = new SimpleProviderPropertyDefinition("ChildObjectTypes", ExchangeObjectVersion.Exchange2003, typeof(ADSchemaObjectIdParameter[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000203 RID: 515
		public static readonly SimpleProviderPropertyDefinition InheritedObjectType = new SimpleProviderPropertyDefinition("InheritedObjectType", ExchangeObjectVersion.Exchange2003, typeof(ADSchemaObjectIdParameter), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000204 RID: 516
		public static readonly SimpleProviderPropertyDefinition Properties = new SimpleProviderPropertyDefinition("Properties", ExchangeObjectVersion.Exchange2003, typeof(ADSchemaObjectIdParameter[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
