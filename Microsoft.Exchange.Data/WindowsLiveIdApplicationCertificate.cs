using System;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002BB RID: 699
	[Serializable]
	public class WindowsLiveIdApplicationCertificate : ISerializable
	{
		// Token: 0x0600191C RID: 6428 RVA: 0x0004F14B File Offset: 0x0004D34B
		public WindowsLiveIdApplicationCertificate(string name, bool isCurrent, X509Certificate2 x509Certificate)
		{
			this.Name = name;
			this.IsCurrent = isCurrent;
			this.Certificate = x509Certificate;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0004F168 File Offset: 0x0004D368
		private WindowsLiveIdApplicationCertificate(SerializationInfo info, StreamingContext context)
		{
			byte[] rawData = (byte[])info.GetValue("CertificateData", typeof(byte[]));
			this.Certificate = new X509Certificate2(rawData);
			this.IsCurrent = info.GetBoolean("IsCurrent");
			this.Name = info.GetString("Name");
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x0004F1C4 File Offset: 0x0004D3C4
		// (set) Token: 0x0600191F RID: 6431 RVA: 0x0004F1CC File Offset: 0x0004D3CC
		public string Name { get; set; }

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x0004F1D5 File Offset: 0x0004D3D5
		// (set) Token: 0x06001921 RID: 6433 RVA: 0x0004F1DD File Offset: 0x0004D3DD
		public bool IsCurrent { get; set; }

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x0004F1E6 File Offset: 0x0004D3E6
		// (set) Token: 0x06001923 RID: 6435 RVA: 0x0004F1EE File Offset: 0x0004D3EE
		public X509Certificate2 Certificate { get; set; }

		// Token: 0x06001924 RID: 6436 RVA: 0x0004F1F7 File Offset: 0x0004D3F7
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Name", this.Name);
			info.AddValue("IsCurrent", this.IsCurrent);
			info.AddValue("CertificateData", this.Certificate.Export(X509ContentType.SerializedCert));
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0004F234 File Offset: 0x0004D434
		internal static X509Certificate2 CertificateFromBase64(string base64Certificate)
		{
			X509Certificate2 result = null;
			if (!string.IsNullOrEmpty(base64Certificate))
			{
				byte[] array = Convert.FromBase64String(base64Certificate);
				if (array != null)
				{
					result = new X509Certificate2(array);
				}
			}
			return result;
		}

		// Token: 0x04000ED3 RID: 3795
		private const string SerializedNameValueName = "Name";

		// Token: 0x04000ED4 RID: 3796
		private const string SerializedIsCurrentValueName = "IsCurrent";

		// Token: 0x04000ED5 RID: 3797
		private const string SerializedCertificateDataValueName = "CertificateData";
	}
}
