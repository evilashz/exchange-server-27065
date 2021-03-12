using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001DC RID: 476
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPhotoEditor
	{
		// Token: 0x060011B9 RID: 4537
		IDictionary<UserPhotoSize, byte[]> CropAndScale(Stream photo);
	}
}
