using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationComponentDiagnosticInfo
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003123 File Offset: 0x00001323
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000312B File Offset: 0x0000132B
		public long Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				this.duration = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003134 File Offset: 0x00001334
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000313C File Offset: 0x0000133C
		public ExDateTime LastRunTime
		{
			get
			{
				return this.lastRunTime;
			}
			set
			{
				this.lastRunTime = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003145 File Offset: 0x00001345
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000314D File Offset: 0x0000134D
		public ExDateTime LastWorkTime
		{
			get
			{
				return this.lastWorkTime;
			}
			set
			{
				this.lastWorkTime = value;
			}
		}

		// Token: 0x04000018 RID: 24
		private long duration;

		// Token: 0x04000019 RID: 25
		private ExDateTime lastRunTime;

		// Token: 0x0400001A RID: 26
		private ExDateTime lastWorkTime;
	}
}
