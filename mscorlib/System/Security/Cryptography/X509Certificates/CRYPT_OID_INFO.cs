using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002AA RID: 682
	internal struct CRYPT_OID_INFO
	{
		// Token: 0x04000D8D RID: 3469
		internal int cbSize;

		// Token: 0x04000D8E RID: 3470
		[MarshalAs(UnmanagedType.LPStr)]
		internal string pszOID;

		// Token: 0x04000D8F RID: 3471
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string pwszName;

		// Token: 0x04000D90 RID: 3472
		internal OidGroup dwGroupId;

		// Token: 0x04000D91 RID: 3473
		internal int AlgId;

		// Token: 0x04000D92 RID: 3474
		internal int cbData;

		// Token: 0x04000D93 RID: 3475
		internal IntPtr pbData;
	}
}
