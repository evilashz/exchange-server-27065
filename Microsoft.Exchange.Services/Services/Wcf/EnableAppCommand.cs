using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Extension;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200098B RID: 2443
	internal sealed class EnableAppCommand : SingleCmdletCommandBase<EnableAppDataRequest, EnableAppDataResponse, EnableApp, object>
	{
		// Token: 0x060045DB RID: 17883 RVA: 0x000F560E File Offset: 0x000F380E
		public EnableAppCommand(CallContext callContext, EnableAppDataRequest request) : base(callContext, request, "Enable-App", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x000F5620 File Offset: 0x000F3820
		protected override void PopulateTaskParameters()
		{
			EnableApp task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new AppIdParameter(this.request.Identity));
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x000F565F File Offset: 0x000F385F
		protected override PSLocalTask<EnableApp, object> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateEnableAppTask(base.CallContext.AccessingPrincipal);
		}
	}
}
