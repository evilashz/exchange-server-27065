using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Security.AntiXss;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	internal abstract class ContactInfo
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000021C1 File Offset: 0x000003C1
		internal virtual IADOrgPerson ADOrgPerson
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021C4 File Offset: 0x000003C4
		internal virtual UMDialPlan DialPlan
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27
		internal abstract string DisplayName { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001C RID: 28
		internal abstract string FirstName { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001D RID: 29
		internal abstract string LastName { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001E RID: 30
		internal abstract string Title { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001F RID: 31
		internal abstract string Company { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000020 RID: 32
		// (set) Token: 0x06000021 RID: 33
		internal abstract string HomePhone { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000022 RID: 34
		// (set) Token: 0x06000023 RID: 35
		internal abstract string MobilePhone { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000024 RID: 36
		internal abstract string FaxNumber { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000025 RID: 37
		// (set) Token: 0x06000026 RID: 38
		internal abstract string BusinessPhone { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000021C7 File Offset: 0x000003C7
		internal virtual string Extension
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000021CA File Offset: 0x000003CA
		internal virtual string SipLine
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000029 RID: 41
		internal abstract string EMailAddress { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002A RID: 42
		internal abstract string IMAddress { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002B RID: 43
		internal abstract FoundByType FoundBy { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600002C RID: 44
		internal abstract string Id { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600002D RID: 45
		internal abstract string EwsId { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600002E RID: 46
		internal abstract string EwsType { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600002F RID: 47
		internal abstract string City { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000030 RID: 48
		internal abstract string Country { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000021CD File Offset: 0x000003CD
		internal virtual bool ResolvesToMultipleContacts
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000032 RID: 50
		internal abstract ICollection<string> SanitizedPhoneNumbers { get; }

		// Token: 0x06000033 RID: 51 RVA: 0x000021D0 File Offset: 0x000003D0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}({1})", new object[]
			{
				this.DisplayName,
				base.GetType().Name
			});
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000220C File Offset: 0x0000040C
		internal static ContactInfo FindByParticipant(UMSubscriber currentUser, Participant participant)
		{
			ContactInfo result = null;
			try
			{
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromUmUser(currentUser);
				ADRecipient adrecipient = iadrecipientLookup.LookupByParticipant(participant);
				IADOrgPerson iadorgPerson = null;
				if (adrecipient != null)
				{
					iadorgPerson = (adrecipient as IADOrgPerson);
				}
				if (iadorgPerson != null)
				{
					result = new ADContactInfo(iadorgPerson);
				}
				else
				{
					result = ContactInfo.Find(currentUser, participant);
				}
			}
			catch (LocalizedException ex)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, 0, "FindByParticipant: {0}", new object[]
				{
					ex
				});
				throw;
			}
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002288 File Offset: 0x00000488
		internal static PersonalContactInfo Find(UMSubscriber currentUser, Participant participant)
		{
			PersonalContactInfo result;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = currentUser.CreateSessionLock())
			{
				using (ContactsFolder contactsFolder = ContactsFolder.Bind(mailboxSessionLock.Session, mailboxSessionLock.Session.GetDefaultFolderId(DefaultFolderType.Contacts)))
				{
					using (FindInfo<Contact> findInfo = contactsFolder.FindByEmailAddress(participant.EmailAddress, PersonalContactInfo.ContactPropertyDefinitions))
					{
						if (findInfo.FindStatus != FindStatus.NotFound && findInfo.Result != null)
						{
							return new PersonalContactInfo(mailboxSessionLock.Session.MailboxOwner.MailboxInfo.MailboxGuid, findInfo.Result);
						}
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002350 File Offset: 0x00000550
		internal static ContactInfo FindContactByCallerId(UMSubscriber calledUser, PhoneNumber callerId)
		{
			ContactInfo contactInfo = null;
			try
			{
				if (calledUser != null && !PhoneNumber.IsNullOrEmpty(callerId))
				{
					contactInfo = ADContactInfo.FindCallerByCallerId(calledUser, callerId);
					if (contactInfo == null)
					{
						contactInfo = PersonalContactInfo.FindContactByCallerId(calledUser, callerId);
					}
				}
			}
			catch (LocalizedException ex)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UnableToResolveVoicemailCaller, null, new object[]
				{
					callerId,
					(calledUser != null && calledUser.MailAddress != null) ? calledUser.MailAddress : string.Empty,
					ex.Message
				});
				CallIdTracer.TraceError(ExTraceGlobals.VoiceMailTracer, 0, "FindContactByCallerId: {0}", new object[]
				{
					ex
				});
				throw;
			}
			return contactInfo;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000023F8 File Offset: 0x000005F8
		internal virtual Participant CreateParticipant(PhoneNumber callerId, CultureInfo cultureInfo)
		{
			string text = this.DisplayName ?? string.Empty;
			string text2 = this.EMailAddress ?? string.Empty;
			if (string.IsNullOrEmpty(text))
			{
				text = text2;
				if (string.IsNullOrEmpty(text))
				{
					text = MessageContentBuilder.FormatCallerId(callerId, cultureInfo);
				}
			}
			return new Participant(text, text2, "SMTP");
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000244C File Offset: 0x0000064C
		internal virtual LocalizedString GetVoicemailBodyDisplayLabel(PhoneNumber callerId, CultureInfo cultureInfo)
		{
			return callerId.IsEmpty ? Strings.VoiceMailBodyCallerResolvedNoCallerId(AntiXssEncoder.HtmlEncode(this.DisplayName, false)) : this.GetVoicemailBodyCallerResolvedLabel(callerId, cultureInfo);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002480 File Offset: 0x00000680
		internal virtual LocalizedString GetMissedCallBodyDisplayLabel(PhoneNumber callerId, CultureInfo cultureInfo)
		{
			LocalizedString phoneLabel = MessageContentBuilder.GetPhoneLabel(this);
			string senderName = AntiXssEncoder.HtmlEncode(this.DisplayName, false);
			string senderPhone = MessageContentBuilder.FormatCallerIdWithAnchor(callerId, cultureInfo);
			return phoneLabel.IsEmpty ? Strings.MissedCallBodyCallerResolvedNoPhoneLabel(senderName, senderPhone) : Strings.MissedCallBodyCallerResolved(senderName, senderPhone, phoneLabel);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000024CC File Offset: 0x000006CC
		internal virtual LocalizedString GetFaxBodyDisplayLabel(PhoneNumber callerId, CultureInfo cultureInfo)
		{
			LocalizedString phoneLabel = MessageContentBuilder.GetPhoneLabel(this);
			string senderName = AntiXssEncoder.HtmlEncode(this.DisplayName, false);
			string senderPhone = MessageContentBuilder.FormatCallerIdWithAnchor(callerId, cultureInfo);
			return phoneLabel.IsEmpty ? Strings.FaxBodyCallerResolvedNoPhoneLabel(senderName, senderPhone) : Strings.FaxBodyCallerResolved(senderName, senderPhone, phoneLabel);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002518 File Offset: 0x00000718
		private LocalizedString GetVoicemailBodyCallerResolvedLabel(PhoneNumber callerId, CultureInfo cultureInfo)
		{
			LocalizedString phoneLabel = MessageContentBuilder.GetPhoneLabel(this);
			string senderName = AntiXssEncoder.HtmlEncode(this.DisplayName, false);
			string senderPhone = MessageContentBuilder.FormatCallerIdWithAnchor(callerId, cultureInfo);
			return phoneLabel.IsEmpty ? Strings.VoiceMailBodyCallerResolvedNoPhoneLabel(senderName, senderPhone) : Strings.VoiceMailBodyCallerResolved(senderName, senderPhone, phoneLabel);
		}
	}
}
