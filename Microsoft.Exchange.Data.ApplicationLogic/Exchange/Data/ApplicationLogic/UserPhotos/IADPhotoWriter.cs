using System;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001CA RID: 458
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IADPhotoWriter
	{
		// Token: 0x0600115C RID: 4444
		void Write(ADObjectId recipientId, Stream photo);
	}
}
