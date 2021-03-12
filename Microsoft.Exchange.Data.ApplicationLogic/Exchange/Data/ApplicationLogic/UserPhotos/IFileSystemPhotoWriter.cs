using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001D7 RID: 471
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFileSystemPhotoWriter
	{
		// Token: 0x060011A0 RID: 4512
		void Write(string photoFullPath, int thumbprint, Stream photo);
	}
}
