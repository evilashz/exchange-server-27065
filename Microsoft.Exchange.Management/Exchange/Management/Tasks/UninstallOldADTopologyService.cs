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
	// Token: 0x020002EB RID: 747
	[Cmdlet("Uninstall", "OldADTopologyService")]
	[LocDescription(Strings.IDs.UninstallADTopologyServiceTask)]
	public class UninstallOldADTopologyService : ManageOldADTopologyService
	{
		// Token: 0x060019BD RID: 6589 RVA: 0x00072A44 File Offset: 0x00070C44
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (ServiceControllerUtils.IsInstalled(this.Name))
			{
				base.WriteVerbose(Strings.AdTopologyServiceWithOldNameInstalled(this.Name));
				using (ServiceController serviceController = new ServiceController(this.Name))
				{
					foreach (ServiceController serviceController2 in serviceController.DependentServices)
					{
						base.WriteVerbose(Strings.ProcessingDependentService(serviceController2.ServiceName));
						StringBuilder stringBuilder = new StringBuilder();
						foreach (ServiceController serviceController3 in serviceController2.ServicesDependedOn)
						{
							if (serviceController3.ServiceName == this.Name)
							{
								stringBuilder.AppendFormat("{0}\0", base.CurrentName);
							}
							else
							{
								stringBuilder.AppendFormat("{0}\0", serviceController3.ServiceName);
							}
						}
						stringBuilder.Append('\0');
						base.WriteVerbose(Strings.ServiceDependencies(serviceController2.ServiceName, stringBuilder.ToString()));
						using (SafeHandle serviceHandle = serviceController2.ServiceHandle)
						{
							if (!UninstallOldADTopologyService.ChangeServiceConfig(serviceHandle, 4294967295U, 4294967295U, 4294967295U, null, null, IntPtr.Zero, stringBuilder.ToString(), null, null, null))
							{
								base.WriteVerbose(Strings.ChangeServiceConfigFailure);
								base.WriteError(new Win32Exception(Marshal.GetLastWin32Error()), ErrorCategory.InvalidOperation, null);
							}
						}
					}
				}
				base.Uninstall();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060019BE RID: 6590
		[DllImport("AdvApi32.dll", CharSet = CharSet.Unicode, EntryPoint = "ChangeServiceConfigW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ChangeServiceConfig(SafeHandle service, uint serviceType, uint startType, uint errorControl, string binaryPathName, string loadOrderGroup, IntPtr tagId, string dependencies, string serviceStartName, string password, string displayName);

		// Token: 0x04000B1F RID: 2847
		private const uint ServiceNoChange = 4294967295U;

		// Token: 0x04000B20 RID: 2848
		private const uint GenericRead = 2147483648U;

		// Token: 0x04000B21 RID: 2849
		private const uint GenericWrite = 1073741824U;
	}
}
