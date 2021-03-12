using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200048E RID: 1166
	[DataContract]
	public class Slab
	{
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x00092CAE File Offset: 0x00090EAE
		// (set) Token: 0x060027A8 RID: 10152 RVA: 0x00092CB6 File Offset: 0x00090EB6
		[DataMember(Order = 0, EmitDefaultValue = false)]
		public string[] Dependencies { get; set; }

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x060027A9 RID: 10153 RVA: 0x00092CBF File Offset: 0x00090EBF
		// (set) Token: 0x060027AA RID: 10154 RVA: 0x00092CC7 File Offset: 0x00090EC7
		[DataMember(Order = 1)]
		public string[] Types { get; set; }

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x00092CD0 File Offset: 0x00090ED0
		// (set) Token: 0x060027AC RID: 10156 RVA: 0x00092CD8 File Offset: 0x00090ED8
		[DataMember(Order = 2)]
		public string[] Templates { get; set; }

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060027AD RID: 10157 RVA: 0x00092CE1 File Offset: 0x00090EE1
		// (set) Token: 0x060027AE RID: 10158 RVA: 0x00092CE9 File Offset: 0x00090EE9
		[DataMember(Order = 3)]
		public SlabStyleFile[] Styles { get; set; }

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060027AF RID: 10159 RVA: 0x00092CF2 File Offset: 0x00090EF2
		// (set) Token: 0x060027B0 RID: 10160 RVA: 0x00092CFA File Offset: 0x00090EFA
		[DataMember(Order = 4)]
		public SlabConfiguration[] Configurations { get; set; }

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x00092D03 File Offset: 0x00090F03
		// (set) Token: 0x060027B2 RID: 10162 RVA: 0x00092D0B File Offset: 0x00090F0B
		[DataMember(Order = 5)]
		public SlabSourceFile[] Sources { get; set; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x00092D14 File Offset: 0x00090F14
		// (set) Token: 0x060027B4 RID: 10164 RVA: 0x00092D1C File Offset: 0x00090F1C
		[DataMember(Order = 6)]
		public SlabSourceFile[] PackagedSources { get; set; }

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x00092D25 File Offset: 0x00090F25
		// (set) Token: 0x060027B6 RID: 10166 RVA: 0x00092D2D File Offset: 0x00090F2D
		[DataMember(Order = 7)]
		public SlabStringFile[] Strings { get; set; }

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x00092D36 File Offset: 0x00090F36
		// (set) Token: 0x060027B8 RID: 10168 RVA: 0x00092D3E File Offset: 0x00090F3E
		[DataMember(Order = 8)]
		public SlabStringFile[] PackagedStrings { get; set; }

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x00092D47 File Offset: 0x00090F47
		// (set) Token: 0x060027BA RID: 10170 RVA: 0x00092D4F File Offset: 0x00090F4F
		public SlabFontFile[] Fonts { get; set; }

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x00092D58 File Offset: 0x00090F58
		// (set) Token: 0x060027BC RID: 10172 RVA: 0x00092D60 File Offset: 0x00090F60
		public SlabImageFile[] Images { get; set; }
	}
}
