using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B71 RID: 2929
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public class ActionParameterName : Attribute
	{
		// Token: 0x06006E2C RID: 28204 RVA: 0x001C05CA File Offset: 0x001BE7CA
		public ActionParameterName(string name)
		{
			this.name = name;
		}

		// Token: 0x170022AA RID: 8874
		// (get) Token: 0x06006E2D RID: 28205 RVA: 0x001C05D9 File Offset: 0x001BE7D9
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04003884 RID: 14468
		private readonly string name;
	}
}
