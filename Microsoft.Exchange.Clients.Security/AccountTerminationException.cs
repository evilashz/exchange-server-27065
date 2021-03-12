using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200001E RID: 30
	public class AccountTerminationException : LocalizedException
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003479 File Offset: 0x00001679
		public AccountTerminationException(AccountState accountState) : base(new LocalizedString(null), null)
		{
			this.AccountState = accountState;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000348F File Offset: 0x0000168F
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003497 File Offset: 0x00001697
		public AccountState AccountState { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000034A0 File Offset: 0x000016A0
		public override string Message
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, Strings.AccountTerminationErrorMessage, new object[]
				{
					this.AccountState.ToString()
				});
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000034D7 File Offset: 0x000016D7
		public Strings.IDs ErrorMessageStringId
		{
			get
			{
				return -4727748;
			}
		}
	}
}
