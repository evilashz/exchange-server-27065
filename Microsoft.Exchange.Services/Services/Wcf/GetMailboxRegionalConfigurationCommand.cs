using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200098D RID: 2445
	internal sealed class GetMailboxRegionalConfigurationCommand : SingleCmdletCommandBase<GetMailboxRegionalConfigurationRequest, GetMailboxRegionalConfigurationResponse, GetMailboxRegionalConfiguration, MailboxRegionalConfiguration>
	{
		// Token: 0x060045E3 RID: 17891 RVA: 0x000F5AC4 File Offset: 0x000F3CC4
		public GetMailboxRegionalConfigurationCommand(CallContext callContext, GetMailboxRegionalConfigurationRequest request) : base(callContext, request, "Get-MailboxRegionalConfiguration", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x000F5AD4 File Offset: 0x000F3CD4
		protected override void PopulateTaskParameters()
		{
			GetMailboxRegionalConfiguration task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
			this.cmdletRunner.SetTaskParameter("VerifyDefaultFolderNameLanguage", task, new SwitchParameter(this.request.VerifyDefaultFolderNameLanguage));
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x000F5B40 File Offset: 0x000F3D40
		protected override void PopulateResponseData(GetMailboxRegionalConfigurationResponse response)
		{
			MailboxRegionalConfiguration result = this.cmdletRunner.TaskWrapper.Result;
			response.Options = new GetMailboxRegionalConfigurationData
			{
				DateFormat = result.DateFormat,
				DefaultFolderNameMatchingUserLanguage = result.DefaultFolderNameMatchingUserLanguage,
				Language = result.Language.Name,
				TimeFormat = result.TimeFormat,
				TimeZone = ((result.TimeZone != null) ? result.TimeZone.ToString() : null)
			};
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x000F5BC2 File Offset: 0x000F3DC2
		protected override PSLocalTask<GetMailboxRegionalConfiguration, MailboxRegionalConfiguration> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetMailboxRegionalConfigurationTask(base.CallContext.AccessingPrincipal);
		}
	}
}
