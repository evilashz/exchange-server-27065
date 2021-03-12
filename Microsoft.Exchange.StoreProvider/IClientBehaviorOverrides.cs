using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200000F RID: 15
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IClientBehaviorOverrides
	{
		// Token: 0x0600003C RID: 60
		bool TryGetClientSpecificOverrides(string clientId, CrossServerBehavior crossServerBehaviorDefaults, out CrossServerBehavior crossServerBehaviorOverrides);
	}
}
