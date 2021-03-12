using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000281 RID: 641
	internal class CmdletNeedsProxyException : Exception
	{
		// Token: 0x0600160A RID: 5642 RVA: 0x00052958 File Offset: 0x00050B58
		public CmdletNeedsProxyException(CmdletProxyInfo cmdletProxyInfo)
		{
			this.CmdletProxyInfo = cmdletProxyInfo;
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00052967 File Offset: 0x00050B67
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x0005296F File Offset: 0x00050B6F
		public CmdletProxyInfo CmdletProxyInfo { get; private set; }
	}
}
