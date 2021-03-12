using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200000E RID: 14
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SchedulerState", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.Common.Database.Scheduler")]
	public enum SchedulerState
	{
		// Token: 0x0400001F RID: 31
		[EnumMember]
		Unscheduled,
		// Token: 0x04000020 RID: 32
		[EnumMember]
		Scheduled,
		// Token: 0x04000021 RID: 33
		[EnumMember]
		UpgradeComplete,
		// Token: 0x04000022 RID: 34
		[EnumMember]
		Deleted
	}
}
