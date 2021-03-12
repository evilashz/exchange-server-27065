using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class SmtpServerParameters : ServerParameters
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00002B11 File Offset: 0x00000D11
		public SmtpServerParameters(string server, int port) : base(server, port)
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002B1B File Offset: 0x00000D1B
		public SmtpServerParameters(string server) : base(server, 465)
		{
		}

		// Token: 0x04000083 RID: 131
		private const int DefaultPort = 465;
	}
}
