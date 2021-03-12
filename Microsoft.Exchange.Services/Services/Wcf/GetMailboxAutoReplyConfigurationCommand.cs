using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200099F RID: 2463
	internal sealed class GetMailboxAutoReplyConfigurationCommand : SingleCmdletCommandBase<object, GetMailboxAutoReplyConfigurationResponse, GetMailboxAutoReplyConfiguration, MailboxAutoReplyConfiguration>
	{
		// Token: 0x06004640 RID: 17984 RVA: 0x000F7EF5 File Offset: 0x000F60F5
		public GetMailboxAutoReplyConfigurationCommand(CallContext callContext) : base(callContext, null, "Get-MailboxAutoReplyConfiguration", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x000F7F08 File Offset: 0x000F6108
		protected override void PopulateTaskParameters()
		{
			GetMailboxAutoReplyConfiguration task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x000F7F4C File Offset: 0x000F614C
		protected override void PopulateResponseData(GetMailboxAutoReplyConfigurationResponse response)
		{
			MailboxAutoReplyConfiguration result = this.cmdletRunner.TaskWrapper.Result;
			response.Options = new MailboxAutoReplyConfigurationOptions
			{
				AutoReplyState = result.AutoReplyState,
				EndTime = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime((ExDateTime)result.EndTime),
				ExternalAudience = result.ExternalAudience,
				ExternalMessage = result.ExternalMessage,
				InternalMessage = result.InternalMessage,
				StartTime = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime((ExDateTime)result.StartTime)
			};
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x000F7FD3 File Offset: 0x000F61D3
		protected override PSLocalTask<GetMailboxAutoReplyConfiguration, MailboxAutoReplyConfiguration> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetMailboxAutoReplyConfigurationTask(base.CallContext.AccessingPrincipal);
		}
	}
}
