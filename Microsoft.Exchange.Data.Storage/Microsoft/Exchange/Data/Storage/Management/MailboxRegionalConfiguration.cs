using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A0E RID: 2574
	[Serializable]
	public class MailboxRegionalConfiguration : UserConfigurationObject
	{
		// Token: 0x170019E9 RID: 6633
		// (get) Token: 0x06005E75 RID: 24181 RVA: 0x0018F4AB File Offset: 0x0018D6AB
		internal override UserConfigurationObjectSchema Schema
		{
			get
			{
				return MailboxRegionalConfiguration.schema;
			}
		}

		// Token: 0x170019EA RID: 6634
		// (get) Token: 0x06005E77 RID: 24183 RVA: 0x0018F4BA File Offset: 0x0018D6BA
		// (set) Token: 0x06005E78 RID: 24184 RVA: 0x0018F4CC File Offset: 0x0018D6CC
		[Parameter(Mandatory = false)]
		public string DateFormat
		{
			get
			{
				return (string)this[MailboxRegionalConfigurationSchema.DateFormat];
			}
			set
			{
				this[MailboxRegionalConfigurationSchema.DateFormat] = value;
			}
		}

		// Token: 0x170019EB RID: 6635
		// (get) Token: 0x06005E79 RID: 24185 RVA: 0x0018F4DA File Offset: 0x0018D6DA
		// (set) Token: 0x06005E7A RID: 24186 RVA: 0x0018F4EC File Offset: 0x0018D6EC
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
		public CultureInfo Language
		{
			get
			{
				return (CultureInfo)this[MailboxRegionalConfigurationSchema.Language];
			}
			set
			{
				this[MailboxRegionalConfigurationSchema.Language] = value;
			}
		}

		// Token: 0x170019EC RID: 6636
		// (get) Token: 0x06005E7B RID: 24187 RVA: 0x0018F4FA File Offset: 0x0018D6FA
		// (set) Token: 0x06005E7C RID: 24188 RVA: 0x0018F50C File Offset: 0x0018D70C
		public bool DefaultFolderNameMatchingUserLanguage
		{
			get
			{
				return (bool)this[MailboxRegionalConfigurationSchema.DefaultFolderNameMatchingUserLanguage];
			}
			internal set
			{
				this[MailboxRegionalConfigurationSchema.DefaultFolderNameMatchingUserLanguage] = value;
			}
		}

		// Token: 0x170019ED RID: 6637
		// (get) Token: 0x06005E7D RID: 24189 RVA: 0x0018F51F File Offset: 0x0018D71F
		// (set) Token: 0x06005E7E RID: 24190 RVA: 0x0018F531 File Offset: 0x0018D731
		[Parameter(Mandatory = false)]
		public string TimeFormat
		{
			get
			{
				return (string)this[MailboxRegionalConfigurationSchema.TimeFormat];
			}
			set
			{
				this[MailboxRegionalConfigurationSchema.TimeFormat] = value;
			}
		}

		// Token: 0x170019EE RID: 6638
		// (get) Token: 0x06005E7F RID: 24191 RVA: 0x0018F53F File Offset: 0x0018D73F
		// (set) Token: 0x06005E80 RID: 24192 RVA: 0x0018F551 File Offset: 0x0018D751
		[Parameter(Mandatory = false)]
		public ExTimeZoneValue TimeZone
		{
			get
			{
				return (ExTimeZoneValue)this[MailboxRegionalConfigurationSchema.TimeZone];
			}
			set
			{
				this[MailboxRegionalConfigurationSchema.TimeZone] = value;
			}
		}

		// Token: 0x06005E81 RID: 24193 RVA: 0x0018F55F File Offset: 0x0018D75F
		public static string GetDefaultDateFormat(CultureInfo language)
		{
			return MailboxRegionalConfiguration.GetDateTimeInfo(language).ShortDatePattern;
		}

		// Token: 0x06005E82 RID: 24194 RVA: 0x0018F56C File Offset: 0x0018D76C
		public static string GetDefaultTimeFormat(CultureInfo language)
		{
			return MailboxRegionalConfiguration.GetDateTimeInfo(language).ShortTimePattern;
		}

		// Token: 0x06005E83 RID: 24195 RVA: 0x0018F57C File Offset: 0x0018D77C
		public static bool ValidateDateFormat(CultureInfo language, object dateFormat, out string defaultFormat)
		{
			DateTimeFormatInfo dateTimeInfo = MailboxRegionalConfiguration.GetDateTimeInfo(language);
			defaultFormat = dateTimeInfo.ShortDatePattern;
			string[] array;
			return dateFormat != null && dateFormat is string && MailboxRegionalConfiguration.ValidateFormat(dateTimeInfo, (string)dateFormat, 'd', out array);
		}

		// Token: 0x06005E84 RID: 24196 RVA: 0x0018F5B8 File Offset: 0x0018D7B8
		public static bool ValidateTimeFormat(CultureInfo language, object timeFormat, out string defaultFormat)
		{
			DateTimeFormatInfo dateTimeInfo = MailboxRegionalConfiguration.GetDateTimeInfo(language);
			defaultFormat = dateTimeInfo.ShortTimePattern;
			string[] array;
			return timeFormat != null && timeFormat is string && MailboxRegionalConfiguration.ValidateFormat(dateTimeInfo, (string)timeFormat, 't', out array);
		}

		// Token: 0x06005E85 RID: 24197 RVA: 0x0018F5F4 File Offset: 0x0018D7F4
		internal static DateTimeFormatInfo GetDateTimeInfo(CultureInfo language)
		{
			return new CultureInfo(language.LCID)
			{
				DateTimeFormat = 
				{
					Calendar = new GregorianCalendar()
				}
			}.DateTimeFormat;
		}

		// Token: 0x06005E86 RID: 24198 RVA: 0x0018F640 File Offset: 0x0018D840
		internal static bool ValidateFormat(DateTimeFormatInfo dateTimeFormatInfo, string format, char dateTimePatternSelector, out string[] validFormats)
		{
			validFormats = dateTimeFormatInfo.GetAllDateTimePatterns(dateTimePatternSelector);
			return Array.Exists<string>(validFormats, (string value) => value == format.Trim());
		}

		// Token: 0x06005E87 RID: 24199 RVA: 0x0018F678 File Offset: 0x0018D878
		internal static object DateFormatGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[MailboxRegionalConfigurationSchema.RawDateFormat];
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			CultureInfo cultureInfo = (CultureInfo)propertyBag[MailboxRegionalConfigurationSchema.Language];
			if (cultureInfo != null)
			{
				return cultureInfo.DateTimeFormat.ShortDatePattern;
			}
			return null;
		}

		// Token: 0x06005E88 RID: 24200 RVA: 0x0018F6C1 File Offset: 0x0018D8C1
		internal static void DateFormatSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[MailboxRegionalConfigurationSchema.RawDateFormat] = value;
		}

		// Token: 0x06005E89 RID: 24201 RVA: 0x0018F6D0 File Offset: 0x0018D8D0
		internal static object TimeFormatGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[MailboxRegionalConfigurationSchema.RawTimeFormat];
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			CultureInfo cultureInfo = (CultureInfo)propertyBag[MailboxRegionalConfigurationSchema.Language];
			if (cultureInfo != null)
			{
				return cultureInfo.DateTimeFormat.ShortTimePattern;
			}
			return null;
		}

		// Token: 0x06005E8A RID: 24202 RVA: 0x0018F719 File Offset: 0x0018D919
		internal static void TimeFormatSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[MailboxRegionalConfigurationSchema.RawTimeFormat] = value;
		}

		// Token: 0x06005E8B RID: 24203 RVA: 0x0018F728 File Offset: 0x0018D928
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (base.IsModified(MailboxRegionalConfigurationSchema.Language) && this.Language == null)
			{
				errors.Add(new ObjectValidationError(ServerStrings.ErrorLanguageIsNull, this.Identity, string.Empty));
			}
			if (base.IsModified(MailboxRegionalConfigurationSchema.Language) || base.IsModified(MailboxRegionalConfigurationSchema.DateFormat) || base.IsModified(MailboxRegionalConfigurationSchema.TimeFormat))
			{
				if (this.Language != null)
				{
					this.Language.DateTimeFormat.Calendar = new GregorianCalendar();
					string[] value;
					if (!MailboxRegionalConfiguration.ValidateFormat(this.Language.DateTimeFormat, this.DateFormat, 'd', out value))
					{
						errors.Add(new ObjectValidationError(ServerStrings.ErrorInvalidDateFormat(this.DateFormat, this.Language.ToString(), string.Join(", ", value)), this.Identity, string.Empty));
					}
					string[] value2;
					if (!MailboxRegionalConfiguration.ValidateFormat(this.Language.DateTimeFormat, this.TimeFormat, 't', out value2))
					{
						errors.Add(new ObjectValidationError(ServerStrings.ErrorInvalidTimeFormat(this.TimeFormat, this.Language.ToString(), string.Join(", ", value2)), this.Identity, string.Empty));
						return;
					}
				}
				else if (!string.IsNullOrEmpty(this.DateFormat) || !string.IsNullOrEmpty(this.TimeFormat))
				{
					errors.Add(new ObjectValidationError(ServerStrings.ErrorSetDateTimeFormatWithoutLanguage, this.Identity, string.Empty));
				}
			}
		}

		// Token: 0x06005E8C RID: 24204 RVA: 0x0018F894 File Offset: 0x0018DA94
		public override IConfigurable Read(MailboxStoreTypeProvider session, ObjectId identity)
		{
			base.Principal = ExchangePrincipal.FromADUser(session.ADUser, null);
			using (UserConfigurationDictionaryAdapter<MailboxRegionalConfiguration> userConfigurationDictionaryAdapter = new UserConfigurationDictionaryAdapter<MailboxRegionalConfiguration>(session.MailboxSession, "OWA.UserOptions", new GetUserConfigurationDelegate(UserConfigurationHelper.GetMailboxConfiguration), MailboxRegionalConfiguration.mailboxProperties))
			{
				userConfigurationDictionaryAdapter.Fill(this);
			}
			if (base.Principal.PreferredCultures.Any<CultureInfo>())
			{
				this.Language = base.Principal.PreferredCultures.First<CultureInfo>();
			}
			return this;
		}

		// Token: 0x06005E8D RID: 24205 RVA: 0x0018F924 File Offset: 0x0018DB24
		public override void Save(MailboxStoreTypeProvider session)
		{
			using (UserConfigurationDictionaryAdapter<MailboxRegionalConfiguration> userConfigurationDictionaryAdapter = new UserConfigurationDictionaryAdapter<MailboxRegionalConfiguration>(session.MailboxSession, "OWA.UserOptions", new GetUserConfigurationDelegate(UserConfigurationHelper.GetMailboxConfiguration), MailboxRegionalConfiguration.mailboxProperties))
			{
				userConfigurationDictionaryAdapter.Save(this);
			}
			if (base.IsModified(MailboxRegionalConfigurationSchema.Language) && this.Language != null)
			{
				this.SaveCultures(session.MailboxSession.GetADRecipientSession(false, ConsistencyMode.FullyConsistent));
			}
			base.ResetChangeTracking();
		}

		// Token: 0x06005E8E RID: 24206 RVA: 0x0018F9A8 File Offset: 0x0018DBA8
		private void SaveCultures(IRecipientSession adRecipientSession)
		{
			PreferredCultures preferredCultures = new PreferredCultures(base.Principal.PreferredCultures);
			preferredCultures.AddSupportedCulture(this.Language, (CultureInfo culture) => true);
			ADUser aduser = adRecipientSession.Read(base.Principal.ObjectId) as ADUser;
			if (aduser != null)
			{
				aduser.Languages.Clear();
				Util.AddRange<CultureInfo, CultureInfo>(aduser.Languages, preferredCultures);
				try
				{
					adRecipientSession.Save(aduser);
				}
				catch (DataValidationException innerException)
				{
					throw new CorruptDataException(ServerStrings.ExCannotSaveInvalidObject(aduser), innerException);
				}
				catch (DataSourceOperationException ex)
				{
					throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex, null, "MailboxRegionalConfiguration::SaveCultures. Failed due to directory exception {0}.", new object[]
					{
						ex
					});
				}
				catch (DataSourceTransientException ex2)
				{
					throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex2, null, "MailboxRegionalConfiguration::SaveCultures. Failed due to directory exception {0}.", new object[]
					{
						ex2
					});
				}
			}
		}

		// Token: 0x040034A7 RID: 13479
		private static MailboxRegionalConfigurationSchema schema = ObjectSchema.GetInstance<MailboxRegionalConfigurationSchema>();

		// Token: 0x040034A8 RID: 13480
		private static readonly SimplePropertyDefinition[] mailboxProperties = new SimplePropertyDefinition[]
		{
			MailboxRegionalConfigurationSchema.DateFormat,
			MailboxRegionalConfigurationSchema.TimeFormat,
			MailboxRegionalConfigurationSchema.TimeZone
		};
	}
}
