using System;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200004D RID: 77
	public abstract class AgentManager
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00006527 File Offset: 0x00004727
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000652F File Offset: 0x0000472F
		internal string AgentName
		{
			get
			{
				return this.agentName;
			}
			set
			{
				this.agentName = value;
			}
		}

		// Token: 0x04000174 RID: 372
		private string agentName;
	}
}
