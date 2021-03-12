﻿using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C78 RID: 3192
	[Flags]
	internal enum SchannelProtocols
	{
		// Token: 0x04003B39 RID: 15161
		Zero = 0,
		// Token: 0x04003B3A RID: 15162
		SP_PROT_PCT1_SERVER = 1,
		// Token: 0x04003B3B RID: 15163
		SP_PROT_PCT1_CLIENT = 2,
		// Token: 0x04003B3C RID: 15164
		SP_PROT_SSL2_SERVER = 4,
		// Token: 0x04003B3D RID: 15165
		SP_PROT_SSL2_CLIENT = 8,
		// Token: 0x04003B3E RID: 15166
		SP_PROT_SSL3_SERVER = 16,
		// Token: 0x04003B3F RID: 15167
		SP_PROT_SSL3_CLIENT = 32,
		// Token: 0x04003B40 RID: 15168
		SP_PROT_TLS1_0_SERVER = 64,
		// Token: 0x04003B41 RID: 15169
		SP_PROT_TLS1_0_CLIENT = 128,
		// Token: 0x04003B42 RID: 15170
		SP_PROT_TLS1_1_SERVER = 256,
		// Token: 0x04003B43 RID: 15171
		SP_PROT_TLS1_1_CLIENT = 512,
		// Token: 0x04003B44 RID: 15172
		SP_PROT_TLS1_2_SERVER = 1024,
		// Token: 0x04003B45 RID: 15173
		SP_PROT_TLS1_2_CLIENT = 2048
	}
}
