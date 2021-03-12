using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Extension;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200098F RID: 2447
	internal sealed class RemoveAppCommand : SingleCmdletCommandBase<RemoveAppDataRequest, RemoveAppDataResponse, RemoveApp, object>
	{
		// Token: 0x060045F3 RID: 17907 RVA: 0x000F61E6 File Offset: 0x000F43E6
		public RemoveAppCommand(CallContext callContext, RemoveAppDataRequest request) : base(callContext, request, "Remove-App", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x000F61F8 File Offset: 0x000F43F8
		protected override void PopulateTaskParameters()
		{
			RemoveApp task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new AppIdParameter(this.request.Identity));
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x000F6237 File Offset: 0x000F4437
		protected override PSLocalTask<RemoveApp, object> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateRemoveAppTask(base.CallContext.AccessingPrincipal);
		}
	}
}
