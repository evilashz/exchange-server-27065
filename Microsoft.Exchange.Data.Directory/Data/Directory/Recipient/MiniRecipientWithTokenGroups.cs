using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000254 RID: 596
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class MiniRecipientWithTokenGroups : MiniRecipient
	{
		// Token: 0x06001D20 RID: 7456 RVA: 0x00078E8E File Offset: 0x0007708E
		internal MiniRecipientWithTokenGroups(IRecipientSession session, PropertyBag propertyBag)
		{
			this.m_Session = session;
			this.propertyBag = (ADPropertyBag)propertyBag;
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x00078EA9 File Offset: 0x000770A9
		public MiniRecipientWithTokenGroups()
		{
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x00078EB1 File Offset: 0x000770B1
		internal override ADObjectSchema Schema
		{
			get
			{
				return MiniRecipientWithTokenGroups.schema;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x00078EB8 File Offset: 0x000770B8
		internal override string MostDerivedObjectClass
		{
			get
			{
				throw new InvalidADObjectOperationException(DirectoryStrings.ExceptionMostDerivedOnBase("MiniRecipientWithTokenGroups"));
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x00078EC9 File Offset: 0x000770C9
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x00078ECC File Offset: 0x000770CC
		public MultiValuedProperty<SecurityIdentifier> TokenGroupsGlobalAndUniversal
		{
			get
			{
				return (MultiValuedProperty<SecurityIdentifier>)this[MiniRecipientWithTokenGroupsSchema.TokenGroupsGlobalAndUniversal];
			}
		}

		// Token: 0x04000DD4 RID: 3540
		private static readonly MiniRecipientWithTokenGroupsSchema schema = ObjectSchema.GetInstance<MiniRecipientWithTokenGroupsSchema>();
	}
}
