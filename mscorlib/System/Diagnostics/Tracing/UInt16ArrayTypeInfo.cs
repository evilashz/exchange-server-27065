using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000438 RID: 1080
	internal sealed class UInt16ArrayTypeInfo : TraceLoggingTypeInfo<ushort[]>
	{
		// Token: 0x0600357B RID: 13691 RVA: 0x000CEB5A File Offset: 0x000CCD5A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000CEB6A File Offset: 0x000CCD6A
		public override void WriteData(TraceLoggingDataCollector collector, ref ushort[] value)
		{
			collector.AddArray(value);
		}
	}
}
