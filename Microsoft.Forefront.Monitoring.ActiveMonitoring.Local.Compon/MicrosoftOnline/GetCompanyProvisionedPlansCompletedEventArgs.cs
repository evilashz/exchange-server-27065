using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001D7 RID: 471
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetCompanyProvisionedPlansCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E5A RID: 3674 RVA: 0x00023451 File Offset: 0x00021651
		internal GetCompanyProvisionedPlansCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x00023464 File Offset: 0x00021664
		public ProvisionedPlanValue[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ProvisionedPlanValue[])this.results[0];
			}
		}

		// Token: 0x040006E7 RID: 1767
		private object[] results;
	}
}
