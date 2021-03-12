using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005B2 RID: 1458
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConversionRecipientList : List<ConversionRecipientEntry>, IConversionParticipantList
	{
		// Token: 0x06003BF1 RID: 15345 RVA: 0x000F6922 File Offset: 0x000F4B22
		internal ConversionRecipientList()
		{
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x000F692A File Offset: 0x000F4B2A
		public new int Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x17001249 RID: 4681
		public Participant this[int index]
		{
			get
			{
				return base[index].Participant;
			}
			set
			{
				base[index].Participant = value;
			}
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x000F694F File Offset: 0x000F4B4F
		public bool IsConversionParticipantAlwaysResolvable(int index)
		{
			return true;
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06003BF6 RID: 15350 RVA: 0x000F6952 File Offset: 0x000F4B52
		internal List<ConversionRecipientEntry> Recipients
		{
			get
			{
				return this;
			}
		}
	}
}
