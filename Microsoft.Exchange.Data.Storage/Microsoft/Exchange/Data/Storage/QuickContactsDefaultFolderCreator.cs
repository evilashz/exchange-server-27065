using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200068C RID: 1676
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class QuickContactsDefaultFolderCreator : MessageClassBasedDefaultFolderCreator
	{
		// Token: 0x06004498 RID: 17560 RVA: 0x00124BC2 File Offset: 0x00122DC2
		internal QuickContactsDefaultFolderCreator() : base(DefaultFolderType.Contacts, "IPF.Contact.MOC.QuickContacts", true)
		{
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x00124BD1 File Offset: 0x00122DD1
		protected override void StampExtraPropertiesOnNewlyCreatedFolder(Folder folder)
		{
			folder[FolderSchema.IsHidden] = true;
			folder.Save();
			folder.Load(null);
		}
	}
}
