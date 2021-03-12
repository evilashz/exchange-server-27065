using System;
using System.Globalization;
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
	// Token: 0x02000991 RID: 2449
	internal sealed class SetMailboxRegionalConfigurationCommand : SingleCmdletCommandBase<SetMailboxRegionalConfigurationRequest, SetMailboxRegionalConfigurationResponse, SetMailboxRegionalConfiguration, MailboxRegionalConfiguration>
	{
		// Token: 0x060045FA RID: 17914 RVA: 0x000F62F5 File Offset: 0x000F44F5
		public SetMailboxRegionalConfigurationCommand(CallContext callContext, SetMailboxRegionalConfigurationRequest request) : base(callContext, request, "Set-MailboxRegionalConfiguration", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060045FB RID: 17915 RVA: 0x000F6308 File Offset: 0x000F4508
		protected override void PopulateTaskParameters()
		{
			SetMailboxRegionalConfiguration task = this.cmdletRunner.TaskWrapper.Task;
			SetMailboxRegionalConfigurationData options = this.request.Options;
			this.cmdletRunner.SetTaskParameter("Identity", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
			MailboxRegionalConfiguration taskParameters = (MailboxRegionalConfiguration)task.GetDynamicParameters();
			if (!string.IsNullOrEmpty(options.Language))
			{
				this.cmdletRunner.SetTaskParameterIfModified("Language", options, taskParameters, new CultureInfo(options.Language));
			}
			this.cmdletRunner.SetTaskParameterIfModified("LocalizeDefaultFolderName", options, task, new SwitchParameter(options.LocalizeDefaultFolderName));
			this.cmdletRunner.SetTaskParameterIfModified("TimeZone", options, taskParameters, (options.TimeZone != null) ? ExTimeZoneValue.Parse(options.TimeZone) : null);
			this.cmdletRunner.SetTaskParameterIfModified("DateFormat", options, taskParameters, options.DateFormat);
			this.cmdletRunner.SetTaskParameterIfModified("TimeFormat", options, taskParameters, options.TimeFormat);
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x000F6408 File Offset: 0x000F4608
		protected override PSLocalTask<SetMailboxRegionalConfiguration, MailboxRegionalConfiguration> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateSetMailboxRegionalConfigurationTask(base.CallContext.AccessingPrincipal);
		}
	}
}
