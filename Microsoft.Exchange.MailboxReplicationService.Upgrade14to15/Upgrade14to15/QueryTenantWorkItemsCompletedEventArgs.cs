using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000CB RID: 203
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class QueryTenantWorkItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x0000D6FF File Offset: 0x0000B8FF
		public QueryTenantWorkItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0000D712 File Offset: 0x0000B912
		public WorkItemInfo[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (WorkItemInfo[])this.results[0];
			}
		}

		// Token: 0x0400032C RID: 812
		private object[] results;
	}
}
