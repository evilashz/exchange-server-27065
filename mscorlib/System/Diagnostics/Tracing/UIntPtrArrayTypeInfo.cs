using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200043E RID: 1086
	internal sealed class UIntPtrArrayTypeInfo : TraceLoggingTypeInfo<UIntPtr[]>
	{
		// Token: 0x0600358D RID: 13709 RVA: 0x000CEC2C File Offset: 0x000CCE2C
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.FormatPtr(format, Statics.UIntPtrType));
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x000CEC40 File Offset: 0x000CCE40
		public override void WriteData(TraceLoggingDataCollector collector, ref UIntPtr[] value)
		{
			collector.AddArray(value);
		}
	}
}
