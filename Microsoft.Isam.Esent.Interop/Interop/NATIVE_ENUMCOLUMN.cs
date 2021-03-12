using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000281 RID: 641
	internal struct NATIVE_ENUMCOLUMN
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00016E81 File Offset: 0x00015081
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x00016E89 File Offset: 0x00015089
		public uint cEnumColumnValue
		{
			get
			{
				return this.cbData;
			}
			set
			{
				this.cbData = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00016E92 File Offset: 0x00015092
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x00016E9F File Offset: 0x0001509F
		public unsafe NATIVE_ENUMCOLUMNVALUE* rgEnumColumnValue
		{
			get
			{
				return (NATIVE_ENUMCOLUMNVALUE*)((void*)this.pvData);
			}
			set
			{
				this.pvData = new IntPtr((void*)value);
			}
		}

		// Token: 0x04000526 RID: 1318
		public uint columnid;

		// Token: 0x04000527 RID: 1319
		public int err;

		// Token: 0x04000528 RID: 1320
		public uint cbData;

		// Token: 0x04000529 RID: 1321
		public IntPtr pvData;
	}
}
