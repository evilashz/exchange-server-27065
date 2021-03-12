using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001DD RID: 477
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IPhotoRequestOutboundAuthenticator
	{
		// Token: 0x060011BA RID: 4538
		HttpWebResponse AuthenticateAndGetResponse(HttpWebRequest request);
	}
}
