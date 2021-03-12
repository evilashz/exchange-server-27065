using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002CB RID: 715
	[Serializable]
	public class JET_SNPROG : IEquatable<JET_SNPROG>
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0001A177 File Offset: 0x00018377
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x0001A17F File Offset: 0x0001837F
		public int cunitDone
		{
			[DebuggerStepThrough]
			get
			{
				return this.completedUnits;
			}
			internal set
			{
				this.completedUnits = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x0001A188 File Offset: 0x00018388
		// (set) Token: 0x06000CFC RID: 3324 RVA: 0x0001A190 File Offset: 0x00018390
		public int cunitTotal
		{
			[DebuggerStepThrough]
			get
			{
				return this.totalUnits;
			}
			internal set
			{
				this.totalUnits = value;
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0001A199 File Offset: 0x00018399
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_SNPROG)obj);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0001A1C0 File Offset: 0x000183C0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SNPROG({0}/{1})", new object[]
			{
				this.cunitDone,
				this.cunitTotal
			});
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0001A200 File Offset: 0x00018400
		public override int GetHashCode()
		{
			return this.cunitDone * 31 ^ this.cunitTotal;
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0001A212 File Offset: 0x00018412
		public bool Equals(JET_SNPROG other)
		{
			return other != null && this.cunitDone == other.cunitDone && this.cunitTotal == other.cunitTotal;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0001A237 File Offset: 0x00018437
		internal void SetFromNative(NATIVE_SNPROG native)
		{
			checked
			{
				this.cunitDone = (int)native.cunitDone;
				this.cunitTotal = (int)native.cunitTotal;
			}
		}

		// Token: 0x04000863 RID: 2147
		private int completedUnits;

		// Token: 0x04000864 RID: 2148
		private int totalUnits;
	}
}
