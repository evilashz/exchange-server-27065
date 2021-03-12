using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001DB RID: 475
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxPhotoWriter
	{
		// Token: 0x060011B5 RID: 4533
		void UploadPreview(int thumbprint, IDictionary<UserPhotoSize, byte[]> photos);

		// Token: 0x060011B6 RID: 4534
		void Clear();

		// Token: 0x060011B7 RID: 4535
		void ClearPreview();

		// Token: 0x060011B8 RID: 4536
		void Save();
	}
}
