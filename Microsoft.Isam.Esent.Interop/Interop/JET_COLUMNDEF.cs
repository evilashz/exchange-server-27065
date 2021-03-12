using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000274 RID: 628
	[Serializable]
	public sealed class JET_COLUMNDEF : IContentEquatable<JET_COLUMNDEF>, IDeepCloneable<JET_COLUMNDEF>
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0001680F File Offset: 0x00014A0F
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00016817 File Offset: 0x00014A17
		public JET_coltyp coltyp
		{
			[DebuggerStepThrough]
			get
			{
				return this.columnType;
			}
			set
			{
				this.columnType = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00016820 File Offset: 0x00014A20
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00016828 File Offset: 0x00014A28
		public JET_CP cp
		{
			[DebuggerStepThrough]
			get
			{
				return this.codePage;
			}
			set
			{
				this.codePage = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00016831 File Offset: 0x00014A31
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x00016839 File Offset: 0x00014A39
		public int cbMax
		{
			[DebuggerStepThrough]
			get
			{
				return this.maxSize;
			}
			set
			{
				this.maxSize = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00016842 File Offset: 0x00014A42
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x0001684A File Offset: 0x00014A4A
		public ColumndefGrbit grbit
		{
			[DebuggerStepThrough]
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00016853 File Offset: 0x00014A53
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x0001685B File Offset: 0x00014A5B
		public JET_COLUMNID columnid
		{
			[DebuggerStepThrough]
			get
			{
				return this.id;
			}
			internal set
			{
				this.id = value;
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00016864 File Offset: 0x00014A64
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_COLUMNDEF({0},{1})", new object[]
			{
				this.columnType,
				this.options
			});
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000168A4 File Offset: 0x00014AA4
		public bool ContentEquals(JET_COLUMNDEF other)
		{
			return other != null && (this.columnType == other.columnType && this.codePage == other.codePage && this.maxSize == other.maxSize && this.id == other.id) && this.options == other.options;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00016903 File Offset: 0x00014B03
		public JET_COLUMNDEF DeepClone()
		{
			return (JET_COLUMNDEF)base.MemberwiseClone();
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00016910 File Offset: 0x00014B10
		internal NATIVE_COLUMNDEF GetNativeColumndef()
		{
			return checked(new NATIVE_COLUMNDEF
			{
				cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_COLUMNDEF)),
				cp = unchecked((ushort)this.cp),
				cbMax = (uint)this.cbMax,
				grbit = (uint)this.grbit,
				coltyp = (uint)this.coltyp
			});
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00016974 File Offset: 0x00014B74
		internal void SetFromNativeColumndef(NATIVE_COLUMNDEF value)
		{
			this.coltyp = (JET_coltyp)value.coltyp;
			this.cp = (JET_CP)value.cp;
			this.cbMax = checked((int)value.cbMax);
			this.grbit = (ColumndefGrbit)value.grbit;
			this.columnid = new JET_COLUMNID
			{
				Value = value.columnid
			};
		}

		// Token: 0x040004BD RID: 1213
		private JET_coltyp columnType;

		// Token: 0x040004BE RID: 1214
		private JET_CP codePage;

		// Token: 0x040004BF RID: 1215
		private int maxSize;

		// Token: 0x040004C0 RID: 1216
		[NonSerialized]
		private JET_COLUMNID id;

		// Token: 0x040004C1 RID: 1217
		private ColumndefGrbit options;
	}
}
