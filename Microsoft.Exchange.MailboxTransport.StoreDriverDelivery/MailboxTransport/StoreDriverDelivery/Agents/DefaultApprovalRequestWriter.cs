using System;
using System.Globalization;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000065 RID: 101
	internal class DefaultApprovalRequestWriter : ApprovalRequestWriter
	{
		// Token: 0x060003CF RID: 975 RVA: 0x00010C96 File Offset: 0x0000EE96
		private DefaultApprovalRequestWriter(InitiationMessage initiationMessage)
		{
			this.initiationMessage = initiationMessage;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00010CA5 File Offset: 0x0000EEA5
		public override bool WriteSubjectAndBody(MessageItemApprovalRequest approvalRequest, CultureInfo cultureInfo, out CultureInfo cultureInfoWritten)
		{
			cultureInfoWritten = null;
			approvalRequest.MessageItem.Subject = this.initiationMessage.Subject;
			approvalRequest.SetBody(this.initiationMessage.EmailMessage.Body);
			return true;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00010CD7 File Offset: 0x0000EED7
		internal static DefaultApprovalRequestWriter GetInstance(InitiationMessage initiationMessage)
		{
			return new DefaultApprovalRequestWriter(initiationMessage);
		}

		// Token: 0x04000203 RID: 515
		private InitiationMessage initiationMessage;
	}
}
