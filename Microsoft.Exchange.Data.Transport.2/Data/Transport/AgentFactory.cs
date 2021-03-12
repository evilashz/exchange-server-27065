using System;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000020 RID: 32
	public abstract class AgentFactory
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00002E09 File Offset: 0x00001009
		public virtual void Close()
		{
		}

		// Token: 0x060000A8 RID: 168
		internal abstract Agent CreateAgent(string typeName, object state);
	}
}
