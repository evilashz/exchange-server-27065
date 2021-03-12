using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000993 RID: 2451
	internal sealed class SetUserCommand : SingleCmdletCommandBase<SetUserRequest, SetUserResponse, SetUser, User>
	{
		// Token: 0x06004600 RID: 17920 RVA: 0x000F6514 File Offset: 0x000F4714
		public SetUserCommand(CallContext callContext, SetUserRequest request) : base(callContext, request, "Set-User", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x000F6524 File Offset: 0x000F4724
		protected override void PopulateTaskParameters()
		{
			SetUser task = this.cmdletRunner.TaskWrapper.Task;
			SetUserData user = this.request.User;
			this.cmdletRunner.SetTaskParameter("Identity", task, new UserIdParameter(base.CallContext.AccessingADUser.ObjectId));
			User taskParameters = (User)task.GetDynamicParameters();
			this.cmdletRunner.SetTaskParameterIfModified("CountryOrRegion", user, taskParameters, (user.CountryOrRegion == null) ? null : CountryInfo.Parse(user.CountryOrRegion));
			this.cmdletRunner.SetRemainingModifiedTaskParameters(user, taskParameters);
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x000F65B5 File Offset: 0x000F47B5
		protected override PSLocalTask<SetUser, User> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateSetUserTask(base.CallContext.AccessingPrincipal);
		}
	}
}
