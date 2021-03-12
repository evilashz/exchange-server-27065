using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001DE RID: 478
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IPhotoRequestOutboundSender
	{
		// Token: 0x060011BB RID: 4539
		HttpWebResponse SendAndGetResponse(HttpWebRequest request);
	}
}
