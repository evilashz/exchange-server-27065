using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Server
{
	// Token: 0x020009EE RID: 2542
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class GetLicensorCertificateCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003778 RID: 14200 RVA: 0x0008CD43 File Offset: 0x0008AF43
		internal GetLicensorCertificateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06003779 RID: 14201 RVA: 0x0008CD56 File Offset: 0x0008AF56
		public LicensorCertChain Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (LicensorCertChain)this.results[0];
			}
		}

		// Token: 0x04002F25 RID: 12069
		private object[] results;
	}
}
