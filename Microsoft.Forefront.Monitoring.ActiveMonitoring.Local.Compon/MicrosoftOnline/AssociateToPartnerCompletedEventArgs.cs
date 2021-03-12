using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001C3 RID: 451
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	public class AssociateToPartnerCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E1C RID: 3612 RVA: 0x000232E9 File Offset: 0x000214E9
		internal AssociateToPartnerCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x000232FC File Offset: 0x000214FC
		public Contract Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Contract)this.results[0];
			}
		}

		// Token: 0x040006DE RID: 1758
		private object[] results;
	}
}
