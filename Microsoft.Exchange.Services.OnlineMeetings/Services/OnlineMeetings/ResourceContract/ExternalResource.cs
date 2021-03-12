using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000074 RID: 116
	internal class ExternalResource : IResource
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00009A20 File Offset: 0x00007C20
		// (set) Token: 0x0600033D RID: 829 RVA: 0x00009A28 File Offset: 0x00007C28
		public object Value { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00009A31 File Offset: 0x00007C31
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00009A39 File Offset: 0x00007C39
		public string Href { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00009A42 File Offset: 0x00007C42
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00009A4A File Offset: 0x00007C4A
		public string ContentId { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00009A53 File Offset: 0x00007C53
		string IResource.SelfUri
		{
			get
			{
				return this.Href;
			}
		}
	}
}
