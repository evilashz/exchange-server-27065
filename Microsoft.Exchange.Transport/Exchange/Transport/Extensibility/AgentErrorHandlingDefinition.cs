using System;

namespace Microsoft.Exchange.Transport.Extensibility
{
	// Token: 0x02000306 RID: 774
	internal class AgentErrorHandlingDefinition
	{
		// Token: 0x060021CD RID: 8653 RVA: 0x000800C0 File Offset: 0x0007E2C0
		public AgentErrorHandlingDefinition(string name, AgentErrorHandlingCondition condition, IErrorHandlingAction action)
		{
			this.Name = name;
			this.Condition = condition;
			this.Action = action;
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x000800DD File Offset: 0x0007E2DD
		// (set) Token: 0x060021CF RID: 8655 RVA: 0x000800E5 File Offset: 0x0007E2E5
		public string Name { get; private set; }

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x000800EE File Offset: 0x0007E2EE
		// (set) Token: 0x060021D1 RID: 8657 RVA: 0x000800F6 File Offset: 0x0007E2F6
		public AgentErrorHandlingCondition Condition { get; private set; }

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x000800FF File Offset: 0x0007E2FF
		// (set) Token: 0x060021D3 RID: 8659 RVA: 0x00080107 File Offset: 0x0007E307
		public IErrorHandlingAction Action { get; private set; }
	}
}
