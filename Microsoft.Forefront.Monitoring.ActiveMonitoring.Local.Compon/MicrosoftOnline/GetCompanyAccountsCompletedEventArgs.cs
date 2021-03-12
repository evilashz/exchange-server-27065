using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001D3 RID: 467
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	public class GetCompanyAccountsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E4E RID: 3662 RVA: 0x00023401 File Offset: 0x00021601
		internal GetCompanyAccountsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x00023414 File Offset: 0x00021614
		public Account[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Account[])this.results[0];
			}
		}

		// Token: 0x040006E5 RID: 1765
		private object[] results;
	}
}
