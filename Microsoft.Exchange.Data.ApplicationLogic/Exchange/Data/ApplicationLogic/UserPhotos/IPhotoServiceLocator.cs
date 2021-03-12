using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001E0 RID: 480
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IPhotoServiceLocator
	{
		// Token: 0x060011BD RID: 4541
		Uri Locate(ADUser target);

		// Token: 0x060011BE RID: 4542
		bool IsServiceOnThisServer(Uri service);
	}
}
