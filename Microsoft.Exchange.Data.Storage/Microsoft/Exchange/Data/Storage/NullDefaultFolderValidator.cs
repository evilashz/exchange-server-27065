using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000678 RID: 1656
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NullDefaultFolderValidator : DefaultFolderValidator
	{
		// Token: 0x06004449 RID: 17481 RVA: 0x001232C9 File Offset: 0x001214C9
		internal override bool EnsureIsValid(DefaultFolderContext context, StoreObjectId folderId, Dictionary<string, DefaultFolderManager.FolderData> folderDataDictionary)
		{
			return true;
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x001232CC File Offset: 0x001214CC
		protected override bool ValidateInternal(DefaultFolderContext context, PropertyBag propertyBag)
		{
			return true;
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x001232CF File Offset: 0x001214CF
		protected override void SetPropertiesInternal(DefaultFolderContext context, Folder folder)
		{
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x001232D1 File Offset: 0x001214D1
		public NullDefaultFolderValidator() : base(new IValidator[0])
		{
		}
	}
}
