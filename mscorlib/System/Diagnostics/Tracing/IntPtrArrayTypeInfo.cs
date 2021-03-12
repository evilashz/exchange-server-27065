using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200043D RID: 1085
	internal sealed class IntPtrArrayTypeInfo : TraceLoggingTypeInfo<IntPtr[]>
	{
		// Token: 0x0600358A RID: 13706 RVA: 0x000CEC06 File Offset: 0x000CCE06
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.FormatPtr(format, Statics.IntPtrType));
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x000CEC1A File Offset: 0x000CCE1A
		public override void WriteData(TraceLoggingDataCollector collector, ref IntPtr[] value)
		{
			collector.AddArray(value);
		}
	}
}
