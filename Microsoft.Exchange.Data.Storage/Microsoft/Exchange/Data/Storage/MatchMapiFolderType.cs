using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200067E RID: 1662
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MatchMapiFolderType : IValidator
	{
		// Token: 0x06004463 RID: 17507 RVA: 0x00123A9C File Offset: 0x00121C9C
		internal MatchMapiFolderType(FolderType folderType)
		{
			this.folderType = folderType;
		}

		// Token: 0x06004464 RID: 17508 RVA: 0x00123AAC File Offset: 0x00121CAC
		public bool Validate(DefaultFolderContext context, PropertyBag propertyBag)
		{
			FolderType? valueAsNullable = propertyBag.GetValueAsNullable<FolderType>(InternalSchema.MapiFolderType);
			return valueAsNullable != null && valueAsNullable.Value == this.folderType;
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x00123ADF File Offset: 0x00121CDF
		public void SetProperties(DefaultFolderContext context, Folder folder)
		{
		}

		// Token: 0x0400254F RID: 9551
		private FolderType folderType;
	}
}
