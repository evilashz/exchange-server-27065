using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000532 RID: 1330
	internal class CASServiceError
	{
		// Token: 0x06002FB2 RID: 12210 RVA: 0x000C0F4C File Offset: 0x000BF14C
		internal CASServiceError(string mbxServer)
		{
			this.mbxServer = mbxServer;
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000C0F5B File Offset: 0x000BF15B
		internal CASServiceError(string mbxServer, string vdir)
		{
			this.mbxServer = mbxServer;
			this.vdir = vdir;
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000C0F71 File Offset: 0x000BF171
		internal string GetKey()
		{
			if (string.IsNullOrEmpty(this.vdir))
			{
				return this.mbxServer.ToLowerInvariant();
			}
			return this.mbxServer.ToLowerInvariant() + "_" + this.vdir.ToLowerInvariant();
		}

		// Token: 0x0400220F RID: 8719
		private readonly string mbxServer;

		// Token: 0x04002210 RID: 8720
		private readonly string vdir;
	}
}
