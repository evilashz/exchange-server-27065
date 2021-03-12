using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000435 RID: 1077
	internal sealed class ByteArrayTypeInfo : TraceLoggingTypeInfo<byte[]>
	{
		// Token: 0x06003572 RID: 13682 RVA: 0x000CEA8C File Offset: 0x000CCC8C
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			switch (format)
			{
			case EventFieldFormat.String:
				collector.AddBinary(name, TraceLoggingDataType.CountedMbcsString);
				return;
			case EventFieldFormat.Boolean:
				collector.AddArray(name, TraceLoggingDataType.Boolean8);
				return;
			case EventFieldFormat.Hexadecimal:
				collector.AddArray(name, TraceLoggingDataType.HexInt8);
				return;
			default:
				if (format == EventFieldFormat.Xml)
				{
					collector.AddBinary(name, TraceLoggingDataType.CountedMbcsXml);
					return;
				}
				if (format != EventFieldFormat.Json)
				{
					collector.AddBinary(name, Statics.MakeDataType(TraceLoggingDataType.Binary, format));
					return;
				}
				collector.AddBinary(name, TraceLoggingDataType.CountedMbcsJson);
				return;
			}
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000CEB04 File Offset: 0x000CCD04
		public override void WriteData(TraceLoggingDataCollector collector, ref byte[] value)
		{
			collector.AddBinary(value);
		}
	}
}
