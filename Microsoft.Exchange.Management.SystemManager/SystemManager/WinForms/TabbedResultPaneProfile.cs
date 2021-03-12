using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000DA RID: 218
	public class TabbedResultPaneProfile : ResultPaneProfile
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0001991E File Offset: 0x00017B1E
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x00019926 File Offset: 0x00017B26
		public ResultPaneProfile[] ResultPanes { get; set; }

		// Token: 0x060007A2 RID: 1954 RVA: 0x00019930 File Offset: 0x00017B30
		public override bool HasPermission()
		{
			bool result = false;
			foreach (ResultPaneProfile resultPaneProfile in this.ResultPanes)
			{
				if (resultPaneProfile.HasPermission())
				{
					result = true;
					break;
				}
			}
			return result;
		}
	}
}
