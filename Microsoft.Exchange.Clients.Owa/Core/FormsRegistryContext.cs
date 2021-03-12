using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000120 RID: 288
	public class FormsRegistryContext
	{
		// Token: 0x0600098B RID: 2443 RVA: 0x00043642 File Offset: 0x00041842
		public FormsRegistryContext(ApplicationElement applicationElement, string type, string state, string action)
		{
			this.ApplicationElement = applicationElement;
			this.Type = type;
			this.State = state;
			this.Action = action;
		}

		// Token: 0x04000703 RID: 1795
		public ApplicationElement ApplicationElement;

		// Token: 0x04000704 RID: 1796
		public string Type;

		// Token: 0x04000705 RID: 1797
		public string State;

		// Token: 0x04000706 RID: 1798
		public string Action;
	}
}
