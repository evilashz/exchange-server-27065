using System;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000BB RID: 187
	internal class DataColumnByteArray : DataColumn
	{
		// Token: 0x06000653 RID: 1619 RVA: 0x000195C7 File Offset: 0x000177C7
		internal DataColumnByteArray(JET_coltyp type, bool fixedSize) : base(type, fixedSize)
		{
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000195D1 File Offset: 0x000177D1
		internal DataColumnByteArray(JET_coltyp type, bool fixedSize, int size) : base(type, fixedSize, size)
		{
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000195DC File Offset: 0x000177DC
		internal override ColumnCache NewCacheCell()
		{
			return new ColumnCacheByteArray();
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000195E4 File Offset: 0x000177E4
		internal override void ColumnValueToCache(ColumnValue data, ColumnCache cache)
		{
			BytesColumnValue bytesColumnValue = (BytesColumnValue)data;
			((ColumnCache<byte[]>)cache).Value = bytesColumnValue.Value;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00019609 File Offset: 0x00017809
		internal override byte[] BytesFromCache(ColumnCache cache)
		{
			return ((ColumnCache<byte[]>)cache).Value;
		}
	}
}
