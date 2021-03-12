using System;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200019C RID: 412
	public interface IDataListViewBulkEditSupport : IBulkEditSupport
	{
		// Token: 0x06001065 RID: 4197
		void FireDataSourceChanged();

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001066 RID: 4198
		// (set) Token: 0x06001067 RID: 4199
		string BulkEditingIndicatorText { get; set; }
	}
}
