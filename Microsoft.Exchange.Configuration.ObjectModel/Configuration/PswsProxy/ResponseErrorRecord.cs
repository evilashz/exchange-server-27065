using System;

namespace Microsoft.Exchange.Configuration.PswsProxy
{
	// Token: 0x020000D4 RID: 212
	internal class ResponseErrorRecord
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0001CF9F File Offset: 0x0001B19F
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0001CFA7 File Offset: 0x0001B1A7
		internal string FullyQualifiedErrorId { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0001CFB0 File Offset: 0x0001B1B0
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0001CFB8 File Offset: 0x0001B1B8
		internal ResponseCategoryInfo CategoryInfo { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0001CFC1 File Offset: 0x0001B1C1
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0001CFC9 File Offset: 0x0001B1C9
		internal ResponseErrorDetail ErrorDetail { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0001CFD2 File Offset: 0x0001B1D2
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0001CFDA File Offset: 0x0001B1DA
		internal string Exception { get; set; }
	}
}
