using System;
using System.Text;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000BA RID: 186
	internal class DataColumnString : DataColumn
	{
		// Token: 0x0600064D RID: 1613 RVA: 0x00019543 File Offset: 0x00017743
		internal DataColumnString(JET_coltyp type, bool fixedSize) : base(type, fixedSize)
		{
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001954D File Offset: 0x0001774D
		internal DataColumnString(JET_coltyp type, bool fixedSize, int size) : base(type, fixedSize, size)
		{
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00019558 File Offset: 0x00017758
		internal override ColumnCache NewCacheCell()
		{
			return new ColumnCacheString();
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00019560 File Offset: 0x00017760
		internal override void ColumnValueToCache(ColumnValue data, ColumnCache cache)
		{
			StringColumnValue stringColumnValue = (StringColumnValue)data;
			((ColumnCache<string>)cache).Value = stringColumnValue.ToString();
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00019588 File Offset: 0x00017788
		internal override byte[] BytesFromCache(ColumnCache cache)
		{
			string value = ((ColumnCache<string>)cache).Value;
			if (string.IsNullOrEmpty(value))
			{
				return DataColumnString.EmptyArray;
			}
			return Encoding.Unicode.GetBytes(value);
		}

		// Token: 0x04000308 RID: 776
		private static readonly byte[] EmptyArray = new byte[0];
	}
}
