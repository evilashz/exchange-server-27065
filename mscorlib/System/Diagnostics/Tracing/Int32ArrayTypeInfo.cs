using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000439 RID: 1081
	internal sealed class Int32ArrayTypeInfo : TraceLoggingTypeInfo<int[]>
	{
		// Token: 0x0600357E RID: 13694 RVA: 0x000CEB7C File Offset: 0x000CCD7C
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000CEB8C File Offset: 0x000CCD8C
		public override void WriteData(TraceLoggingDataCollector collector, ref int[] value)
		{
			collector.AddArray(value);
		}
	}
}
