using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Publish
{
	// Token: 0x020009DB RID: 2523
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	public class AcquireIssuanceLicenseCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600370F RID: 14095 RVA: 0x0008C519 File Offset: 0x0008A719
		internal AcquireIssuanceLicenseCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06003710 RID: 14096 RVA: 0x0008C52C File Offset: 0x0008A72C
		public AcquireIssuanceLicenseResponse[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AcquireIssuanceLicenseResponse[])this.results[0];
			}
		}

		// Token: 0x04002EEA RID: 12010
		private object[] results;
	}
}
