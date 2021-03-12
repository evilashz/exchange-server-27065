using System;

namespace Microsoft.Exchange.MessageSecurity.EdgeSync
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public struct EdgeSubscriptionData
	{
		// Token: 0x0400004C RID: 76
		public string EdgeServerName;

		// Token: 0x0400004D RID: 77
		public string EdgeServerFQDN;

		// Token: 0x0400004E RID: 78
		public byte[] EdgeCertificateBlob;

		// Token: 0x0400004F RID: 79
		public byte[] PfxKPKCertificateBlob;

		// Token: 0x04000050 RID: 80
		public string ESRAUsername;

		// Token: 0x04000051 RID: 81
		public string ESRAPassword;

		// Token: 0x04000052 RID: 82
		public long EffectiveDate;

		// Token: 0x04000053 RID: 83
		public long Duration;

		// Token: 0x04000054 RID: 84
		public int AdamSslPort;

		// Token: 0x04000055 RID: 85
		public string ServerType;

		// Token: 0x04000056 RID: 86
		public string ProductID;

		// Token: 0x04000057 RID: 87
		public int VersionNumber;

		// Token: 0x04000058 RID: 88
		public string SerialNumber;
	}
}
