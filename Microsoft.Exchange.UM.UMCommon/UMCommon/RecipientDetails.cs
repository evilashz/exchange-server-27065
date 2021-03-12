using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200014F RID: 335
	internal class RecipientDetails
	{
		// Token: 0x06000AD6 RID: 2774 RVA: 0x00028D90 File Offset: 0x00026F90
		internal RecipientDetails(RecipientCollection recipients)
		{
			this.count = recipients.Count;
			if (recipients.Count > 0)
			{
				foreach (Recipient recipient in recipients)
				{
					this.participants.Add(recipient.Participant);
				}
				if (recipients.Count == 1)
				{
					if (recipients[0].IsDistributionList() != null && recipients[0].IsDistributionList().Value)
					{
						this.isDistributionList = true;
						return;
					}
					if (string.Equals(recipients[0].Participant.RoutingType, "MAPIPDL", StringComparison.OrdinalIgnoreCase))
					{
						this.isPersonalDistributionList = true;
					}
				}
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00028E70 File Offset: 0x00027070
		internal bool IsDistributionList
		{
			get
			{
				return this.isDistributionList;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00028E78 File Offset: 0x00027078
		internal bool IsPersonalDistributionList
		{
			get
			{
				return this.isPersonalDistributionList;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00028E80 File Offset: 0x00027080
		internal List<Participant> Participants
		{
			get
			{
				return this.participants;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00028E88 File Offset: 0x00027088
		internal int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x040005D2 RID: 1490
		private int count;

		// Token: 0x040005D3 RID: 1491
		private bool isDistributionList;

		// Token: 0x040005D4 RID: 1492
		private bool isPersonalDistributionList;

		// Token: 0x040005D5 RID: 1493
		private List<Participant> participants = new List<Participant>();
	}
}
