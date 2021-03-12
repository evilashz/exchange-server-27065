using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C77 RID: 3191
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class MailboxIdProperty : IdProperty
	{
		// Token: 0x06007018 RID: 28696 RVA: 0x001F0714 File Offset: 0x001EE914
		public MailboxIdProperty() : base("MailboxId", typeof(VersionedId), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.EntryId, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x17001E23 RID: 7715
		// (get) Token: 0x06007019 RID: 28697 RVA: 0x001F0752 File Offset: 0x001EE952
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.None;
			}
		}

		// Token: 0x0600701A RID: 28698 RVA: 0x001F0755 File Offset: 0x001EE955
		protected override byte[] GetChangeKey(PropertyBag.BasicPropertyStore propertyBag)
		{
			return Array<byte>.Empty;
		}

		// Token: 0x0600701B RID: 28699 RVA: 0x001F075C File Offset: 0x001EE95C
		protected override StoreObjectType GetStoreObjectType(PropertyBag.BasicPropertyStore propertyBag)
		{
			return StoreObjectType.Mailbox;
		}

		// Token: 0x0600701C RID: 28700 RVA: 0x001F0763 File Offset: 0x001EE963
		protected override bool IsCompatibleId(StoreId id, ICoreObject coreObject)
		{
			return coreObject is CoreMailboxObject;
		}

		// Token: 0x0600701D RID: 28701 RVA: 0x001F076E File Offset: 0x001EE96E
		internal override void RegisterFilterTranslation()
		{
		}
	}
}
