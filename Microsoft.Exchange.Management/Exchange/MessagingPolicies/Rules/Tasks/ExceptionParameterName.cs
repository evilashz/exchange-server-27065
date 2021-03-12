using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B73 RID: 2931
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public class ExceptionParameterName : Attribute
	{
		// Token: 0x06006E30 RID: 28208 RVA: 0x001C05F8 File Offset: 0x001BE7F8
		public ExceptionParameterName(string name)
		{
			this.name = name;
		}

		// Token: 0x170022AC RID: 8876
		// (get) Token: 0x06006E31 RID: 28209 RVA: 0x001C0607 File Offset: 0x001BE807
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04003886 RID: 14470
		private readonly string name;
	}
}
