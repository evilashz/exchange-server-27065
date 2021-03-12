using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200010C RID: 268
	internal sealed class ServiceProxyIdentity : IEquatable<ServiceProxyIdentity>
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x00016838 File Offset: 0x00014A38
		public ServiceProxyIdentity(EndpointAddress endpointAddress, X509Certificate2 certificate, string partnerName)
		{
			ArgumentValidator.ThrowIfNull("endpointAddress", endpointAddress);
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			ArgumentValidator.ThrowIfNullOrEmpty("partnerName", partnerName);
			this.EndpointAddress = endpointAddress;
			this.Certificate = certificate;
			this.PartnerName = partnerName;
			this.useICredentials = false;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00016888 File Offset: 0x00014A88
		public ServiceProxyIdentity(EndpointAddress endpointAddress, ICredentials credentials, string partnerName)
		{
			ArgumentValidator.ThrowIfNull("endpointAddress", endpointAddress);
			ArgumentValidator.ThrowIfNull("credentials", credentials);
			ArgumentValidator.ThrowIfNullOrEmpty("partnerName", partnerName);
			this.EndpointAddress = endpointAddress;
			this.Credentials = credentials;
			this.PartnerName = partnerName;
			this.useICredentials = true;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x000168D8 File Offset: 0x00014AD8
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x000168E0 File Offset: 0x00014AE0
		public EndpointAddress EndpointAddress { get; private set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x000168E9 File Offset: 0x00014AE9
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x000168F1 File Offset: 0x00014AF1
		public X509Certificate2 Certificate { get; private set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x000168FA File Offset: 0x00014AFA
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x00016902 File Offset: 0x00014B02
		public ICredentials Credentials { get; private set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001690B File Offset: 0x00014B0B
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x00016913 File Offset: 0x00014B13
		public string PartnerName { get; private set; }

		// Token: 0x06000750 RID: 1872 RVA: 0x0001691C File Offset: 0x00014B1C
		public override bool Equals(object other)
		{
			return !object.ReferenceEquals(other, null) && (object.ReferenceEquals(this, other) || (!(base.GetType() != other.GetType()) && this.Equals(other as ServiceProxyIdentity)));
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00016958 File Offset: 0x00014B58
		public bool Equals(ServiceProxyIdentity other)
		{
			if (object.ReferenceEquals(other, null))
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			if (base.GetType() != other.GetType())
			{
				return false;
			}
			if (this.EndpointAddress == null || other.EndpointAddress == null)
			{
				return false;
			}
			if (this.useICredentials != other.useICredentials)
			{
				return false;
			}
			if (this.useICredentials)
			{
				return this.EndpointAddress.Equals(other.EndpointAddress) && this.Credentials.Equals(other.Credentials) && this.PartnerName.Equals(other.PartnerName);
			}
			return this.EndpointAddress.Equals(other.EndpointAddress) && this.Certificate.Equals(other.Certificate) && this.PartnerName.Equals(other.PartnerName);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00016A3C File Offset: 0x00014C3C
		public override int GetHashCode()
		{
			if (this.useICredentials)
			{
				return this.EndpointAddress.GetHashCode() ^ this.Credentials.GetHashCode() ^ this.PartnerName.GetHashCode();
			}
			return this.EndpointAddress.GetHashCode() ^ this.Certificate.GetHashCode() ^ this.PartnerName.GetHashCode();
		}

		// Token: 0x04000411 RID: 1041
		private readonly bool useICredentials;
	}
}
