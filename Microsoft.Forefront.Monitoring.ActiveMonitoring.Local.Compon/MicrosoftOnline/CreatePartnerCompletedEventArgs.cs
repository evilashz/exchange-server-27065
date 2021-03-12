using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001C6 RID: 454
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	public class CreatePartnerCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E26 RID: 3622 RVA: 0x00023311 File Offset: 0x00021511
		internal CreatePartnerCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x00023324 File Offset: 0x00021524
		public ProvisionInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ProvisionInfo)this.results[0];
			}
		}

		// Token: 0x040006DF RID: 1759
		private object[] results;
	}
}
