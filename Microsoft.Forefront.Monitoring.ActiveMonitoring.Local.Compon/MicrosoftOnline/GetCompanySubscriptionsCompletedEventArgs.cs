using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001D9 RID: 473
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	public class GetCompanySubscriptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E60 RID: 3680 RVA: 0x00023479 File Offset: 0x00021679
		internal GetCompanySubscriptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0002348C File Offset: 0x0002168C
		public Subscription[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Subscription[])this.results[0];
			}
		}

		// Token: 0x040006E8 RID: 1768
		private object[] results;
	}
}
