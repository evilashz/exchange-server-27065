using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200054D RID: 1357
	internal class SubjectListEndpoint : IEndpoint
	{
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x000CD0EC File Offset: 0x000CB2EC
		public List<string> AllHistoricalSubjects
		{
			get
			{
				return this.allHistoricalSubjects;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x000CD0F4 File Offset: 0x000CB2F4
		// (set) Token: 0x060021BA RID: 8634 RVA: 0x000CD0FC File Offset: 0x000CB2FC
		public bool RestartOnChange
		{
			get
			{
				return this.signalForRestart;
			}
			set
			{
				this.signalForRestart = value;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x000CD105 File Offset: 0x000CB305
		// (set) Token: 0x060021BC RID: 8636 RVA: 0x000CD10D File Offset: 0x000CB30D
		public Exception Exception { get; set; }

		// Token: 0x060021BD RID: 8637 RVA: 0x000CD116 File Offset: 0x000CB316
		public void Initialize()
		{
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000CD118 File Offset: 0x000CB318
		public bool DetectChange()
		{
			return this.signalForRestart;
		}

		// Token: 0x0400189A RID: 6298
		private List<string> allHistoricalSubjects = new List<string>();

		// Token: 0x0400189B RID: 6299
		private bool signalForRestart;
	}
}
