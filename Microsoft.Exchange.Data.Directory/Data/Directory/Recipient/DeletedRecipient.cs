using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000214 RID: 532
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class DeletedRecipient : DeletedObject
	{
		// Token: 0x06001C6C RID: 7276 RVA: 0x00076001 File Offset: 0x00074201
		public DeletedRecipient()
		{
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x00076009 File Offset: 0x00074209
		internal DeletedRecipient(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x00076014 File Offset: 0x00074214
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new AndFilter(new QueryFilter[]
				{
					base.ImplicitFilter,
					new ExistsFilter(ADRecipientSchema.LegacyExchangeDN)
				});
			}
		}
	}
}
