using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200025F RID: 607
	public class GuidColumnValue : ColumnValueOfStruct<Guid>
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x000152BA File Offset: 0x000134BA
		protected override int Size
		{
			[DebuggerStepThrough]
			get
			{
				return 16;
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000152C0 File Offset: 0x000134C0
		internal unsafe override int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i)
		{
			Guid valueOrDefault = base.Value.GetValueOrDefault();
			return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, (void*)(&valueOrDefault), this.Size, base.Value != null);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00015300 File Offset: 0x00013500
		protected unsafe override void GetValueFromBytes(byte[] value, int startIndex, int count, int err)
		{
			if (1004 == err)
			{
				base.Value = null;
				return;
			}
			base.CheckDataCount(count);
			Guid value2;
			void* ptr = (void*)(&value2);
			byte* ptr2 = (byte*)ptr;
			for (int i = 0; i < this.Size; i++)
			{
				ptr2[i] = value[startIndex + i];
			}
			base.Value = new Guid?(value2);
		}
	}
}
