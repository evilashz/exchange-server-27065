using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000F7 RID: 247
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LogonProgressToken
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x0000F66A File Offset: 0x0000D86A
		internal LogonProgressToken(RopId ropId, byte logonId)
		{
			this.ropId = ropId;
			this.logonId = logonId;
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000F680 File Offset: 0x0000D880
		internal RopId RopId
		{
			get
			{
				return this.ropId;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0000F688 File Offset: 0x0000D888
		internal byte LogonId
		{
			get
			{
				return this.logonId;
			}
		}

		// Token: 0x040002F7 RID: 759
		private readonly RopId ropId;

		// Token: 0x040002F8 RID: 760
		private readonly byte logonId;
	}
}
