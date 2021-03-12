using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002D1 RID: 721
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractDistributionListMember : IDistributionListMember, IRecipientBase
	{
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x000861D3 File Offset: 0x000843D3
		public virtual RecipientId Id
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x000861DA File Offset: 0x000843DA
		public bool? IsDistributionList()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x000861E1 File Offset: 0x000843E1
		public virtual Participant Participant
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
