using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000D2 RID: 210
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[DebuggerStepThrough]
	public class QueryBlackoutCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600065F RID: 1631 RVA: 0x0000DD58 File Offset: 0x0000BF58
		public QueryBlackoutCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0000DD6B File Offset: 0x0000BF6B
		public GroupBlackout[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GroupBlackout[])this.results[0];
			}
		}

		// Token: 0x0400033C RID: 828
		private object[] results;
	}
}
