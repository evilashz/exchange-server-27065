using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200009F RID: 159
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public interface ITraceEntryWriter
	{
		// Token: 0x060003C7 RID: 967
		void Write(TraceEntry entry);
	}
}
