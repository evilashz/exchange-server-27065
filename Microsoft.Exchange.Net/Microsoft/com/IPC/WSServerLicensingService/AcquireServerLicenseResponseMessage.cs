using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using Microsoft.com.IPC.WSService;

namespace Microsoft.com.IPC.WSServerLicensingService
{
	// Token: 0x02000A0B RID: 2571
	[DebuggerStepThrough]
	[MessageContract(WrapperName = "AcquireServerLicenseResponseMessage", WrapperNamespace = "http://microsoft.com/IPC/WSServerLicensingService", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "3.0.0.0")]
	public class AcquireServerLicenseResponseMessage
	{
		// Token: 0x0600380B RID: 14347 RVA: 0x0008D66F File Offset: 0x0008B86F
		public AcquireServerLicenseResponseMessage()
		{
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x0008D677 File Offset: 0x0008B877
		public AcquireServerLicenseResponseMessage(VersionData VersionData, BatchLicenseResponses AcquireServerLicenseResponses)
		{
			this.VersionData = VersionData;
			this.AcquireServerLicenseResponses = AcquireServerLicenseResponses;
		}

		// Token: 0x04002F6F RID: 12143
		[MessageHeader(Namespace = "http://microsoft.com/IPC/WSService")]
		public VersionData VersionData;

		// Token: 0x04002F70 RID: 12144
		[MessageBodyMember(Namespace = "http://microsoft.com/IPC/WSServerLicensingService", Order = 0)]
		public BatchLicenseResponses AcquireServerLicenseResponses;
	}
}
