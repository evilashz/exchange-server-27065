using System;
using System.Globalization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000992 RID: 2450
	internal sealed class SetTextMessagingAccountCommand : SingleCmdletCommandBase<SetTextMessagingAccountRequest, SetTextMessagingAccountResponse, SetTextMessagingAccount, object>
	{
		// Token: 0x060045FD RID: 17917 RVA: 0x000F641F File Offset: 0x000F461F
		public SetTextMessagingAccountCommand(CallContext callContext, SetTextMessagingAccountRequest request) : base(callContext, request, "Set-TextMessagingAccount", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x000F6430 File Offset: 0x000F4630
		protected override void PopulateTaskParameters()
		{
			SetTextMessagingAccount task = this.cmdletRunner.TaskWrapper.Task;
			SetTextMessagingAccountData options = this.request.Options;
			this.cmdletRunner.SetTaskParameter("Identity", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
			Microsoft.Exchange.Data.Storage.Management.TextMessagingAccount taskParameters = (Microsoft.Exchange.Data.Storage.Management.TextMessagingAccount)task.GetDynamicParameters();
			if (!string.IsNullOrEmpty(options.CountryRegionId))
			{
				this.cmdletRunner.SetTaskParameterIfModified("CountryRegionId", options, taskParameters, new RegionInfo(options.CountryRegionId));
			}
			E164Number newPropertyValue = (options.NotificationPhoneNumber != null) ? E164Number.Parse(options.NotificationPhoneNumber) : null;
			this.cmdletRunner.SetTaskParameterIfModified("NotificationPhoneNumber", options, taskParameters, newPropertyValue);
			this.cmdletRunner.SetTaskParameterIfModified("MobileOperatorId", options, taskParameters, options.MobileOperatorId);
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x000F64FD File Offset: 0x000F46FD
		protected override PSLocalTask<SetTextMessagingAccount, object> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateSetTextMessagingAccountTask(base.CallContext.AccessingPrincipal);
		}
	}
}
