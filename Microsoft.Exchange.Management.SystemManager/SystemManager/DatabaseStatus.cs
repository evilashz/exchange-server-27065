using System;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000092 RID: 146
	public enum DatabaseStatus
	{
		// Token: 0x04000194 RID: 404
		[LocDescription(Strings.IDs.DatabaseStatusUnknown)]
		Unknown,
		// Token: 0x04000195 RID: 405
		[LocDescription(Strings.IDs.DatabaseStatusMounted)]
		Mounted,
		// Token: 0x04000196 RID: 406
		[LocDescription(Strings.IDs.DatabaseStatusDismounted)]
		Dismounted
	}
}
