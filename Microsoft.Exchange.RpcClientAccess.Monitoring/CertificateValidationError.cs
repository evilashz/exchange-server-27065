using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class CertificateValidationError
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002145 File Offset: 0x00000345
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000214D File Offset: 0x0000034D
		public X509Certificate Certificate { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002156 File Offset: 0x00000356
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000215E File Offset: 0x0000035E
		public X509Chain Chain { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002167 File Offset: 0x00000367
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000216F File Offset: 0x0000036F
		public SslPolicyErrors SslPolicyErrors { get; set; }

		// Token: 0x06000012 RID: 18 RVA: 0x00002178 File Offset: 0x00000378
		public override string ToString()
		{
			return this.SslPolicyErrors.ToString();
		}
	}
}
