using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200067D RID: 1661
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DefaultSubFolderValidator : DefaultFolderValidator
	{
		// Token: 0x06004461 RID: 17505 RVA: 0x00123A50 File Offset: 0x00121C50
		internal DefaultSubFolderValidator(DefaultFolderType parentFolderType, params IValidator[] validators) : base(validators)
		{
			this.parentFolderType = parentFolderType;
		}

		// Token: 0x06004462 RID: 17506 RVA: 0x00123A60 File Offset: 0x00121C60
		internal override bool EnsureIsValid(DefaultFolderContext context, Folder folder)
		{
			if (!base.EnsureIsValid(context, folder.PropertyBag))
			{
				return false;
			}
			StoreObjectId storeObjectId = context[this.parentFolderType];
			return storeObjectId == null || storeObjectId.Equals(folder.ParentId);
		}

		// Token: 0x0400254E RID: 9550
		private DefaultFolderType parentFolderType;
	}
}
