using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000D0 RID: 208
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[DebuggerStepThrough]
	public class QueryGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x0000DD08 File Offset: 0x0000BF08
		public QueryGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0000DD1B File Offset: 0x0000BF1B
		public Group[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Group[])this.results[0];
			}
		}

		// Token: 0x0400033A RID: 826
		private object[] results;
	}
}
