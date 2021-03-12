using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200009D RID: 157
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[DebuggerStepThrough]
	public class GetPilotUsersCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x00005C85 File Offset: 0x00003E85
		public GetPilotUsersCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00005C98 File Offset: 0x00003E98
		public UserWorkloadStatusInfo[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UserWorkloadStatusInfo[])this.results[0];
			}
		}

		// Token: 0x040001C1 RID: 449
		private object[] results;
	}
}
