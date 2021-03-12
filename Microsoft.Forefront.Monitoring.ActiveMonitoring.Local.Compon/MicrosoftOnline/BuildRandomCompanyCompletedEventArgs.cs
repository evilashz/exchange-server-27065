using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001E1 RID: 481
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	public class BuildRandomCompanyCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E7A RID: 3706 RVA: 0x000234F1 File Offset: 0x000216F1
		internal BuildRandomCompanyCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00023504 File Offset: 0x00021704
		public Company Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Company)this.results[0];
			}
		}

		// Token: 0x040006EB RID: 1771
		private object[] results;
	}
}
