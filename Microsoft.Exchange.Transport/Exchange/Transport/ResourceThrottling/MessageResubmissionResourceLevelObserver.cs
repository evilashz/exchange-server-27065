using System;
using Microsoft.Exchange.Transport.MessageResubmission;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x02000031 RID: 49
	internal class MessageResubmissionResourceLevelObserver : ResourceLevelObserver
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00004E46 File Offset: 0x00003046
		public MessageResubmissionResourceLevelObserver(MessageResubmissionComponent messageResubmissionComponent) : base("MessageResubmission", messageResubmissionComponent, null)
		{
		}

		// Token: 0x04000080 RID: 128
		internal const string ResourceObserverName = "MessageResubmission";
	}
}
