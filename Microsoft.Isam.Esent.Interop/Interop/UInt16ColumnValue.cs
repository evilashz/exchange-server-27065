using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002F8 RID: 760
	[CLSCompliant(false)]
	public class UInt16ColumnValue : ColumnValueOfStruct<ushort>
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0001C2D2 File Offset: 0x0001A4D2
		protected override int Size
		{
			[DebuggerStepThrough]
			get
			{
				return 2;
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0001C2D8 File Offset: 0x0001A4D8
		internal unsafe override int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i)
		{
			ushort valueOrDefault = base.Value.GetValueOrDefault();
			return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, (void*)(&valueOrDefault), 2, base.Value != null);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0001C314 File Offset: 0x0001A514
		protected override void GetValueFromBytes(byte[] value, int startIndex, int count, int err)
		{
			if (1004 == err)
			{
				base.Value = null;
				return;
			}
			base.CheckDataCount(count);
			base.Value = new ushort?(BitConverter.ToUInt16(value, startIndex));
		}
	}
}
