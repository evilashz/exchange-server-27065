using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000403 RID: 1027
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NullOpRumInfo : RumInfo
	{
		// Token: 0x06002ED5 RID: 11989 RVA: 0x000C0FE4 File Offset: 0x000BF1E4
		private NullOpRumInfo() : base(RumType.None, null)
		{
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000C1001 File Offset: 0x000BF201
		public static NullOpRumInfo CreateInstance()
		{
			return new NullOpRumInfo();
		}
	}
}
