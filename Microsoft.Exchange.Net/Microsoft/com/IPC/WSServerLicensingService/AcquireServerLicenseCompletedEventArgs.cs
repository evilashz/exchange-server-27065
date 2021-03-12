using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.com.IPC.WSService;

namespace Microsoft.com.IPC.WSServerLicensingService
{
	// Token: 0x02000A0D RID: 2573
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "3.0.0.0")]
	public class AcquireServerLicenseCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600380D RID: 14349 RVA: 0x0008D68D File Offset: 0x0008B88D
		public AcquireServerLicenseCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x0600380E RID: 14350 RVA: 0x0008D6A0 File Offset: 0x0008B8A0
		public BatchLicenseResponses AcquireServerLicenseResponses
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (BatchLicenseResponses)this.results[0];
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x0600380F RID: 14351 RVA: 0x0008D6B5 File Offset: 0x0008B8B5
		public VersionData Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (VersionData)this.results[1];
			}
		}

		// Token: 0x04002F71 RID: 12145
		private object[] results;
	}
}
