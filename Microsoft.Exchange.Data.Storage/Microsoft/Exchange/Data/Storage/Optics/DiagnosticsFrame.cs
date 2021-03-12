using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Optics
{
	// Token: 0x02000AD9 RID: 2777
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DiagnosticsFrame : DisposableFrame, IDiagnosticsFrame, IForceReportDisposeTrackable, IDisposeTrackable, IDisposable
	{
		// Token: 0x060064D6 RID: 25814 RVA: 0x001ABC64 File Offset: 0x001A9E64
		public DiagnosticsFrame(string operationContext, string operationName, ITracer tracer, IExtensibleLogger logger, IMailboxPerformanceTracker performanceTracker) : base(null)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("operationContext", operationContext);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("operationName", operationName);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			ArgumentValidator.ThrowIfNull("logger", logger);
			ArgumentValidator.ThrowIfNull("performanceTracker", performanceTracker);
			this.operationContext = operationContext;
			this.operationName = operationName;
			this.tracer = tracer;
			this.logger = logger;
			this.performanceTracker = performanceTracker;
			this.Start();
		}

		// Token: 0x060064D7 RID: 25815 RVA: 0x001ABCDC File Offset: 0x001A9EDC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DiagnosticsFrame>(this);
		}

		// Token: 0x060064D8 RID: 25816 RVA: 0x001ABCE4 File Offset: 0x001A9EE4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.Finish();
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x060064D9 RID: 25817 RVA: 0x001ABCF8 File Offset: 0x001A9EF8
		private void Start()
		{
			this.tracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0}: Starting {1}.", this.operationContext, this.operationName);
			this.logger.LogEvent(new SchemaBasedLogEvent<DiagnosticsFrame.OperationStart>
			{
				{
					DiagnosticsFrame.OperationStart.OperationName,
					this.operationName
				}
			});
			this.performanceTracker.Start();
		}

		// Token: 0x060064DA RID: 25818 RVA: 0x001ABD54 File Offset: 0x001A9F54
		private void Finish()
		{
			this.performanceTracker.Stop();
			this.logger.LogEvent(this.performanceTracker.GetLogEvent(this.operationName));
			this.tracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0}: Finishing {1}.", this.operationContext, this.operationName);
		}

		// Token: 0x04003975 RID: 14709
		private readonly string operationContext;

		// Token: 0x04003976 RID: 14710
		private readonly string operationName;

		// Token: 0x04003977 RID: 14711
		private readonly ITracer tracer;

		// Token: 0x04003978 RID: 14712
		private readonly IExtensibleLogger logger;

		// Token: 0x04003979 RID: 14713
		private readonly IMailboxPerformanceTracker performanceTracker;

		// Token: 0x02000ADA RID: 2778
		private enum OperationStart
		{
			// Token: 0x0400397B RID: 14715
			OperationName
		}
	}
}
