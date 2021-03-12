using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000794 RID: 1940
	internal interface IIdConverter
	{
		// Token: 0x060039C4 RID: 14788
		IdAndSession ConvertFolderIdToIdAndSessionReadOnly(BaseFolderId folderId);
	}
}
