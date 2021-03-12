using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.License
{
	// Token: 0x020009D3 RID: 2515
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	public class AcquirePreLicenseCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060036E2 RID: 14050 RVA: 0x0008C128 File Offset: 0x0008A328
		internal AcquirePreLicenseCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x0008C13B File Offset: 0x0008A33B
		public AcquirePreLicenseResponse[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AcquirePreLicenseResponse[])this.results[0];
			}
		}

		// Token: 0x04002EDD RID: 11997
		private object[] results;
	}
}
