using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200040D RID: 1037
	internal sealed class ArrayTypeInfo<ElementType> : TraceLoggingTypeInfo<ElementType[]>
	{
		// Token: 0x060034B9 RID: 13497 RVA: 0x000CD1DB File Offset: 0x000CB3DB
		public ArrayTypeInfo(TraceLoggingTypeInfo<ElementType> elementInfo)
		{
			this.elementInfo = elementInfo;
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000CD1EA File Offset: 0x000CB3EA
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.BeginBufferedArray();
			this.elementInfo.WriteMetadata(collector, name, format);
			collector.EndBufferedArray();
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000CD208 File Offset: 0x000CB408
		public override void WriteData(TraceLoggingDataCollector collector, ref ElementType[] value)
		{
			int bookmark = collector.BeginBufferedArray();
			int count = 0;
			if (value != null)
			{
				count = value.Length;
				for (int i = 0; i < value.Length; i++)
				{
					this.elementInfo.WriteData(collector, ref value[i]);
				}
			}
			collector.EndBufferedArray(bookmark, count);
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000CD254 File Offset: 0x000CB454
		public override object GetData(object value)
		{
			ElementType[] array = (ElementType[])value;
			object[] array2 = new object[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this.elementInfo.GetData(array[i]);
			}
			return array2;
		}

		// Token: 0x04001752 RID: 5970
		private readonly TraceLoggingTypeInfo<ElementType> elementInfo;
	}
}
