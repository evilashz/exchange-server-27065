using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001C9 RID: 457
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	public class GetCompanyContextIdCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E30 RID: 3632 RVA: 0x00023339 File Offset: 0x00021539
		internal GetCompanyContextIdCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0002334C File Offset: 0x0002154C
		public Guid? Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Guid?)this.results[0];
			}
		}

		// Token: 0x040006E0 RID: 1760
		private object[] results;
	}
}
