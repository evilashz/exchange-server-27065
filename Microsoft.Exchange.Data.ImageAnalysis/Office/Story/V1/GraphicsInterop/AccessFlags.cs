﻿using System;

namespace Microsoft.Office.Story.V1.GraphicsInterop
{
	// Token: 0x02000014 RID: 20
	[Flags]
	internal enum AccessFlags
	{
		// Token: 0x04000046 RID: 70
		DELETE = 65536,
		// Token: 0x04000047 RID: 71
		READ_CONTROL = 131072,
		// Token: 0x04000048 RID: 72
		SYNCHRONIZE = 1048576,
		// Token: 0x04000049 RID: 73
		WRITE_DAC = 262144,
		// Token: 0x0400004A RID: 74
		WRITE_OWNER = 524288,
		// Token: 0x0400004B RID: 75
		EVENT_ALL_ACCESS = 2031619,
		// Token: 0x0400004C RID: 76
		EVENT_MODIFY_STATE = 2,
		// Token: 0x0400004D RID: 77
		MUTEX_ALL_ACCESS = 2031617,
		// Token: 0x0400004E RID: 78
		MUTEX_MODIFY_STATE = 1,
		// Token: 0x0400004F RID: 79
		SEMAPHORE_ALL_ACCESS = 2031619,
		// Token: 0x04000050 RID: 80
		SEMAPHORE_MODIFY_STATE = 2,
		// Token: 0x04000051 RID: 81
		TIMER_ALL_ACCESS = 2031619,
		// Token: 0x04000052 RID: 82
		TIMER_MODIFY_STATE = 2,
		// Token: 0x04000053 RID: 83
		TIMER_QUERY_STATE = 1
	}
}