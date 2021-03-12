using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x0200002E RID: 46
	internal class WizardPageEventArgs : CancelEventArgs
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000BE20 File Offset: 0x0000A020
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000BE28 File Offset: 0x0000A028
		public string Page { get; set; }
	}
}
