using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000075 RID: 117
	[DataContract]
	internal abstract class HyperReference
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00009A63 File Offset: 0x00007C63
		// (set) Token: 0x06000345 RID: 837 RVA: 0x00009A6B File Offset: 0x00007C6B
		[IgnoreDataMember]
		public string Relationship { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00009A74 File Offset: 0x00007C74
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00009A7C File Offset: 0x00007C7C
		[IgnoreDataMember]
		public string Href { get; set; }
	}
}
