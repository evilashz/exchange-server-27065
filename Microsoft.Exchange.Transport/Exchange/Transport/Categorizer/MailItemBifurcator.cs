using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000283 RID: 643
	internal class MailItemBifurcator<T> where T : IEquatable<T>, IComparable<T>, new()
	{
		// Token: 0x06001BB1 RID: 7089 RVA: 0x00071F22 File Offset: 0x00070122
		public MailItemBifurcator(TransportMailItem mailItem, IMailBifurcationHelper<T> bifurcationHelper)
		{
			this.originalMailItem = mailItem;
			this.bifurcatedMailItems = new List<TransportMailItem>();
			this.recipientsToBifurcate = new List<RecipientWithBifurcationInfo<T>>();
			this.bifurcationHelper = bifurcationHelper;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00071F4E File Offset: 0x0007014E
		public List<TransportMailItem> GetBifurcatedMailItems()
		{
			if (!this.bifurcationHelper.NeedsBifurcation())
			{
				return this.bifurcatedMailItems;
			}
			this.GetRecipientsToBifurcate();
			if (!this.HasRecipientsToBifurcate())
			{
				return this.bifurcatedMailItems;
			}
			this.SortRecipients();
			this.CreateMailItems();
			return this.bifurcatedMailItems;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x00071F8C File Offset: 0x0007018C
		private void CreateMailItems()
		{
			int startRecipientIndex = 0;
			T other = this.recipientsToBifurcate[0].BifurcationInfo;
			for (int i = 1; i < this.recipientsToBifurcate.Count; i++)
			{
				T bifurcationInfo = this.recipientsToBifurcate[i].BifurcationInfo;
				if (!bifurcationInfo.Equals(other))
				{
					int endRecipientIndex = i - 1;
					this.CreateItemForRecipients(startRecipientIndex, endRecipientIndex);
					startRecipientIndex = i;
					other = bifurcationInfo;
				}
			}
			this.CreateItemForRecipients(startRecipientIndex, this.recipientsToBifurcate.Count - 1);
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00072018 File Offset: 0x00070218
		private void CreateItemForRecipients(int startRecipientIndex, int endRecipientIndex)
		{
			List<MailRecipient> list = new List<MailRecipient>(endRecipientIndex - startRecipientIndex + 1);
			for (int i = startRecipientIndex; i <= endRecipientIndex; i++)
			{
				list.Add(this.recipientsToBifurcate[i].Recipient);
			}
			TransportMailItem item = this.bifurcationHelper.GenerateNewMailItem(list, this.recipientsToBifurcate[startRecipientIndex].BifurcationInfo);
			this.bifurcatedMailItems.Add(item);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x00072084 File Offset: 0x00070284
		private void GetRecipientsToBifurcate()
		{
			foreach (MailRecipient recipient in this.originalMailItem.Recipients)
			{
				T bifurcationInfo;
				if (this.bifurcationHelper.GetBifurcationInfo(recipient, out bifurcationInfo))
				{
					this.recipientsToBifurcate.Add(new RecipientWithBifurcationInfo<T>(recipient, bifurcationInfo));
				}
			}
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x000720F4 File Offset: 0x000702F4
		private bool HasRecipientsToBifurcate()
		{
			return this.recipientsToBifurcate.Count != 0;
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x00072107 File Offset: 0x00070307
		private void SortRecipients()
		{
			this.recipientsToBifurcate.Sort(new MailItemBifurcator<T>.RecipientWithBifurcationInfoComparer());
		}

		// Token: 0x04000D18 RID: 3352
		private IMailBifurcationHelper<T> bifurcationHelper;

		// Token: 0x04000D19 RID: 3353
		private TransportMailItem originalMailItem;

		// Token: 0x04000D1A RID: 3354
		private List<TransportMailItem> bifurcatedMailItems;

		// Token: 0x04000D1B RID: 3355
		private List<RecipientWithBifurcationInfo<T>> recipientsToBifurcate;

		// Token: 0x02000284 RID: 644
		private class RecipientWithBifurcationInfoComparer : IComparer<RecipientWithBifurcationInfo<T>>
		{
			// Token: 0x06001BB8 RID: 7096 RVA: 0x0007211C File Offset: 0x0007031C
			public int Compare(RecipientWithBifurcationInfo<T> recip1, RecipientWithBifurcationInfo<T> recip2)
			{
				T bifurcationInfo = recip1.BifurcationInfo;
				return bifurcationInfo.CompareTo(recip2.BifurcationInfo);
			}
		}
	}
}
