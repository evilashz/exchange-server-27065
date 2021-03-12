using System;
using Microsoft.Exchange.Diagnostics.Components.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001A1 RID: 417
	public class CsvClusteredIndex
	{
		// Token: 0x06000B9A RID: 2970 RVA: 0x0002A34A File Offset: 0x0002854A
		public CsvClusteredIndex(int idx)
		{
			ExTraceGlobals.CommonTracer.TraceDebug<int>((long)this.GetHashCode(), "CsvClusteredIndex is constructed for idex {0}", idx);
			this.idx = idx;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002A370 File Offset: 0x00028570
		public bool Find(IComparable key, CsvFieldCache cursor)
		{
			ExTraceGlobals.CommonTracer.TraceDebug((long)this.GetHashCode(), "CsvClusteredIndex Find");
			long position = cursor.Position;
			long num = cursor.Length;
			bool flag = false;
			while (position <= num - (long)cursor.ReadSize)
			{
				long num2 = cursor.Seek((num - position) / 2L + position);
				if (!cursor.MoveNext(false))
				{
					num = num2;
				}
				else
				{
					object obj = null;
					while (obj == null && cursor.MoveNext(false))
					{
						if (cursor.FieldCount != cursor.SchemaFieldCount)
						{
							flag = true;
						}
						obj = cursor.GetField(this.idx);
					}
					if (obj == null)
					{
						num = num2;
					}
					else if (key.CompareTo(obj) > 0)
					{
						position = cursor.Position;
					}
					else
					{
						num = num2;
					}
				}
			}
			cursor.Seek(position);
			bool flag2 = this.Scan(key, cursor);
			return flag || flag2;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002A43C File Offset: 0x0002863C
		private bool Scan(IComparable key, CsvFieldCache cursor)
		{
			ExTraceGlobals.CommonTracer.TraceDebug((long)this.GetHashCode(), "CsvClusteredIndex Scan");
			bool result = false;
			while (cursor.MoveNext(false))
			{
				if (cursor.FieldCount != cursor.SchemaFieldCount)
				{
					result = true;
				}
				object field = cursor.GetField(this.idx);
				if (field != null && key.CompareTo(field) <= 0)
				{
					break;
				}
			}
			return result;
		}

		// Token: 0x0400086F RID: 2159
		private int idx;
	}
}
