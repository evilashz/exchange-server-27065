using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001D4 RID: 468
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFileSystemPhotoReader
	{
		// Token: 0x0600118F RID: 4495
		PhotoMetadata Read(string photoFullPath, Stream output);

		// Token: 0x06001190 RID: 4496
		int ReadThumbprint(string photoFullPath);

		// Token: 0x06001191 RID: 4497
		DateTime GetLastModificationTimeUtc(string photoFullPath);
	}
}
