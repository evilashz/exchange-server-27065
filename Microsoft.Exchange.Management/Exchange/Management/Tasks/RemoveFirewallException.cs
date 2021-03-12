using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000221 RID: 545
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("remove", "firewallexception")]
	public sealed class RemoveFirewallException : ManageFirewallException
	{
		// Token: 0x06001284 RID: 4740 RVA: 0x00051670 File Offset: 0x0004F870
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.FirewallRule != null)
			{
				TaskLogger.Trace("Removing Windows Firewall Rule: {0}", new object[]
				{
					base.FirewallRule.Name
				});
				base.FirewallRule.Remove();
			}
			else if (!string.IsNullOrEmpty(base.Name) && base.BinaryPath != null)
			{
				TaskLogger.Trace("Removing binary {0} from windows firewall exception", new object[]
				{
					base.Name
				});
				ManageService.RemoveAssemblyFromFirewallExceptions(base.Name, base.BinaryPath.PathName, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			else
			{
				TaskLogger.Trace("ERROR: No Firewall Rule/Exception specified!", new object[0]);
			}
			TaskLogger.LogExit();
		}
	}
}
