using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C76 RID: 3190
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class FolderIdProperty : IdProperty
	{
		// Token: 0x06007014 RID: 28692 RVA: 0x001F059C File Offset: 0x001EE79C
		public FolderIdProperty() : base("FolderId", typeof(VersionedId), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.EntryId, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.ChangeKey, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.ContainerClass, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.MapiFolderType, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.ExtendedFolderFlagsInternal, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06007015 RID: 28693 RVA: 0x001F0612 File Offset: 0x001EE812
		protected override StoreObjectType GetStoreObjectType(PropertyBag.BasicPropertyStore propertyBag)
		{
			return FolderIdProperty.GetFolderType(propertyBag);
		}

		// Token: 0x06007016 RID: 28694 RVA: 0x001F061C File Offset: 0x001EE81C
		internal static StoreObjectType GetFolderType(PropertyBag.BasicPropertyStore propertyBag)
		{
			int? num = propertyBag.GetValue(InternalSchema.MapiFolderType) as int?;
			FolderType? folderType = (num != null) ? new FolderType?((FolderType)num.GetValueOrDefault()) : null;
			StoreObjectType storeObjectType = ObjectClass.GetObjectType(propertyBag.GetValue(InternalSchema.ContainerClass) as string);
			bool? flag = propertyBag.GetValue(InternalSchema.IsOutlookSearchFolder) as bool?;
			if (storeObjectType == StoreObjectType.TasksFolder && folderType == FolderType.Search)
			{
				storeObjectType = StoreObjectType.SearchFolder;
			}
			else if (!Folder.IsFolderType(storeObjectType) || storeObjectType == StoreObjectType.Folder)
			{
				if (folderType == FolderType.Search)
				{
					if (flag == true)
					{
						storeObjectType = StoreObjectType.OutlookSearchFolder;
					}
					else
					{
						storeObjectType = StoreObjectType.SearchFolder;
					}
				}
				else
				{
					storeObjectType = StoreObjectType.Folder;
				}
			}
			return storeObjectType;
		}

		// Token: 0x06007017 RID: 28695 RVA: 0x001F06F9 File Offset: 0x001EE8F9
		protected override bool IsCompatibleId(StoreId id, ICoreObject coreObject)
		{
			return (coreObject == null || coreObject is CoreFolder) && IdConverter.IsFolderId(StoreId.GetStoreObjectId(id));
		}
	}
}
