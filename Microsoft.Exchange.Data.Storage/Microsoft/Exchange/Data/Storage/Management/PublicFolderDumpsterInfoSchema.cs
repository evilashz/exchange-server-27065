using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A6C RID: 2668
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderDumpsterInfoSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04003758 RID: 14168
		public static readonly ProviderPropertyDefinition DumpsterHolderEntryId = new SimpleProviderPropertyDefinition("DumpsterHolderEntryId", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.PersistDefaultValue, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003759 RID: 14169
		public static readonly ProviderPropertyDefinition CountTotalFolders = new SimpleProviderPropertyDefinition("CountTotalFolders", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400375A RID: 14170
		public static readonly ProviderPropertyDefinition HasDumpsterExtended = new SimpleProviderPropertyDefinition("HasDumpsterExtended", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400375B RID: 14171
		public static readonly ProviderPropertyDefinition CountLegacyDumpsters = new SimpleProviderPropertyDefinition("CountLegacyDumpsters", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400375C RID: 14172
		public static readonly ProviderPropertyDefinition CountContainerLevel1 = new SimpleProviderPropertyDefinition("CountContainerLevel1", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400375D RID: 14173
		public static readonly ProviderPropertyDefinition CountContainerLevel2 = new SimpleProviderPropertyDefinition("CountContainerLevel2", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400375E RID: 14174
		public static readonly ProviderPropertyDefinition CountDumpsters = new SimpleProviderPropertyDefinition("CountDumpsters", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400375F RID: 14175
		public static readonly ProviderPropertyDefinition CountDeletedFolders = new SimpleProviderPropertyDefinition("CountDeletedFolders", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
