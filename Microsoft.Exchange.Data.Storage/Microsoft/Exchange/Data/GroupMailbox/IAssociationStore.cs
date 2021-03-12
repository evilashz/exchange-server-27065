using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200080A RID: 2058
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAssociationStore : IDisposeTrackable, IDisposable
	{
		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x06004CA7 RID: 19623
		IMailboxLocator MailboxLocator { get; }

		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x06004CA8 RID: 19624
		string ServerFullyQualifiedDomainName { get; }

		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x06004CA9 RID: 19625
		MailboxAssociationProcessingFlags AssociationProcessingFlags { get; }

		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x06004CAA RID: 19626
		ExDateTime MailboxNextSyncTime { get; }

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x06004CAB RID: 19627
		IExchangePrincipal MailboxOwner { get; }

		// Token: 0x06004CAC RID: 19628
		IMailboxAssociationGroup CreateGroupAssociation();

		// Token: 0x06004CAD RID: 19629
		IMailboxAssociationUser CreateUserAssociation();

		// Token: 0x06004CAE RID: 19630
		void SaveAssociation(IMailboxAssociationBaseItem association);

		// Token: 0x06004CAF RID: 19631
		void OpenAssociationAsReadWrite(IMailboxAssociationBaseItem associationItem);

		// Token: 0x06004CB0 RID: 19632
		IEnumerable<IPropertyBag> GetAllAssociations(string associationItemClass, ICollection<PropertyDefinition> propertiesToRetrieve);

		// Token: 0x06004CB1 RID: 19633
		IEnumerable<IPropertyBag> GetAssociationsByType(string associationItemClass, PropertyDefinition associationTypeProperty, params PropertyDefinition[] propertiesToRetrieve);

		// Token: 0x06004CB2 RID: 19634
		IEnumerable<IPropertyBag> GetAssociationsWithMembershipChangedAfter(ExDateTime date, params PropertyDefinition[] properties);

		// Token: 0x06004CB3 RID: 19635
		IEnumerable<IPropertyBag> GetAssociationsByType(string associationItemClass, PropertyDefinition associationTypeProperty, int? maxItems, params PropertyDefinition[] propertiesToRetrieve);

		// Token: 0x06004CB4 RID: 19636
		IMailboxAssociationGroup GetGroupAssociationByItemId(VersionedId itemId);

		// Token: 0x06004CB5 RID: 19637
		IMailboxAssociationGroup GetGroupAssociationByIdProperty(PropertyDefinition idProperty, params object[] idValues);

		// Token: 0x06004CB6 RID: 19638
		IMailboxAssociationUser GetUserAssociationByItemId(VersionedId itemId);

		// Token: 0x06004CB7 RID: 19639
		IMailboxAssociationUser GetUserAssociationByIdProperty(PropertyDefinition idProperty, params object[] idValues);

		// Token: 0x06004CB8 RID: 19640
		TValue GetValueOrDefault<TValue>(IPropertyBag propertyBag, PropertyDefinition propertyDefinition, TValue defaultValue);

		// Token: 0x06004CB9 RID: 19641
		void DeleteAssociation(IMailboxAssociationBaseItem associationItem);

		// Token: 0x06004CBA RID: 19642
		void SaveMailboxAsOutOfSync();

		// Token: 0x06004CBB RID: 19643
		void SaveMailboxSyncStatus(ExDateTime nextReplicationTime);

		// Token: 0x06004CBC RID: 19644
		void SaveMailboxSyncStatus(ExDateTime nextReplicationTime, MailboxAssociationProcessingFlags mailboxAssociationProcessingFlags);
	}
}
