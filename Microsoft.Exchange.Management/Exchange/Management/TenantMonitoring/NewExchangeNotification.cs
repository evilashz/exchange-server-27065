using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Security;
using System.Text;
using System.Web;
using Microsoft.Exchange.CommonHelpProvider;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.TenantMonitoring
{
	// Token: 0x02000CEC RID: 3308
	[Cmdlet("New", "ExchangeNotification", SupportsShouldProcess = true)]
	public sealed class NewExchangeNotification : NewTenantADTaskBase<Notification>
	{
		// Token: 0x17002782 RID: 10114
		// (get) Token: 0x06007F2A RID: 32554 RVA: 0x00207338 File Offset: 0x00205538
		// (set) Token: 0x06007F2B RID: 32555 RVA: 0x0020734F File Offset: 0x0020554F
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x17002783 RID: 10115
		// (get) Token: 0x06007F2C RID: 32556 RVA: 0x00207362 File Offset: 0x00205562
		// (set) Token: 0x06007F2D RID: 32557 RVA: 0x00207379 File Offset: 0x00205579
		[Parameter(Mandatory = true)]
		public uint EventInstanceId
		{
			get
			{
				return (uint)base.Fields["EventInstanceId"];
			}
			set
			{
				base.Fields["EventInstanceId"] = value;
			}
		}

		// Token: 0x17002784 RID: 10116
		// (get) Token: 0x06007F2E RID: 32558 RVA: 0x00207391 File Offset: 0x00205591
		// (set) Token: 0x06007F2F RID: 32559 RVA: 0x002073A8 File Offset: 0x002055A8
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		[ValidateLength(1, 256)]
		public string EventSource
		{
			get
			{
				return (string)base.Fields["EventSource"];
			}
			set
			{
				base.Fields["EventSource"] = value;
			}
		}

		// Token: 0x17002785 RID: 10117
		// (get) Token: 0x06007F30 RID: 32560 RVA: 0x002073BB File Offset: 0x002055BB
		// (set) Token: 0x06007F31 RID: 32561 RVA: 0x002073E6 File Offset: 0x002055E6
		[Parameter(Mandatory = false)]
		public int EventCategoryId
		{
			get
			{
				if (base.Fields["EventCategoryId"] == null)
				{
					return 0;
				}
				return (int)base.Fields["EventCategoryId"];
			}
			set
			{
				base.Fields["EventCategoryId"] = value;
			}
		}

		// Token: 0x17002786 RID: 10118
		// (get) Token: 0x06007F32 RID: 32562 RVA: 0x002073FE File Offset: 0x002055FE
		// (set) Token: 0x06007F33 RID: 32563 RVA: 0x0020742D File Offset: 0x0020562D
		[Parameter(Mandatory = false)]
		public ExDateTime EventTime
		{
			get
			{
				if (base.Fields["EventTime"] == null)
				{
					return ExDateTime.UtcNow;
				}
				return (ExDateTime)base.Fields["EventTime"];
			}
			set
			{
				base.Fields["EventTime"] = value;
			}
		}

		// Token: 0x17002787 RID: 10119
		// (get) Token: 0x06007F34 RID: 32564 RVA: 0x00207445 File Offset: 0x00205645
		// (set) Token: 0x06007F35 RID: 32565 RVA: 0x0020745C File Offset: 0x0020565C
		[ValidateCount(0, 100)]
		[Parameter(Mandatory = false)]
		public string[] InsertionStrings
		{
			get
			{
				return (string[])base.Fields["InsertionStrings"];
			}
			set
			{
				base.Fields["InsertionStrings"] = value;
			}
		}

		// Token: 0x17002788 RID: 10120
		// (get) Token: 0x06007F36 RID: 32566 RVA: 0x0020746F File Offset: 0x0020566F
		// (set) Token: 0x06007F37 RID: 32567 RVA: 0x00207486 File Offset: 0x00205686
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] NotificationRecipients
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["NotificationRecipients"];
			}
			set
			{
				base.Fields["NotificationRecipients"] = value;
			}
		}

		// Token: 0x17002789 RID: 10121
		// (get) Token: 0x06007F38 RID: 32568 RVA: 0x00207499 File Offset: 0x00205699
		// (set) Token: 0x06007F39 RID: 32569 RVA: 0x002074C8 File Offset: 0x002056C8
		[Parameter(Mandatory = false)]
		public ExDateTime CreationTime
		{
			get
			{
				if (base.Fields["CreationTime"] == null)
				{
					return ExDateTime.UtcNow;
				}
				return (ExDateTime)base.Fields["CreationTime"];
			}
			set
			{
				base.Fields["CreationTime"] = value;
			}
		}

		// Token: 0x1700278A RID: 10122
		// (get) Token: 0x06007F3A RID: 32570 RVA: 0x002074E0 File Offset: 0x002056E0
		// (set) Token: 0x06007F3B RID: 32571 RVA: 0x00207500 File Offset: 0x00205700
		[Parameter(Mandatory = false)]
		public string PeriodicKey
		{
			get
			{
				return ((string)base.Fields["PeriodicKey"]) ?? string.Empty;
			}
			set
			{
				base.Fields["PeriodicKey"] = value;
			}
		}

		// Token: 0x06007F3C RID: 32572 RVA: 0x00207514 File Offset: 0x00205714
		protected override IConfigDataProvider CreateSession()
		{
			this.federatedUser = (ADUser)base.GetDataObject<ADUser>(NewExchangeNotification.FederatedMailboxId, base.TenantGlobalCatalogSession, null, null, new LocalizedString?(Strings.ErrorUserNotFound(NewExchangeNotification.FederatedMailboxId.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(NewExchangeNotification.FederatedMailboxId.ToString())));
			return new NotificationDataProvider(this.federatedUser, base.SessionSettings);
		}

		// Token: 0x06007F3D RID: 32573 RVA: 0x00207578 File Offset: 0x00205778
		protected override OrganizationId ResolveCurrentOrganization()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 210, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\TenantMonitoring\\NewExchangeNotification.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = true;
			tenantOrTopologyConfigurationSession.UseGlobalCatalog = false;
			ExchangeConfigurationUnit exchangeConfigurationUnit = (ExchangeConfigurationUnit)base.GetDataObject<ExchangeConfigurationUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
			if (exchangeConfigurationUnit.OrganizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				throw new ArgumentException("OrganizationId.ForestWideOrgId is not supported.", "Organization");
			}
			return exchangeConfigurationUnit.OrganizationId;
		}

		// Token: 0x06007F3E RID: 32574 RVA: 0x0020764C File Offset: 0x0020584C
		protected override IConfigurable PrepareDataObject()
		{
			if (this.federatedUser == null)
			{
				base.WriteError(new LocalizedException(Strings.ErrorRecipientNotFound(NewExchangeNotification.FederatedMailboxId.ToString())), ErrorCategory.InvalidArgument, NewExchangeNotification.FederatedMailboxId.ToString());
			}
			IEnumerable<NewExchangeNotification.LookupResult> enumerable = this.LookupSmtpAddressesAndLanguages();
			this.DataObject.EventInstanceId = (int)this.EventInstanceId;
			this.DataObject.EventSource = this.EventSource;
			((NotificationIdentity)this.DataObject.Identity).EventSource = this.EventSource;
			this.DataObject.EventCategoryId = this.EventCategoryId;
			this.DataObject.EventTimeUtc = this.EventTime.ToUtc();
			this.DataObject.InsertionStrings = this.InsertionStrings;
			this.DataObject.CreationTimeUtc = this.CreationTime.ToUtc();
			this.DataObject.EntryType = Utils.ExtractEntryTypeFromInstanceId(this.EventInstanceId);
			this.DataObject.PeriodicKey = this.PeriodicKey;
			if (this.ShouldSendNotificationEmail(enumerable))
			{
				this.DataObject.EmailSent = true;
				IEnumerable<string> enumerable2 = (from r in enumerable
				select r.SmtpAddress.ToString()).Distinct(StringComparer.OrdinalIgnoreCase);
				foreach (string item in enumerable2)
				{
					this.DataObject.NotificationRecipients.Add(item);
				}
				this.notificationEmailInfos = this.ForkRecipientsBasedOnLanguage(enumerable).ToList<NewExchangeNotification.NotificationEmailInfo>();
				IEnumerable<string> enumerable3 = (from e in this.notificationEmailInfos
				select e.MessageId).Distinct(StringComparer.OrdinalIgnoreCase);
				using (IEnumerator<string> enumerator2 = enumerable3.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string item2 = enumerator2.Current;
						this.DataObject.NotificationMessageIds.Add(item2);
					}
					goto IL_1FB;
				}
			}
			this.DataObject.EmailSent = false;
			IL_1FB:
			return base.PrepareDataObject();
		}

		// Token: 0x06007F3F RID: 32575 RVA: 0x00207878 File Offset: 0x00205A78
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			HelpProvider.Initialize(HelpProvider.HelpAppName.TenantMonitoring);
		}

		// Token: 0x06007F40 RID: 32576 RVA: 0x00207886 File Offset: 0x00205A86
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (this.DataObject.EmailSent)
			{
				this.SendEmail();
			}
		}

		// Token: 0x06007F41 RID: 32577 RVA: 0x002078A1 File Offset: 0x00205AA1
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(StoragePermanentException).IsInstanceOfType(exception);
		}

		// Token: 0x06007F42 RID: 32578 RVA: 0x002078C8 File Offset: 0x00205AC8
		protected override void WriteResult(IConfigurable result)
		{
			Notification notification = result as Notification;
			if (notification == null)
			{
				base.WriteResult(result);
				return;
			}
			notification.EventMessage = notification.InsertionStrings.Aggregate(string.Empty, (string concatenated, string insertionString) => concatenated + insertionString);
			base.WriteResult(result);
		}

		// Token: 0x06007F43 RID: 32579 RVA: 0x00207924 File Offset: 0x00205B24
		private bool ShouldSendNotificationEmail(IEnumerable<NewExchangeNotification.LookupResult> recipients)
		{
			if (recipients == null || recipients.Count<NewExchangeNotification.LookupResult>() == 0)
			{
				base.WriteVerbose(Strings.TenantNotificationVerboseNotSendingEmailNoRecipients);
				return false;
			}
			switch (((NotificationDataProvider)base.DataSession).RecentNotificationEmailExists(this.DataObject))
			{
			case RecentNotificationEmailTestResult.DailyCapReached:
				base.WriteVerbose(Strings.TenantNotificationVerboseNotSendingEmailDailyCap);
				return false;
			case RecentNotificationEmailTestResult.PastDay:
				base.WriteVerbose(Strings.TenantNotificationVerboseNotSendingEmailPastDay);
				return false;
			}
			base.WriteVerbose(Strings.TenantNotificationVerboseSendingEmail);
			return true;
		}

		// Token: 0x06007F44 RID: 32580 RVA: 0x0020799C File Offset: 0x00205B9C
		private void SendEmail()
		{
			if (this.notificationEmailInfos.Count<NewExchangeNotification.NotificationEmailInfo>() == 0)
			{
				return;
			}
			using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(ExchangePrincipal.FromADUser(base.SessionSettings, this.federatedUser), CultureInfo.InvariantCulture, "Client=Management;Action=Send-TenantNotification"))
			{
				foreach (NewExchangeNotification.NotificationEmailInfo emailInfo in this.notificationEmailInfos)
				{
					this.SendLocalizedEmail(mailboxSession, emailInfo);
				}
			}
		}

		// Token: 0x06007F45 RID: 32581 RVA: 0x00207A34 File Offset: 0x00205C34
		private void SendLocalizedEmail(MailboxSession session, NewExchangeNotification.NotificationEmailInfo emailInfo)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (emailInfo.Recipients == null || emailInfo.Recipients.Count<SmtpAddress>() == 0)
			{
				return;
			}
			using (MessageItem messageItem = MessageItem.Create(session, session.GetDefaultFolderId(DefaultFolderType.Drafts)))
			{
				foreach (SmtpAddress smtpAddress in emailInfo.Recipients)
				{
					messageItem.Recipients.Add(new Participant(string.Empty, smtpAddress.ToString(), "SMTP"));
				}
				string eventCategory;
				string localizedEventMessageAndCategory = this.GetLocalizedEventMessageAndCategory(emailInfo.Language, out eventCategory);
				messageItem.Subject = Strings.TenantNotificationSubject(eventCategory, this.DataObject.EventDisplayId).ToString(emailInfo.Language);
				using (Stream stream = messageItem.Body.OpenWriteStream(new BodyWriteConfiguration(BodyFormat.TextHtml, Charset.Unicode)))
				{
					using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Unicode))
					{
						string helpUrlForNotification = this.GetHelpUrlForNotification(emailInfo.Language);
						OrganizationId currentOrganizationId = base.CurrentTaskContext.UserInfo.CurrentOrganizationId;
						streamWriter.WriteLine(Strings.TenantNotificationBody(HttpUtility.HtmlEncode((currentOrganizationId == null) ? string.Empty : currentOrganizationId.GetFriendlyName()), HttpUtility.HtmlEncode(this.DataObject.EventTimeUtc.ToString("f", emailInfo.Language)), (!string.IsNullOrEmpty(localizedEventMessageAndCategory)) ? HttpUtility.HtmlEncode(localizedEventMessageAndCategory) : Strings.TenantNotificationUnavailableEventMessage.ToString(emailInfo.Language), HttpUtility.HtmlEncode(helpUrlForNotification)).ToString(emailInfo.Language));
					}
				}
				messageItem.AutoResponseSuppress = AutoResponseSuppress.All;
				messageItem.InternetMessageId = emailInfo.MessageId;
				messageItem.SendWithoutSavingMessage();
			}
		}

		// Token: 0x06007F46 RID: 32582 RVA: 0x00207C70 File Offset: 0x00205E70
		private string GetLocalizedEventMessageAndCategory(CultureInfo language, out string category)
		{
			string text = string.Empty;
			category = string.Empty;
			try
			{
				text = Utils.GetResourcesFilePath(this.DataObject.EventSource);
				if (string.IsNullOrEmpty(text))
				{
					base.WriteDebug(Strings.TenantNotificationDebugEventResourceFileNotFound(this.DataObject.EventSource));
					return string.Empty;
				}
			}
			catch (IOException exception)
			{
				this.WriteError(exception, ErrorCategory.ResourceUnavailable, this.DataObject.EventSource, false);
				return string.Empty;
			}
			catch (SecurityException exception2)
			{
				this.WriteError(exception2, ErrorCategory.ResourceUnavailable, this.DataObject.EventSource, false);
				return string.Empty;
			}
			catch (UnauthorizedAccessException exception3)
			{
				this.WriteError(exception3, ErrorCategory.ResourceUnavailable, this.DataObject.EventSource, false);
				return string.Empty;
			}
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.CurrentUICulture))
			{
				string localizedEventMessageAndCategory = Utils.GetLocalizedEventMessageAndCategory(text, (uint)this.DataObject.EventInstanceId, (uint)this.DataObject.EventCategoryId, this.DataObject.InsertionStrings, language, stringWriter, out category);
				if (base.IsDebugOn)
				{
					string text2 = stringWriter.ToString();
					if (!string.IsNullOrEmpty(text2))
					{
						base.WriteDebug(text2);
					}
				}
				result = localizedEventMessageAndCategory;
			}
			return result;
		}

		// Token: 0x06007F47 RID: 32583 RVA: 0x00207DD0 File Offset: 0x00205FD0
		private CultureInfo GetNotificationEmailFallbackLanguage()
		{
			TransportConfigContainer transportConfigContainer = this.ConfigurationSession.FindSingletonConfigurationObject<TransportConfigContainer>();
			if (transportConfigContainer == null)
			{
				return CultureInfo.CurrentUICulture;
			}
			CultureInfo result;
			if ((result = transportConfigContainer.InternalDsnDefaultLanguage) == null)
			{
				result = (transportConfigContainer.ExternalDsnDefaultLanguage ?? CultureInfo.CurrentUICulture);
			}
			return result;
		}

		// Token: 0x06007F48 RID: 32584 RVA: 0x00207E0C File Offset: 0x0020600C
		private string GetHelpUrlForNotification(CultureInfo language)
		{
			string text = (language != null) ? language.Name : CultureInfo.InvariantCulture.Name;
			Uri uri = HelpProvider.ConstructTenantEventUrl(this.DataObject.EventSource, string.Empty, this.DataObject.EventDisplayId.ToString(CultureInfo.InvariantCulture), text);
			if (uri == null)
			{
				base.WriteDebug(Strings.TenantNotificationDebugHelpProviderReturnedEmptyUrl(this.DataObject.EventSource, this.DataObject.EventDisplayId, text));
				return "http://help.outlook.com/ms.exch.evt.default.aspx";
			}
			return uri.ToString();
		}

		// Token: 0x06007F49 RID: 32585 RVA: 0x00207E98 File Offset: 0x00206098
		private IEnumerable<NewExchangeNotification.LookupResult> LookupSmtpAddressesAndLanguages()
		{
			if (this.NotificationRecipients == null || this.NotificationRecipients.Length == 0)
			{
				base.WriteVerbose(Strings.TenantNotificationNoNotificationRecipientsSpecified);
				return NewExchangeNotification.EmptyLookupResultsArray;
			}
			LinkedList<NewExchangeNotification.LookupResult> linkedList = new LinkedList<NewExchangeNotification.LookupResult>();
			foreach (RecipientIdParameter recipientIdParameter in this.NotificationRecipients)
			{
				NewExchangeNotification.LookupResult value = NewExchangeNotification.LookupSmtpAddressAndLanguage(base.TenantGlobalCatalogSession, recipientIdParameter);
				if (value.Success)
				{
					linkedList.AddLast(value);
					if (linkedList.Count >= 64)
					{
						break;
					}
				}
				else
				{
					this.WriteWarning(value.ErrorMessage);
				}
			}
			return linkedList;
		}

		// Token: 0x06007F4A RID: 32586 RVA: 0x00207F24 File Offset: 0x00206124
		private static NewExchangeNotification.LookupResult LookupSmtpAddressAndLanguage(IRecipientSession recipientSession, RecipientIdParameter recipientIdParameter)
		{
			IEnumerable<ADRecipient> objects = recipientIdParameter.GetObjects<ADRecipient>(null, recipientSession);
			ADRecipient adrecipient = null;
			using (IEnumerator<ADRecipient> enumerator = objects.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					if (SmtpAddress.IsValidSmtpAddress(recipientIdParameter.RawIdentity))
					{
						return new NewExchangeNotification.LookupResult
						{
							Success = true,
							SmtpAddress = SmtpAddress.Parse(recipientIdParameter.RawIdentity),
							Languages = new CultureInfo[0]
						};
					}
					return new NewExchangeNotification.LookupResult
					{
						Success = false,
						ErrorMessage = Strings.NoRecipientsForRecipientId(recipientIdParameter.ToString())
					};
				}
				else
				{
					adrecipient = enumerator.Current;
					if (enumerator.MoveNext())
					{
						return new NewExchangeNotification.LookupResult
						{
							Success = false,
							ErrorMessage = Strings.MoreThanOneRecipientForRecipientId(recipientIdParameter.ToString())
						};
					}
				}
			}
			if (SmtpAddress.Empty.Equals(adrecipient.PrimarySmtpAddress))
			{
				return new NewExchangeNotification.LookupResult
				{
					Success = false,
					ErrorMessage = Strings.NoSmtpAddressForRecipientId(recipientIdParameter.ToString())
				};
			}
			IADOrgPerson iadorgPerson = adrecipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return new NewExchangeNotification.LookupResult
				{
					Success = true,
					SmtpAddress = adrecipient.PrimarySmtpAddress,
					Languages = new CultureInfo[0]
				};
			}
			return new NewExchangeNotification.LookupResult
			{
				Success = true,
				SmtpAddress = adrecipient.PrimarySmtpAddress,
				Languages = iadorgPerson.Languages
			};
		}

		// Token: 0x06007F4B RID: 32587 RVA: 0x00208360 File Offset: 0x00206560
		private IEnumerable<NewExchangeNotification.NotificationEmailInfo> ForkRecipientsBasedOnLanguage(IEnumerable<NewExchangeNotification.LookupResult> recipients)
		{
			CultureInfo fallbackLanguage = this.GetNotificationEmailFallbackLanguage();
			string fqdn = this.GetLocalServerFqdn();
			IEnumerable<IGrouping<CultureInfo, SmtpAddress>> languageBuckets = from recipient in recipients
			group recipient.SmtpAddress by recipient.GetPreferredSupportedLanguage(fallbackLanguage);
			foreach (IGrouping<CultureInfo, SmtpAddress> languageBucket in languageBuckets)
			{
				string messageId = string.Format(CultureInfo.InvariantCulture, "<{0}@{1}>", new object[]
				{
					Guid.NewGuid(),
					fqdn
				});
				yield return new NewExchangeNotification.NotificationEmailInfo
				{
					Recipients = languageBucket,
					Language = languageBucket.Key,
					MessageId = messageId
				};
			}
			yield break;
		}

		// Token: 0x06007F4C RID: 32588 RVA: 0x00208384 File Offset: 0x00206584
		private string GetLocalServerFqdn()
		{
			Server server = ((ITopologyConfigurationSession)this.ConfigurationSession).FindLocalServer();
			if (server == null)
			{
				return Environment.MachineName;
			}
			if (string.IsNullOrEmpty(server.Fqdn))
			{
				return Environment.MachineName;
			}
			return server.Fqdn;
		}

		// Token: 0x04003E55 RID: 15957
		private static readonly MailboxIdParameter FederatedMailboxId = new MailboxIdParameter("FederatedEmail.4c1f4d8b-8179-4148-93bf-00a95fa1e042");

		// Token: 0x04003E56 RID: 15958
		private static readonly HashSet<CultureInfo> SupportedClientLanguages = new HashSet<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackSpecificCultures(LanguagePackType.Client));

		// Token: 0x04003E57 RID: 15959
		private static readonly NewExchangeNotification.LookupResult[] EmptyLookupResultsArray = new NewExchangeNotification.LookupResult[0];

		// Token: 0x04003E58 RID: 15960
		private ADUser federatedUser;

		// Token: 0x04003E59 RID: 15961
		private IEnumerable<NewExchangeNotification.NotificationEmailInfo> notificationEmailInfos = new NewExchangeNotification.NotificationEmailInfo[0];

		// Token: 0x02000CED RID: 3309
		private struct LookupResult
		{
			// Token: 0x06007F53 RID: 32595 RVA: 0x00208404 File Offset: 0x00206604
			public CultureInfo GetPreferredSupportedLanguage(CultureInfo fallbackLanguage)
			{
				if (this.preferredSupportedLanguage == null)
				{
					this.preferredSupportedLanguage = fallbackLanguage;
					if (this.Languages != null)
					{
						foreach (CultureInfo item in this.Languages)
						{
							if (NewExchangeNotification.SupportedClientLanguages.Contains(item))
							{
								this.preferredSupportedLanguage = item;
								break;
							}
						}
					}
				}
				return this.preferredSupportedLanguage;
			}

			// Token: 0x04003E5E RID: 15966
			private CultureInfo preferredSupportedLanguage;

			// Token: 0x04003E5F RID: 15967
			public SmtpAddress SmtpAddress;

			// Token: 0x04003E60 RID: 15968
			public ICollection<CultureInfo> Languages;

			// Token: 0x04003E61 RID: 15969
			public bool Success;

			// Token: 0x04003E62 RID: 15970
			public LocalizedString ErrorMessage;
		}

		// Token: 0x02000CEE RID: 3310
		private sealed class NotificationEmailInfo
		{
			// Token: 0x04003E63 RID: 15971
			public IEnumerable<SmtpAddress> Recipients;

			// Token: 0x04003E64 RID: 15972
			public string MessageId;

			// Token: 0x04003E65 RID: 15973
			public CultureInfo Language;
		}
	}
}
