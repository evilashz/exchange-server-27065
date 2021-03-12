using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000412 RID: 1042
	internal sealed class EnumerableTypeInfo<IterableType, ElementType> : TraceLoggingTypeInfo<IterableType> where IterableType : IEnumerable<!1>
	{
		// Token: 0x060034D3 RID: 13523 RVA: 0x000CD83D File Offset: 0x000CBA3D
		public EnumerableTypeInfo(TraceLoggingTypeInfo<ElementType> elementInfo)
		{
			this.elementInfo = elementInfo;
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000CD84C File Offset: 0x000CBA4C
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.BeginBufferedArray();
			this.elementInfo.WriteMetadata(collector, name, format);
			collector.EndBufferedArray();
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000CD868 File Offset: 0x000CBA68
		public override void WriteData(TraceLoggingDataCollector collector, ref IterableType value)
		{
			int bookmark = collector.BeginBufferedArray();
			int num = 0;
			if (value != null)
			{
				foreach (ElementType elementType in value)
				{
					ElementType elementType2 = elementType;
					this.elementInfo.WriteData(collector, ref elementType2);
					num++;
				}
			}
			collector.EndBufferedArray(bookmark, num);
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000CD8E4 File Offset: 0x000CBAE4
		public override object GetData(object value)
		{
			IterableType iterableType = (IterableType)((object)value);
			List<object> list = new List<object>();
			foreach (ElementType elementType in iterableType)
			{
				list.Add(this.elementInfo.GetData(elementType));
			}
			return list.ToArray();
		}

		// Token: 0x04001760 RID: 5984
		private readonly TraceLoggingTypeInfo<ElementType> elementInfo;
	}
}
