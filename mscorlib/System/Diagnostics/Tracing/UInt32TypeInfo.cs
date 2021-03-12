using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200042C RID: 1068
	internal sealed class UInt32TypeInfo : TraceLoggingTypeInfo<uint>
	{
		// Token: 0x06003557 RID: 13655 RVA: 0x000CE945 File Offset: 0x000CCB45
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000CE955 File Offset: 0x000CCB55
		public override void WriteData(TraceLoggingDataCollector collector, ref uint value)
		{
			collector.AddScalar(value);
		}
	}
}
