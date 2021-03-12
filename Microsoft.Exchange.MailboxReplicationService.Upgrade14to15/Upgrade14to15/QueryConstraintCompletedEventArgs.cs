using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000D3 RID: 211
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class QueryConstraintCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x0000DD80 File Offset: 0x0000BF80
		public QueryConstraintCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0000DD93 File Offset: 0x0000BF93
		public Constraint[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Constraint[])this.results[0];
			}
		}

		// Token: 0x0400033D RID: 829
		private object[] results;
	}
}
