using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x020000A0 RID: 160
	internal class CachedDelegate
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00013CFD File Offset: 0x00011EFD
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x00013D05 File Offset: 0x00011F05
		internal Delegate CompiledDelegate { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00013D0E File Offset: 0x00011F0E
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x00013D16 File Offset: 0x00011F16
		internal DataRow TemplateDataRow { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00013D1F File Offset: 0x00011F1F
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x00013D27 File Offset: 0x00011F27
		internal DataRow TemplateInputRow { get; set; }
	}
}
