using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000267 RID: 615
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("start", "setupservice")]
	public sealed class StartSetupService : ManageSetupService
	{
		// Token: 0x060016DE RID: 5854 RVA: 0x000616CA File Offset: 0x0005F8CA
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (!base.Fields.IsModified("FailIfServiceNotInstalled"))
			{
				base.FailIfServiceNotInstalled = true;
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x000616F5 File Offset: 0x0005F8F5
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.StartService(base.ServiceName, base.IgnoreTimeout, base.FailIfServiceNotInstalled, base.MaximumWaitTime, base.ServiceParameters);
			TaskLogger.LogExit();
		}
	}
}
