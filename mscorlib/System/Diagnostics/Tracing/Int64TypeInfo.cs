using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200042D RID: 1069
	internal sealed class Int64TypeInfo : TraceLoggingTypeInfo<long>
	{
		// Token: 0x0600355A RID: 13658 RVA: 0x000CE967 File Offset: 0x000CCB67
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000CE978 File Offset: 0x000CCB78
		public override void WriteData(TraceLoggingDataCollector collector, ref long value)
		{
			collector.AddScalar(value);
		}
	}
}
