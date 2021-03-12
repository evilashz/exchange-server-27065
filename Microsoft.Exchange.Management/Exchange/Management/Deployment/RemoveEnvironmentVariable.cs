using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200021E RID: 542
	[Cmdlet("Remove", "EnvironmentVariable")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public sealed class RemoveEnvironmentVariable : ManageEnvironmentVariable
	{
		// Token: 0x0600127F RID: 4735 RVA: 0x0005161C File Offset: 0x0004F81C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.SetVariable(base.Name, null, base.Target);
			TaskLogger.LogExit();
		}
	}
}
