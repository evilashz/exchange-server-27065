using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000A8 RID: 168
	[DataContract]
	public class RegionalSettingsConfiguration : BaseRow
	{
		// Token: 0x06001C1A RID: 7194 RVA: 0x000581A0 File Offset: 0x000563A0
		public RegionalSettingsConfiguration(MailboxRegionalConfiguration mailboxRegionalConfiguration) : base(mailboxRegionalConfiguration)
		{
			this.MailboxRegionalConfiguration = mailboxRegionalConfiguration;
		}

		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x000581B0 File Offset: 0x000563B0
		// (set) Token: 0x06001C1C RID: 7196 RVA: 0x000581B8 File Offset: 0x000563B8
		public MailboxRegionalConfiguration MailboxRegionalConfiguration { get; private set; }

		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x000581C1 File Offset: 0x000563C1
		// (set) Token: 0x06001C1E RID: 7198 RVA: 0x000581C9 File Offset: 0x000563C9
		public MailboxCalendarConfiguration MailboxCalendarConfiguration { get; set; }

		// Token: 0x170018D3 RID: 6355
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x000581D2 File Offset: 0x000563D2
		private bool IsUserLanguageSupported
		{
			get
			{
				return this.MailboxRegionalConfiguration.Language != null && Culture.IsSupportedCulture(this.MailboxRegionalConfiguration.Language);
			}
		}

		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x000581F3 File Offset: 0x000563F3
		// (set) Token: 0x06001C21 RID: 7201 RVA: 0x00058230 File Offset: 0x00056430
		[DataMember]
		public string DateFormat
		{
			get
			{
				if (this.IsUserLanguageSupported && this.MailboxRegionalConfiguration.DateFormat != null)
				{
					return this.MailboxRegionalConfiguration.DateFormat;
				}
				if (this.UserCulture == null)
				{
					return null;
				}
				return this.UserCulture.DateTimeFormat.ShortDatePattern;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x00058238 File Offset: 0x00056438
		internal CultureInfo UserCulture
		{
			get
			{
				if (this.userCulture == null)
				{
					this.userCulture = (this.IsUserLanguageSupported ? Culture.GetCultureInfoInstance(this.MailboxRegionalConfiguration.Language.LCID) : Culture.GetPreferredCulture(LocalSession.Current.RbacConfiguration.ExecutingUserLanguages));
				}
				return this.userCulture;
			}
		}

		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0005828C File Offset: 0x0005648C
		// (set) Token: 0x06001C24 RID: 7204 RVA: 0x00058299 File Offset: 0x00056499
		[DataMember]
		public int Language
		{
			get
			{
				return this.UserCulture.LCID;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x000582A0 File Offset: 0x000564A0
		// (set) Token: 0x06001C26 RID: 7206 RVA: 0x000582AD File Offset: 0x000564AD
		[DataMember]
		public bool DefaultFolderNameMatchingUserLanguage
		{
			get
			{
				return this.MailboxRegionalConfiguration.DefaultFolderNameMatchingUserLanguage;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x000582B4 File Offset: 0x000564B4
		// (set) Token: 0x06001C28 RID: 7208 RVA: 0x000582F1 File Offset: 0x000564F1
		[DataMember]
		public string TimeFormat
		{
			get
			{
				if (this.IsUserLanguageSupported && this.MailboxRegionalConfiguration.TimeFormat != null)
				{
					return this.MailboxRegionalConfiguration.TimeFormat;
				}
				if (this.UserCulture == null)
				{
					return null;
				}
				return this.UserCulture.DateTimeFormat.ShortTimePattern;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x000582F8 File Offset: 0x000564F8
		// (set) Token: 0x06001C2A RID: 7210 RVA: 0x0005831F File Offset: 0x0005651F
		[DataMember]
		public string TimeZone
		{
			get
			{
				if (this.MailboxRegionalConfiguration.TimeZone != null)
				{
					return this.MailboxRegionalConfiguration.TimeZone.ToString();
				}
				return null;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018DA RID: 6362
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x00058326 File Offset: 0x00056526
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x00058355 File Offset: 0x00056555
		[DataMember]
		public string WorkingHoursTimeZone
		{
			get
			{
				if (this.MailboxCalendarConfiguration != null && this.MailboxCalendarConfiguration.WorkingHoursTimeZone != null)
				{
					return this.MailboxCalendarConfiguration.WorkingHoursTimeZone.ToString();
				}
				return null;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04001B85 RID: 7045
		private CultureInfo userCulture;
	}
}
