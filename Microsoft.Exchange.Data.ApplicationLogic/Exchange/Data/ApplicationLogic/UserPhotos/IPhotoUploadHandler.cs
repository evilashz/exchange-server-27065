using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001C8 RID: 456
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPhotoUploadHandler
	{
		// Token: 0x06001152 RID: 4434
		PhotoResponse Upload(PhotoRequest request, PhotoResponse response);

		// Token: 0x06001153 RID: 4435
		IPhotoUploadHandler Then(IPhotoUploadHandler next);
	}
}
