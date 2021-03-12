using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000431 RID: 1073
	internal sealed class DoubleTypeInfo : TraceLoggingTypeInfo<double>
	{
		// Token: 0x06003566 RID: 13670 RVA: 0x000CE9F9 File Offset: 0x000CCBF9
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Double));
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000CEA0A File Offset: 0x000CCC0A
		public override void WriteData(TraceLoggingDataCollector collector, ref double value)
		{
			collector.AddScalar(value);
		}
	}
}
