using System;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000023 RID: 35
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AutoProvisionOverrideProvider : IAutoProvisionOverrideProvider
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00006902 File Offset: 0x00004B02
		public bool TryGetOverrides(string domain, ConnectionType type, out string[] overrideHosts, out bool trustForSendAs)
		{
			return AutoProvisionOverride.TryGetOverrides(domain, type, out overrideHosts, out trustForSendAs);
		}
	}
}
