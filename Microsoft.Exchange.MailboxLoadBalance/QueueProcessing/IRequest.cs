using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x0200007F RID: 127
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IRequest
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000474 RID: 1140
		bool IsBlocked { get; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000475 RID: 1141
		IRequestQueue Queue { get; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000476 RID: 1142
		IEnumerable<ResourceKey> Resources { get; }

		// Token: 0x06000477 RID: 1143
		void Abort();

		// Token: 0x06000478 RID: 1144
		void AssignQueue(IRequestQueue queue);

		// Token: 0x06000479 RID: 1145
		RequestDiagnosticData GetDiagnosticData(bool verbose);

		// Token: 0x0600047A RID: 1146
		void Process();

		// Token: 0x0600047B RID: 1147
		bool ShouldCancel(TimeSpan queueTimeout);

		// Token: 0x0600047C RID: 1148
		bool WaitExecution();

		// Token: 0x0600047D RID: 1149
		bool WaitExecution(TimeSpan timeout);

		// Token: 0x0600047E RID: 1150
		bool WaitExecutionAndThrowOnFailure(TimeSpan timeout);
	}
}
