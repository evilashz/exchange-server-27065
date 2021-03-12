using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000041 RID: 65
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LocationEntryIdStrategy : EntryIdStrategy
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x0002D55C File Offset: 0x0002B75C
		internal static PropertyBag GetMailboxPropertyBag(DefaultFolderContext context)
		{
			return context.GetMailboxPropertyBag();
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0002D564 File Offset: 0x0002B764
		internal static PropertyBag GetInboxOrConfigurationFolderPropertyBag(DefaultFolderContext context)
		{
			return context.GetInboxOrConfigurationFolderPropertyBag();
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0002D56C File Offset: 0x0002B76C
		internal LocationEntryIdStrategy(StorePropertyDefinition property, LocationEntryIdStrategy.GetLocationPropertyBagDelegate getLocationPropertyBag)
		{
			this.Property = property;
			this.GetLocationPropertyBag = getLocationPropertyBag;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0002D582 File Offset: 0x0002B782
		internal override void GetDependentProperties(object location, IList<StorePropertyDefinition> result)
		{
			if (object.Equals(location, this.GetLocationPropertyBag))
			{
				result.Add(this.Property);
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0002D5A0 File Offset: 0x0002B7A0
		internal override byte[] GetEntryId(DefaultFolderContext context)
		{
			PropertyBag propertyBag = this.GetLocationPropertyBag(context);
			return propertyBag.TryGetProperty(this.Property) as byte[];
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0002D5CB File Offset: 0x0002B7CB
		internal override void SetEntryId(DefaultFolderContext context, byte[] entryId)
		{
			this.SetEntryValueInternal(context, entryId);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0002D5D5 File Offset: 0x0002B7D5
		internal override FolderSaveResult UnsetEntryId(DefaultFolderContext context)
		{
			return this.UnsetEntryValueInternal(context);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0002D5E0 File Offset: 0x0002B7E0
		private FolderSaveResult UnsetEntryValueInternal(DefaultFolderContext context)
		{
			FolderSaveResult folderSaveResult;
			using (Folder folder = Folder.Bind(context.Session, this.GetFolderId(context, DefaultFolderType.Inbox)))
			{
				folder.Delete(this.Property);
				folderSaveResult = folder.Save();
			}
			if (folderSaveResult.OperationResult != OperationResult.Succeeded)
			{
				return folderSaveResult;
			}
			FolderSaveResult result;
			using (Folder folder2 = Folder.Bind(context.Session, this.GetFolderId(context, DefaultFolderType.Configuration)))
			{
				folder2.Delete(this.Property);
				result = folder2.Save();
			}
			return result;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0002D67C File Offset: 0x0002B87C
		protected void SetEntryValueInternal(DefaultFolderContext context, object propertyValue)
		{
			using (Folder folder = Folder.Bind(context.Session, this.GetFolderId(context, DefaultFolderType.Inbox)))
			{
				folder[this.Property] = propertyValue;
				folder.Save();
			}
			using (Folder folder2 = Folder.Bind(context.Session, this.GetFolderId(context, DefaultFolderType.Configuration)))
			{
				folder2[this.Property] = propertyValue;
				folder2.Save();
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0002D710 File Offset: 0x0002B910
		private StoreObjectId GetFolderId(DefaultFolderContext context, DefaultFolderType folderType)
		{
			StoreObjectId storeObjectId = context[folderType];
			if (storeObjectId == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ExDefaultFolderNotFound(folderType));
			}
			return storeObjectId;
		}

		// Token: 0x04000173 RID: 371
		protected readonly LocationEntryIdStrategy.GetLocationPropertyBagDelegate GetLocationPropertyBag;

		// Token: 0x04000174 RID: 372
		protected readonly StorePropertyDefinition Property;

		// Token: 0x02000042 RID: 66
		// (Invoke) Token: 0x0600058B RID: 1419
		internal delegate PropertyBag GetLocationPropertyBagDelegate(DefaultFolderContext context);
	}
}
