using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000117 RID: 279
	internal class PostSavePropertyCollection : Dictionary<StoreObjectId, List<IPostSavePropertyCommand>>
	{
		// Token: 0x06000817 RID: 2071 RVA: 0x00027958 File Offset: 0x00025B58
		public void Add(StoreObjectId itemId, IPostSavePropertyCommand propertyCommand)
		{
			List<IPostSavePropertyCommand> list;
			if (!base.ContainsKey(itemId))
			{
				list = new List<IPostSavePropertyCommand>();
				base[itemId] = list;
			}
			else
			{
				list = base[itemId];
			}
			list.Add(propertyCommand);
		}
	}
}
