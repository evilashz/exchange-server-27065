using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B72 RID: 2930
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public class ConditionParameterName : Attribute
	{
		// Token: 0x06006E2E RID: 28206 RVA: 0x001C05E1 File Offset: 0x001BE7E1
		public ConditionParameterName(string name)
		{
			this.name = name;
		}

		// Token: 0x170022AB RID: 8875
		// (get) Token: 0x06006E2F RID: 28207 RVA: 0x001C05F0 File Offset: 0x001BE7F0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04003885 RID: 14469
		private readonly string name;
	}
}
