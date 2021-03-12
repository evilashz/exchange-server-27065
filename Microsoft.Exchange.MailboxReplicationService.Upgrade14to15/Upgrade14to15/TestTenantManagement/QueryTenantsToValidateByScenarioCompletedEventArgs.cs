using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.TestTenantManagement
{
	// Token: 0x020000B7 RID: 183
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class QueryTenantsToValidateByScenarioCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x000090C6 File Offset: 0x000072C6
		public QueryTenantsToValidateByScenarioCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x000090D9 File Offset: 0x000072D9
		public Tenant[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Tenant[])this.results[0];
			}
		}

		// Token: 0x0400028D RID: 653
		private object[] results;
	}
}
