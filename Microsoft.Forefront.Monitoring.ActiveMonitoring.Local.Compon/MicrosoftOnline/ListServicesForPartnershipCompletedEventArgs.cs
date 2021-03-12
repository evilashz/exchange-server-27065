using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001C1 RID: 449
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	public class ListServicesForPartnershipCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E16 RID: 3606 RVA: 0x000232C1 File Offset: 0x000214C1
		internal ListServicesForPartnershipCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x000232D4 File Offset: 0x000214D4
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x040006DD RID: 1757
		private object[] results;
	}
}
