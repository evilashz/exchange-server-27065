﻿using System;

namespace Microsoft.Exchange.Security.RightsManagement.StructuredStorage
{
	// Token: 0x02000A23 RID: 2595
	internal enum STGMFlags
	{
		// Token: 0x04002FD2 RID: 12242
		STGM_READ,
		// Token: 0x04002FD3 RID: 12243
		STGM_WRITE,
		// Token: 0x04002FD4 RID: 12244
		STGM_READWRITE,
		// Token: 0x04002FD5 RID: 12245
		STGM_SHARE_DENY_NONE = 64,
		// Token: 0x04002FD6 RID: 12246
		STGM_SHARE_DENY_READ = 48,
		// Token: 0x04002FD7 RID: 12247
		STGM_SHARE_DENY_WRITE = 32,
		// Token: 0x04002FD8 RID: 12248
		STGM_SHARE_EXCLUSIVE = 16,
		// Token: 0x04002FD9 RID: 12249
		STGM_PRIORITY = 262144,
		// Token: 0x04002FDA RID: 12250
		STGM_CREATE = 4096,
		// Token: 0x04002FDB RID: 12251
		STGM_CONVERT = 131072,
		// Token: 0x04002FDC RID: 12252
		STGM_FAILIFTHERE = 0,
		// Token: 0x04002FDD RID: 12253
		STGM_DIRECT = 0,
		// Token: 0x04002FDE RID: 12254
		STGM_TRANSACTED = 65536,
		// Token: 0x04002FDF RID: 12255
		STGM_NOSCRATCH = 1048576,
		// Token: 0x04002FE0 RID: 12256
		STGM_NOSNAPSHOT = 2097152,
		// Token: 0x04002FE1 RID: 12257
		STGM_SIMPLE = 134217728,
		// Token: 0x04002FE2 RID: 12258
		STGM_DIRECT_SWMR = 4194304,
		// Token: 0x04002FE3 RID: 12259
		STGM_DELETEONRELEASE = 67108864,
		// Token: 0x04002FE4 RID: 12260
		StandardCreateFlags = 4114,
		// Token: 0x04002FE5 RID: 12261
		StandardOpenFlags = 16
	}
}
