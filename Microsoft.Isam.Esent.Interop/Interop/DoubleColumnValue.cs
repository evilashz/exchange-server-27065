using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000A2 RID: 162
	public class DoubleColumnValue : ColumnValueOfStruct<double>
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x000110BA File Offset: 0x0000F2BA
		protected override int Size
		{
			[DebuggerStepThrough]
			get
			{
				return 8;
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000110C0 File Offset: 0x0000F2C0
		internal unsafe override int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i)
		{
			double valueOrDefault = base.Value.GetValueOrDefault();
			return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, (void*)(&valueOrDefault), 8, base.Value != null);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000110FC File Offset: 0x0000F2FC
		protected override void GetValueFromBytes(byte[] value, int startIndex, int count, int err)
		{
			if (1004 == err)
			{
				base.Value = null;
				return;
			}
			base.CheckDataCount(count);
			base.Value = new double?(BitConverter.ToDouble(value, startIndex));
		}
	}
}
