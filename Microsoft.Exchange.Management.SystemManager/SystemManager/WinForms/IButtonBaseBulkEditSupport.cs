using System;
using System.ComponentModel;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000198 RID: 408
	public interface IButtonBaseBulkEditSupport : IOwnerDrawBulkEditSupport, IBulkEditSupport
	{
		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06001051 RID: 4177
		// (remove) Token: 0x06001052 RID: 4178
		event HandledEventHandler CheckedChangedRaising;

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06001053 RID: 4179
		// (remove) Token: 0x06001054 RID: 4180
		event HandledEventHandler Entering;

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001055 RID: 4181
		// (set) Token: 0x06001056 RID: 4182
		bool CheckedValue { get; set; }
	}
}
