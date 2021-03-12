using System;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.PhotoGarbageCollection;
using Microsoft.Exchange.ServiceHost;

namespace Microsoft.Exchange.Servicelets.PhotoGarbageCollection
{
	// Token: 0x02000002 RID: 2
	public sealed class PhotoGarbageCollectionServicelet : Servicelet
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public override void Work()
		{
			using (PhotoGarbageCollectionLogger photoGarbageCollectionLogger = new PhotoGarbageCollectionLogger(PhotoGarbageCollectionServicelet.PhotosConfiguration, "PhotoGarbageCollectionServicelet"))
			{
				new PhotoGarbageCollectionScheduler(PhotoGarbageCollectionServicelet.PhotosConfiguration, photoGarbageCollectionLogger, photoGarbageCollectionLogger.Compose(PhotoGarbageCollectionServicelet.Tracer)).Run(base.StopEvent);
			}
		}

		// Token: 0x04000001 RID: 1
		private const string LogFileName = "PhotoGarbageCollectionServicelet";

		// Token: 0x04000002 RID: 2
		private static readonly Trace Tracer = ExTraceGlobals.GarbageCollectionTracer;

		// Token: 0x04000003 RID: 3
		private static readonly PhotosConfiguration PhotosConfiguration = new PhotosConfiguration(ExchangeSetupContext.InstallPath);
	}
}
