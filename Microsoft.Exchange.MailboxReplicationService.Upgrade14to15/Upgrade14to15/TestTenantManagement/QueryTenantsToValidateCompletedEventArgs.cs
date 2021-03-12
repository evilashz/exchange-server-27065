using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.TestTenantManagement
{
	// Token: 0x020000B6 RID: 182
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class QueryTenantsToValidateCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x0000909E File Offset: 0x0000729E
		public QueryTenantsToValidateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x000090B1 File Offset: 0x000072B1
		public Tenant[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Tenant[])this.results[0];
			}
		}

		// Token: 0x0400028C RID: 652
		private object[] results;
	}
}
