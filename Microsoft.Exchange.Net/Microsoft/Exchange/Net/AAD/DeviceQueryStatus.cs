using System;

namespace Microsoft.Exchange.Net.AAD
{
	// Token: 0x02000587 RID: 1415
	public enum DeviceQueryStatus
	{
		// Token: 0x04001874 RID: 6260
		Unknown,
		// Token: 0x04001875 RID: 6261
		Success,
		// Token: 0x04001876 RID: 6262
		DeviceNotFound,
		// Token: 0x04001877 RID: 6263
		DeviceNotManaged,
		// Token: 0x04001878 RID: 6264
		DeviceNotCompliant,
		// Token: 0x04001879 RID: 6265
		DeviceNotEnabled,
		// Token: 0x0400187A RID: 6266
		PolicyEvaluationFailure
	}
}
