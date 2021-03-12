using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x02000026 RID: 38
	internal class EnumFormatter<T>
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00005A28 File Offset: 0x00003C28
		public EnumFormatter(string format, Func<T, int> converterToInt32)
		{
			EnumFormatter<T> <>4__this = this;
			this.converterToInt32 = converterToInt32;
			Type typeFromHandle = typeof(T);
			Attribute customAttribute = Attribute.GetCustomAttribute(typeFromHandle, typeof(FlagsAttribute));
			if (customAttribute != null)
			{
				throw new ArgumentException("Flags enums are not supported.", typeFromHandle.ToString());
			}
			T[] source = (T[])Enum.GetValues(typeFromHandle);
			IEnumerable<int> source2 = from v in source
			select this.converterToInt32(v);
			this.minValue = source2.Min<int>();
			int num = source2.Max<int>() - this.minValue + 1;
			if ((long)num > 1000L)
			{
				throw new ArgumentException("Enum's value range is not supported.", typeFromHandle.ToString());
			}
			this.indexLookup = new int[num];
			int[] array = source2.ToArray<int>();
			for (int i = 0; i < array.Length; i++)
			{
				this.indexLookup[array[i] - this.minValue] = i;
			}
			this.names = (from v in Enum.GetNames(typeFromHandle)
			select string.Format(format, v)).ToArray<string>();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005B57 File Offset: 0x00003D57
		public string Format(T value)
		{
			return this.names[this.GetIndex(value)];
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005B67 File Offset: 0x00003D67
		public EnumFormatter<T> OverrideFormat(T value, string format)
		{
			this.names[this.GetIndex(value)] = format;
			return this;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005B7C File Offset: 0x00003D7C
		[Conditional("DEBUG")]
		private void Validate(string format)
		{
			foreach (T t in (T[])Enum.GetValues(typeof(T)))
			{
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005BB4 File Offset: 0x00003DB4
		private int GetIndex(T value)
		{
			return this.indexLookup[this.converterToInt32(value) - this.minValue];
		}

		// Token: 0x04000135 RID: 309
		private const uint MaxSupportedRange = 1000U;

		// Token: 0x04000136 RID: 310
		private readonly string[] names;

		// Token: 0x04000137 RID: 311
		private readonly int[] indexLookup;

		// Token: 0x04000138 RID: 312
		private readonly int minValue;

		// Token: 0x04000139 RID: 313
		private readonly Func<T, int> converterToInt32;
	}
}
