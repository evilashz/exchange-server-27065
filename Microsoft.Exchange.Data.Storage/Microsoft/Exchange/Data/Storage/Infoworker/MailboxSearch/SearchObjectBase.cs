using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D27 RID: 3367
	[Serializable]
	public abstract class SearchObjectBase : ConfigurableObject
	{
		// Token: 0x17001EFD RID: 7933
		// (get) Token: 0x0600743B RID: 29755
		internal abstract SearchObjectBaseSchema Schema { get; }

		// Token: 0x17001EFE RID: 7934
		// (get) Token: 0x0600743C RID: 29756 RVA: 0x0020405D File Offset: 0x0020225D
		internal sealed override ObjectSchema ObjectSchema
		{
			get
			{
				return this.Schema;
			}
		}

		// Token: 0x17001EFF RID: 7935
		// (get) Token: 0x0600743D RID: 29757 RVA: 0x00204065 File Offset: 0x00202265
		internal SearchObjectId Id
		{
			get
			{
				return (SearchObjectId)this.propertyBag[SearchObjectBaseSchema.Id];
			}
		}

		// Token: 0x17001F00 RID: 7936
		// (get) Token: 0x0600743E RID: 29758 RVA: 0x0020407C File Offset: 0x0020227C
		// (set) Token: 0x0600743F RID: 29759 RVA: 0x00204093 File Offset: 0x00202293
		[ValidateNotNullOrEmpty]
		[Parameter]
		public string Name
		{
			get
			{
				return (string)this.propertyBag[SearchObjectBaseSchema.Name];
			}
			set
			{
				this.propertyBag[SearchObjectBaseSchema.Name] = value;
			}
		}

		// Token: 0x17001F01 RID: 7937
		// (get) Token: 0x06007440 RID: 29760
		internal abstract ObjectType ObjectType { get; }

		// Token: 0x06007441 RID: 29761 RVA: 0x002040A8 File Offset: 0x002022A8
		internal void SetId(SearchObjectId identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (identity.ObjectType != this.ObjectType)
			{
				throw new ArgumentException("identity.ObjectType " + identity.ObjectType.ToString());
			}
			this[SearchObjectBaseSchema.Id] = identity;
		}

		// Token: 0x06007442 RID: 29762 RVA: 0x002040FD File Offset: 0x002022FD
		internal void SetId(ADObjectId mailboxOwner, Guid guid)
		{
			this.SetId(new SearchObjectId(mailboxOwner, this.ObjectType, guid));
		}

		// Token: 0x06007443 RID: 29763 RVA: 0x00204112 File Offset: 0x00202312
		internal void SetId(ADObjectId mailboxOwner)
		{
			this.SetId(new SearchObjectId(mailboxOwner, this.ObjectType, Guid.Empty));
		}

		// Token: 0x06007444 RID: 29764 RVA: 0x0020412B File Offset: 0x0020232B
		internal virtual void OnSaving()
		{
		}

		// Token: 0x06007445 RID: 29765 RVA: 0x0020412D File Offset: 0x0020232D
		internal SearchObjectBase(SearchObjectPropertyBag propertyBag) : base(propertyBag)
		{
			this.propertyBag.SetField(SearchObjectBaseSchema.ExchangeVersion, this.MaximumSupportedExchangeObjectVersion);
			this.propertyBag.ResetChangeTracking();
		}

		// Token: 0x06007446 RID: 29766 RVA: 0x00204158 File Offset: 0x00202358
		public SearchObjectBase() : this(new SearchObjectPropertyBag())
		{
		}
	}
}
