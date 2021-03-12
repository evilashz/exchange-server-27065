using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001E7 RID: 487
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class BuildRandomSubscriptionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E8C RID: 3724 RVA: 0x00023569 File Offset: 0x00021769
		internal BuildRandomSubscriptionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0002357C File Offset: 0x0002177C
		public Subscription Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Subscription)this.results[0];
			}
		}

		// Token: 0x040006EE RID: 1774
		private object[] results;
	}
}
