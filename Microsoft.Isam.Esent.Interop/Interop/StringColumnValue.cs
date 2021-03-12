using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002E7 RID: 743
	public class StringColumnValue : ColumnValue
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0001B2A9 File Offset: 0x000194A9
		public override object ValueAsObject
		{
			[DebuggerStepThrough]
			get
			{
				return this.Value;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0001B2B1 File Offset: 0x000194B1
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x0001B2B9 File Offset: 0x000194B9
		public string Value { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0001B2C2 File Offset: 0x000194C2
		public override int Length
		{
			get
			{
				if (this.Value == null)
				{
					return 0;
				}
				return this.Value.Length * 2;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x0001B2DB File Offset: 0x000194DB
		protected override int Size
		{
			[DebuggerStepThrough]
			get
			{
				return 0;
			}
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0001B2DE File Offset: 0x000194DE
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0001B2E8 File Offset: 0x000194E8
		internal unsafe override int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i)
		{
			if (this.Value != null)
			{
				fixed (void* value = this.Value)
				{
					return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, value, checked(this.Value.Length * 2), true);
				}
			}
			return base.SetColumns(sesid, tableid, columnValues, nativeColumns, i, null, 0, false);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0001B342 File Offset: 0x00019542
		protected override void GetValueFromBytes(byte[] value, int startIndex, int count, int err)
		{
			if (1004 == err)
			{
				this.Value = null;
				return;
			}
			this.Value = StringCache.GetString(value, startIndex, count);
		}
	}
}
