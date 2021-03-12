using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000264 RID: 612
	public class Int16ColumnValue : ColumnValueOfStruct<short>
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00015AB4 File Offset: 0x00013CB4
		protected override int Size
		{
			[DebuggerStepThrough]
			get
			{
				return 2;
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00015AB8 File Offset: 0x00013CB8
		internal unsafe override int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i)
		{
			short valueOrDefault = base.Value.GetValueOrDefault();
			return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, (void*)(&valueOrDefault), 2, base.Value != null);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00015AF4 File Offset: 0x00013CF4
		protected override void GetValueFromBytes(byte[] value, int startIndex, int count, int err)
		{
			if (1004 == err)
			{
				base.Value = null;
				return;
			}
			base.CheckDataCount(count);
			base.Value = new short?(BitConverter.ToInt16(value, startIndex));
		}
	}
}
