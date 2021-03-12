using System;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.GroupMetricsGenerator
{
	// Token: 0x020001A2 RID: 418
	internal class TypedHashSet : HashSet<ulong>
	{
		// Token: 0x06001078 RID: 4216 RVA: 0x0005FEAF File Offset: 0x0005E0AF
		public TypedHashSet(int capacity) : base(capacity)
		{
		}
	}
}
