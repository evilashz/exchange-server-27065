using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000429 RID: 1065
	internal sealed class Int16TypeInfo : TraceLoggingTypeInfo<short>
	{
		// Token: 0x0600354E RID: 13646 RVA: 0x000CE8DF File Offset: 0x000CCADF
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000CE8EF File Offset: 0x000CCAEF
		public override void WriteData(TraceLoggingDataCollector collector, ref short value)
		{
			collector.AddScalar(value);
		}
	}
}
