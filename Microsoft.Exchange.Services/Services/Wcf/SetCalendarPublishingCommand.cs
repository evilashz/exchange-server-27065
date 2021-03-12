using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200096D RID: 2413
	internal sealed class SetCalendarPublishingCommand : ServiceCommand<SetCalendarPublishingResponse>
	{
		// Token: 0x0600454A RID: 17738 RVA: 0x000F2AF7 File Offset: 0x000F0CF7
		public SetCalendarPublishingCommand(CallContext callContext, SetCalendarPublishingRequest request) : base(callContext)
		{
			this.Request = request;
			this.TraceDebug("Validating Request", new object[0]);
			this.Request.ValidateRequest();
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x0600454B RID: 17739 RVA: 0x000F2B23 File Offset: 0x000F0D23
		// (set) Token: 0x0600454C RID: 17740 RVA: 0x000F2B2B File Offset: 0x000F0D2B
		private SetCalendarPublishingRequest Request { get; set; }

		// Token: 0x0600454D RID: 17741 RVA: 0x000F2B34 File Offset: 0x000F0D34
		protected override SetCalendarPublishingResponse InternalExecute()
		{
			if (this.Request.Publish)
			{
				return this.PublishCalendar();
			}
			return this.StopPublishedCalendar();
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x000F2B50 File Offset: 0x000F0D50
		private SetCalendarPublishingResponse PublishCalendar()
		{
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			SetCalendarPublishingResponse setCalendarPublishingResponse;
			if (PublishedCalendar.IsCalendarPublished(mailboxIdentityMailboxSession, this.Request.CalendarStoreId))
			{
				setCalendarPublishingResponse = new SetCalendarPublishingResponse(CalendarActionError.CalendarActionCalendarAlreadyPublished);
			}
			else
			{
				setCalendarPublishingResponse = new SetCalendarPublishingResponse();
				MailboxCalendarFolderDataProvider mailboxCalendarFolderDataProvider = new MailboxCalendarFolderDataProvider(base.CallContext.ADRecipientSessionContext.GetADRecipientSession().SessionSettings, (ADUser)DirectoryHelper.ReadADRecipient(base.CallContext.AccessingPrincipal.MailboxInfo.MailboxGuid, base.CallContext.AccessingPrincipal.MailboxInfo.IsArchive, mailboxIdentityMailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid)), "PublishCalendarCommand");
				MailboxCalendarFolder mailboxCalendarFolder = new MailboxCalendarFolder();
				mailboxCalendarFolder.MailboxFolderId = new MailboxFolderId(base.CallContext.AccessingPrincipal.ObjectId, this.Request.CalendarStoreId, null);
				mailboxCalendarFolder.PublishDateRangeFrom = DateRangeEnumType.ThreeMonths;
				mailboxCalendarFolder.PublishDateRangeTo = DateRangeEnumType.SixMonths;
				mailboxCalendarFolder.PublishEnabled = true;
				mailboxCalendarFolder.SearchableUrlEnabled = false;
				SharingPolicyAction allowedForAnonymousCalendarSharing = DirectoryHelper.ReadSharingPolicy(mailboxIdentityMailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, mailboxIdentityMailboxSession.MailboxOwner.MailboxInfo.IsArchive, mailboxIdentityMailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid)).GetAllowedForAnonymousCalendarSharing();
				mailboxCalendarFolder.DetailLevel = CalendarSharingPermissionsUtils.GetAdjustedDetailLevel(allowedForAnonymousCalendarSharing, this.Request.DetailLevelEnum);
				setCalendarPublishingResponse.CurrentDetailLevel = mailboxCalendarFolder.DetailLevel.ToString();
				DetailLevelEnumType detailLevelEnumType;
				if (!CalendarSharingPermissionsUtils.TryGetMaximumDetailLevel(allowedForAnonymousCalendarSharing, out detailLevelEnumType))
				{
					detailLevelEnumType = DetailLevelEnumType.AvailabilityOnly;
				}
				setCalendarPublishingResponse.MaxDetailLevel = detailLevelEnumType.ToString();
				Uri owaVdirUrl = OwaAnonymousVdirLocater.Instance.GetOwaVdirUrl(base.CallContext.AccessingPrincipal, new FrontEndLocator());
				SmtpAddress smtpAddress = SmtpAddress.Parse(base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
				Guid mailboxGuid = base.CallContext.AccessingPrincipal.MailboxInfo.MailboxGuid;
				ObscureUrl arg = ObscureUrl.CreatePublishCalendarUrl(owaVdirUrl.ToString(), mailboxGuid, smtpAddress.Domain);
				mailboxCalendarFolder.PublishedICalUrl = arg + ".ics";
				mailboxCalendarFolder.PublishedCalendarUrl = arg + ".html";
				setCalendarPublishingResponse.ICalUrl = mailboxCalendarFolder.PublishedICalUrl;
				setCalendarPublishingResponse.BrowseUrl = mailboxCalendarFolder.PublishedCalendarUrl;
				mailboxCalendarFolderDataProvider.Save(mailboxCalendarFolder);
			}
			return setCalendarPublishingResponse;
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x000F2D74 File Offset: 0x000F0F74
		private SetCalendarPublishingResponse StopPublishedCalendar()
		{
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			PublishedCalendar publishedCalendar = null;
			try
			{
				if (PublishedCalendar.TryGetPublishedCalendar(mailboxIdentityMailboxSession, this.Request.CalendarStoreId, new ObscureKind?(ObscureKind.Normal), out publishedCalendar))
				{
					publishedCalendar.PublishedOptions.MailboxFolderId = new MailboxFolderId(base.CallContext.AccessingPrincipal.ObjectId, this.Request.CalendarStoreId, null);
					publishedCalendar.PublishedOptions.PublishEnabled = false;
					publishedCalendar.SavePublishedOptions();
				}
				else
				{
					this.TraceDebug("Calendar isn't published. Ignoring request", new object[0]);
				}
			}
			finally
			{
				if (publishedCalendar != null)
				{
					publishedCalendar.Dispose();
				}
			}
			return new SetCalendarPublishingResponse();
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x000F2E24 File Offset: 0x000F1024
		private void TraceDebug(string messageFormat, params object[] args)
		{
			ExTraceGlobals.SetCalendarPublishingCallTracer.TraceDebug((long)this.GetHashCode(), messageFormat, args);
		}
	}
}
