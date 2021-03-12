using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D99 RID: 3481
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class IdAndName
	{
		// Token: 0x060077CA RID: 30666 RVA: 0x0021110F File Offset: 0x0020F30F
		public IdAndName(StoreObjectId id, LocalizedString name)
		{
			this.Id = id;
			this.Name = name;
		}

		// Token: 0x17002005 RID: 8197
		// (get) Token: 0x060077CB RID: 30667 RVA: 0x00211125 File Offset: 0x0020F325
		// (set) Token: 0x060077CC RID: 30668 RVA: 0x0021112D File Offset: 0x0020F32D
		public StoreObjectId Id { get; private set; }

		// Token: 0x17002006 RID: 8198
		// (get) Token: 0x060077CD RID: 30669 RVA: 0x00211136 File Offset: 0x0020F336
		// (set) Token: 0x060077CE RID: 30670 RVA: 0x0021113E File Offset: 0x0020F33E
		public LocalizedString Name { get; private set; }

		// Token: 0x060077CF RID: 30671 RVA: 0x00211147 File Offset: 0x0020F347
		public override string ToString()
		{
			return string.Format("Id={0},Name={1}", this.Id, this.Name);
		}
	}
}
