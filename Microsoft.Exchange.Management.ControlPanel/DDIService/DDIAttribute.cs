using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000164 RID: 356
	public abstract class DDIAttribute : Attribute
	{
		// Token: 0x17001A89 RID: 6793
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x00066747 File Offset: 0x00064947
		// (set) Token: 0x060021FD RID: 8701 RVA: 0x0006674F File Offset: 0x0006494F
		public string Description { get; set; }

		// Token: 0x060021FE RID: 8702 RVA: 0x00066758 File Offset: 0x00064958
		public DDIAttribute(string description)
		{
			this.Description = description;
		}
	}
}
