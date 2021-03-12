using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001AB RID: 427
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateSyndicatedCompanyCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DC6 RID: 3526 RVA: 0x00023221 File Offset: 0x00021421
		internal CreateSyndicatedCompanyCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00023234 File Offset: 0x00021434
		public ProvisionInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ProvisionInfo)this.results[0];
			}
		}

		// Token: 0x040006D9 RID: 1753
		private object[] results;
	}
}
