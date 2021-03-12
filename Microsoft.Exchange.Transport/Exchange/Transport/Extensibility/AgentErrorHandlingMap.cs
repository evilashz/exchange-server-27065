using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Extensibility
{
	// Token: 0x02000307 RID: 775
	internal class AgentErrorHandlingMap
	{
		// Token: 0x040011BB RID: 4539
		public static IErrorHandlingAction AllowAction = new AgentErrorHandlingAllowAction();

		// Token: 0x040011BC RID: 4540
		public static IErrorHandlingAction DropAction = new AgentErrorHandlingDropAction();

		// Token: 0x040011BD RID: 4541
		public static IErrorHandlingAction NdrActionBadContent = new AgentErrorHandlingNdrAction(SmtpResponse.InvalidContent);

		// Token: 0x040011BE RID: 4542
		public static IErrorHandlingAction DeferAction30MinConstant = new AgentErrorHandlingDeferAction(TimeSpan.FromMinutes(5.0), false);

		// Token: 0x040011BF RID: 4543
		public static IErrorHandlingAction DeferAction5MinProgressive = new AgentErrorHandlingDeferAction(TimeSpan.FromMinutes(5.0), true);

		// Token: 0x040011C0 RID: 4544
		public static IEnumerable<AgentErrorHandlingDefinition> DefaultMap = new AgentErrorHandlingDefinition[0];
	}
}
