using System;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000006 RID: 6
	internal interface IResponseItem
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000BD RID: 189
		BaseSession.SendCompleteDelegate SendCompleteDelegate { get; }

		// Token: 0x060000BE RID: 190
		int GetNextChunk(BaseSession session, out byte[] buffer, out int offset);
	}
}
