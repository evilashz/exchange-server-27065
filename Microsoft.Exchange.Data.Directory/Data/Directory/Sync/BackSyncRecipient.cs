using System;
using Microsoft.Exchange.Data.Directory.DirSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007B4 RID: 1972
	[Serializable]
	public class BackSyncRecipient : ADDirSyncResult
	{
		// Token: 0x060061FD RID: 25085 RVA: 0x0014FC76 File Offset: 0x0014DE76
		public BackSyncRecipient()
		{
		}

		// Token: 0x060061FE RID: 25086 RVA: 0x0014FC7E File Offset: 0x0014DE7E
		private BackSyncRecipient(PropertyBag bag) : base((ADPropertyBag)bag)
		{
		}

		// Token: 0x170022F0 RID: 8944
		// (get) Token: 0x060061FF RID: 25087 RVA: 0x0014FC8C File Offset: 0x0014DE8C
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return SyncSchema.Instance;
			}
		}

		// Token: 0x06006200 RID: 25088 RVA: 0x0014FC93 File Offset: 0x0014DE93
		internal override ADDirSyncResult CreateInstance(PropertyBag propertyBag)
		{
			return new BackSyncRecipient(propertyBag);
		}
	}
}
