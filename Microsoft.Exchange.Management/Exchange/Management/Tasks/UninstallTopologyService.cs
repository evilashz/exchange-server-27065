using System;
using System.ComponentModel;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002ED RID: 749
	[LocDescription(Strings.IDs.UninstallADTopologyServiceTask)]
	[Cmdlet("Uninstall", "TopologyService")]
	public class UninstallTopologyService : ManageTopologyService
	{
		// Token: 0x060019C4 RID: 6596 RVA: 0x00072C4C File Offset: 0x00070E4C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (ServiceControllerUtils.IsInstalled(this.Name))
			{
				using (ServiceController serviceController = new ServiceController(this.Name))
				{
					foreach (ServiceController serviceController2 in serviceController.DependentServices)
					{
						base.WriteVerbose(Strings.ProcessingDependentService(serviceController2.ServiceName));
						StringBuilder stringBuilder = new StringBuilder();
						foreach (ServiceController serviceController3 in serviceController2.ServicesDependedOn)
						{
							if (!serviceController3.ServiceName.Equals(this.Name))
							{
								stringBuilder.AppendFormat("{0}\0", serviceController3.ServiceName);
							}
						}
						stringBuilder.Append('\0');
						base.WriteVerbose(Strings.ServiceDependencies(serviceController2.ServiceName, stringBuilder.ToString()));
						using (SafeHandle serviceHandle = serviceController2.ServiceHandle)
						{
							if (!NativeMethods.ChangeServiceConfig(serviceHandle.DangerousGetHandle(), 4294967295U, 4294967295U, 4294967295U, null, null, null, stringBuilder.ToString(), null, null, null))
							{
								base.WriteVerbose(Strings.ChangeServiceConfigFailure);
								base.WriteError(new Win32Exception(Marshal.GetLastWin32Error()), ErrorCategory.InvalidOperation, null);
							}
						}
					}
				}
			}
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
