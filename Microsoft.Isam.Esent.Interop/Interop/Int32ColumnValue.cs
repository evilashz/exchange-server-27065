using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000265 RID: 613
	public class Int32ColumnValue : ColumnValueOfStruct<int>
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00015B3B File Offset: 0x00013D3B
		protected override int Size
		{
			[DebuggerStepThrough]
			get
			{
				return 4;
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00015B40 File Offset: 0x00013D40
		internal unsafe override int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i)
		{
			int valueOrDefault = base.Value.GetValueOrDefault();
			return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, (void*)(&valueOrDefault), 4, base.Value != null);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00015B7C File Offset: 0x00013D7C
		protected override void GetValueFromBytes(byte[] value, int startIndex, int count, int err)
		{
			if (1004 == err)
			{
				base.Value = null;
				return;
			}
			base.CheckDataCount(count);
			base.Value = new int?(BitConverter.ToInt32(value, startIndex));
		}
	}
}
