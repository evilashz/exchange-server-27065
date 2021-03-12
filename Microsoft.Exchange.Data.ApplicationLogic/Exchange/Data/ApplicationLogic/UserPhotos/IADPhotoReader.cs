using System;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001C6 RID: 454
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IADPhotoReader
	{
		// Token: 0x0600114E RID: 4430
		PhotoMetadata Read(IRecipientSession session, ADObjectId recipientId, Stream output);
	}
}
