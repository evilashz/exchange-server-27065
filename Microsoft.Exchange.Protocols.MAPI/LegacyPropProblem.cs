using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000047 RID: 71
	public struct LegacyPropProblem
	{
		// Token: 0x0600019C RID: 412 RVA: 0x0000B046 File Offset: 0x00009246
		public override string ToString()
		{
			return string.Format("[idx: {0}, tag: {1:X}, error: {2:X}]", this.Idx, this.PropTag, this.ErrorCode);
		}

		// Token: 0x04000109 RID: 265
		public int Idx;

		// Token: 0x0400010A RID: 266
		public uint PropTag;

		// Token: 0x0400010B RID: 267
		public ErrorCodeValue ErrorCode;
	}
}
