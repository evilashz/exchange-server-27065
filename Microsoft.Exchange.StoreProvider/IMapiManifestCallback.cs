using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000068 RID: 104
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMapiManifestCallback
	{
		// Token: 0x060002C9 RID: 713
		ManifestCallbackStatus Change(byte[] entryId, byte[] sourceKey, byte[] changeKey, byte[] changeList, DateTime lastModificationTime, ManifestChangeType changeType, bool associated, PropValue[] props);

		// Token: 0x060002CA RID: 714
		ManifestCallbackStatus Delete(byte[] entryId, bool softDelete, bool expiry);

		// Token: 0x060002CB RID: 715
		ManifestCallbackStatus ReadUnread(byte[] entryId, bool read);
	}
}
