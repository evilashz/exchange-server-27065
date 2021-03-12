using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000120 RID: 288
	internal class DisambiguatedName
	{
		// Token: 0x0600081E RID: 2078 RVA: 0x00022480 File Offset: 0x00020680
		public DisambiguatedName(string name, string disambiguationText, DisambiguationFieldEnum disambiguationField)
		{
			this.name = name;
			this.disambiguationText = disambiguationText;
			this.disambiguationField = disambiguationField;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x000224A4 File Offset: 0x000206A4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x000224AC File Offset: 0x000206AC
		public string DisambiguationText
		{
			get
			{
				return this.disambiguationText;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x000224B4 File Offset: 0x000206B4
		public DisambiguationFieldEnum DisambiguationField
		{
			get
			{
				return this.disambiguationField;
			}
		}

		// Token: 0x04000884 RID: 2180
		private string name;

		// Token: 0x04000885 RID: 2181
		private string disambiguationText;

		// Token: 0x04000886 RID: 2182
		private DisambiguationFieldEnum disambiguationField = DisambiguationFieldEnum.None;
	}
}
