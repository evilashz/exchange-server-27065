using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000427 RID: 1063
	internal sealed class ByteTypeInfo : TraceLoggingTypeInfo<byte>
	{
		// Token: 0x06003548 RID: 13640 RVA: 0x000CE89B File Offset: 0x000CCA9B
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.UInt8));
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000CE8AB File Offset: 0x000CCAAB
		public override void WriteData(TraceLoggingDataCollector collector, ref byte value)
		{
			collector.AddScalar(value);
		}
	}
}
