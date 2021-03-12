using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200042B RID: 1067
	internal sealed class Int32TypeInfo : TraceLoggingTypeInfo<int>
	{
		// Token: 0x06003554 RID: 13652 RVA: 0x000CE923 File Offset: 0x000CCB23
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000CE933 File Offset: 0x000CCB33
		public override void WriteData(TraceLoggingDataCollector collector, ref int value)
		{
			collector.AddScalar(value);
		}
	}
}
