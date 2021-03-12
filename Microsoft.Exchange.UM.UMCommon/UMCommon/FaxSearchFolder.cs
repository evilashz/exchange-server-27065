using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200007D RID: 125
	internal class FaxSearchFolder : DefaultUMSearchFolder
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x0000F2A3 File Offset: 0x0000D4A3
		internal FaxSearchFolder(MailboxSession itemStore) : base(itemStore)
		{
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000F2AC File Offset: 0x0000D4AC
		protected override DefaultFolderType DefaultFolderType
		{
			get
			{
				return DefaultFolderType.UMFax;
			}
		}
	}
}
