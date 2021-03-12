using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Band
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DatabaseWeightAggregator : ILoadEntityVisitor
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005C61 File Offset: 0x00003E61
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00005C69 File Offset: 0x00003E69
		public int TotalWeight { get; private set; }

		// Token: 0x060000F6 RID: 246 RVA: 0x00005C72 File Offset: 0x00003E72
		public bool Visit(LoadContainer container)
		{
			if (container.ContainerType != ContainerType.Database)
			{
				return container.CanAcceptRegularLoad;
			}
			if (!container.CanAcceptRegularLoad)
			{
				return false;
			}
			this.TotalWeight += container.RelativeLoadWeight;
			return false;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005CA2 File Offset: 0x00003EA2
		public bool Visit(LoadEntity entity)
		{
			return false;
		}
	}
}
