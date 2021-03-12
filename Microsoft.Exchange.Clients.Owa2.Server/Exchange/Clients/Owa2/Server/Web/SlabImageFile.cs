using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000495 RID: 1173
	public class SlabImageFile : LayoutDependentResource
	{
		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x000930EF File Offset: 0x000912EF
		// (set) Token: 0x060027E7 RID: 10215 RVA: 0x000930F7 File Offset: 0x000912F7
		public string Name { get; set; }

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x00093100 File Offset: 0x00091300
		// (set) Token: 0x060027E9 RID: 10217 RVA: 0x00093108 File Offset: 0x00091308
		public string Type { get; set; }

		// Token: 0x060027EA RID: 10218 RVA: 0x00093111 File Offset: 0x00091311
		public bool IsThemed()
		{
			return this.Type != null && this.Type.Equals("Themed", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x00093130 File Offset: 0x00091330
		public override bool Equals(object obj)
		{
			SlabImageFile slabImageFile = obj as SlabImageFile;
			return slabImageFile != null && this.Type == slabImageFile.Type && this.Name == slabImageFile.Name && base.Equals(obj);
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x00093176 File Offset: 0x00091376
		public override int GetHashCode()
		{
			return this.Type.GetHashCode() ^ this.Name.GetHashCode() ^ base.GetHashCode();
		}
	}
}
