using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Text;
using System.Web.Security.AntiXss;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000003 RID: 3
	[Cmdlet("Get", "CASMailbox", DefaultParameterSetName = "Identity")]
	public sealed class GetCASMailbox : GetCASMailboxBase<MailboxIdParameter>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000215A File Offset: 0x0000035A
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002180 File Offset: 0x00000380
		[Parameter(Mandatory = false)]
		public SwitchParameter GetPopProtocolLog
		{
			get
			{
				return (SwitchParameter)(base.Fields["GetPopProtocolLog"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["GetPopProtocolLog"] = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002198 File Offset: 0x00000398
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000021BE File Offset: 0x000003BE
		[Parameter(Mandatory = false)]
		public SwitchParameter GetImapProtocolLog
		{
			get
			{
				return (SwitchParameter)(base.Fields["GetImapProtocolLog"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["GetImapProtocolLog"] = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021D6 File Offset: 0x000003D6
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021ED File Offset: 0x000003ED
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpAddress> SendLogsTo
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)base.Fields["SendLogsTo"];
			}
			set
			{
				base.Fields["SendLogsTo"] = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002200 File Offset: 0x00000400
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002226 File Offset: 0x00000426
		[Parameter(Mandatory = false)]
		public SwitchParameter ProtocolSettings
		{
			get
			{
				return (SwitchParameter)(base.Fields["ProtocolSettings"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ProtocolSettings"] = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000223E File Offset: 0x0000043E
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002264 File Offset: 0x00000464
		[Parameter(Mandatory = false)]
		public SwitchParameter ActiveSyncDebugLogging
		{
			get
			{
				return (SwitchParameter)(base.Fields["ActiveSyncDebugLogging"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ActiveSyncDebugLogging"] = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000227C File Offset: 0x0000047C
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000022A2 File Offset: 0x000004A2
		[Parameter(Mandatory = false)]
		public SwitchParameter RecalculateHasActiveSyncDevicePartnership
		{
			get
			{
				return (SwitchParameter)(base.Fields["RecalculateHasActiveSyncDevicePartnership"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RecalculateHasActiveSyncDevicePartnership"] = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022BA File Offset: 0x000004BA
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022E0 File Offset: 0x000004E0
		[Parameter(Mandatory = false)]
		public SwitchParameter Monitoring
		{
			get
			{
				return (SwitchParameter)(base.Fields["Monitoring"] ?? false);
			}
			set
			{
				base.Fields["Monitoring"] = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022F8 File Offset: 0x000004F8
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				if (this.Monitoring)
				{
					return GetUser.MonitoringAllowedRecipientTypeDetails;
				}
				return GetCASMailbox.AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002314 File Offset: 0x00000514
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if ((this.GetPopProtocolLog || this.GetImapProtocolLog) && (this.SendLogsTo == null || this.SendLogsTo.Count == 0))
			{
				base.WriteError(new ArgumentException(Strings.GetProtocolLogNeedsRecipients), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002370 File Offset: 0x00000570
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			ADUser aduser = (ADUser)dataObject;
			ADObjectId owaMailboxPolicy;
			if (aduser.OwaMailboxPolicy != null && OwaSegmentationSettings.UpdateOwaMailboxPolicy(aduser.OrganizationId, aduser.OwaMailboxPolicy, out owaMailboxPolicy))
			{
				aduser.OwaMailboxPolicy = owaMailboxPolicy;
			}
			if (this.RecalculateHasActiveSyncDevicePartnership == true)
			{
				IConfigurationSession configurationSession = this.GetConfigurationSession();
				MobileDevice[] array = configurationSession.Find<MobileDevice>(aduser.ObjectId, QueryScope.SubTree, null, null, 0);
				bool flag = array.Length > 0;
				if (flag != aduser.HasActiveSyncDevicePartnership)
				{
					aduser.HasActiveSyncDevicePartnership = flag;
					IRecipientSession recipientSession = (IRecipientSession)this.CreateSession();
					recipientSession.Save(aduser);
				}
			}
			base.WriteResult(aduser);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000240C File Offset: 0x0000060C
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			ADUser aduser = (ADUser)dataObject;
			if (null != aduser.MasterAccountSid)
			{
				aduser.LinkedMasterAccount = SecurityPrincipalIdParameter.GetFriendlyUserName(aduser.MasterAccountSid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				aduser.ResetChangeTracking();
			}
			CASMailbox casmailbox = CASMailbox.FromDataObject(aduser);
			if ((this.GetPopProtocolLog || this.GetImapProtocolLog || this.ActiveSyncDebugLogging) && CmdletProxy.TryToProxyOutputObject(casmailbox, base.CurrentTaskContext, aduser, this.Identity == null, this.ConfirmationMessage, CmdletProxy.AppendIdentityToProxyCmdlet(aduser)))
			{
				return casmailbox;
			}
			if (casmailbox.ActiveSyncMailboxPolicy == null && !casmailbox.ExchangeVersion.IsOlderThan(CASMailboxSchema.ActiveSyncMailboxPolicy.VersionAdded))
			{
				ADObjectId defaultPolicyId = base.GetDefaultPolicyId(aduser);
				if (defaultPolicyId != null)
				{
					casmailbox.SetActiveSyncMailboxPolicyLocally(defaultPolicyId);
					casmailbox.ActiveSyncMailboxPolicyIsDefaulted = true;
				}
			}
			if (this.GetPopProtocolLog || this.GetImapProtocolLog)
			{
				this.GetProtocolLogs(aduser);
			}
			if (this.ProtocolSettings)
			{
				this.PopulateProtocolSettings(casmailbox);
			}
			if (this.ActiveSyncDebugLogging)
			{
				casmailbox.ActiveSyncDebugLogging = this.GetActiveSyncLoggingEnabled(aduser);
			}
			return casmailbox;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002534 File Offset: 0x00000734
		private IConfigurationSession GetConfigurationSession()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, false);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 342, "GetConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\cas\\GetCASMailbox.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			tenantOrTopologyConfigurationSession.UseGlobalCatalog = (base.DomainController == null && base.ServerSettings.ViewEntireForest);
			return tenantOrTopologyConfigurationSession;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025A8 File Offset: 0x000007A8
		private static ProtocolConnectionSettings GetPreferredConnectionSettingDetail(ICollection<ProtocolConnectionSettings> collection)
		{
			ProtocolConnectionSettings protocolConnectionSettings = null;
			foreach (ProtocolConnectionSettings protocolConnectionSettings2 in collection)
			{
				if (protocolConnectionSettings2 != null)
				{
					if (GetCASMailbox.GetConnectionPreferenceRanking(protocolConnectionSettings2) == 0)
					{
						protocolConnectionSettings = protocolConnectionSettings2;
						break;
					}
					if (protocolConnectionSettings == null || GetCASMailbox.GetConnectionPreferenceRanking(protocolConnectionSettings2) < GetCASMailbox.GetConnectionPreferenceRanking(protocolConnectionSettings))
					{
						protocolConnectionSettings = protocolConnectionSettings2;
					}
				}
			}
			return protocolConnectionSettings;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002610 File Offset: 0x00000810
		private static int GetConnectionPreferenceRanking(ProtocolConnectionSettings settings)
		{
			EncryptionType valueOrDefault = settings.EncryptionType.GetValueOrDefault();
			EncryptionType? encryptionType;
			if (encryptionType != null)
			{
				switch (valueOrDefault)
				{
				case EncryptionType.SSL:
					return 0;
				case EncryptionType.TLS:
					return 1;
				}
			}
			return 2;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000264C File Offset: 0x0000084C
		private void PopulateProtocolSettings(CASMailbox mailbox)
		{
			this.AddConnectionSettingDetails(mailbox, ServiceType.Pop3, true, FrontEndLocator.GetFrontEndPop3SettingsForLocalServer());
			this.AddConnectionSettingDetails(mailbox, ServiceType.Imap4, true, FrontEndLocator.GetFrontEndImap4SettingsForLocalServer());
			this.AddConnectionSettingDetails(mailbox, ServiceType.Smtp, true, FrontEndLocator.GetFrontEndSmtpSettingsForLocalServer());
			this.AddConnectionSettingDetails(mailbox, ServiceType.Pop3, false, FrontEndLocator.GetInternalPop3SettingsForLocalServer());
			this.AddConnectionSettingDetails(mailbox, ServiceType.Imap4, false, FrontEndLocator.GetInternalImap4SettingsForLocalServer());
			this.AddConnectionSettingDetails(mailbox, ServiceType.Smtp, false, FrontEndLocator.GetInternalSmtpSettingsForLocalServer());
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026B4 File Offset: 0x000008B4
		private void AddConnectionSettingDetails(CASMailbox mailbox, ServiceType settingType, bool isExternal, ProtocolConnectionSettings settings)
		{
			if (settings == null)
			{
				return;
			}
			string text;
			if (settings.EncryptionType != null)
			{
				text = Strings.ProtocolSettingsFullDetails(settings.Hostname.ToString(), settings.Port.ToString(), settings.EncryptionType.ToString());
			}
			else
			{
				text = Strings.ProtocolSettingsDetails(settings.Hostname.ToString(), settings.Port.ToString());
			}
			switch (settingType)
			{
			case ServiceType.Pop3:
				if (isExternal)
				{
					mailbox.ExternalPopSettings = text;
					return;
				}
				mailbox.InternalPopSettings = text;
				return;
			case ServiceType.Imap4:
				if (isExternal)
				{
					mailbox.ExternalImapSettings = text;
					return;
				}
				mailbox.InternalImapSettings = text;
				return;
			case ServiceType.Smtp:
				if (isExternal)
				{
					mailbox.ExternalSmtpSettings = text;
					return;
				}
				mailbox.InternalSmtpSettings = text;
				return;
			default:
				throw new InvalidOperationException("Invalid settingType: " + settingType);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027A4 File Offset: 0x000009A4
		private void GetProtocolLogs(ADUser user)
		{
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(user, RemotingOptions.AllowCrossSite);
			if (exchangePrincipal == null)
			{
				base.WriteVerbose(Strings.ExchangePrincipalNotFoundException(user.ToString()));
				return;
			}
			try
			{
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(exchangePrincipal, CultureInfo.InvariantCulture, "Client=Management;Action=Get-CasMailbox"))
				{
					if (this.GetPopProtocolLog)
					{
						this.GetProtocolLogs(mailboxSession, "POP3");
					}
					if (this.GetImapProtocolLog)
					{
						this.GetProtocolLogs(mailboxSession, "IMAP4");
					}
				}
			}
			catch (LocalizedException ex)
			{
				this.WriteWarning(Strings.FailedToGetLogs(user.ToString(), ex.LocalizedString));
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000285C File Offset: 0x00000A5C
		private void GetProtocolLogs(MailboxSession mailboxSession, string protocol)
		{
			using (MailboxLogger mailboxLogger = new MailboxLogger(mailboxSession, protocol))
			{
				if (mailboxLogger.LastError != null)
				{
					this.WriteWarning(Strings.FailedToGetLogs(mailboxSession.DisplayName, mailboxLogger.LastError.LocalizedString));
				}
				else if (!mailboxLogger.LogsExist)
				{
					this.WriteWarning(Strings.NoLogsFound(mailboxSession.DisplayName, protocol));
				}
				else
				{
					StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts);
					using (MessageItem messageItem = MessageItem.Create(mailboxSession, defaultFolderId))
					{
						CultureInfo preferedCulture = mailboxSession.PreferedCulture;
						messageItem.ClassName = "IPM.Note.Exchange.ProtocolLog";
						messageItem.Subject = Strings.ProtocolLogSubject(mailboxSession.DisplayName, protocol).ToString(preferedCulture);
						using (TextWriter textWriter = messageItem.Body.OpenTextWriter(BodyFormat.TextHtml))
						{
							textWriter.Write("\r\n            <html>\r\n                {0}\r\n                <body>\r\n                    <h1>{1}</h1>\r\n                    <br>\r\n                    <p>\r\n                    {2}\r\n                    </p>\r\n                </body>\r\n            </html>\r\n            ", "\r\n                <style>\r\n                    body\r\n                    {\r\n                        font-family: Tahoma;\r\n                        background-color: rgb(255,255,255);\r\n                        color: #000000;\r\n                        font-size:x-small;\r\n                        width: 600px\r\n                    }\r\n                    p\r\n                    {\r\n                        margin:0in;\r\n                    }\r\n                    h1\r\n                    {\r\n                        font-family: Arial;\r\n                        color: #000066;\r\n                        margin: 0in;\r\n                        font-size: medium; font-weight:bold\r\n                    }\r\n                </style>\r\n                ", AntiXssEncoder.HtmlEncode(messageItem.Subject, false), AntiXssEncoder.HtmlEncode(Strings.ProtocolLogAttachmentNote(protocol).ToString(preferedCulture), false));
						}
						foreach (SmtpAddress smtpAddress in this.SendLogsTo)
						{
							Participant participant = new Participant(null, smtpAddress.ToString(), "SMTP");
							messageItem.Recipients.Add(participant, RecipientItemType.To);
						}
						string fileName = string.Format(CultureInfo.InvariantCulture, "{0}_protocolLog_{1}.txt", new object[]
						{
							protocol,
							mailboxSession.DisplayName.ToString(preferedCulture)
						});
						using (StreamAttachment streamAttachment = (StreamAttachment)messageItem.AttachmentCollection.Create(AttachmentType.Stream))
						{
							streamAttachment.FileName = fileName;
							using (Stream contentStream = streamAttachment.GetContentStream())
							{
								char[] array = new char[1024];
								byte[] array2 = new byte[Encoding.UTF8.GetMaxByteCount(array.Length)];
								int bytes = Encoding.UTF8.GetBytes("<log>", 0, 5, array2, 0);
								contentStream.Write(array2, 0, bytes);
								foreach (TextReader textReader in mailboxLogger)
								{
									int charCount;
									while ((charCount = textReader.Read(array, 0, array.Length)) > 0)
									{
										bytes = Encoding.UTF8.GetBytes(array, 0, charCount, array2, 0);
										contentStream.Write(array2, 0, bytes);
									}
								}
								bytes = Encoding.UTF8.GetBytes("</log>", 0, 6, array2, 0);
								contentStream.Write(array2, 0, bytes);
							}
							streamAttachment.Save();
						}
						messageItem.Send();
					}
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002BAC File Offset: 0x00000DAC
		private bool GetActiveSyncLoggingEnabled(ADUser user)
		{
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(user, RemotingOptions.AllowCrossSite);
			if (exchangePrincipal == null)
			{
				base.WriteVerbose(Strings.ExchangePrincipalNotFoundException(user.ToString()));
				return false;
			}
			try
			{
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(exchangePrincipal, CultureInfo.InvariantCulture, "Client=Management;Action=Get-CasMailbox"))
				{
					return SyncStateStorage.GetMailboxLoggingEnabled(mailboxSession, null);
				}
			}
			catch (LocalizedException ex)
			{
				this.WriteWarning(Strings.FailedToReadAirSyncDebugLogging(user.ToString(), ex.LocalizedString));
			}
			return false;
		}

		// Token: 0x04000002 RID: 2
		private const string ProtocolLogReportStyle = "\r\n                <style>\r\n                    body\r\n                    {\r\n                        font-family: Tahoma;\r\n                        background-color: rgb(255,255,255);\r\n                        color: #000000;\r\n                        font-size:x-small;\r\n                        width: 600px\r\n                    }\r\n                    p\r\n                    {\r\n                        margin:0in;\r\n                    }\r\n                    h1\r\n                    {\r\n                        font-family: Arial;\r\n                        color: #000066;\r\n                        margin: 0in;\r\n                        font-size: medium; font-weight:bold\r\n                    }\r\n                </style>\r\n                ";

		// Token: 0x04000003 RID: 3
		private const string ProtocolLogReportBody = "\r\n            <html>\r\n                {0}\r\n                <body>\r\n                    <h1>{1}</h1>\r\n                    <br>\r\n                    <p>\r\n                    {2}\r\n                    </p>\r\n                </body>\r\n            </html>\r\n            ";

		// Token: 0x04000004 RID: 4
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.RoomMailbox,
			RecipientTypeDetails.EquipmentMailbox,
			RecipientTypeDetails.LegacyMailbox,
			RecipientTypeDetails.LinkedMailbox,
			RecipientTypeDetails.UserMailbox,
			RecipientTypeDetails.SharedMailbox,
			RecipientTypeDetails.TeamMailbox,
			RecipientTypeDetails.DiscoveryMailbox
		};
	}
}
