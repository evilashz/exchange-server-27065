using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AEF RID: 2799
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FolderImportPropertyBag : FolderPropertyBag
	{
		// Token: 0x060065AB RID: 26027 RVA: 0x001B04A2 File Offset: 0x001AE6A2
		internal FolderImportPropertyBag(HierarchySynchronizationUploadContext context, StoreObjectId parentFolderId, StoreObjectId folderId, ICollection<PropertyDefinition> properties) : base(context.Session, null, properties)
		{
			this.context = context;
			this.parentFolderId = parentFolderId;
			this.folderId = folderId;
		}

		// Token: 0x060065AC RID: 26028 RVA: 0x001B04C8 File Offset: 0x001AE6C8
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FolderImportPropertyBag>(this);
		}

		// Token: 0x060065AD RID: 26029 RVA: 0x001B04D0 File Offset: 0x001AE6D0
		internal override FolderSaveResult SaveFolderPropertyBag(bool needVersionCheck)
		{
			if (this.Context.CoreObject.Origin == Origin.New)
			{
				try
				{
					try
					{
						using (Folder folder = Folder.Bind(this.context.Session, this.parentFolderId, null))
						{
							if (folder is SearchFolder)
							{
								throw new InvalidParentFolderException(ServerStrings.ExCannotCreateSubfolderUnderSearchFolder);
							}
						}
					}
					catch (ObjectNotFoundException)
					{
					}
					List<PropertyDefinition> list = new List<PropertyDefinition>(base.MemoryPropertyBag.ChangeList.Count);
					List<object> list2 = new List<object>(list.Count);
					foreach (PropertyDefinition propertyDefinition in base.MemoryPropertyBag.ChangeList)
					{
						object obj = base.MemoryPropertyBag.TryGetProperty(propertyDefinition);
						if (!PropertyError.IsPropertyError(obj))
						{
							list.Add(propertyDefinition);
							list2.Add(obj);
						}
					}
					this.context.ImportChange(this.ExTimeZone, list, list2);
					this.Context.CoreState.Origin = Origin.Existing;
					return FolderPropertyBag.SuccessfulSave;
				}
				finally
				{
					this.Clear();
				}
			}
			return base.SaveFolderPropertyBag(needVersionCheck);
		}

		// Token: 0x060065AE RID: 26030 RVA: 0x001B061C File Offset: 0x001AE81C
		protected override void LazyCreateMapiPropertyBag()
		{
			if (this.Context.CoreObject != null)
			{
				if (this.Context.CoreState.Origin == Origin.New)
				{
					return;
				}
				base.MapiPropertyBag = MapiPropertyBag.CreateMapiPropertyBag(this.context.Session, this.folderId);
			}
		}

		// Token: 0x040039D6 RID: 14806
		private readonly HierarchySynchronizationUploadContext context;

		// Token: 0x040039D7 RID: 14807
		private readonly StoreObjectId parentFolderId;

		// Token: 0x040039D8 RID: 14808
		private readonly StoreObjectId folderId;
	}
}
