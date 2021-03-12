using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A6F RID: 2671
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderHierarchyInfoSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04003766 RID: 14182
		public static readonly ProviderPropertyDefinition TotalFolderCount = new SimpleProviderPropertyDefinition("TotalFolderCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003767 RID: 14183
		public static readonly ProviderPropertyDefinition MaxFolderChildCount = new SimpleProviderPropertyDefinition("MaxFolderChildCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003768 RID: 14184
		public static readonly ProviderPropertyDefinition HierarchyDepth = new SimpleProviderPropertyDefinition("HierarchyDepth", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003769 RID: 14185
		public static readonly ProviderPropertyDefinition MailPublicFolderCount = new SimpleProviderPropertyDefinition("MailPublicFolderCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400376A RID: 14186
		public static readonly ProviderPropertyDefinition CalendarFolderCount = new SimpleProviderPropertyDefinition("CalendarFolderCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400376B RID: 14187
		public static readonly ProviderPropertyDefinition ContactFolderCount = new SimpleProviderPropertyDefinition("ContactFolderCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400376C RID: 14188
		public static readonly ProviderPropertyDefinition InfoPathFolderCount = new SimpleProviderPropertyDefinition("InfoPathFolderCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400376D RID: 14189
		public static readonly ProviderPropertyDefinition JournalFolderCount = new SimpleProviderPropertyDefinition("JournalFolderCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400376E RID: 14190
		public static readonly ProviderPropertyDefinition StickyNoteFolderCount = new SimpleProviderPropertyDefinition("StickyNoteFolderCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400376F RID: 14191
		public static readonly ProviderPropertyDefinition TaskFolderCount = new SimpleProviderPropertyDefinition("TaskFolderCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003770 RID: 14192
		public static readonly ProviderPropertyDefinition NoteFolderCount = new SimpleProviderPropertyDefinition("NoteFolderCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003771 RID: 14193
		public static readonly ProviderPropertyDefinition OtherFolderCount = new SimpleProviderPropertyDefinition("OtherFolderCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
