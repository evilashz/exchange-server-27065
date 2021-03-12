using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000445 RID: 1093
	internal sealed class EnumUInt16TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060035A5 RID: 13733 RVA: 0x000CED48 File Offset: 0x000CCF48
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000CED58 File Offset: 0x000CCF58
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<ushort>.Cast<EnumType>(value));
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000CED6B File Offset: 0x000CCF6B
		public override object GetData(object value)
		{
			return value;
		}
	}
}
