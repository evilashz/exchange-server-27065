using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000D1 RID: 209
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class QueryCapacityCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x0000DD30 File Offset: 0x0000BF30
		public QueryCapacityCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0000DD43 File Offset: 0x0000BF43
		public GroupCapacity[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GroupCapacity[])this.results[0];
			}
		}

		// Token: 0x0400033B RID: 827
		private object[] results;
	}
}
