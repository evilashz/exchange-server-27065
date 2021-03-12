using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000098 RID: 152
	public class BytesColumnValue : ColumnValue
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x000103F3 File Offset: 0x0000E5F3
		public override object ValueAsObject
		{
			[DebuggerStepThrough]
			get
			{
				return this.Value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x000103FB File Offset: 0x0000E5FB
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x00010403 File Offset: 0x0000E603
		public byte[] Value { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001040C File Offset: 0x0000E60C
		public override int Length
		{
			get
			{
				if (this.Value == null)
				{
					return 0;
				}
				return this.Value.Length;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00010420 File Offset: 0x0000E620
		protected override int Size
		{
			[DebuggerStepThrough]
			get
			{
				return 0;
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00010423 File Offset: 0x0000E623
		public override string ToString()
		{
			if (this.Value == null)
			{
				return string.Empty;
			}
			return BitConverter.ToString(this.Value, 0, Math.Min(this.Value.Length, 16));
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00010450 File Offset: 0x0000E650
		internal unsafe override int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i)
		{
			if (this.Value != null)
			{
				fixed (IntPtr* value = this.Value)
				{
					return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, (void*)value, this.Value.Length, true);
				}
			}
			return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, null, 0, false);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000104AF File Offset: 0x0000E6AF
		protected override void GetValueFromBytes(byte[] value, int startIndex, int count, int err)
		{
			if (1004 == err)
			{
				this.Value = null;
				return;
			}
			this.Value = new byte[count];
			Array.Copy(value, startIndex, this.Value, 0, count);
		}
	}
}
