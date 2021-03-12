using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A10 RID: 2576
	[Serializable]
	public class MailboxSpellingConfiguration : UserConfigurationObject
	{
		// Token: 0x170019EF RID: 6639
		// (get) Token: 0x06005E93 RID: 24211 RVA: 0x0018FBCD File Offset: 0x0018DDCD
		internal override UserConfigurationObjectSchema Schema
		{
			get
			{
				return MailboxSpellingConfiguration.schema;
			}
		}

		// Token: 0x170019F0 RID: 6640
		// (get) Token: 0x06005E95 RID: 24213 RVA: 0x0018FBDC File Offset: 0x0018DDDC
		// (set) Token: 0x06005E96 RID: 24214 RVA: 0x0018FBEE File Offset: 0x0018DDEE
		[Parameter(Mandatory = false)]
		public bool CheckBeforeSend
		{
			get
			{
				return (bool)this[MailboxSpellingConfigurationSchema.CheckBeforeSend];
			}
			set
			{
				this[MailboxSpellingConfigurationSchema.CheckBeforeSend] = value;
			}
		}

		// Token: 0x170019F1 RID: 6641
		// (get) Token: 0x06005E97 RID: 24215 RVA: 0x0018FC01 File Offset: 0x0018DE01
		// (set) Token: 0x06005E98 RID: 24216 RVA: 0x0018FC13 File Offset: 0x0018DE13
		[Parameter(Mandatory = false)]
		public SpellcheckerSupportedLanguage DictionaryLanguage
		{
			get
			{
				return (SpellcheckerSupportedLanguage)this[MailboxSpellingConfigurationSchema.DictionaryLanguage];
			}
			set
			{
				this[MailboxSpellingConfigurationSchema.DictionaryLanguage] = value;
			}
		}

		// Token: 0x170019F2 RID: 6642
		// (get) Token: 0x06005E99 RID: 24217 RVA: 0x0018FC26 File Offset: 0x0018DE26
		// (set) Token: 0x06005E9A RID: 24218 RVA: 0x0018FC38 File Offset: 0x0018DE38
		[Parameter(Mandatory = false)]
		public bool IgnoreUppercase
		{
			get
			{
				return (bool)this[MailboxSpellingConfigurationSchema.IgnoreUppercase];
			}
			set
			{
				this[MailboxSpellingConfigurationSchema.IgnoreUppercase] = value;
			}
		}

		// Token: 0x170019F3 RID: 6643
		// (get) Token: 0x06005E9B RID: 24219 RVA: 0x0018FC4B File Offset: 0x0018DE4B
		// (set) Token: 0x06005E9C RID: 24220 RVA: 0x0018FC5D File Offset: 0x0018DE5D
		[Parameter(Mandatory = false)]
		public bool IgnoreMixedDigits
		{
			get
			{
				return (bool)this[MailboxSpellingConfigurationSchema.IgnoreMixedDigits];
			}
			set
			{
				this[MailboxSpellingConfigurationSchema.IgnoreMixedDigits] = value;
			}
		}

		// Token: 0x06005E9D RID: 24221 RVA: 0x0018FC70 File Offset: 0x0018DE70
		public override IConfigurable Read(MailboxStoreTypeProvider session, ObjectId identity)
		{
			base.Principal = ExchangePrincipal.FromADUser(session.ADUser, null);
			IConfigurable result;
			using (UserConfigurationDictionaryAdapter<MailboxSpellingConfiguration> userConfigurationDictionaryAdapter = new UserConfigurationDictionaryAdapter<MailboxSpellingConfiguration>(session.MailboxSession, "OWA.UserOptions", new GetUserConfigurationDelegate(UserConfigurationHelper.GetMailboxConfiguration), MailboxSpellingConfiguration.mailboxProperties))
			{
				result = userConfigurationDictionaryAdapter.Read(base.Principal);
			}
			return result;
		}

		// Token: 0x06005E9E RID: 24222 RVA: 0x0018FCDC File Offset: 0x0018DEDC
		public override void Save(MailboxStoreTypeProvider session)
		{
			using (UserConfigurationDictionaryAdapter<MailboxSpellingConfiguration> userConfigurationDictionaryAdapter = new UserConfigurationDictionaryAdapter<MailboxSpellingConfiguration>(session.MailboxSession, "OWA.UserOptions", new GetUserConfigurationDelegate(UserConfigurationHelper.GetMailboxConfiguration), MailboxSpellingConfiguration.mailboxProperties))
			{
				userConfigurationDictionaryAdapter.Save(this);
			}
			base.ResetChangeTracking();
		}

		// Token: 0x040034AE RID: 13486
		private static MailboxSpellingConfigurationSchema schema = ObjectSchema.GetInstance<MailboxSpellingConfigurationSchema>();

		// Token: 0x040034AF RID: 13487
		private static SimplePropertyDefinition[] mailboxProperties = new SimplePropertyDefinition[]
		{
			MailboxSpellingConfigurationSchema.CheckBeforeSend,
			MailboxSpellingConfigurationSchema.DictionaryLanguage,
			MailboxSpellingConfigurationSchema.IgnoreMixedDigits,
			MailboxSpellingConfigurationSchema.IgnoreUppercase
		};
	}
}
