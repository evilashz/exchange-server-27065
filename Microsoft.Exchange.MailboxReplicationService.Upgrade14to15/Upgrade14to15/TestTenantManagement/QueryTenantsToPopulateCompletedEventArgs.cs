using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.TestTenantManagement
{
	// Token: 0x020000B5 RID: 181
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[DebuggerStepThrough]
	public class QueryTenantsToPopulateCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x00009076 File Offset: 0x00007276
		public QueryTenantsToPopulateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00009089 File Offset: 0x00007289
		public Tenant[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Tenant[])this.results[0];
			}
		}

		// Token: 0x0400028B RID: 651
		private object[] results;
	}
}
