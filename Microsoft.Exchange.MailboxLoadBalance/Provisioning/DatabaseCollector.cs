using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Provisioning
{
	// Token: 0x020000CC RID: 204
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DatabaseCollector : ILoadEntityVisitor
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00012541 File Offset: 0x00010741
		public IEnumerable<LoadContainer> Databases
		{
			get
			{
				return this.databases;
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00012549 File Offset: 0x00010749
		public bool Visit(LoadContainer container)
		{
			if (container.ContainerType == ContainerType.Database)
			{
				this.databases.Add(container);
			}
			return true;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00012561 File Offset: 0x00010761
		public bool Visit(LoadEntity entity)
		{
			return false;
		}

		// Token: 0x04000275 RID: 629
		private readonly List<LoadContainer> databases = new List<LoadContainer>(100);
	}
}
