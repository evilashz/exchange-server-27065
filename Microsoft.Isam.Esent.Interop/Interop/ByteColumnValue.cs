using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000097 RID: 151
	public class ByteColumnValue : ColumnValueOfStruct<byte>
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001036F File Offset: 0x0000E56F
		protected override int Size
		{
			[DebuggerStepThrough]
			get
			{
				return 1;
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00010374 File Offset: 0x0000E574
		internal unsafe override int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i)
		{
			byte valueOrDefault = base.Value.GetValueOrDefault();
			return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, (void*)(&valueOrDefault), 1, base.Value != null);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000103B0 File Offset: 0x0000E5B0
		protected override void GetValueFromBytes(byte[] value, int startIndex, int count, int err)
		{
			if (1004 == err)
			{
				base.Value = null;
				return;
			}
			base.CheckDataCount(count);
			base.Value = new byte?(value[startIndex]);
		}
	}
}
