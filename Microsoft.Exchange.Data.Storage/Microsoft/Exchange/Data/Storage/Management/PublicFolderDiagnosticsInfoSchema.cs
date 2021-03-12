using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A92 RID: 2706
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PublicFolderDiagnosticsInfoSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04003801 RID: 14337
		public static readonly ProviderPropertyDefinition DisplayName = new SimpleProviderPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003802 RID: 14338
		public static readonly ProviderPropertyDefinition AssistantInfo = new SimpleProviderPropertyDefinition("AssistantInfo", ExchangeObjectVersion.Exchange2010, typeof(PublicFolderMailboxAssistantInfo), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003803 RID: 14339
		public static readonly ProviderPropertyDefinition SyncInfo = new SimpleProviderPropertyDefinition("SyncInfo", ExchangeObjectVersion.Exchange2010, typeof(PublicFolderMailboxSynchronizerInfo), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003804 RID: 14340
		public static readonly ProviderPropertyDefinition DumpsterInfo = new SimpleProviderPropertyDefinition("DumpsterInfo", ExchangeObjectVersion.Exchange2012, typeof(PublicFolderMailboxDumpsterInfo), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003805 RID: 14341
		public static readonly ProviderPropertyDefinition HierarchyInfo = new SimpleProviderPropertyDefinition("HierarchyInfo", ExchangeObjectVersion.Exchange2012, typeof(PublicFolderMailboxHierarchyInfo), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
