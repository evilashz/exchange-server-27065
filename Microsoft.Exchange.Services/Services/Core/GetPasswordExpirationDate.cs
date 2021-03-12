using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200031B RID: 795
	internal sealed class GetPasswordExpirationDate : SingleStepServiceCommand<GetPasswordExpirationDateRequest, DateTime>
	{
		// Token: 0x06001684 RID: 5764 RVA: 0x000763D0 File Offset: 0x000745D0
		public GetPasswordExpirationDate(CallContext callContext, GetPasswordExpirationDateRequest request) : base(callContext, request)
		{
			this.mailboxSmtpAddress = request.MailboxSmtpAddress;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x000763E6 File Offset: 0x000745E6
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetPasswordExpirationDateResponse(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00076410 File Offset: 0x00074610
		internal override ServiceResult<DateTime> Execute()
		{
			ExchangePrincipal principal = GetPasswordExpirationDate.GetPrincipal(base.CallContext, this.mailboxSmtpAddress);
			DateTime value = DateTime.MaxValue;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Ews.UserPasswordExpirationDate.Enabled)
			{
				if (principal != base.CallContext.AccessingPrincipal)
				{
					throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
				}
				CommonAccessToken commonAccessToken = base.CallContext.HttpContext.Items["Item-CommonAccessToken"] as CommonAccessToken;
				if (commonAccessToken == null || commonAccessToken.TokenType != AccessTokenType.LiveIdBasic.ToString())
				{
					throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
				}
				string text = null;
				if (commonAccessToken.ExtensionData.ContainsKey("PasswordExpiry"))
				{
					text = commonAccessToken.ExtensionData["PasswordExpiry"];
				}
				uint num;
				if (!string.IsNullOrEmpty(text) && uint.TryParse(text, out num))
				{
					value = DateTime.UtcNow.AddDays(num);
				}
			}
			else if (principal.MasterAccountSid == null)
			{
				value = DirectoryHelper.GetPasswordExpirationDate(principal.ObjectId, base.MailboxIdentityMailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid)).UniversalTime;
			}
			return new ServiceResult<DateTime>(value);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00076540 File Offset: 0x00074740
		private static ExchangePrincipal GetPrincipal(CallContext callContext, string smtpAddress)
		{
			ExchangePrincipal exchangePrincipal;
			if (!string.IsNullOrEmpty(smtpAddress))
			{
				exchangePrincipal = ExchangePrincipalCache.GetFromCache(smtpAddress, callContext.ADRecipientSessionContext);
			}
			else
			{
				exchangePrincipal = callContext.AccessingPrincipal;
				if (exchangePrincipal == null)
				{
					throw new NonExistentMailboxException((CoreResources.IDs)4088802584U, string.Empty);
				}
			}
			return exchangePrincipal;
		}

		// Token: 0x04000F22 RID: 3874
		private string mailboxSmtpAddress;
	}
}
