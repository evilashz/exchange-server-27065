using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000BF RID: 191
	internal sealed class RecipientQueryResults : IList<RecipientData>, ICollection<RecipientData>, IEnumerable<RecipientData>, IEnumerable
	{
		// Token: 0x060004C4 RID: 1220 RVA: 0x000156EA File Offset: 0x000138EA
		internal RecipientQueryResults(RecipientQuery recipientQuery, EmailAddress[] emailAddressArray)
		{
			this.recipientQuery = recipientQuery;
			this.emailAddressArray = emailAddressArray;
			this.recipientDataArray = new RecipientData[emailAddressArray.Length];
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001570E File Offset: 0x0001390E
		internal RecipientQuery RecipientQuery
		{
			get
			{
				return this.recipientQuery;
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00015716 File Offset: 0x00013916
		public int IndexOf(RecipientData item)
		{
			if (this.recipientDataArray == null)
			{
				return -1;
			}
			return Array.IndexOf<RecipientData>(this.recipientDataArray, item);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001572E File Offset: 0x0001392E
		public void Insert(int index, RecipientData item)
		{
			throw new NotSupportedException("RecipientQueryResults does not support insertion.");
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001573A File Offset: 0x0001393A
		public void RemoveAt(int index)
		{
			throw new NotSupportedException("RecipientQueryResults does not support removal.");
		}

		// Token: 0x17000112 RID: 274
		public RecipientData this[int index]
		{
			get
			{
				if (this.recipientDataArray[index] == null)
				{
					this.PopulateAtIndex(index);
				}
				return this.recipientDataArray[index];
			}
			set
			{
				throw new NotSupportedException("RecipientQueryResults is readonly and does not support assignment.");
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001576D File Offset: 0x0001396D
		public void Add(RecipientData item)
		{
			throw new NotSupportedException("RecipientQueryResults does not support add.");
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00015779 File Offset: 0x00013979
		public void Clear()
		{
			throw new NotSupportedException("RecipientQueryResults does not support clear.");
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00015785 File Offset: 0x00013985
		public bool Contains(RecipientData item)
		{
			return -1 != this.IndexOf(item);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00015794 File Offset: 0x00013994
		public void CopyTo(RecipientData[] array, int arrayIndex)
		{
			throw new NotSupportedException("RecipientQueryResults does not support copy.");
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x000157A0 File Offset: 0x000139A0
		public int Count
		{
			get
			{
				return this.recipientDataArray.Length;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x000157AA File Offset: 0x000139AA
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000157AD File Offset: 0x000139AD
		public bool Remove(RecipientData item)
		{
			throw new NotSupportedException("RecipientQueryResults does not support remove.");
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000157B9 File Offset: 0x000139B9
		public IEnumerator<RecipientData> GetEnumerator()
		{
			if (this.recipientDataArray == null)
			{
				return null;
			}
			return (IEnumerator<RecipientData>)this.recipientDataArray.GetEnumerator();
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000157D5 File Offset: 0x000139D5
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.recipientDataArray == null)
			{
				return null;
			}
			return this.recipientDataArray.GetEnumerator();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000157EC File Offset: 0x000139EC
		private void PopulateAtIndex(int index)
		{
			int num;
			IEnumerable<RecipientData> enumerable = this.recipientQuery.LookupRecipientsBatchAtIndex(this.emailAddressArray, index, out num);
			foreach (RecipientData recipientData in enumerable)
			{
				this.recipientDataArray[num++] = recipientData;
			}
		}

		// Token: 0x040002C4 RID: 708
		private RecipientQuery recipientQuery;

		// Token: 0x040002C5 RID: 709
		private EmailAddress[] emailAddressArray;

		// Token: 0x040002C6 RID: 710
		private RecipientData[] recipientDataArray;
	}
}
