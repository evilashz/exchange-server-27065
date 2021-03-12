using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000266 RID: 614
	public class Int64ColumnValue : ColumnValueOfStruct<long>
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00015BC3 File Offset: 0x00013DC3
		protected override int Size
		{
			[DebuggerStepThrough]
			get
			{
				return 8;
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00015BC8 File Offset: 0x00013DC8
		internal unsafe override int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i)
		{
			long valueOrDefault = base.Value.GetValueOrDefault();
			return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, (void*)(&valueOrDefault), 8, base.Value != null);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00015C04 File Offset: 0x00013E04
		protected override void GetValueFromBytes(byte[] value, int startIndex, int count, int err)
		{
			if (1004 == err)
			{
				base.Value = null;
				return;
			}
			base.CheckDataCount(count);
			base.Value = new long?(BitConverter.ToInt64(value, startIndex));
		}
	}
}
