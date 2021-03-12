using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage.Approval;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000063 RID: 99
	internal class AutoGroupApprovalRequestWriter : ApprovalRequestWriter
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x00010896 File Offset: 0x0000EA96
		private AutoGroupApprovalRequestWriter(InitiationMessage initiationMessage)
		{
			this.initiationMessage = initiationMessage;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000108A8 File Offset: 0x0000EAA8
		public override bool WriteSubjectAndBody(MessageItemApprovalRequest approvalRequest, CultureInfo cultureInfo, out CultureInfo cultureInfoWritten)
		{
			approvalRequest.MessageItem.Subject = this.initiationMessage.Subject;
			cultureInfoWritten = null;
			int? messageItemLocale = this.initiationMessage.MessageItemLocale;
			string address = (string)this.initiationMessage.Requestor;
			string approvalData = this.initiationMessage.ApprovalData;
			if (string.IsNullOrEmpty(approvalData))
			{
				return false;
			}
			Culture culture = null;
			if (messageItemLocale != null && Culture.TryGetCulture(messageItemLocale.Value, out culture))
			{
				cultureInfoWritten = culture.GetCultureInfo();
			}
			else
			{
				cultureInfoWritten = cultureInfo;
			}
			string displayNameFromSmtpAddress = ApprovalProcessor.GetDisplayNameFromSmtpAddress(address);
			string group = ApprovalProcessor.ResolveDisplayNameForDistributionGroupFromApprovalData(approvalData, ApprovalProcessor.CreateRecipientSessionFromSmtpAddress(address));
			string body = ApprovalProcessor.GenerateMessageBodyForRequestMessage(Strings.AutoGroupRequestHeader(displayNameFromSmtpAddress, group), Strings.AutoGroupRequestBody, LocalizedString.Empty, cultureInfoWritten);
			approvalRequest.SetBody(body);
			return true;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00010964 File Offset: 0x0000EB64
		internal static AutoGroupApprovalRequestWriter GetInstance(InitiationMessage initiationMessage)
		{
			return new AutoGroupApprovalRequestWriter(initiationMessage);
		}

		// Token: 0x040001FD RID: 509
		private InitiationMessage initiationMessage;
	}
}
