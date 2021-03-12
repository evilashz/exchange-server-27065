using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200037D RID: 893
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RecipientBaseCollection<ITEM_TYPE> : IRecipientBaseCollection<ITEM_TYPE>, IList<ITEM_TYPE>, ICollection<ITEM_TYPE>, IEnumerable<ITEM_TYPE>, IEnumerable where ITEM_TYPE : RecipientBase
	{
		// Token: 0x06002775 RID: 10101 RVA: 0x0009DA87 File Offset: 0x0009BC87
		internal RecipientBaseCollection(CoreRecipientCollection coreRecipientCollection)
		{
			StorageGlobals.TraceConstructIDisposable(this);
			this.coreRecipientCollection = coreRecipientCollection;
		}

		// Token: 0x17000D16 RID: 3350
		public ITEM_TYPE this[RecipientId id]
		{
			get
			{
				foreach (CoreRecipient coreRecipient in this.coreRecipientCollection)
				{
					if (coreRecipient.Id.Equals(id))
					{
						return this.ConstructStronglyTypedRecipient(coreRecipient);
					}
				}
				return default(ITEM_TYPE);
			}
		}

		// Token: 0x06002777 RID: 10103
		protected abstract ITEM_TYPE ConstructStronglyTypedRecipient(CoreRecipient coreRecipient);

		// Token: 0x06002778 RID: 10104
		public abstract ITEM_TYPE Add(Participant participant);

		// Token: 0x06002779 RID: 10105 RVA: 0x0009DB08 File Offset: 0x0009BD08
		public virtual void Remove(RecipientId id)
		{
			int num = this.coreRecipientCollection.IndexOf(id);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x17000D17 RID: 3351
		public ITEM_TYPE this[int index]
		{
			get
			{
				return this.ConstructStronglyTypedRecipient(this.coreRecipientCollection.GetCoreRecipient(index));
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x0009DB48 File Offset: 0x0009BD48
		public virtual void Clear()
		{
			for (int i = this.coreRecipientCollection.Count - 1; i >= 0; i--)
			{
				this.RemoveAt(i);
			}
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x0009DB74 File Offset: 0x0009BD74
		public int IndexOf(ITEM_TYPE item)
		{
			return this.coreRecipientCollection.IndexOf((item != null) ? item.CoreRecipient : null);
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x0009DB99 File Offset: 0x0009BD99
		public void Insert(int index, ITEM_TYPE item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x0009DBA0 File Offset: 0x0009BDA0
		public virtual void RemoveAt(int index)
		{
			this.coreRecipientCollection.RemoveAt(index);
			this.OnRemoveRecipient();
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x0009DBB4 File Offset: 0x0009BDB4
		protected virtual void OnRemoveRecipient()
		{
		}

		// Token: 0x06002781 RID: 10113
		public abstract void Add(ITEM_TYPE item);

		// Token: 0x06002782 RID: 10114 RVA: 0x0009DBB6 File Offset: 0x0009BDB6
		public bool Contains(ITEM_TYPE item)
		{
			return this.coreRecipientCollection.Contains(item.CoreRecipient);
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x0009DBD0 File Offset: 0x0009BDD0
		public void CopyTo(ITEM_TYPE[] array, int arrayIndex)
		{
			int num = 0;
			foreach (CoreRecipient coreRecipient in this.coreRecipientCollection)
			{
				array[arrayIndex + num++] = this.ConstructStronglyTypedRecipient(coreRecipient);
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06002784 RID: 10116 RVA: 0x0009DC2C File Offset: 0x0009BE2C
		public int Count
		{
			get
			{
				return this.coreRecipientCollection.Count;
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06002785 RID: 10117 RVA: 0x0009DC39 File Offset: 0x0009BE39
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0009DC3C File Offset: 0x0009BE3C
		public bool Remove(ITEM_TYPE item)
		{
			int num = this.IndexOf(item);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
			return num != -1;
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x0009DDA4 File Offset: 0x0009BFA4
		public IEnumerator<ITEM_TYPE> GetEnumerator()
		{
			foreach (CoreRecipient coreRecipient in this.coreRecipientCollection)
			{
				yield return this.ConstructStronglyTypedRecipient(coreRecipient);
			}
			yield break;
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0009DDC0 File Offset: 0x0009BFC0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x0009DDC8 File Offset: 0x0009BFC8
		internal bool IsDirty
		{
			get
			{
				return this.coreRecipientCollection.IsDirty;
			}
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x0009DDD5 File Offset: 0x0009BFD5
		public void LoadAdditionalParticipantProperties(params PropertyDefinition[] keyProperties)
		{
			this.coreRecipientCollection.LoadAdditionalParticipantProperties(keyProperties);
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x0009DDE3 File Offset: 0x0009BFE3
		internal ICoreItem CoreItem
		{
			get
			{
				return this.coreRecipientCollection.CoreItem;
			}
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x0009DDF0 File Offset: 0x0009BFF0
		internal void CopyTo(RecipientBaseCollection<ITEM_TYPE> target)
		{
			foreach (ITEM_TYPE item in this)
			{
				target.Add(item);
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x0009DE38 File Offset: 0x0009C038
		protected CoreRecipientCollection CoreRecipientCollection
		{
			get
			{
				return this.coreRecipientCollection;
			}
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x0009DE40 File Offset: 0x0009C040
		protected CoreRecipient CreateCoreRecipient(CoreRecipient.SetDefaultPropertiesDelegate setdefaultRecipientPropertiesDelegate, Participant participant)
		{
			if (participant == null)
			{
				throw new ArgumentNullException("participant");
			}
			CoreRecipient sourceCoreRecipient = null;
			CoreRecipient result;
			if (this.coreRecipientCollection.FindRemovedRecipient(participant, out sourceCoreRecipient))
			{
				result = this.CoreRecipientCollection.CreateCoreRecipient(sourceCoreRecipient);
			}
			else
			{
				result = this.CoreRecipientCollection.CreateCoreRecipient(setdefaultRecipientPropertiesDelegate, participant);
			}
			return result;
		}

		// Token: 0x04001743 RID: 5955
		private readonly CoreRecipientCollection coreRecipientCollection;
	}
}
