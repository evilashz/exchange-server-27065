using System;
using System.Collections.Generic;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200015F RID: 351
	internal sealed class ItemIdProperty : ItemIdPropertyBase
	{
		// Token: 0x060009DF RID: 2527 RVA: 0x0002FC94 File Offset: 0x0002DE94
		private ItemIdProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0002FC9D File Offset: 0x0002DE9D
		public static ItemIdProperty CreateCommand(CommandContext commandContext)
		{
			return new ItemIdProperty(commandContext);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0002FCA8 File Offset: 0x0002DEA8
		internal override ServiceObjectId CreateServiceObjectId(string id, string changeKey)
		{
			return new ItemId
			{
				Id = id,
				ChangeKey = changeKey
			};
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0002FCCC File Offset: 0x0002DECC
		internal override Array CreateServiceObjectidArray(List<ConcatenatedIdAndChangeKey> ids)
		{
			ItemId[] array = new ItemId[ids.Count];
			for (int i = 0; i < ids.Count; i++)
			{
				array[i] = new ItemId
				{
					Id = ids[i].Id,
					ChangeKey = ids[i].ChangeKey
				};
			}
			return array;
		}
	}
}
