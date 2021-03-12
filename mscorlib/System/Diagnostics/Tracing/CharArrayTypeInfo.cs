using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200043F RID: 1087
	internal sealed class CharArrayTypeInfo : TraceLoggingTypeInfo<char[]>
	{
		// Token: 0x06003590 RID: 13712 RVA: 0x000CEC52 File Offset: 0x000CCE52
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.Char16));
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000CEC66 File Offset: 0x000CCE66
		public override void WriteData(TraceLoggingDataCollector collector, ref char[] value)
		{
			collector.AddArray(value);
		}
	}
}
