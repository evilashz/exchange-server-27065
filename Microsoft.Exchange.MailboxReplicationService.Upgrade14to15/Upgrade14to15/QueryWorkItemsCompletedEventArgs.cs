using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000CA RID: 202
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class QueryWorkItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x0000D6D7 File Offset: 0x0000B8D7
		public QueryWorkItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0000D6EA File Offset: 0x0000B8EA
		public WorkItemQueryResult Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (WorkItemQueryResult)this.results[0];
			}
		}

		// Token: 0x0400032B RID: 811
		private object[] results;
	}
}
