using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200055E RID: 1374
	internal class StringsExpressionGroup
	{
		// Token: 0x0600402C RID: 16428 RVA: 0x000C370D File Offset: 0x000C190D
		public StringsExpressionGroup(Type stringsType, Type idsType)
		{
			this.StringsType = stringsType;
			this.IdsType = idsType;
		}

		// Token: 0x170024E1 RID: 9441
		// (get) Token: 0x0600402D RID: 16429 RVA: 0x000C3723 File Offset: 0x000C1923
		// (set) Token: 0x0600402E RID: 16430 RVA: 0x000C372B File Offset: 0x000C192B
		public Type StringsType { get; set; }

		// Token: 0x170024E2 RID: 9442
		// (get) Token: 0x0600402F RID: 16431 RVA: 0x000C3734 File Offset: 0x000C1934
		// (set) Token: 0x06004030 RID: 16432 RVA: 0x000C373C File Offset: 0x000C193C
		public Type IdsType { get; set; }
	}
}
