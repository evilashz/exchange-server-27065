using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Publish
{
	// Token: 0x020009DD RID: 2525
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class GetClientLicensorCertCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003715 RID: 14101 RVA: 0x0008C541 File Offset: 0x0008A741
		internal GetClientLicensorCertCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06003716 RID: 14102 RVA: 0x0008C554 File Offset: 0x0008A754
		public GetClientLicensorCertResponse[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetClientLicensorCertResponse[])this.results[0];
			}
		}

		// Token: 0x04002EEB RID: 12011
		private object[] results;
	}
}
