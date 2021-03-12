using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementConsole;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200028B RID: 651
	public interface IExchangeSnapIn : IServiceProvider
	{
		// Token: 0x06001B8F RID: 7055
		void Initialize(IProgressProvider progressProvider);

		// Token: 0x06001B90 RID: 7056
		int RegisterIcon(string name, Icon icon);

		// Token: 0x06001B91 RID: 7057
		ExchangeSettings CreateSettings(IComponent owner);

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001B92 RID: 7058
		IUIService ShellUI { get; }

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001B93 RID: 7059
		ExchangeSettings Settings { get; }

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001B94 RID: 7060
		ScopeNodeCollection ScopeNodeCollection { get; }

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001B95 RID: 7061
		string SnapInGuidString { get; }

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001B96 RID: 7062
		string RootNodeDisplayName { get; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001B97 RID: 7063
		Icon RootNodeIcon { get; }
	}
}
