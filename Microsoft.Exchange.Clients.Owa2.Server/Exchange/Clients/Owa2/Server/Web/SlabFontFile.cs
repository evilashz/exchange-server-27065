using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000494 RID: 1172
	public class SlabFontFile : LayoutDependentResource
	{
		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x060027E1 RID: 10209 RVA: 0x0009308E File Offset: 0x0009128E
		// (set) Token: 0x060027E2 RID: 10210 RVA: 0x00093096 File Offset: 0x00091296
		public string Name { get; set; }

		// Token: 0x060027E3 RID: 10211 RVA: 0x000930A0 File Offset: 0x000912A0
		public override bool Equals(object obj)
		{
			SlabFontFile slabFontFile = obj as SlabFontFile;
			return slabFontFile != null && this.Name == slabFontFile.Name && base.Equals(obj);
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000930D3 File Offset: 0x000912D3
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ base.GetHashCode();
		}
	}
}
