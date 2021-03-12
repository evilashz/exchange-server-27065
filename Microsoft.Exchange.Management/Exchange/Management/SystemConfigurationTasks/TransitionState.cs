using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008A7 RID: 2215
	public enum TransitionState : short
	{
		// Token: 0x04002E8A RID: 11914
		[LocDescription(Strings.IDs.TransitionStateUnknown)]
		Unknown,
		// Token: 0x04002E8B RID: 11915
		[LocDescription(Strings.IDs.TransitionStateActive)]
		Active,
		// Token: 0x04002E8C RID: 11916
		[LocDescription(Strings.IDs.TransitionStateInactive)]
		Inactive
	}
}
