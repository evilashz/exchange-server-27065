using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x02000107 RID: 263
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class TopologyExtractorFactory
	{
		// Token: 0x060007CE RID: 1998 RVA: 0x000162A4 File Offset: 0x000144A4
		public TopologyExtractorFactory(ILogger logger)
		{
			this.Logger = logger;
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x000162B3 File Offset: 0x000144B3
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x000162BB File Offset: 0x000144BB
		private protected ILogger Logger { protected get; private set; }

		// Token: 0x060007D1 RID: 2001 RVA: 0x000162C4 File Offset: 0x000144C4
		public virtual TopologyExtractor GetExtractor(DirectoryObject directoryObject)
		{
			DirectoryMailbox directoryMailbox = directoryObject as DirectoryMailbox;
			if (directoryMailbox != null)
			{
				return this.CreateMailboxExtractor(directoryMailbox);
			}
			DirectoryDatabase directoryDatabase = directoryObject as DirectoryDatabase;
			if (directoryDatabase != null)
			{
				return this.CreateDatabaseExtractor(directoryDatabase);
			}
			DirectoryServer directoryServer = directoryObject as DirectoryServer;
			if (directoryServer != null)
			{
				return this.CreateServerExtractor(directoryServer);
			}
			DirectoryDatabaseAvailabilityGroup directoryDatabaseAvailabilityGroup = directoryObject as DirectoryDatabaseAvailabilityGroup;
			if (directoryDatabaseAvailabilityGroup != null)
			{
				return this.CreateDagExtractor(directoryDatabaseAvailabilityGroup);
			}
			DirectoryForest directoryForest = directoryObject as DirectoryForest;
			if (directoryForest != null)
			{
				return this.CreateForestExtractor(directoryForest);
			}
			DirectoryContainerParent directoryContainerParent = directoryObject as DirectoryContainerParent;
			if (directoryContainerParent != null)
			{
				return this.CreateContainerParentExtractor(directoryContainerParent);
			}
			return null;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00016344 File Offset: 0x00014544
		protected virtual TopologyExtractor CreateContainerParentExtractor(DirectoryContainerParent container)
		{
			return null;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00016347 File Offset: 0x00014547
		protected virtual TopologyExtractor CreateDagExtractor(DirectoryDatabaseAvailabilityGroup directoryDag)
		{
			return this.CreateContainerParentExtractor(directoryDag);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00016350 File Offset: 0x00014550
		protected virtual TopologyExtractor CreateDatabaseExtractor(DirectoryDatabase database)
		{
			return null;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00016353 File Offset: 0x00014553
		protected virtual TopologyExtractor CreateForestExtractor(DirectoryForest directoryForest)
		{
			return this.CreateContainerParentExtractor(directoryForest);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001635C File Offset: 0x0001455C
		protected virtual TopologyExtractor CreateMailboxExtractor(DirectoryMailbox mailbox)
		{
			return null;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001635F File Offset: 0x0001455F
		protected virtual TopologyExtractor CreateServerExtractor(DirectoryServer directoryServer)
		{
			return this.CreateContainerParentExtractor(directoryServer);
		}
	}
}
