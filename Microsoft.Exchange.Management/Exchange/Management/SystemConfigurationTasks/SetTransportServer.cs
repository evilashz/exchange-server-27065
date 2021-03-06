using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009CF RID: 2511
	[Cmdlet("Set", "TransportServer", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetTransportServer : SetTransportServiceBase
	{
		// Token: 0x06005A07 RID: 23047 RVA: 0x001794F4 File Offset: 0x001776F4
		protected override void InternalValidate()
		{
			string cmdletName = SystemConfigurationTasksHelper.GetCmdletName(typeof(SetTransportServer));
			string cmdletName2 = SystemConfigurationTasksHelper.GetCmdletName(typeof(SetTransportService));
			this.WriteWarning(Strings.TransportServerCmdletDeprecated(cmdletName, cmdletName2));
			ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_TransportServerCmdletsDeprecated, new string[]
			{
				cmdletName,
				cmdletName2
			});
			base.InternalValidate();
		}
	}
}
