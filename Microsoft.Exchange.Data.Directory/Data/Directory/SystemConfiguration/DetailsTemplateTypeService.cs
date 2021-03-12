using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002BF RID: 703
	internal class DetailsTemplateTypeService
	{
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x0008ED6C File Offset: 0x0008CF6C
		// (set) Token: 0x06002029 RID: 8233 RVA: 0x0008ED74 File Offset: 0x0008CF74
		public string TemplateType { get; private set; }

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x0008ED7D File Offset: 0x0008CF7D
		// (set) Token: 0x0600202B RID: 8235 RVA: 0x0008ED85 File Offset: 0x0008CF85
		public MAPIPropertiesDictionary MAPIPropertiesDictionary { get; private set; }

		// Token: 0x0600202C RID: 8236 RVA: 0x0008ED8E File Offset: 0x0008CF8E
		public DetailsTemplateTypeService(string templateType, MAPIPropertiesDictionary propertiesDictionary)
		{
			this.TemplateType = templateType;
			this.MAPIPropertiesDictionary = propertiesDictionary;
		}
	}
}
