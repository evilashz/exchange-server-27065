﻿using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000240 RID: 576
	[ComVisible(true)]
	[Serializable]
	public enum CipherMode
	{
		// Token: 0x04000BD3 RID: 3027
		CBC = 1,
		// Token: 0x04000BD4 RID: 3028
		ECB,
		// Token: 0x04000BD5 RID: 3029
		OFB,
		// Token: 0x04000BD6 RID: 3030
		CFB,
		// Token: 0x04000BD7 RID: 3031
		CTS
	}
}
