using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200027A RID: 634
	[Serializable]
	public sealed class JET_CONDITIONALCOLUMN : IContentEquatable<JET_CONDITIONALCOLUMN>, IDeepCloneable<JET_CONDITIONALCOLUMN>
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00016DA0 File Offset: 0x00014FA0
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x00016DA8 File Offset: 0x00014FA8
		public string szColumnName
		{
			[DebuggerStepThrough]
			get
			{
				return this.columnName;
			}
			set
			{
				this.columnName = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00016DB1 File Offset: 0x00014FB1
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x00016DB9 File Offset: 0x00014FB9
		public ConditionalColumnGrbit grbit
		{
			[DebuggerStepThrough]
			get
			{
				return this.option;
			}
			set
			{
				this.option = value;
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00016DC2 File Offset: 0x00014FC2
		public JET_CONDITIONALCOLUMN DeepClone()
		{
			return (JET_CONDITIONALCOLUMN)base.MemberwiseClone();
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00016DD0 File Offset: 0x00014FD0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_CONDITIONALCOLUMN({0}:{1})", new object[]
			{
				this.columnName,
				this.option
			});
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00016E0B File Offset: 0x0001500B
		public bool ContentEquals(JET_CONDITIONALCOLUMN other)
		{
			return other != null && this.columnName == other.columnName && this.option == other.option;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00016E38 File Offset: 0x00015038
		internal NATIVE_CONDITIONALCOLUMN GetNativeConditionalColumn()
		{
			return new NATIVE_CONDITIONALCOLUMN
			{
				cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_CONDITIONALCOLUMN)),
				grbit = (uint)this.grbit
			};
		}

		// Token: 0x040004E5 RID: 1253
		private string columnName;

		// Token: 0x040004E6 RID: 1254
		private ConditionalColumnGrbit option;
	}
}
