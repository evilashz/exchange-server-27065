using System;
using System.Collections;
using System.Configuration;
using System.Windows.Forms;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000DE RID: 222
	public interface IResultPaneControl : IPersistComponentSettings
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060007EF RID: 2031
		// (set) Token: 0x060007F0 RID: 2032
		ExchangeSettings SharedSettings { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060007F1 RID: 2033
		// (set) Token: 0x060007F2 RID: 2034
		string Status { get; set; }

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060007F3 RID: 2035
		// (remove) Token: 0x060007F4 RID: 2036
		event EventHandler StatusChanged;

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060007F5 RID: 2037
		// (set) Token: 0x060007F6 RID: 2038
		bool IsModified { get; set; }

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060007F7 RID: 2039
		// (remove) Token: 0x060007F8 RID: 2040
		event EventHandler IsModifiedChanged;

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060007F9 RID: 2041
		CommandCollection ResultPaneCommands { get; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060007FA RID: 2042
		CommandCollection ExportListCommands { get; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060007FB RID: 2043
		CommandCollection ViewModeCommands { get; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060007FC RID: 2044
		CommandCollection SelectionCommands { get; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060007FD RID: 2045
		Command RefreshCommand { get; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060007FE RID: 2046
		// (set) Token: 0x060007FF RID: 2047
		IRefreshableNotification RefreshableDataSource { get; set; }

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000800 RID: 2048
		// (remove) Token: 0x06000801 RID: 2049
		event EventHandler RefreshableDataSourceChanged;

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000802 RID: 2050
		DataObject SelectionDataObject { get; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000803 RID: 2051
		ICollection SelectedObjects { get; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000804 RID: 2052
		string SelectionHelpTopic { get; }

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000805 RID: 2053
		// (remove) Token: 0x06000806 RID: 2054
		event HelpEventHandler HelpRequested;

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000807 RID: 2055
		bool HasSelection { get; }

		// Token: 0x06000808 RID: 2056
		void OnSetActive();

		// Token: 0x06000809 RID: 2057
		void OnKillActive();
	}
}
