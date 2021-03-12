using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x020000A1 RID: 161
	[DataContract(Name = "MultipartRelatedRequest")]
	internal class MultipartRelatedRequest<T>
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000A8A7 File Offset: 0x00008AA7
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0000A8AF File Offset: 0x00008AAF
		public T Root { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		public Dictionary<string, object> Parts { get; set; }
	}
}
