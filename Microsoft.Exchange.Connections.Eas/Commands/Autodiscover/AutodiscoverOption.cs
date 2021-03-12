using System;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x0200001B RID: 27
	[Flags]
	public enum AutodiscoverOption
	{
		// Token: 0x040000D3 RID: 211
		ExistingEndpoint = 1,
		// Token: 0x040000D4 RID: 212
		Probes = 2,
		// Token: 0x040000D5 RID: 213
		ExistingEndpointAndProbes = 3
	}
}
