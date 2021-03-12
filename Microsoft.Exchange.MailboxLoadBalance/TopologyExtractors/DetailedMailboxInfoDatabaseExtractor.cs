using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x0200010D RID: 269
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DetailedMailboxInfoDatabaseExtractor : FullMailboxInfoDatabaseTopologyExtractor
	{
		// Token: 0x060007EE RID: 2030 RVA: 0x00016777 File Offset: 0x00014977
		public DetailedMailboxInfoDatabaseExtractor(DirectoryDatabase directoryObject, TopologyExtractorFactory extractorFactory, IList<Guid> nonMovableOrgs) : base(directoryObject, extractorFactory, nonMovableOrgs)
		{
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00016782 File Offset: 0x00014982
		protected override void AddEntityToContainer(LoadContainer databaseContainer, LoadEntity extractedEntity)
		{
			databaseContainer.AddChild(extractedEntity);
			base.AddEntityToContainer(databaseContainer, extractedEntity);
		}
	}
}
