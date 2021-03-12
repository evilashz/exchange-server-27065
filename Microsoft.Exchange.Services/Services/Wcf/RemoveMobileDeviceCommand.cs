using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009BA RID: 2490
	internal sealed class RemoveMobileDeviceCommand : SingleCmdletCommandBase<RemoveMobileDeviceRequest, RemoveMobileDeviceResponse, RemoveMobileDevice, MobileDevice>
	{
		// Token: 0x060046AA RID: 18090 RVA: 0x000FB1F9 File Offset: 0x000F93F9
		public RemoveMobileDeviceCommand(CallContext callContext, RemoveMobileDeviceRequest request) : base(callContext, request, "Remove-MobileDevice", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x000FB20C File Offset: 0x000F940C
		protected override void PopulateTaskParameters()
		{
			RemoveMobileDevice task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new MobileDeviceIdParameter(this.request.Identity));
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x000FB24B File Offset: 0x000F944B
		protected override PSLocalTask<RemoveMobileDevice, MobileDevice> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateRemoveMobileDeviceTask(base.CallContext.AccessingPrincipal);
		}
	}
}
