using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001E2 RID: 482
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IRemoteForestPhotoRetrievalPipelineFactory
	{
		// Token: 0x060011C0 RID: 4544
		IPhotoHandler Create();
	}
}
