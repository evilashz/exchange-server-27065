using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A72 RID: 2674
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderStatisticsSchema : XsoMailboxConfigurationObjectSchema
	{
		// Token: 0x04003776 RID: 14198
		public static readonly XsoDriverPropertyDefinition AssociatedItemCount = new XsoDriverPropertyDefinition(FolderSchema.AssociatedItemCount, "AssociatedItemCount", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003777 RID: 14199
		public static readonly XsoDriverPropertyDefinition CreationTime = new XsoDriverPropertyDefinition(StoreObjectSchema.CreationTime, "CreationTime", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003778 RID: 14200
		public static readonly XsoDriverPropertyDefinition EntryId = new XsoDriverPropertyDefinition(FolderSchema.Id, "Id", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003779 RID: 14201
		public static readonly XsoDriverPropertyDefinition FolderPath = new XsoDriverPropertyDefinition(FolderSchema.FolderPathName, "FolderPath", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400377A RID: 14202
		public static readonly XsoDriverPropertyDefinition ItemCount = new XsoDriverPropertyDefinition(FolderSchema.ItemCount, "ItemCount", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400377B RID: 14203
		public static readonly XsoDriverPropertyDefinition LastModificationTime = new XsoDriverPropertyDefinition(StoreObjectSchema.LastModifiedTime, "LastModifiedTime", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400377C RID: 14204
		public static readonly XsoDriverPropertyDefinition Name = new XsoDriverPropertyDefinition(FolderSchema.DisplayName, "DisplayName", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400377D RID: 14205
		public static readonly XsoDriverPropertyDefinition TotalAssociatedItemSize = new XsoDriverPropertyDefinition(InternalSchema.ExtendedAssociatedItemSize, "TotalAssociatedItemSize", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400377E RID: 14206
		public static readonly XsoDriverPropertyDefinition TotalItemSize = new XsoDriverPropertyDefinition(FolderSchema.ExtendedSize, "ExtendedSize", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
