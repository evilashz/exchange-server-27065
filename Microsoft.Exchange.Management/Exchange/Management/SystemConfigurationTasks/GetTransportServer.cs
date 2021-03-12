using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009B7 RID: 2487
	[Cmdlet("Get", "TransportServer", DefaultParameterSetName = "Identity")]
	public sealed class GetTransportServer : GetTransportServiceBase
	{
		// Token: 0x060058AF RID: 22703 RVA: 0x00172184 File Offset: 0x00170384
		protected override void InternalValidate()
		{
			string cmdletName = SystemConfigurationTasksHelper.GetCmdletName(typeof(GetTransportServer));
			string cmdletName2 = SystemConfigurationTasksHelper.GetCmdletName(typeof(GetTransportService));
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
