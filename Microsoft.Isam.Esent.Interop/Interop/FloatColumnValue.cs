using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000226 RID: 550
	public class FloatColumnValue : ColumnValueOfStruct<float>
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00015165 File Offset: 0x00013365
		protected override int Size
		{
			[DebuggerStepThrough]
			get
			{
				return 4;
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00015168 File Offset: 0x00013368
		internal unsafe override int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i)
		{
			float valueOrDefault = base.Value.GetValueOrDefault();
			return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, (void*)(&valueOrDefault), 4, base.Value != null);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x000151A4 File Offset: 0x000133A4
		protected override void GetValueFromBytes(byte[] value, int startIndex, int count, int err)
		{
			if (1004 == err)
			{
				base.Value = null;
				return;
			}
			base.CheckDataCount(count);
			base.Value = new float?(BitConverter.ToSingle(value, startIndex));
		}
	}
}
