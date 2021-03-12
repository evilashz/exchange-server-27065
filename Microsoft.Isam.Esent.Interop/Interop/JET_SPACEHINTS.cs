using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002CE RID: 718
	[Serializable]
	public sealed class JET_SPACEHINTS : IContentEquatable<JET_SPACEHINTS>, IDeepCloneable<JET_SPACEHINTS>
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0001A25D File Offset: 0x0001845D
		// (set) Token: 0x06000D04 RID: 3332 RVA: 0x0001A265 File Offset: 0x00018465
		public int ulInitialDensity
		{
			[DebuggerStepThrough]
			get
			{
				return this.initialDensity;
			}
			set
			{
				this.initialDensity = value;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0001A26E File Offset: 0x0001846E
		// (set) Token: 0x06000D06 RID: 3334 RVA: 0x0001A276 File Offset: 0x00018476
		public int cbInitial
		{
			[DebuggerStepThrough]
			get
			{
				return this.initialSize;
			}
			set
			{
				this.initialSize = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0001A27F File Offset: 0x0001847F
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x0001A287 File Offset: 0x00018487
		public SpaceHintsGrbit grbit
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

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0001A290 File Offset: 0x00018490
		// (set) Token: 0x06000D0A RID: 3338 RVA: 0x0001A298 File Offset: 0x00018498
		public int ulMaintDensity
		{
			[DebuggerStepThrough]
			get
			{
				return this.maintenanceDensity;
			}
			set
			{
				this.maintenanceDensity = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0001A2A1 File Offset: 0x000184A1
		// (set) Token: 0x06000D0C RID: 3340 RVA: 0x0001A2A9 File Offset: 0x000184A9
		public int ulGrowth
		{
			[DebuggerStepThrough]
			get
			{
				return this.growthPercent;
			}
			set
			{
				this.growthPercent = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0001A2B2 File Offset: 0x000184B2
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x0001A2BA File Offset: 0x000184BA
		public int cbMinExtent
		{
			[DebuggerStepThrough]
			get
			{
				return this.minimumExtent;
			}
			set
			{
				this.minimumExtent = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0001A2C3 File Offset: 0x000184C3
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x0001A2CB File Offset: 0x000184CB
		public int cbMaxExtent
		{
			[DebuggerStepThrough]
			get
			{
				return this.maximumExtent;
			}
			set
			{
				this.maximumExtent = value;
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0001A2D4 File Offset: 0x000184D4
		public bool ContentEquals(JET_SPACEHINTS other)
		{
			return other != null && (this.ulInitialDensity == other.ulInitialDensity && this.cbInitial == other.cbInitial && this.grbit == other.grbit && this.ulMaintDensity == other.ulMaintDensity && this.ulGrowth == other.ulGrowth && this.cbMinExtent == other.cbMinExtent) && this.cbMaxExtent == other.cbMaxExtent;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0001A34C File Offset: 0x0001854C
		public JET_SPACEHINTS DeepClone()
		{
			return (JET_SPACEHINTS)base.MemberwiseClone();
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0001A368 File Offset: 0x00018568
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SPACEHINTS({0})", new object[]
			{
				this.grbit
			});
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0001A39C File Offset: 0x0001859C
		internal NATIVE_SPACEHINTS GetNativeSpaceHints()
		{
			return checked(new NATIVE_SPACEHINTS
			{
				cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_SPACEHINTS)),
				ulInitialDensity = (uint)this.ulInitialDensity,
				cbInitial = (uint)this.cbInitial,
				grbit = (uint)this.grbit,
				ulMaintDensity = (uint)this.ulMaintDensity,
				ulGrowth = (uint)this.ulGrowth,
				cbMinExtent = (uint)this.cbMinExtent,
				cbMaxExtent = (uint)this.cbMaxExtent
			});
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0001A42C File Offset: 0x0001862C
		internal void SetFromNativeSpaceHints(NATIVE_SPACEHINTS value)
		{
			checked
			{
				this.ulInitialDensity = (int)value.ulInitialDensity;
				this.cbInitial = (int)value.cbInitial;
				this.grbit = (SpaceHintsGrbit)value.grbit;
				this.ulMaintDensity = (int)value.ulMaintDensity;
				this.ulGrowth = (int)value.ulGrowth;
				this.cbMinExtent = (int)value.cbMinExtent;
				this.cbMaxExtent = (int)value.cbMaxExtent;
			}
		}

		// Token: 0x04000873 RID: 2163
		private int initialDensity;

		// Token: 0x04000874 RID: 2164
		private int initialSize;

		// Token: 0x04000875 RID: 2165
		private SpaceHintsGrbit options;

		// Token: 0x04000876 RID: 2166
		private int maintenanceDensity;

		// Token: 0x04000877 RID: 2167
		private int growthPercent;

		// Token: 0x04000878 RID: 2168
		private int minimumExtent;

		// Token: 0x04000879 RID: 2169
		private int maximumExtent;
	}
}
