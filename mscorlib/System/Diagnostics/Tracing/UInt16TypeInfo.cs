using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200042A RID: 1066
	internal sealed class UInt16TypeInfo : TraceLoggingTypeInfo<ushort>
	{
		// Token: 0x06003551 RID: 13649 RVA: 0x000CE901 File Offset: 0x000CCB01
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000CE911 File Offset: 0x000CCB11
		public override void WriteData(TraceLoggingDataCollector collector, ref ushort value)
		{
			collector.AddScalar(value);
		}
	}
}
