using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009B8 RID: 2488
	internal sealed class ClearMobileDeviceCommand : SingleCmdletCommandBase<ClearMobileDeviceRequest, ClearMobileDeviceResponse, ClearMobileDevice, MobileDevice>
	{
		// Token: 0x060046A1 RID: 18081 RVA: 0x000FADF0 File Offset: 0x000F8FF0
		public ClearMobileDeviceCommand(CallContext callContext, ClearMobileDeviceRequest request) : base(callContext, request, "Clear-MobileDevice", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060046A2 RID: 18082 RVA: 0x000FAE00 File Offset: 0x000F9000
		protected override void PopulateTaskParameters()
		{
			ClearMobileDevice task = this.cmdletRunner.TaskWrapper.Task;
			ClearMobileDeviceOptions options = this.request.Options;
			this.cmdletRunner.SetTaskParameter("Identity", task, new MobileDeviceIdParameter(options.Identity));
			this.cmdletRunner.SetTaskParameterIfModified("Cancel", this.request.Options, task, new SwitchParameter(options.Cancel));
		}

		// Token: 0x060046A3 RID: 18083 RVA: 0x000FAE72 File Offset: 0x000F9072
		protected override PSLocalTask<ClearMobileDevice, MobileDevice> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateClearMobileDeviceTask(base.CallContext.AccessingPrincipal);
		}
	}
}
