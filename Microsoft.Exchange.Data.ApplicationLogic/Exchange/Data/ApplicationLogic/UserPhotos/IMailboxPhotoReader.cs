using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001DA RID: 474
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxPhotoReader
	{
		// Token: 0x060011B0 RID: 4528
		PhotoMetadata Read(IMailboxSession session, UserPhotoSize size, bool preview, Stream output, IPerformanceDataLogger perfLogger);

		// Token: 0x060011B1 RID: 4529
		int ReadThumbprint(IMailboxSession session, bool preview, bool forceReloadThumbprint);

		// Token: 0x060011B2 RID: 4530
		int ReadThumbprint(IMailboxSession session, bool preview);

		// Token: 0x060011B3 RID: 4531
		int ReadAllPreviewSizes(IMailboxSession session, IDictionary<UserPhotoSize, byte[]> output);

		// Token: 0x060011B4 RID: 4532
		bool HasPhotoBeenDeleted(Exception e);
	}
}
