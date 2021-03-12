using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000820 RID: 2080
	internal sealed class NonExistentMailboxException : ServicePermanentException
	{
		// Token: 0x06003C3F RID: 15423 RVA: 0x000D5A98 File Offset: 0x000D3C98
		public NonExistentMailboxException(Enum messageId, string mailbox) : base(ResponseCodeType.ErrorNonExistentMailbox, messageId)
		{
			CoreResources.IDs ds = (CoreResources.IDs)messageId;
			if (ds == (CoreResources.IDs)3279543955U || ds == (CoreResources.IDs)4074099229U)
			{
				base.ConstantValues.Add("MailboxGuid", mailbox);
				return;
			}
			if (ds != (CoreResources.IDs)4088802584U)
			{
				return;
			}
			base.ConstantValues.Add("SmtpAddress", mailbox);
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06003C40 RID: 15424 RVA: 0x000D5AF4 File Offset: 0x000D3CF4
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}

		// Token: 0x04002148 RID: 8520
		private const string SmtpAddressKey = "SmtpAddress";

		// Token: 0x04002149 RID: 8521
		private const string MailboxGuid = "MailboxGuid";
	}
}
