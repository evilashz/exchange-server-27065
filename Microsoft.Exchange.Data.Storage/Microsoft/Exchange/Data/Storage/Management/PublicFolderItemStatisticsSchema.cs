using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A75 RID: 2677
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderItemStatisticsSchema : MailMessageSchema
	{
		// Token: 0x0400378E RID: 14222
		public static readonly XsoDriverPropertyDefinition EntryId = new XsoDriverPropertyDefinition(StoreObjectSchema.EntryId, "EntryId", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400378F RID: 14223
		public static readonly XsoDriverPropertyDefinition LastModifiedTime = new XsoDriverPropertyDefinition(StoreObjectSchema.LastModifiedTime, "LastModifiedTime", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003790 RID: 14224
		public static readonly XsoDriverPropertyDefinition CreationTime = new XsoDriverPropertyDefinition(StoreObjectSchema.CreationTime, "CreationTime", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003791 RID: 14225
		public static readonly XsoDriverPropertyDefinition HasAttachment = new XsoDriverPropertyDefinition(ItemSchema.HasAttachment, "HasAttachment", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003792 RID: 14226
		public static readonly XsoDriverPropertyDefinition ItemClass = new XsoDriverPropertyDefinition(StoreObjectSchema.ItemClass, "ItemClass", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003793 RID: 14227
		public static readonly XsoDriverPropertyDefinition Size = new XsoDriverPropertyDefinition(ItemSchema.Size, "Size", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
