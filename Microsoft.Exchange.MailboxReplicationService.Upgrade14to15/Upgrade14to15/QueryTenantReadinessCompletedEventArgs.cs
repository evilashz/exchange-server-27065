using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000CF RID: 207
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class QueryTenantReadinessCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x0000DCE0 File Offset: 0x0000BEE0
		public QueryTenantReadinessCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0000DCF3 File Offset: 0x0000BEF3
		public TenantReadiness[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (TenantReadiness[])this.results[0];
			}
		}

		// Token: 0x04000339 RID: 825
		private object[] results;
	}
}
