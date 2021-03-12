using System;

namespace Microsoft.Exchange.Management.FederationProvisioning
{
	// Token: 0x02000333 RID: 819
	internal sealed class CertificateRecord : IEquatable<CertificateRecord>
	{
		// Token: 0x06001BD4 RID: 7124 RVA: 0x0007C228 File Offset: 0x0007A428
		public bool Equals(CertificateRecord other)
		{
			return this.Thumbprint == other.Thumbprint;
		}

		// Token: 0x04000C26 RID: 3110
		public FederationCertificateType Type;

		// Token: 0x04000C27 RID: 3111
		public string Thumbprint;
	}
}
