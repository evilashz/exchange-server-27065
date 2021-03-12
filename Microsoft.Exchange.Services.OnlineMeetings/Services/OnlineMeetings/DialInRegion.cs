using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000006 RID: 6
	internal class DialInRegion
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002211 File Offset: 0x00000411
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002219 File Offset: 0x00000419
		public string Name { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002222 File Offset: 0x00000422
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000222A File Offset: 0x0000042A
		public string Number { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002233 File Offset: 0x00000433
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000223B File Offset: 0x0000043B
		public Collection<string> Languages { get; set; }
	}
}
