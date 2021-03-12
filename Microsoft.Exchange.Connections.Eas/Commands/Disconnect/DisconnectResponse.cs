using System;
using Microsoft.Exchange.Connections.Eas.Commands.Autodiscover;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Disconnect
{
	// Token: 0x0200002F RID: 47
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class DisconnectResponse : IHaveAnHttpStatus
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004629 File Offset: 0x00002829
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00004631 File Offset: 0x00002831
		public HttpStatus HttpStatus { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000463A File Offset: 0x0000283A
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00004642 File Offset: 0x00002842
		internal DisconnectStatus DisconnectStatus { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000464B File Offset: 0x0000284B
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00004653 File Offset: 0x00002853
		internal AutodiscoverEndpoint LastResolvedEndpoint { get; set; }
	}
}
