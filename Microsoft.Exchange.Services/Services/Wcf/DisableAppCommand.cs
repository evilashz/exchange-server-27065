using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Extension;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200098A RID: 2442
	internal sealed class DisableAppCommand : SingleCmdletCommandBase<DisableAppDataRequest, DisableAppDataResponse, DisableApp, object>
	{
		// Token: 0x060045D8 RID: 17880 RVA: 0x000F55A7 File Offset: 0x000F37A7
		public DisableAppCommand(CallContext callContext, DisableAppDataRequest request) : base(callContext, request, "Disable-App", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060045D9 RID: 17881 RVA: 0x000F55B8 File Offset: 0x000F37B8
		protected override void PopulateTaskParameters()
		{
			DisableApp task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new AppIdParameter(this.request.Identity));
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x000F55F7 File Offset: 0x000F37F7
		protected override PSLocalTask<DisableApp, object> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateDisableAppTask(base.CallContext.AccessingPrincipal);
		}
	}
}
