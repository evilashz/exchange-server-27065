using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000039 RID: 57
	internal class UMUserMobileAccount
	{
		// Token: 0x060002CF RID: 719 RVA: 0x0000B10E File Offset: 0x0000930E
		private UMUserMobileAccount(ADUser user)
		{
			this.user = user;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000B120 File Offset: 0x00009320
		public static bool TryCreateUMUserMobileAccount(ExchangePrincipal userExchangePrincipal, out UMUserMobileAccount account)
		{
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(userExchangePrincipal.MailboxInfo.OrganizationId, null, null, false);
			ADRecipient adrecipient = iadrecipientLookup.LookupByExchangePrincipal(userExchangePrincipal);
			ADUser aduser = adrecipient as ADUser;
			if (aduser != null && aduser.UMEnabled)
			{
				account = new UMUserMobileAccount(aduser);
				return true;
			}
			account = null;
			return false;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000B16C File Offset: 0x0000936C
		public bool TryRegisterNumber(PhoneNumber telephoneNumber, out TelephoneNumberProcessStatus status)
		{
			bool result = AirSyncUtils.AddAirSyncPhoneNumber(this.user, telephoneNumber.ToDial, out status);
			if (status == TelephoneNumberProcessStatus.Success)
			{
				this.user.Session.Save(this.user);
			}
			return result;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000B1A8 File Offset: 0x000093A8
		public bool TryDeregisterNumber(PhoneNumber telephoneNumber, out TelephoneNumberProcessStatus status)
		{
			bool result = AirSyncUtils.RemoveAirSyncPhoneNumber(this.user, telephoneNumber.ToDial, out status);
			if (status == TelephoneNumberProcessStatus.Success)
			{
				this.user.Session.Save(this.user);
			}
			return result;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000B1E3 File Offset: 0x000093E3
		public bool TryGetTelephonyInfo(PhoneNumber number, out TelephonyInfo ti)
		{
			return AirSyncUtils.GetTelephonyInfo(this.user, number.ToDial, out ti);
		}

		// Token: 0x040000E8 RID: 232
		private ADUser user;
	}
}
