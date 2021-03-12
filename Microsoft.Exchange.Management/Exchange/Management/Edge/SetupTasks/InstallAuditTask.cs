using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics.Audit;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x020002F8 RID: 760
	[Cmdlet("Install", "Audit")]
	public sealed class InstallAuditTask : Task
	{
		// Token: 0x06001A10 RID: 6672 RVA: 0x00073FE0 File Offset: 0x000721E0
		protected override void InternalValidate()
		{
			base.InternalValidate();
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00073FE8 File Offset: 0x000721E8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			string installPath = ConfigurationContext.Setup.InstallPath;
			try
			{
				this.InstallMPAuditLog(installPath);
			}
			catch (InvalidOperationException)
			{
				EventSourceInstaller.UninstallSecurityEventSource("MSExchange Messaging Policies");
				this.InstallMPAuditLog(installPath);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00074034 File Offset: 0x00072234
		private void InstallMPAuditLog(string path)
		{
			EventSourceInstaller.InstallSecurityEventSource("MSExchange Messaging Policies", Path.Combine(path, "bin\\RulesAuditMsg.DLL"), null, null, Path.Combine(path, "bin\\EdgeTransport.exe"), false);
		}
	}
}
