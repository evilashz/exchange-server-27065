using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.Implementation)]
	public interface IServerCapabilities
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000067 RID: 103
		IEnumerable<string> Capabilities { get; }

		// Token: 0x06000068 RID: 104
		IServerCapabilities Add(string capability);

		// Token: 0x06000069 RID: 105
		IServerCapabilities Remove(string capability);

		// Token: 0x0600006A RID: 106
		bool Supports(string capability);

		// Token: 0x0600006B RID: 107
		bool Supports(IEnumerable<string> desiredCapabilitiesList);

		// Token: 0x0600006C RID: 108
		bool Supports(IServerCapabilities desiredCapabilities);

		// Token: 0x0600006D RID: 109
		IEnumerable<string> NotIn(IServerCapabilities desiredCapabilities);
	}
}
