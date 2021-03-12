using System;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x0200001C RID: 28
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class ImapServerParameters : ServerParameters
	{
		// Token: 0x060001CB RID: 459 RVA: 0x0000A873 File Offset: 0x00008A73
		public ImapServerParameters(string server, int port) : base(server, port)
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000A87D File Offset: 0x00008A7D
		public ImapServerParameters(string server) : base(server, 143)
		{
		}
	}
}
