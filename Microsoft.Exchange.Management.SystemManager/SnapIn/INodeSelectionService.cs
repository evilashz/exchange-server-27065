using System;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000279 RID: 633
	public interface INodeSelectionService
	{
		// Token: 0x06001AFB RID: 6907
		void SelectNodeByName(string nodeName);

		// Token: 0x06001AFC RID: 6908
		void SelectNodeAndResultPaneByName(string nodeName, string resultPaneName);
	}
}
