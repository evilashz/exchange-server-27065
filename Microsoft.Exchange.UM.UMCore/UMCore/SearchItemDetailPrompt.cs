using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000103 RID: 259
	internal class SearchItemDetailPrompt : VariablePrompt<ContactSearchItem>
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x0001D004 File Offset: 0x0001B204
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"searchItemDetail",
				base.Config.PromptName,
				string.Empty,
				base.SbLog.ToString()
			});
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001D054 File Offset: 0x0001B254
		internal override string ToSsml()
		{
			return base.SbSsml.ToString();
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001D064 File Offset: 0x0001B264
		protected override void InternalInitialize()
		{
			this.item = base.InitVal;
			if (this.item.IsPersonalContact && this.item.IsGroup)
			{
				this.AddGroupDetailPrompts();
			}
			else
			{
				this.AddContactDetailPrompts();
			}
			if (base.SbSsml.Length == 0)
			{
				this.AddNoDetailsPrompt();
				return;
			}
			this.InsertDetailIntroPromptToBeginning();
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001D0C0 File Offset: 0x0001B2C0
		private void AddContactDetailPrompts()
		{
			if (this.item.HasBusinessAddress)
			{
				AddressPrompt paramPrompt = new AddressPrompt("businessAddress", base.Culture, this.item.BusinessAddress.ToString(base.Culture));
				this.AddStatementPrompt("tuiContactsBusinessAddress", base.Culture, paramPrompt);
			}
			if (this.item.HasHomeAddress)
			{
				AddressPrompt paramPrompt2 = new AddressPrompt("homeAddress", base.Culture, this.item.HomeAddress);
				this.AddStatementPrompt("tuiContactsHomeAddress", base.Culture, paramPrompt2);
			}
			if (this.item.HasOtherAddress)
			{
				AddressPrompt paramPrompt3 = new AddressPrompt("otherAddress", base.Culture, this.item.OtherAddress);
				this.AddStatementPrompt("tuiContactsOtherAddress", base.Culture, paramPrompt3);
			}
			if (this.item.HasMobileNumber)
			{
				TelephoneNumberPrompt paramPrompt4 = new TelephoneNumberPrompt("mobileNumber", base.Culture, this.item.MobilePhone);
				this.AddStatementPrompt("tuiContactsMobilePhoneNumber", base.Culture, paramPrompt4);
			}
			if (this.item.HasBusinessNumber)
			{
				TelephoneNumberPrompt paramPrompt5 = new TelephoneNumberPrompt("businessNumber", base.Culture, this.item.BusinessPhone);
				this.AddStatementPrompt("tuiContactsBusinessPhoneNumber", base.Culture, paramPrompt5);
			}
			if (this.item.HasHomeNumber)
			{
				TelephoneNumberPrompt paramPrompt6 = new TelephoneNumberPrompt("homeNumber", base.Culture, this.item.HomePhone);
				this.AddStatementPrompt("tuiContactsHomePhoneNumber", base.Culture, paramPrompt6);
			}
			if (this.item.HasAlias)
			{
				EmailPrompt paramPrompt7 = new EmailPrompt("alias", base.Culture, new EmailNormalizedText(this.item.Alias));
				this.AddStatementPrompt("tuiContactsEmailAlias", base.Culture, paramPrompt7);
			}
			if (this.item.HasEmail1)
			{
				EmailPrompt paramPrompt8 = new EmailPrompt("email1", base.Culture, new EmailNormalizedText(this.item.ContactEmailAddresses[0]));
				this.AddStatementPrompt("tuiContactsEmail1", base.Culture, paramPrompt8);
			}
			if (this.item.HasEmail2)
			{
				EmailPrompt paramPrompt9 = new EmailPrompt("email2", base.Culture, new EmailNormalizedText(this.item.ContactEmailAddresses[1]));
				this.AddStatementPrompt("tuiContactsEmail2", base.Culture, paramPrompt9);
			}
			if (this.item.HasEmail3)
			{
				EmailPrompt paramPrompt10 = new EmailPrompt("email3", base.Culture, new EmailNormalizedText(this.item.ContactEmailAddresses[2]));
				this.AddStatementPrompt("tuiContactsEmail3", base.Culture, paramPrompt10);
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001D35C File Offset: 0x0001B55C
		private void AddGroupDetailPrompts()
		{
			this.AddStatementPrompt("tuiGroupDetails", base.Culture, null);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001D370 File Offset: 0x0001B570
		private void InsertDetailIntroPromptToBeginning()
		{
			SpokenNamePrompt spokenNamePrompt = new SpokenNamePrompt("userName", base.Culture, this.item.FullName);
			SingleStatementPrompt singleStatementPrompt = PromptUtils.CreateSingleStatementPrompt("tuiContactsDetailsIntro", base.Culture, new Prompt[]
			{
				spokenNamePrompt
			});
			base.SbSsml.Insert(0, singleStatementPrompt.ToSsml());
			base.SbLog.Insert(0, singleStatementPrompt.ToString());
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001D3DC File Offset: 0x0001B5DC
		private void AddNoDetailsPrompt()
		{
			SpokenNamePrompt paramPrompt = new SpokenNamePrompt("userName", base.Culture, this.item.FullName);
			this.AddStatementPrompt("tuiContactsNoDetails", base.Culture, paramPrompt);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001D418 File Offset: 0x0001B618
		private void AddStatementPrompt(string promptName, CultureInfo culture, Prompt paramPrompt)
		{
			SingleStatementPrompt p = PromptUtils.CreateSingleStatementPrompt(promptName, culture, new Prompt[]
			{
				paramPrompt
			});
			base.AddPrompt(p);
		}

		// Token: 0x040007FD RID: 2045
		private const string GroupDetailsStatementPromptName = "tuiGroupDetails";

		// Token: 0x040007FE RID: 2046
		private const string DetailsIntroStatementPromptName = "tuiContactsDetailsIntro";

		// Token: 0x040007FF RID: 2047
		private const string NoDetailsStatementPromptName = "tuiContactsNoDetails";

		// Token: 0x04000800 RID: 2048
		private const string UserNamePromptName = "userName";

		// Token: 0x04000801 RID: 2049
		private const string BusinessAddressStatementPromptName = "tuiContactsBusinessAddress";

		// Token: 0x04000802 RID: 2050
		private const string BusinessAddressPromptName = "businessAddress";

		// Token: 0x04000803 RID: 2051
		private const string HomeAddressStatementPromptName = "tuiContactsHomeAddress";

		// Token: 0x04000804 RID: 2052
		private const string HomeAddressPromptName = "homeAddress";

		// Token: 0x04000805 RID: 2053
		private const string OtherAddressStatementPromptName = "tuiContactsOtherAddress";

		// Token: 0x04000806 RID: 2054
		private const string OtherAddressPromptName = "otherAddress";

		// Token: 0x04000807 RID: 2055
		private const string MobileNumberStatementPromptName = "tuiContactsMobilePhoneNumber";

		// Token: 0x04000808 RID: 2056
		private const string MobileNumberPromptName = "mobileNumber";

		// Token: 0x04000809 RID: 2057
		private const string BusinessNumberStatementPromptName = "tuiContactsBusinessPhoneNumber";

		// Token: 0x0400080A RID: 2058
		private const string BusinessNumberPromptName = "businessNumber";

		// Token: 0x0400080B RID: 2059
		private const string HomeNumberStatementPromptName = "tuiContactsHomePhoneNumber";

		// Token: 0x0400080C RID: 2060
		private const string HomeNumberPromptName = "homeNumber";

		// Token: 0x0400080D RID: 2061
		private const string Email1StatementPromptName = "tuiContactsEmail1";

		// Token: 0x0400080E RID: 2062
		private const string Email1PromptName = "email1";

		// Token: 0x0400080F RID: 2063
		private const string Email2StatementPromptName = "tuiContactsEmail2";

		// Token: 0x04000810 RID: 2064
		private const string Email2PromptName = "email2";

		// Token: 0x04000811 RID: 2065
		private const string Email3StatementPromptName = "tuiContactsEmail3";

		// Token: 0x04000812 RID: 2066
		private const string Email3PromptName = "email3";

		// Token: 0x04000813 RID: 2067
		private const string OfficeNumberStatementPromptName = "tuiContactsOfficePhoneNumber";

		// Token: 0x04000814 RID: 2068
		private const string LocationStatementPromptName = "tuiContactsLocation";

		// Token: 0x04000815 RID: 2069
		private const string EmailAliasStatementPromptName = "tuiContactsEmailAlias";

		// Token: 0x04000816 RID: 2070
		private const string EmailAliasPromptName = "alias";

		// Token: 0x04000817 RID: 2071
		private ContactSearchItem item;
	}
}
