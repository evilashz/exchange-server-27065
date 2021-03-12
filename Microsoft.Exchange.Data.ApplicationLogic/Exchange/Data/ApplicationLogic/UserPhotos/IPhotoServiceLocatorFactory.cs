using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001E1 RID: 481
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IPhotoServiceLocatorFactory
	{
		// Token: 0x060011BF RID: 4543
		IPhotoServiceLocator CreateForLocalForest(IPerformanceDataLogger perfLogger);
	}
}
