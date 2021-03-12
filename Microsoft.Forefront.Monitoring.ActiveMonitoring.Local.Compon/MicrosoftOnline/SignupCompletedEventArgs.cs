using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001B8 RID: 440
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	public class SignupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DF6 RID: 3574 RVA: 0x00023271 File Offset: 0x00021471
		internal SignupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00023284 File Offset: 0x00021484
		public ProvisionInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ProvisionInfo)this.results[0];
			}
		}

		// Token: 0x040006DB RID: 1755
		private object[] results;
	}
}
