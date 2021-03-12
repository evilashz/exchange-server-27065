using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200009C RID: 156
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class AddPilotUsersCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x00005C5D File Offset: 0x00003E5D
		public AddPilotUsersCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00005C70 File Offset: 0x00003E70
		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		// Token: 0x040001C0 RID: 448
		private object[] results;
	}
}
