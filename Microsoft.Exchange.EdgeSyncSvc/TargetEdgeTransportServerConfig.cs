using System;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000013 RID: 19
	internal class TargetEdgeTransportServerConfig : TargetServerConfig
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x000088D0 File Offset: 0x00006AD0
		public TargetEdgeTransportServerConfig(string name, string host, int port, int versionNumber) : base(name, host, port)
		{
			this.versionNumber = versionNumber;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000088E3 File Offset: 0x00006AE3
		public int VersionNumber
		{
			get
			{
				return this.versionNumber;
			}
		}

		// Token: 0x0400006C RID: 108
		private readonly int versionNumber;
	}
}
