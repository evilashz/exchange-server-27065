using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002F9 RID: 761
	[CLSCompliant(false)]
	public class UInt32ColumnValue : ColumnValueOfStruct<uint>
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0001C35B File Offset: 0x0001A55B
		protected override int Size
		{
			[DebuggerStepThrough]
			get
			{
				return 4;
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0001C360 File Offset: 0x0001A560
		internal unsafe override int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i)
		{
			uint valueOrDefault = base.Value.GetValueOrDefault();
			return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, (void*)(&valueOrDefault), 4, base.Value != null);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0001C39C File Offset: 0x0001A59C
		protected override void GetValueFromBytes(byte[] value, int startIndex, int count, int err)
		{
			if (1004 == err)
			{
				base.Value = null;
				return;
			}
			base.CheckDataCount(count);
			base.Value = new uint?(BitConverter.ToUInt32(value, startIndex));
		}
	}
}
