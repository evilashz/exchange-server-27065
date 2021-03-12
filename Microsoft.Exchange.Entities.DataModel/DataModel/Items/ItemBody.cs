using System;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000090 RID: 144
	public class ItemBody
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00007150 File Offset: 0x00005350
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00007158 File Offset: 0x00005358
		public BodyType ContentType { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00007161 File Offset: 0x00005361
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00007169 File Offset: 0x00005369
		public string Content { get; set; }
	}
}
