using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x0200020B RID: 523
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PhotoServiceLocatorFactory : IPhotoServiceLocatorFactory
	{
		// Token: 0x0600130C RID: 4876 RVA: 0x0004EED2 File Offset: 0x0004D0D2
		public PhotoServiceLocatorFactory(ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.upstreamTracer = upstreamTracer;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0004EEEC File Offset: 0x0004D0EC
		public IPhotoServiceLocator CreateForLocalForest(IPerformanceDataLogger perfLogger)
		{
			ArgumentValidator.ThrowIfNull("perfLogger", perfLogger);
			return new LocalForestPhotoServiceLocatorUsingMailboxServerLocator(perfLogger, this.upstreamTracer);
		}

		// Token: 0x04000A88 RID: 2696
		private readonly ITracer upstreamTracer;
	}
}
