using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001F7 RID: 503
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoGarbageCollectionScheduler
	{
		// Token: 0x0600125A RID: 4698 RVA: 0x0004D76C File Offset: 0x0004B96C
		public PhotoGarbageCollectionScheduler(PhotosConfiguration configuration, IPerformanceDataLogger perfLogger, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.tracer = tracer;
			this.configuration = configuration;
			this.collector = new PhotoGarbageCollector(configuration, perfLogger, tracer);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0004D7A8 File Offset: 0x0004B9A8
		public void Run(WaitHandle stopRequested)
		{
			this.PerformInitialCollection();
			while (!stopRequested.WaitOne(this.configuration.GarbageCollectionInterval))
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "Garbage collection scheduler: performing collection.");
				this.Collect();
				this.tracer.TraceDebug((long)this.GetHashCode(), "Garbage collection scheduler: collection complete.");
			}
			this.tracer.TraceDebug((long)this.GetHashCode(), "Garbage collection scheduler: stop requested.");
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0004D81B File Offset: 0x0004BA1B
		private void PerformInitialCollection()
		{
			this.tracer.TraceDebug((long)this.GetHashCode(), "Garbage collection scheduler: performing initial collection.");
			this.Collect();
			this.tracer.TraceDebug((long)this.GetHashCode(), "Garbage collection scheduler: initial collection complete.");
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0004D851 File Offset: 0x0004BA51
		private void Collect()
		{
			this.collector.Collect(DateTime.UtcNow);
		}

		// Token: 0x040009D2 RID: 2514
		private readonly PhotosConfiguration configuration;

		// Token: 0x040009D3 RID: 2515
		private readonly ITracer tracer;

		// Token: 0x040009D4 RID: 2516
		private readonly PhotoGarbageCollector collector;
	}
}
