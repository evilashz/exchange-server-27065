using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000430 RID: 1072
	internal sealed class UIntPtrTypeInfo : TraceLoggingTypeInfo<UIntPtr>
	{
		// Token: 0x06003563 RID: 13667 RVA: 0x000CE9D3 File Offset: 0x000CCBD3
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.FormatPtr(format, Statics.UIntPtrType));
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000CE9E7 File Offset: 0x000CCBE7
		public override void WriteData(TraceLoggingDataCollector collector, ref UIntPtr value)
		{
			collector.AddScalar(value);
		}
	}
}
