using System;
using System.Globalization;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200086F RID: 2159
	[Serializable]
	internal class DataClassificationLocalizableDetails
	{
		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x06004A88 RID: 19080 RVA: 0x00132F84 File Offset: 0x00131184
		// (set) Token: 0x06004A89 RID: 19081 RVA: 0x00132F8C File Offset: 0x0013118C
		internal string Name { get; set; }

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x06004A8A RID: 19082 RVA: 0x00132F95 File Offset: 0x00131195
		// (set) Token: 0x06004A8B RID: 19083 RVA: 0x00132F9D File Offset: 0x0013119D
		internal string Description { get; set; }

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06004A8C RID: 19084 RVA: 0x00132FA6 File Offset: 0x001311A6
		// (set) Token: 0x06004A8D RID: 19085 RVA: 0x00132FAE File Offset: 0x001311AE
		internal CultureInfo Culture { get; set; }
	}
}
