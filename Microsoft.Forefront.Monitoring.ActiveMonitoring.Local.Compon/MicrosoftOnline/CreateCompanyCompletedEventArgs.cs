using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001A9 RID: 425
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CreateCompanyCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DC0 RID: 3520 RVA: 0x000231F9 File Offset: 0x000213F9
		internal CreateCompanyCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0002320C File Offset: 0x0002140C
		public ProvisionInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ProvisionInfo)this.results[0];
			}
		}

		// Token: 0x040006D8 RID: 1752
		private object[] results;
	}
}
