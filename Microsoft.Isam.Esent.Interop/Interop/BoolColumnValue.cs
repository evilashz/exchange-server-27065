using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000095 RID: 149
	public class BoolColumnValue : ColumnValueOfStruct<bool>
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00010218 File Offset: 0x0000E418
		public override object ValueAsObject
		{
			get
			{
				if (base.Value == null)
				{
					return null;
				}
				if (!base.Value.Value)
				{
					return BoolColumnValue.BoxedFalse;
				}
				return BoolColumnValue.BoxedTrue;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00010252 File Offset: 0x0000E452
		protected override int Size
		{
			[DebuggerStepThrough]
			get
			{
				return 1;
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00010258 File Offset: 0x0000E458
		internal unsafe override int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i)
		{
			byte b = base.Value.GetValueOrDefault() ? byte.MaxValue : 0;
			return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, (void*)(&b), 1, base.Value != null);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000102A0 File Offset: 0x0000E4A0
		protected override void GetValueFromBytes(byte[] value, int startIndex, int count, int err)
		{
			if (1004 == err)
			{
				base.Value = null;
				return;
			}
			base.CheckDataCount(count);
			base.Value = new bool?(BitConverter.ToBoolean(value, startIndex));
		}

		// Token: 0x0400030C RID: 780
		private static readonly object BoxedTrue = true;

		// Token: 0x0400030D RID: 781
		private static readonly object BoxedFalse = false;
	}
}
