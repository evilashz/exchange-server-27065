using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000F2 RID: 242
	public abstract class DDIAttribute : Attribute
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x000203CC File Offset: 0x0001E5CC
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x000203D4 File Offset: 0x0001E5D4
		public string Description { get; set; }

		// Token: 0x06000933 RID: 2355 RVA: 0x000203DD File Offset: 0x0001E5DD
		public DDIAttribute(string description)
		{
			this.Description = description;
		}
	}
}
