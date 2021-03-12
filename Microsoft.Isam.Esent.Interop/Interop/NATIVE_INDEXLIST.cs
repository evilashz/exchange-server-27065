using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200029A RID: 666
	internal struct NATIVE_INDEXLIST
	{
		// Token: 0x0400072D RID: 1837
		public uint cbStruct;

		// Token: 0x0400072E RID: 1838
		public IntPtr tableid;

		// Token: 0x0400072F RID: 1839
		public uint cRecord;

		// Token: 0x04000730 RID: 1840
		public uint columnidindexname;

		// Token: 0x04000731 RID: 1841
		public uint columnidgrbitIndex;

		// Token: 0x04000732 RID: 1842
		public uint columnidcKey;

		// Token: 0x04000733 RID: 1843
		public uint columnidcEntry;

		// Token: 0x04000734 RID: 1844
		public uint columnidcPage;

		// Token: 0x04000735 RID: 1845
		public uint columnidcColumn;

		// Token: 0x04000736 RID: 1846
		public uint columnidiColumn;

		// Token: 0x04000737 RID: 1847
		public uint columnidcolumnid;

		// Token: 0x04000738 RID: 1848
		public uint columnidcoltyp;

		// Token: 0x04000739 RID: 1849
		[Obsolete("Deprecated")]
		public uint columnidCountry;

		// Token: 0x0400073A RID: 1850
		public uint columnidLangid;

		// Token: 0x0400073B RID: 1851
		public uint columnidCp;

		// Token: 0x0400073C RID: 1852
		[Obsolete("Deprecated")]
		public uint columnidCollate;

		// Token: 0x0400073D RID: 1853
		public uint columnidgrbitColumn;

		// Token: 0x0400073E RID: 1854
		public uint columnidcolumnname;

		// Token: 0x0400073F RID: 1855
		public uint columnidLCMapFlags;
	}
}
