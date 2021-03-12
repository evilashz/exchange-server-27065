using System;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IAutoProvisionOverrideProvider
	{
		// Token: 0x0600011C RID: 284
		bool TryGetOverrides(string domain, ConnectionType type, out string[] overrideHosts, out bool trustForSendAs);
	}
}
