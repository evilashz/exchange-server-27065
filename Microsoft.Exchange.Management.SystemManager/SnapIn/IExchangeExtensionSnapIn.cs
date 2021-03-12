using System;
using System.Drawing;
using Microsoft.ManagementConsole;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200028F RID: 655
	public interface IExchangeExtensionSnapIn
	{
		// Token: 0x06001BC5 RID: 7109
		void AddRootNode(OrganizationType type, string displayName, Icon icon);

		// Token: 0x06001BC6 RID: 7110
		void HideRootNode();

		// Token: 0x06001BC7 RID: 7111
		void ShowRootNode();

		// Token: 0x06001BC8 RID: 7112
		void RemoveRootNode();

		// Token: 0x06001BC9 RID: 7113
		void ForceSaveSetting();

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001BCA RID: 7114
		SharedDataItem SharedDataItem { get; }

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001BCB RID: 7115
		SharedDataItem CallbackSharedDataItem { get; }
	}
}
