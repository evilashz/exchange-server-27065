using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001A7 RID: 423
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class IsDomainAvailableCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DBA RID: 3514 RVA: 0x000231D1 File Offset: 0x000213D1
		internal IsDomainAvailableCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x000231E4 File Offset: 0x000213E4
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x040006D7 RID: 1751
		private object[] results;
	}
}
