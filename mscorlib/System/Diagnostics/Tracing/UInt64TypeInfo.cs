using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200042E RID: 1070
	internal sealed class UInt64TypeInfo : TraceLoggingTypeInfo<ulong>
	{
		// Token: 0x0600355D RID: 13661 RVA: 0x000CE98A File Offset: 0x000CCB8A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000CE99B File Offset: 0x000CCB9B
		public override void WriteData(TraceLoggingDataCollector collector, ref ulong value)
		{
			collector.AddScalar(value);
		}
	}
}
