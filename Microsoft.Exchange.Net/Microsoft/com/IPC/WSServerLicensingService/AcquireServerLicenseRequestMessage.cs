using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using Microsoft.com.IPC.WSService;

namespace Microsoft.com.IPC.WSServerLicensingService
{
	// Token: 0x02000A0A RID: 2570
	[MessageContract(WrapperName = "AcquireServerLicenseRequestMessage", WrapperNamespace = "http://microsoft.com/IPC/WSServerLicensingService", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "3.0.0.0")]
	[DebuggerStepThrough]
	public class AcquireServerLicenseRequestMessage
	{
		// Token: 0x06003809 RID: 14345 RVA: 0x0008D642 File Offset: 0x0008B842
		public AcquireServerLicenseRequestMessage()
		{
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x0008D64A File Offset: 0x0008B84A
		public AcquireServerLicenseRequestMessage(VersionData VersionData, XrmlCertificateChain GroupIdentityCredential, XrmlCertificateChain IssuanceLicense, LicenseeIdentity[] LicenseeIdentities)
		{
			this.VersionData = VersionData;
			this.GroupIdentityCredential = GroupIdentityCredential;
			this.IssuanceLicense = IssuanceLicense;
			this.LicenseeIdentities = LicenseeIdentities;
		}

		// Token: 0x04002F6B RID: 12139
		[MessageHeader(Namespace = "http://microsoft.com/IPC/WSService")]
		public VersionData VersionData;

		// Token: 0x04002F6C RID: 12140
		[MessageBodyMember(Namespace = "http://microsoft.com/IPC/WSServerLicensingService", Order = 0)]
		public XrmlCertificateChain GroupIdentityCredential;

		// Token: 0x04002F6D RID: 12141
		[MessageBodyMember(Namespace = "http://microsoft.com/IPC/WSServerLicensingService", Order = 1)]
		public XrmlCertificateChain IssuanceLicense;

		// Token: 0x04002F6E RID: 12142
		[MessageBodyMember(Namespace = "http://microsoft.com/IPC/WSServerLicensingService", Order = 2)]
		public LicenseeIdentity[] LicenseeIdentities;
	}
}
