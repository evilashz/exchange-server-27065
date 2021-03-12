using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000A1 RID: 161
	public class DateTimeColumnValue : ColumnValueOfStruct<DateTime>
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x00011024 File Offset: 0x0000F224
		protected override int Size
		{
			[DebuggerStepThrough]
			get
			{
				return 8;
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00011028 File Offset: 0x0000F228
		internal unsafe override int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i)
		{
			double num = base.Value.GetValueOrDefault().ToOADate();
			return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, (void*)(&num), 8, base.Value != null);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001106C File Offset: 0x0000F26C
		protected override void GetValueFromBytes(byte[] value, int startIndex, int count, int err)
		{
			if (1004 == err)
			{
				base.Value = null;
				return;
			}
			base.CheckDataCount(count);
			double d = BitConverter.ToDouble(value, startIndex);
			base.Value = new DateTime?(Conversions.ConvertDoubleToDateTime(d));
		}
	}
}
