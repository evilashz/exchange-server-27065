using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005F9 RID: 1529
	public class InlineSearchBarCommand : Command
	{
		// Token: 0x06004492 RID: 17554 RVA: 0x000CF139 File Offset: 0x000CD339
		public InlineSearchBarCommand()
		{
			this.Name = "MoveToToolbar";
		}

		// Token: 0x17002680 RID: 9856
		// (get) Token: 0x06004493 RID: 17555 RVA: 0x000CF14C File Offset: 0x000CD34C
		// (set) Token: 0x06004494 RID: 17556 RVA: 0x000CF154 File Offset: 0x000CD354
		public string ControlIdToMove { get; set; }

		// Token: 0x17002681 RID: 9857
		// (get) Token: 0x06004495 RID: 17557 RVA: 0x000CF15D File Offset: 0x000CD35D
		// (set) Token: 0x06004496 RID: 17558 RVA: 0x000CF165 File Offset: 0x000CD365
		public string MovedControlCss { get; set; }
	}
}
