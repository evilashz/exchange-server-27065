using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Assistants;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000322 RID: 802
	[Cmdlet("Stop", "ManagedFolderAssistant", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public class StopElcAssistant : SystemConfigurationObjectActionTask<ServerIdParameter, Server>
	{
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x00078FD5 File Offset: 0x000771D5
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageStopELCAssistant;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x00078FDC File Offset: 0x000771DC
		// (set) Token: 0x06001B49 RID: 6985 RVA: 0x00078FFC File Offset: 0x000771FC
		[Parameter(Mandatory = false, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true)]
		public override ServerIdParameter Identity
		{
			get
			{
				return (ServerIdParameter)(base.Fields["Identity"] ?? new ServerIdParameter());
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0007900F File Offset: 0x0007720F
		protected override IConfigDataProvider CreateSession()
		{
			return base.GlobalConfigSession;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00079018 File Offset: 0x00077218
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ELCTaskHelper.VerifyIsInConfigScopes(this.DataObject, base.SessionSettings, new Task.TaskErrorLoggingDelegate(base.WriteError));
			string fqdn = this.DataObject.Fqdn;
			AssistantsRpcClient assistantsRpcClient = new AssistantsRpcClient(fqdn);
			int num = 3;
			try
			{
				IL_37:
				assistantsRpcClient.Stop("ElcAssistant");
			}
			catch (RpcException ex)
			{
				num--;
				if ((ex.ErrorCode == 1753 || ex.ErrorCode == 1727) && num > 0)
				{
					goto IL_37;
				}
				base.WriteError(new TaskException(RpcUtility.MapRpcErrorCodeToMessage(ex.ErrorCode, fqdn)), ErrorCategory.InvalidOperation, null);
				goto IL_37;
			}
			TaskLogger.LogExit();
		}
	}
}
