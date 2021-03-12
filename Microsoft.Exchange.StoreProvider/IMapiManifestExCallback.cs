using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000069 RID: 105
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMapiManifestExCallback
	{
		// Token: 0x060002CC RID: 716
		ManifestCallbackStatus Change(bool newMessage, PropValue[] headerProps, PropValue[] props);

		// Token: 0x060002CD RID: 717
		ManifestCallbackStatus Delete(byte[] idsetDeleted, bool softDeleted, bool expired);

		// Token: 0x060002CE RID: 718
		ManifestCallbackStatus ReadUnread(byte[] idsetReadUnread, bool read);
	}
}
