using System;
using System.Globalization;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200086E RID: 2158
	[Serializable]
	internal class ClassificationRuleCollectionLocalizableDetails
	{
		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x06004A7F RID: 19071 RVA: 0x00132F38 File Offset: 0x00131138
		// (set) Token: 0x06004A80 RID: 19072 RVA: 0x00132F40 File Offset: 0x00131140
		internal string PublisherName { get; set; }

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x06004A81 RID: 19073 RVA: 0x00132F49 File Offset: 0x00131149
		// (set) Token: 0x06004A82 RID: 19074 RVA: 0x00132F51 File Offset: 0x00131151
		internal string Name { get; set; }

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x06004A83 RID: 19075 RVA: 0x00132F5A File Offset: 0x0013115A
		// (set) Token: 0x06004A84 RID: 19076 RVA: 0x00132F62 File Offset: 0x00131162
		internal string Description { get; set; }

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x06004A85 RID: 19077 RVA: 0x00132F6B File Offset: 0x0013116B
		// (set) Token: 0x06004A86 RID: 19078 RVA: 0x00132F73 File Offset: 0x00131173
		internal CultureInfo Culture { get; set; }
	}
}
