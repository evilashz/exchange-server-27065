using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001C4 RID: 452
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPhotoHandler
	{
		// Token: 0x06001146 RID: 4422
		PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response);

		// Token: 0x06001147 RID: 4423
		IPhotoHandler Then(IPhotoHandler next);
	}
}
