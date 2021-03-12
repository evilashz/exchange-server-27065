using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044A RID: 1098
	internal sealed class StringTypeInfo : TraceLoggingTypeInfo<string>
	{
		// Token: 0x060035B9 RID: 13753 RVA: 0x000CEE30 File Offset: 0x000CD030
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddBinary(name, Statics.MakeDataType(TraceLoggingDataType.CountedUtf16String, format));
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000CEE41 File Offset: 0x000CD041
		public override void WriteData(TraceLoggingDataCollector collector, ref string value)
		{
			collector.AddBinary(value);
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000CEE4C File Offset: 0x000CD04C
		public override object GetData(object value)
		{
			object obj = base.GetData(value);
			if (obj == null)
			{
				obj = "";
			}
			return obj;
		}
	}
}
