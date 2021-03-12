using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200043B RID: 1083
	internal sealed class Int64ArrayTypeInfo : TraceLoggingTypeInfo<long[]>
	{
		// Token: 0x06003584 RID: 13700 RVA: 0x000CEBC0 File Offset: 0x000CCDC0
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x000CEBD1 File Offset: 0x000CCDD1
		public override void WriteData(TraceLoggingDataCollector collector, ref long[] value)
		{
			collector.AddArray(value);
		}
	}
}
