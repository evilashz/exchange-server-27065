using System;
using System.Linq;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000497 RID: 1175
	public class SlabBinding
	{
		// Token: 0x060027EE RID: 10222 RVA: 0x000931A0 File Offset: 0x000913A0
		public SlabBinding()
		{
			this.Configurations = new SlabConfiguration[0];
			this.Dependencies = new string[0];
			this.Features = new string[0];
			this.PackagedSources = new SlabSourceFile[0];
			this.Sources = new SlabSourceFile[0];
			this.Styles = new SlabStyleFile[0];
			this.PackagedStrings = new SlabStringFile[0];
			this.Strings = new SlabStringFile[0];
			this.Fonts = new SlabFontFile[0];
			this.Images = new SlabImageFile[0];
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x0009322B File Offset: 0x0009142B
		public bool IsDefault
		{
			get
			{
				return string.IsNullOrWhiteSpace(this.Scope) && (this.Features == null || this.Features.Length == 0);
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x00093251 File Offset: 0x00091451
		// (set) Token: 0x060027F1 RID: 10225 RVA: 0x00093259 File Offset: 0x00091459
		public string Scope { get; set; }

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x00093262 File Offset: 0x00091462
		// (set) Token: 0x060027F3 RID: 10227 RVA: 0x0009326A File Offset: 0x0009146A
		public SlabConfiguration[] Configurations { get; set; }

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060027F4 RID: 10228 RVA: 0x00093273 File Offset: 0x00091473
		// (set) Token: 0x060027F5 RID: 10229 RVA: 0x0009327B File Offset: 0x0009147B
		public SlabSourceFile[] Sources { get; set; }

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x00093284 File Offset: 0x00091484
		// (set) Token: 0x060027F7 RID: 10231 RVA: 0x0009328C File Offset: 0x0009148C
		public SlabSourceFile[] PackagedSources { get; set; }

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060027F8 RID: 10232 RVA: 0x00093295 File Offset: 0x00091495
		// (set) Token: 0x060027F9 RID: 10233 RVA: 0x0009329D File Offset: 0x0009149D
		public SlabStyleFile[] Styles { get; set; }

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060027FA RID: 10234 RVA: 0x000932A6 File Offset: 0x000914A6
		// (set) Token: 0x060027FB RID: 10235 RVA: 0x000932AE File Offset: 0x000914AE
		public SlabStringFile[] Strings { get; set; }

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x000932B7 File Offset: 0x000914B7
		// (set) Token: 0x060027FD RID: 10237 RVA: 0x000932BF File Offset: 0x000914BF
		public SlabStringFile[] PackagedStrings { get; set; }

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x000932C8 File Offset: 0x000914C8
		// (set) Token: 0x060027FF RID: 10239 RVA: 0x000932D0 File Offset: 0x000914D0
		public SlabFontFile[] Fonts { get; set; }

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002800 RID: 10240 RVA: 0x000932D9 File Offset: 0x000914D9
		// (set) Token: 0x06002801 RID: 10241 RVA: 0x000932E1 File Offset: 0x000914E1
		public SlabImageFile[] Images { get; set; }

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x000932EA File Offset: 0x000914EA
		// (set) Token: 0x06002803 RID: 10243 RVA: 0x000932F2 File Offset: 0x000914F2
		public string[] Dependencies { get; set; }

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002804 RID: 10244 RVA: 0x000932FB File Offset: 0x000914FB
		// (set) Token: 0x06002805 RID: 10245 RVA: 0x00093303 File Offset: 0x00091503
		public string[] Features { get; set; }

		// Token: 0x06002806 RID: 10246 RVA: 0x0009331F File Offset: 0x0009151F
		public bool Implement(string[] features)
		{
			if (features == null)
			{
				throw new ArgumentNullException("features");
			}
			return features.All((string f) => this.Features.Contains(f, StringComparer.OrdinalIgnoreCase));
		}
	}
}
