using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BAE RID: 2990
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConditionList : IList<Condition>, ICollection<Condition>, IEnumerable<Condition>, IEnumerable
	{
		// Token: 0x06006AE3 RID: 27363 RVA: 0x001C7FA6 File Offset: 0x001C61A6
		public ConditionList(Rule rule)
		{
			this.internalList = new List<Condition>();
			this.rule = rule;
		}

		// Token: 0x06006AE4 RID: 27364 RVA: 0x001C7FC0 File Offset: 0x001C61C0
		int IList<Condition>.IndexOf(Condition condition)
		{
			return this.internalList.IndexOf(condition);
		}

		// Token: 0x06006AE5 RID: 27365 RVA: 0x001C7FCE File Offset: 0x001C61CE
		void IList<Condition>.Insert(int index, Condition condition)
		{
			this.internalList.Insert(index, condition);
			this.rule.SetDirty();
		}

		// Token: 0x06006AE6 RID: 27366 RVA: 0x001C7FE8 File Offset: 0x001C61E8
		void IList<Condition>.RemoveAt(int index)
		{
			this.internalList.RemoveAt(index);
			this.rule.SetDirty();
		}

		// Token: 0x17001D1B RID: 7451
		Condition IList<Condition>.this[int index]
		{
			get
			{
				return this.internalList[index];
			}
			set
			{
				this.internalList[index] = value;
				this.rule.SetDirty();
			}
		}

		// Token: 0x06006AE9 RID: 27369 RVA: 0x001C8029 File Offset: 0x001C6229
		void ICollection<Condition>.Add(Condition condition)
		{
			this.CheckForDuplicate(condition.ConditionType);
			this.internalList.Add(condition);
			this.rule.SetDirty();
		}

		// Token: 0x06006AEA RID: 27370 RVA: 0x001C804E File Offset: 0x001C624E
		void ICollection<Condition>.Clear()
		{
			this.internalList.Clear();
			this.rule.SetDirty();
		}

		// Token: 0x06006AEB RID: 27371 RVA: 0x001C8066 File Offset: 0x001C6266
		bool ICollection<Condition>.Contains(Condition condition)
		{
			return this.internalList.Contains(condition);
		}

		// Token: 0x06006AEC RID: 27372 RVA: 0x001C8074 File Offset: 0x001C6274
		void ICollection<Condition>.CopyTo(Condition[] conditions, int index)
		{
			this.internalList.CopyTo(conditions, index);
		}

		// Token: 0x06006AED RID: 27373 RVA: 0x001C8083 File Offset: 0x001C6283
		bool ICollection<Condition>.Remove(Condition condition)
		{
			this.rule.SetDirty();
			return this.internalList.Remove(condition);
		}

		// Token: 0x17001D1C RID: 7452
		// (get) Token: 0x06006AEE RID: 27374 RVA: 0x001C809C File Offset: 0x001C629C
		int ICollection<Condition>.Count
		{
			get
			{
				return this.internalList.Count;
			}
		}

		// Token: 0x17001D1D RID: 7453
		// (get) Token: 0x06006AEF RID: 27375 RVA: 0x001C80A9 File Offset: 0x001C62A9
		bool ICollection<Condition>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006AF0 RID: 27376 RVA: 0x001C80AC File Offset: 0x001C62AC
		IEnumerator<Condition> IEnumerable<Condition>.GetEnumerator()
		{
			return this.internalList.GetEnumerator();
		}

		// Token: 0x06006AF1 RID: 27377 RVA: 0x001C80BE File Offset: 0x001C62BE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.internalList.GetEnumerator();
		}

		// Token: 0x06006AF2 RID: 27378 RVA: 0x001C80D0 File Offset: 0x001C62D0
		public Condition[] ToArray()
		{
			return this.internalList.ToArray();
		}

		// Token: 0x06006AF3 RID: 27379 RVA: 0x001C80F0 File Offset: 0x001C62F0
		private void CheckForDuplicate(ConditionType type)
		{
			foreach (Condition condition in ((IEnumerable<Condition>)this))
			{
				if (condition.ConditionType == type)
				{
					this.rule.ThrowValidateException(delegate
					{
						throw new ArgumentException(ServerStrings.DuplicateCondition);
					}, ServerStrings.DuplicateCondition);
				}
			}
		}

		// Token: 0x04003CF2 RID: 15602
		private List<Condition> internalList;

		// Token: 0x04003CF3 RID: 15603
		private Rule rule;
	}
}
