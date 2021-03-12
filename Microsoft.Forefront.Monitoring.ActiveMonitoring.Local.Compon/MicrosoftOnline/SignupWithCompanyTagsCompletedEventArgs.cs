using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001BA RID: 442
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class SignupWithCompanyTagsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DFC RID: 3580 RVA: 0x00023299 File Offset: 0x00021499
		internal SignupWithCompanyTagsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x000232AC File Offset: 0x000214AC
		public ProvisionInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ProvisionInfo)this.results[0];
			}
		}

		// Token: 0x040006DC RID: 1756
		private object[] results;
	}
}
