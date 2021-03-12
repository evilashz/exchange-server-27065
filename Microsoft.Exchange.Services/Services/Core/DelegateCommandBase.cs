using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000293 RID: 659
	internal abstract class DelegateCommandBase<RequestType> : MultiStepServiceCommand<RequestType, DelegateUserType> where RequestType : BaseDelegateRequest
	{
		// Token: 0x0600119D RID: 4509 RVA: 0x000557E1 File Offset: 0x000539E1
		public DelegateCommandBase(CallContext callContext, RequestType request) : base(callContext, request)
		{
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000557EB File Offset: 0x000539EB
		internal static void ValidateEmailAddress(string emailAddress)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				throw new MissingInformationEmailAddressException((CoreResources.IDs)2555117076U);
			}
			Util.ValidateSmtpAddress(emailAddress);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0005580B File Offset: 0x00053A0B
		internal override void PreExecuteCommand()
		{
			this.InitializeXsoDelegateUsers();
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00055814 File Offset: 0x00053A14
		protected void InitializeXsoDelegateUsers()
		{
			RequestType request = base.Request;
			string emailAddress = request.Mailbox.EmailAddress;
			DelegateCommandBase<RequestType>.ValidateEmailAddress(emailAddress);
			MailboxSession mailboxSession = base.GetMailboxSession(emailAddress);
			this.xsoDelegateUsers = new DelegateUserCollection(mailboxSession);
		}

		// Token: 0x04000CB9 RID: 3257
		protected DelegateUserCollection xsoDelegateUsers;
	}
}
