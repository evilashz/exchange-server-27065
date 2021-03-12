using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000272 RID: 626
	[Serializable]
	public sealed class JET_COLUMNCREATE : IContentEquatable<JET_COLUMNCREATE>, IDeepCloneable<JET_COLUMNCREATE>
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x000164B5 File Offset: 0x000146B5
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x000164BD File Offset: 0x000146BD
		public string szColumnName
		{
			[DebuggerStepThrough]
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x000164C6 File Offset: 0x000146C6
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x000164CE File Offset: 0x000146CE
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

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x000164D7 File Offset: 0x000146D7
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x000164DF File Offset: 0x000146DF
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

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x000164E8 File Offset: 0x000146E8
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x000164F0 File Offset: 0x000146F0
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

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x000164F9 File Offset: 0x000146F9
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x00016501 File Offset: 0x00014701
		public byte[] pvDefault
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x0001650A File Offset: 0x0001470A
		// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x00016512 File Offset: 0x00014712
		public int cbDefault
		{
			get
			{
				return this.defaultValueSize;
			}
			set
			{
				this.defaultValueSize = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0001651B File Offset: 0x0001471B
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x00016523 File Offset: 0x00014723
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

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0001652C File Offset: 0x0001472C
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x00016534 File Offset: 0x00014734
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

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0001653D File Offset: 0x0001473D
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x00016545 File Offset: 0x00014745
		public JET_err err
		{
			[DebuggerStepThrough]
			get
			{
				return this.errorCode;
			}
			set
			{
				this.errorCode = value;
			}
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00016550 File Offset: 0x00014750
		public bool ContentEquals(JET_COLUMNCREATE other)
		{
			if (other == null)
			{
				return false;
			}
			this.CheckMembersAreValid();
			other.CheckMembersAreValid();
			return this.err == other.err && this.szColumnName == other.szColumnName && this.coltyp == other.coltyp && this.cbMax == other.cbMax && this.grbit == other.grbit && this.cbDefault == other.cbDefault && this.cp == other.cp && this.columnid == other.columnid && Util.ArrayEqual(this.pvDefault, other.pvDefault, 0, other.cbDefault);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00016608 File Offset: 0x00014808
		public JET_COLUMNCREATE DeepClone()
		{
			JET_COLUMNCREATE jet_COLUMNCREATE = (JET_COLUMNCREATE)base.MemberwiseClone();
			if (this.pvDefault != null)
			{
				jet_COLUMNCREATE.pvDefault = new byte[this.pvDefault.Length];
				Array.Copy(this.pvDefault, jet_COLUMNCREATE.pvDefault, this.pvDefault.Length);
			}
			return jet_COLUMNCREATE;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00016658 File Offset: 0x00014858
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_COLUMNCREATE({0},{1},{2})", new object[]
			{
				this.szColumnName,
				this.coltyp,
				this.grbit
			});
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000166A4 File Offset: 0x000148A4
		internal void CheckMembersAreValid()
		{
			if (this.szColumnName == null)
			{
				throw new ArgumentNullException("szColumnName");
			}
			if (this.cbDefault < 0)
			{
				throw new ArgumentOutOfRangeException("cbDefault", this.cbDefault, "cannot be negative");
			}
			if (this.pvDefault == null && this.cbDefault != 0)
			{
				throw new ArgumentOutOfRangeException("cbDefault", this.cbDefault, "must be 0");
			}
			if (this.pvDefault != null && this.cbDefault > this.pvDefault.Length)
			{
				throw new ArgumentOutOfRangeException("cbDefault", this.cbDefault, "can't be greater than pvDefault.Length");
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00016748 File Offset: 0x00014948
		internal NATIVE_COLUMNCREATE GetNativeColumnCreate()
		{
			return checked(new NATIVE_COLUMNCREATE
			{
				cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_COLUMNCREATE)),
				szColumnName = IntPtr.Zero,
				coltyp = (uint)this.coltyp,
				cbMax = (uint)this.cbMax,
				grbit = (uint)this.grbit,
				pvDefault = IntPtr.Zero,
				cbDefault = (uint)this.cbDefault,
				cp = (uint)this.cp
			});
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000167D0 File Offset: 0x000149D0
		internal void SetFromNativeColumnCreate(NATIVE_COLUMNCREATE value)
		{
			this.columnid = new JET_COLUMNID
			{
				Value = value.columnid
			};
			this.err = (JET_err)value.err;
		}

		// Token: 0x040004AB RID: 1195
		private string name;

		// Token: 0x040004AC RID: 1196
		private JET_coltyp columnType;

		// Token: 0x040004AD RID: 1197
		private int maxSize;

		// Token: 0x040004AE RID: 1198
		private ColumndefGrbit options;

		// Token: 0x040004AF RID: 1199
		private byte[] defaultValue;

		// Token: 0x040004B0 RID: 1200
		private int defaultValueSize;

		// Token: 0x040004B1 RID: 1201
		private JET_CP codePage;

		// Token: 0x040004B2 RID: 1202
		[NonSerialized]
		private JET_COLUMNID id;

		// Token: 0x040004B3 RID: 1203
		private JET_err errorCode;
	}
}
