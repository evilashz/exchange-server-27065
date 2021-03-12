using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000D7 RID: 215
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class PingCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x0000EE2C File Offset: 0x0000D02C
		public PingCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0000EE3F File Offset: 0x0000D03F
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x04000366 RID: 870
		private object[] results;
	}
}
