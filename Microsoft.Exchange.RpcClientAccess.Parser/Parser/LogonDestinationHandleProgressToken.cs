using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000F8 RID: 248
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LogonDestinationHandleProgressToken : LogonProgressToken
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x0000F690 File Offset: 0x0000D890
		internal LogonDestinationHandleProgressToken(RopId ropId, uint destinationObjectHandleIndex, byte logonId) : base(ropId, logonId)
		{
			this.destinationObjectHandleIndex = destinationObjectHandleIndex;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		internal uint DestinationObjectHandleIndex
		{
			get
			{
				return this.destinationObjectHandleIndex;
			}
		}

		// Token: 0x040002F9 RID: 761
		private readonly uint destinationObjectHandleIndex;
	}
}
