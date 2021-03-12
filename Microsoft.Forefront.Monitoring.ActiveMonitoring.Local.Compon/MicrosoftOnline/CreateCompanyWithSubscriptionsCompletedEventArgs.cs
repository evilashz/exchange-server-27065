using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001B6 RID: 438
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateCompanyWithSubscriptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DF0 RID: 3568 RVA: 0x00023249 File Offset: 0x00021449
		internal CreateCompanyWithSubscriptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0002325C File Offset: 0x0002145C
		public ProvisionInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ProvisionInfo)this.results[0];
			}
		}

		// Token: 0x040006DA RID: 1754
		private object[] results;
	}
}
