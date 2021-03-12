using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009B3 RID: 2483
	internal sealed class SetMailboxAutoReplyConfigurationCommand : SingleCmdletCommandBase<SetMailboxAutoReplyConfigurationRequest, OptionsResponseBase, SetMailboxAutoReplyConfiguration, MailboxAutoReplyConfiguration>
	{
		// Token: 0x06004692 RID: 18066 RVA: 0x000FA989 File Offset: 0x000F8B89
		public SetMailboxAutoReplyConfigurationCommand(CallContext callContext, SetMailboxAutoReplyConfigurationRequest request) : base(callContext, request, "Set-MailboxAutoReplyConfiguration", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x000FA99C File Offset: 0x000F8B9C
		protected override void PopulateTaskParameters()
		{
			MailboxAutoReplyConfigurationOptions options = this.request.Options;
			SetMailboxAutoReplyConfiguration task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
			object dynamicParameters = task.GetDynamicParameters();
			if (options.EndTime != null)
			{
				this.cmdletRunner.SetTaskParameter("EndTime", dynamicParameters, (DateTime)ExDateTimeConverter.Parse(options.EndTime));
			}
			if (options.StartTime != null)
			{
				this.cmdletRunner.SetTaskParameter("StartTime", dynamicParameters, (DateTime)ExDateTimeConverter.Parse(options.StartTime));
			}
			this.cmdletRunner.SetRemainingModifiedTaskParameters(options, dynamicParameters);
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x000FAA5C File Offset: 0x000F8C5C
		protected override PSLocalTask<SetMailboxAutoReplyConfiguration, MailboxAutoReplyConfiguration> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateSetMailboxAutoReplyConfigurationTask(base.CallContext.AccessingPrincipal);
		}
	}
}
