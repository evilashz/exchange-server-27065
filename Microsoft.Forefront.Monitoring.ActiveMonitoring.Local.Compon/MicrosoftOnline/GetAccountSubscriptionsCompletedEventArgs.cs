using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001D1 RID: 465
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	public class GetAccountSubscriptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E48 RID: 3656 RVA: 0x000233D9 File Offset: 0x000215D9
		internal GetAccountSubscriptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x000233EC File Offset: 0x000215EC
		public Subscription[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Subscription[])this.results[0];
			}
		}

		// Token: 0x040006E4 RID: 1764
		private object[] results;
	}
}
