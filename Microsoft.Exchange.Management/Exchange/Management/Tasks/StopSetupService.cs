using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000268 RID: 616
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("stop", "setupservice")]
	public sealed class StopSetupService : ManageSetupService
	{
		// Token: 0x060016E1 RID: 5857 RVA: 0x0006172D File Offset: 0x0005F92D
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (!base.Fields.IsModified("FailIfServiceNotInstalled"))
			{
				base.FailIfServiceNotInstalled = false;
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00061758 File Offset: 0x0005F958
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.StopService(base.ServiceName, base.IgnoreTimeout, base.FailIfServiceNotInstalled, base.MaximumWaitTime);
			TaskLogger.LogExit();
		}
	}
}
