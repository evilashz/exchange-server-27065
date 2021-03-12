using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000501 RID: 1281
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OscFolderContactSourcesEnumerator : IEnumerable<OscNetworkMoniker>, IEnumerable
	{
		// Token: 0x06003787 RID: 14215 RVA: 0x000DFAA8 File Offset: 0x000DDCA8
		public OscFolderContactSourcesEnumerator(IStorePropertyBag item)
		{
			Util.ThrowOnNullArgument(item, "item");
			this.item = item;
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x000DFAD8 File Offset: 0x000DDCD8
		public IEnumerator<OscNetworkMoniker> GetEnumerator()
		{
			return (from moniker in this.GetNetworkMonikers()
			where !string.IsNullOrWhiteSpace(moniker)
			select new OscNetworkMoniker(moniker)).GetEnumerator();
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x000DFB34 File Offset: 0x000DDD34
		private string[] GetNetworkMonikers()
		{
			object obj = this.item.TryGetProperty(MessageItemSchema.OscContactSources);
			if (obj == null || obj is PropertyError)
			{
				return Array<string>.Empty;
			}
			return (string[])obj;
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000DFB69 File Offset: 0x000DDD69
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generic version of GetEnumerator.");
		}

		// Token: 0x04001D7E RID: 7550
		private readonly IStorePropertyBag item;
	}
}
