using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Constraints;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors
{
	// Token: 0x0200010C RID: 268
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FullMailboxInfoDatabaseTopologyExtractor : TopologyExtractor
	{
		// Token: 0x060007E9 RID: 2025 RVA: 0x000165D4 File Offset: 0x000147D4
		public FullMailboxInfoDatabaseTopologyExtractor(DirectoryDatabase directoryObject, TopologyExtractorFactory extractorFactory, IList<Guid> nonMovableOrganizations) : base(directoryObject, extractorFactory)
		{
			this.nonMovableOrganizations = nonMovableOrganizations;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000165E8 File Offset: 0x000147E8
		public override LoadContainer ExtractTopology()
		{
			LoadContainer loadContainer = this.CreateDatabaseContainer();
			this.ExtractConstraintSetHierarchy(loadContainer);
			return loadContainer;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00016604 File Offset: 0x00014804
		protected virtual void AddEntityToContainer(LoadContainer databaseContainer, LoadEntity extractedEntity)
		{
			databaseContainer.ConsumedLoad += extractedEntity.ConsumedLoad;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00016620 File Offset: 0x00014820
		private void ExtractConstraintSetHierarchy(LoadContainer databaseContainer)
		{
			DirectoryDatabase directoryDatabase = (DirectoryDatabase)base.DirectoryObject;
			Dictionary<string, LoadContainer> dictionary = new Dictionary<string, LoadContainer>();
			foreach (DirectoryMailbox directoryMailbox in directoryDatabase.GetMailboxes())
			{
				TopologyExtractor extractor = base.ExtractorFactory.GetExtractor(directoryMailbox);
				IMailboxProvisioningConstraints mailboxProvisioningConstraints = directoryMailbox.MailboxProvisioningConstraints;
				string text = null;
				if (mailboxProvisioningConstraints != null)
				{
					text = mailboxProvisioningConstraints.HardConstraint.Value;
				}
				text = (text ?? string.Empty);
				if (!dictionary.ContainsKey(text))
				{
					DirectoryIdentity identity = new DirectoryIdentity(DirectoryObjectType.ConstraintSet, Guid.NewGuid(), text, directoryMailbox.Identity.OrganizationId);
					DirectoryObject directoryObject = new DirectoryObject(base.DirectoryObject.Directory, identity);
					LoadContainer value = new LoadContainer(directoryObject, ContainerType.ConstraintSet);
					dictionary.Add(text, value);
					databaseContainer.AddChild(dictionary[text]);
				}
				LoadEntity extractedEntity = extractor.ExtractEntity();
				this.AddEntityToContainer(dictionary[text], extractedEntity);
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001672C File Offset: 0x0001492C
		private LoadContainer CreateDatabaseContainer()
		{
			DirectoryDatabase directoryDatabase = (DirectoryDatabase)base.DirectoryObject;
			LoadContainer loadContainer = directoryDatabase.ToLoadContainer();
			loadContainer.Constraint = new AllAcceptConstraint(new IAllocationConstraint[]
			{
				loadContainer.Constraint,
				new SpecialMailboxPlacementConstraint(this.nonMovableOrganizations)
			});
			return loadContainer;
		}

		// Token: 0x0400031B RID: 795
		private readonly IList<Guid> nonMovableOrganizations;
	}
}
