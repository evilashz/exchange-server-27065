using System;
using Microsoft.Exchange.Common.Cache;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000050 RID: 80
	internal class ADObjectIdCachableItem : CachableItem
	{
		// Token: 0x0600041A RID: 1050 RVA: 0x00017A90 File Offset: 0x00015C90
		internal ADObjectIdCachableItem(ADObjectId value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Value = value;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00017AAD File Offset: 0x00015CAD
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x00017AB5 File Offset: 0x00015CB5
		internal ADObjectId Value { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00017ABE File Offset: 0x00015CBE
		public override long ItemSize
		{
			get
			{
				return (long)((this.Value.DistinguishedName ?? string.Empty).Length * 2 + 16);
			}
		}
	}
}
