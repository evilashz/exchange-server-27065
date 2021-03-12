using System;
using System.Globalization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000487 RID: 1159
	public sealed class RegionData
	{
		// Token: 0x170022E5 RID: 8933
		// (get) Token: 0x060039F7 RID: 14839 RVA: 0x000AFC03 File Offset: 0x000ADE03
		// (set) Token: 0x060039F8 RID: 14840 RVA: 0x000AFC0B File Offset: 0x000ADE0B
		public RegionInfo RegionInfo { private get; set; }

		// Token: 0x170022E6 RID: 8934
		// (get) Token: 0x060039F9 RID: 14841 RVA: 0x000AFC14 File Offset: 0x000ADE14
		// (set) Token: 0x060039FA RID: 14842 RVA: 0x000AFC1C File Offset: 0x000ADE1C
		public string ID { get; set; }

		// Token: 0x170022E7 RID: 8935
		// (get) Token: 0x060039FB RID: 14843 RVA: 0x000AFC28 File Offset: 0x000ADE28
		public string Name
		{
			get
			{
				if (this.RegionInfo.EnglishName == this.RegionInfo.NativeName)
				{
					return this.RegionInfo.EnglishName;
				}
				return string.Format("{0} ({1})", this.RegionInfo.NativeName, this.RegionInfo.EnglishName);
			}
		}

		// Token: 0x170022E8 RID: 8936
		// (get) Token: 0x060039FC RID: 14844 RVA: 0x000AFC7E File Offset: 0x000ADE7E
		// (set) Token: 0x060039FD RID: 14845 RVA: 0x000AFC86 File Offset: 0x000ADE86
		public string CountryCode { get; set; }

		// Token: 0x170022E9 RID: 8937
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x000AFC8F File Offset: 0x000ADE8F
		// (set) Token: 0x060039FF RID: 14847 RVA: 0x000AFC97 File Offset: 0x000ADE97
		public string[] CarrierIDs { get; set; }
	}
}
