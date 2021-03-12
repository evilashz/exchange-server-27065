using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200003D RID: 61
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GALContactsRefreshRequestPerformanceLogger : IPerformanceDataLogger
	{
		// Token: 0x060001FA RID: 506 RVA: 0x0000A6A3 File Offset: 0x000088A3
		internal GALContactsRefreshRequestPerformanceLogger(RequestDetailsLogger logger, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("logger", logger);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.logger = logger;
			this.tracer = tracer;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000A6CF File Offset: 0x000088CF
		public void Log(string marker, string counter, TimeSpan dataPoint)
		{
			this.tracer.TracePerformance<string, string, double>((long)this.GetHashCode(), "{0}.{1}={2}ms", marker, counter, dataPoint.TotalMilliseconds);
			this.MapToMetadataAndLog(marker, counter, dataPoint.TotalMilliseconds);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000A705 File Offset: 0x00008905
		public void Log(string marker, string counter, uint dataPoint)
		{
			this.tracer.TracePerformance<string, string, uint>((long)this.GetHashCode(), "{0}.{1}={2}", marker, counter, dataPoint);
			this.MapToMetadataAndLog(marker, counter, dataPoint);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000A72F File Offset: 0x0000892F
		public void Log(string marker, string counter, string dataPoint)
		{
			this.tracer.TracePerformance<string, string, string>((long)this.GetHashCode(), "{0}.{1}={2}", marker, counter, dataPoint);
			this.MapToMetadataAndLog(marker, counter, dataPoint);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000A754 File Offset: 0x00008954
		private void MapToMetadataAndLog(string marker, string counter, object dataPoint)
		{
			GALContactsRefreshLoggingMetadata galcontactsRefreshLoggingMetadata;
			if (!GALContactsRefreshRequestPerformanceLogger.MarkerAndCounterToMetadataMap.TryGetValue(Tuple.Create<string, string>(marker, counter), out galcontactsRefreshLoggingMetadata))
			{
				return;
			}
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(this.logger, galcontactsRefreshLoggingMetadata, dataPoint);
		}

		// Token: 0x040002BC RID: 700
		private const string ElapsedTime = "ElapsedTime";

		// Token: 0x040002BD RID: 701
		private const string CpuTime = "CpuTime";

		// Token: 0x040002BE RID: 702
		private const string LdapCount = "LdapCount";

		// Token: 0x040002BF RID: 703
		private const string LdapLatency = "LdapLatency";

		// Token: 0x040002C0 RID: 704
		private const string StoreRpcCount = "StoreRpcCount";

		// Token: 0x040002C1 RID: 705
		private const string StoreRpcLatency = "StoreRpcLatency";

		// Token: 0x040002C2 RID: 706
		private static readonly Dictionary<Tuple<string, string>, GALContactsRefreshLoggingMetadata> MarkerAndCounterToMetadataMap = new Dictionary<Tuple<string, string>, GALContactsRefreshLoggingMetadata>
		{
			{
				Tuple.Create<string, string>("GALFolderInitialLoad", "ElapsedTime"),
				GALContactsRefreshLoggingMetadata.GALFolderInitialLoad
			}
		};

		// Token: 0x040002C3 RID: 707
		private readonly RequestDetailsLogger logger;

		// Token: 0x040002C4 RID: 708
		private readonly ITracer tracer;
	}
}
