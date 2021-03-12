using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200051E RID: 1310
	internal abstract class ADPresentationSchema : ADObjectSchema
	{
		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06003A02 RID: 14850 RVA: 0x000E02DE File Offset: 0x000DE4DE
		// (set) Token: 0x06003A03 RID: 14851 RVA: 0x000E02E6 File Offset: 0x000DE4E6
		internal ADObjectSchema ParentSchema { get; private set; }

		// Token: 0x06003A04 RID: 14852
		internal abstract ADObjectSchema GetParentSchema();

		// Token: 0x06003A05 RID: 14853 RVA: 0x000E02EF File Offset: 0x000DE4EF
		public ADPresentationSchema()
		{
			this.ParentSchema = this.GetParentSchema();
		}
	}
}
