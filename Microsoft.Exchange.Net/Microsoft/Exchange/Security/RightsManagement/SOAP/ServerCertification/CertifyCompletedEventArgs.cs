using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.ServerCertification
{
	// Token: 0x020009E4 RID: 2532
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	public class CertifyCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600373F RID: 14143 RVA: 0x0008C7E0 File Offset: 0x0008A9E0
		internal CertifyCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06003740 RID: 14144 RVA: 0x0008C7F3 File Offset: 0x0008A9F3
		public CertifyResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CertifyResponse)this.results[0];
			}
		}

		// Token: 0x04002EF9 RID: 12025
		private object[] results;
	}
}
