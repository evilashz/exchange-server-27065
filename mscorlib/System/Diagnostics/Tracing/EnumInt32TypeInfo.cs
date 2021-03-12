using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000446 RID: 1094
	internal sealed class EnumInt32TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060035A9 RID: 13737 RVA: 0x000CED76 File Offset: 0x000CCF76
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000CED86 File Offset: 0x000CCF86
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<int>.Cast<EnumType>(value));
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000CED99 File Offset: 0x000CCF99
		public override object GetData(object value)
		{
			return value;
		}
	}
}
