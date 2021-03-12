using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.License
{
	// Token: 0x020009D1 RID: 2513
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class AcquireLicenseCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060036DC RID: 14044 RVA: 0x0008C100 File Offset: 0x0008A300
		internal AcquireLicenseCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x060036DD RID: 14045 RVA: 0x0008C113 File Offset: 0x0008A313
		public AcquireLicenseResponse[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AcquireLicenseResponse[])this.results[0];
			}
		}

		// Token: 0x04002EDC RID: 11996
		private object[] results;
	}
}
