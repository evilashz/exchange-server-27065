using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200022C RID: 556
	[CollectionDataContract]
	public class UserSkippedItemList : List<UserSkippedItem>
	{
		// Token: 0x060027BB RID: 10171 RVA: 0x0007CE4D File Offset: 0x0007B04D
		public UserSkippedItemList()
		{
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x0007CE58 File Offset: 0x0007B058
		public UserSkippedItemList(ICollection<MigrationUserSkippedItem> skippedItems) : base((skippedItems == null) ? 0 : skippedItems.Count)
		{
			if (skippedItems != null)
			{
				foreach (MigrationUserSkippedItem skippedItem in skippedItems)
				{
					base.Add(new UserSkippedItem(skippedItem));
				}
			}
		}
	}
}
