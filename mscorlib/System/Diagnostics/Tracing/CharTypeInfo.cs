using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000433 RID: 1075
	internal sealed class CharTypeInfo : TraceLoggingTypeInfo<char>
	{
		// Token: 0x0600356C RID: 13676 RVA: 0x000CEA3F File Offset: 0x000CCC3F
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Char16));
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000CEA53 File Offset: 0x000CCC53
		public override void WriteData(TraceLoggingDataCollector collector, ref char value)
		{
			collector.AddScalar(value);
		}
	}
}
