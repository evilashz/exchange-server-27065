using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002A4 RID: 676
	internal struct NATIVE_OBJECTLIST
	{
		// Token: 0x04000774 RID: 1908
		public uint cbStruct;

		// Token: 0x04000775 RID: 1909
		public IntPtr tableid;

		// Token: 0x04000776 RID: 1910
		public uint cRecord;

		// Token: 0x04000777 RID: 1911
		public uint columnidcontainername;

		// Token: 0x04000778 RID: 1912
		public uint columnidobjectname;

		// Token: 0x04000779 RID: 1913
		public uint columnidobjtyp;

		// Token: 0x0400077A RID: 1914
		[Obsolete("Unused member")]
		public uint columniddtCreate;

		// Token: 0x0400077B RID: 1915
		[Obsolete("Unused member")]
		public uint columniddtUpdate;

		// Token: 0x0400077C RID: 1916
		public uint columnidgrbit;

		// Token: 0x0400077D RID: 1917
		public uint columnidflags;

		// Token: 0x0400077E RID: 1918
		public uint columnidcRecord;

		// Token: 0x0400077F RID: 1919
		public uint columnidcPage;
	}
}
