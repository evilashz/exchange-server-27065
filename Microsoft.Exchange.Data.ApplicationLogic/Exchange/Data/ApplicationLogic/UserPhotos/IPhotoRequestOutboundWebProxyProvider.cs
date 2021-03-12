using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001DF RID: 479
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IPhotoRequestOutboundWebProxyProvider
	{
		// Token: 0x060011BC RID: 4540
		IWebProxy Create();
	}
}
