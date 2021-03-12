using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002CE RID: 718
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractDistributionList : AbstractContactBase, IDistributionList, IContactBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable, IRecipientBaseCollection<DistributionListMember>, IList<DistributionListMember>, ICollection<DistributionListMember>, IEnumerable<DistributionListMember>, IEnumerable
	{
		// Token: 0x170009BA RID: 2490
		public virtual DistributionListMember this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009BB RID: 2491
		public virtual DistributionListMember this[RecipientId id]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x00086153 File Offset: 0x00084353
		public virtual bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x0008615A File Offset: 0x0008435A
		public virtual int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00086161 File Offset: 0x00084361
		public virtual void Add(DistributionListMember item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x00086168 File Offset: 0x00084368
		public virtual DistributionListMember Add(Participant participant)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x0008616F File Offset: 0x0008436F
		public virtual void Insert(int index, DistributionListMember item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00086176 File Offset: 0x00084376
		public virtual int IndexOf(DistributionListMember item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x0008617D File Offset: 0x0008437D
		public virtual void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x00086184 File Offset: 0x00084384
		public virtual bool Remove(IDistributionListMember item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x0008618B File Offset: 0x0008438B
		public virtual void Remove(RecipientId id)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00086192 File Offset: 0x00084392
		public virtual bool Remove(DistributionListMember item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00086199 File Offset: 0x00084399
		public virtual void Clear()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x000861A0 File Offset: 0x000843A0
		public virtual bool Contains(DistributionListMember item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x000861A7 File Offset: 0x000843A7
		public virtual void CopyTo(DistributionListMember[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x000861AE File Offset: 0x000843AE
		public virtual IEnumerator<DistributionListMember> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000861B5 File Offset: 0x000843B5
		public virtual IEnumerator<IDistributionListMember> IGetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000861BC File Offset: 0x000843BC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000861C4 File Offset: 0x000843C4
		public override void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
