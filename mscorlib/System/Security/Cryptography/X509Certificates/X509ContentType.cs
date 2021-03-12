using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002AC RID: 684
	[ComVisible(true)]
	public enum X509ContentType
	{
		// Token: 0x04000D95 RID: 3477
		Unknown,
		// Token: 0x04000D96 RID: 3478
		Cert,
		// Token: 0x04000D97 RID: 3479
		SerializedCert,
		// Token: 0x04000D98 RID: 3480
		Pfx,
		// Token: 0x04000D99 RID: 3481
		Pkcs12 = 3,
		// Token: 0x04000D9A RID: 3482
		SerializedStore,
		// Token: 0x04000D9B RID: 3483
		Pkcs7,
		// Token: 0x04000D9C RID: 3484
		Authenticode
	}
}
