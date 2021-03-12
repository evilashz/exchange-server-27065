using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Logging
{
	// Token: 0x02000088 RID: 136
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct TSLID
	{
		// Token: 0x060003BD RID: 957 RVA: 0x00015864 File Offset: 0x00013A64
		private TSLID(ulong id)
		{
			this.id = id;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001586D File Offset: 0x00013A6D
		public static explicit operator TSLID(ulong id)
		{
			return new TSLID(id);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00015875 File Offset: 0x00013A75
		public override string ToString()
		{
			return this.id.ToString();
		}

		// Token: 0x040001E2 RID: 482
		private ulong id;
	}
}
