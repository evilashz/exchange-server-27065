using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009A2 RID: 2466
	internal sealed class GetMailboxMessageConfigurationCommand : SingleCmdletCommandBase<object, GetMailboxMessageConfigurationResponse, GetMailboxMessageConfiguration, MailboxMessageConfiguration>
	{
		// Token: 0x0600464C RID: 17996 RVA: 0x000F821C File Offset: 0x000F641C
		public GetMailboxMessageConfigurationCommand(CallContext callContext) : base(callContext, null, "Get-MailboxMessageConfiguration", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x000F822C File Offset: 0x000F642C
		protected override void PopulateTaskParameters()
		{
			PSLocalTask<GetMailboxMessageConfiguration, MailboxMessageConfiguration> taskWrapper = this.cmdletRunner.TaskWrapper;
			this.cmdletRunner.SetTaskParameter("Identity", taskWrapper.Task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x000F8270 File Offset: 0x000F6470
		protected override void PopulateResponseData(GetMailboxMessageConfigurationResponse response)
		{
			PSLocalTask<GetMailboxMessageConfiguration, MailboxMessageConfiguration> taskWrapper = this.cmdletRunner.TaskWrapper;
			MailboxMessageConfiguration result = taskWrapper.Result;
			response.Options = new MailboxMessageConfigurationOptions
			{
				AfterMoveOrDeleteBehavior = result.AfterMoveOrDeleteBehavior,
				AlwaysShowBcc = result.AlwaysShowBcc,
				AlwaysShowFrom = result.AlwaysShowFrom,
				AutoAddSignature = result.AutoAddSignature,
				AutoAddSignatureOnMobile = result.AutoAddSignatureOnMobile,
				CheckForForgottenAttachments = result.CheckForForgottenAttachments,
				ConversationSortOrder = result.ConversationSortOrder,
				DefaultFontColor = result.DefaultFontColor,
				DefaultFontFlags = result.DefaultFontFlags,
				DefaultFontName = result.DefaultFontName,
				DefaultFontSize = result.DefaultFontSize,
				DefaultFormat = result.DefaultFormat,
				EmailComposeMode = result.EmailComposeMode,
				EmptyDeletedItemsOnLogoff = result.EmptyDeletedItemsOnLogoff,
				HideDeletedItems = result.HideDeletedItems,
				NewItemNotification = result.NewItemNotification,
				PreviewMarkAsReadBehavior = result.PreviewMarkAsReadBehavior,
				PreviewMarkAsReadDelaytime = result.PreviewMarkAsReadDelaytime,
				ReadReceiptResponse = result.ReadReceiptResponse,
				ShowConversationAsTree = result.ShowConversationAsTree,
				SendAddressDefault = result.SendAddressDefault,
				SignatureHtml = result.SignatureHtml,
				SignatureText = result.SignatureText,
				SignatureTextOnMobile = result.SignatureTextOnMobile,
				UseDefaultSignatureOnMobile = result.UseDefaultSignatureOnMobile
			};
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x000F83C9 File Offset: 0x000F65C9
		protected override PSLocalTask<GetMailboxMessageConfiguration, MailboxMessageConfiguration> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetMailboxMessageConfigurationTask(base.CallContext.AccessingPrincipal);
		}
	}
}
