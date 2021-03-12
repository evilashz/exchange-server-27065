using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002FA RID: 762
	[CLSCompliant(false)]
	public class UInt64ColumnValue : ColumnValueOfStruct<ulong>
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0001C3E3 File Offset: 0x0001A5E3
		protected override int Size
		{
			[DebuggerStepThrough]
			get
			{
				return 8;
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0001C3E8 File Offset: 0x0001A5E8
		internal unsafe override int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i)
		{
			ulong valueOrDefault = base.Value.GetValueOrDefault();
			return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, (void*)(&valueOrDefault), 8, base.Value != null);
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0001C424 File Offset: 0x0001A624
		protected override void GetValueFromBytes(byte[] value, int startIndex, int count, int err)
		{
			if (1004 == err)
			{
				base.Value = null;
				return;
			}
			base.CheckDataCount(count);
			base.Value = new ulong?(BitConverter.ToUInt64(value, startIndex));
		}
	}
}
