using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000193 RID: 403
	internal class RequestStatisticsDiagnosticArgument : DiagnosableArgument
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x0002275A File Offset: 0x0002095A
		public RequestStatisticsDiagnosticArgument(string argument)
		{
			base.Initialize(argument);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00022769 File Offset: 0x00020969
		protected override void InitializeSchema(Dictionary<string, Type> schema)
		{
			schema["showtimeslots"] = typeof(bool);
		}

		// Token: 0x0400088D RID: 2189
		public const string ShowTimeSlots = "showtimeslots";
	}
}
