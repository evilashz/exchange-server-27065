using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C75 RID: 3189
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ItemIdProperty : IdProperty
	{
		// Token: 0x06007011 RID: 28689 RVA: 0x001F0510 File Offset: 0x001EE710
		public ItemIdProperty() : base("ItemId", typeof(VersionedId), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.EntryId, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.ChangeKey, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.ItemClass, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06007012 RID: 28690 RVA: 0x001F056A File Offset: 0x001EE76A
		protected override StoreObjectType GetStoreObjectType(PropertyBag.BasicPropertyStore propertyBag)
		{
			return ObjectClass.GetObjectType(propertyBag.GetValue(InternalSchema.ItemClass) as string);
		}

		// Token: 0x06007013 RID: 28691 RVA: 0x001F0582 File Offset: 0x001EE782
		protected override bool IsCompatibleId(StoreId id, ICoreObject coreObject)
		{
			return (coreObject == null || coreObject is ICoreItem) && IdConverter.IsMessageId(StoreId.GetStoreObjectId(id));
		}
	}
}
