using System;
using System.IO;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000967 RID: 2407
	internal sealed class SendCalendarSharingInvite : CalendarActionBase<CalendarShareInviteResponse>
	{
		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x0600452B RID: 17707 RVA: 0x000F1AD7 File Offset: 0x000EFCD7
		// (set) Token: 0x0600452C RID: 17708 RVA: 0x000F1ADF File Offset: 0x000EFCDF
		private ExchangePrincipal AccessingPrincipal { get; set; }

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x0600452D RID: 17709 RVA: 0x000F1AE8 File Offset: 0x000EFCE8
		// (set) Token: 0x0600452E RID: 17710 RVA: 0x000F1AF0 File Offset: 0x000EFCF0
		private CalendarShareInviteRequest Request { get; set; }

		// Token: 0x0600452F RID: 17711 RVA: 0x000F1AFC File Offset: 0x000EFCFC
		public SendCalendarSharingInvite(MailboxSession session, CalendarShareInviteRequest request, ExchangePrincipal accessingPrincipal, ADRecipientSessionContext adRecipientSessionContext) : this(session, request, accessingPrincipal, adRecipientSessionContext, Guid.NewGuid().ToString())
		{
			OwsLogRegistry.Register(SendCalendarSharingInvite.SendCalendarSharingInviteActionName, typeof(SendCalendarSharingInviteMetadata), new Type[0]);
		}

		// Token: 0x06004530 RID: 17712 RVA: 0x000F1B58 File Offset: 0x000EFD58
		public SendCalendarSharingInvite(MailboxSession session, CalendarShareInviteRequest request, ExchangePrincipal accessingPrincipal, ADRecipientSessionContext adRecipientSessionContext, string loggingContextIdentifier) : base(session)
		{
			this.Request = request;
			this.AccessingPrincipal = accessingPrincipal;
			this.SharingProviderLocater = new SharingProviderLocator(accessingPrincipal, () => session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
			this.adRecipientSessionContext = adRecipientSessionContext;
			this.loggingContextIdentifier = loggingContextIdentifier;
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x000F1BBC File Offset: 0x000EFDBC
		public override CalendarShareInviteResponse Execute()
		{
			this.TraceDebug("CalendarShareInviteResponse.Execute", new object[0]);
			CalendarShareInviteResponse calendarShareInviteResponse = new CalendarShareInviteResponse();
			StoreObjectId defaultFolderId = base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar);
			bool flag = defaultFolderId.Equals(this.Request.CalendarStoreId);
			SharingPermissionLogger.LogEntry(base.MailboxSession, string.Format("SendCalendarSharingInvite calendar type: {0}, No of recipients: {1}", flag ? "Primary" : "Seconday", this.Request.SharingRecipients.Length), this.loggingContextIdentifier);
			SendCalendarSharingInviteStatistic sendCalendarSharingInviteStatistic = new SendCalendarSharingInviteStatistic();
			foreach (CalendarSharingRecipient calendarSharingRecipient in this.Request.SharingRecipients)
			{
				this.TraceDebug("Create sharing message for recipient:{0}", new object[]
				{
					calendarSharingRecipient.SmtpAddress
				});
				try
				{
					using (SharingMessageItem sharingMessageItem = this.CreateMessage(calendarSharingRecipient))
					{
						if (flag && calendarSharingRecipient.ADRecipient != null && calendarSharingRecipient.DetailLevelType != CalendarSharingDetailLevel.Delegate)
						{
							this.RevokeDelegatePermissionIfADelegateUser(calendarSharingRecipient.ADRecipient);
						}
						switch (calendarSharingRecipient.DetailLevelType)
						{
						case CalendarSharingDetailLevel.AvailabilityOnly:
							sharingMessageItem.SharingDetail = SharingContextDetailLevel.AvailabilityOnly;
							break;
						case CalendarSharingDetailLevel.LimitedDetails:
							sharingMessageItem.SharingDetail = SharingContextDetailLevel.Limited;
							break;
						case CalendarSharingDetailLevel.FullDetails:
							sharingMessageItem.SharingDetail = SharingContextDetailLevel.FullDetails;
							break;
						case CalendarSharingDetailLevel.Editor:
							if (calendarSharingRecipient.ADRecipient == null)
							{
								throw new NotSupportedException("Editor permission is not supported for external user!");
							}
							sharingMessageItem.SharingDetail = SharingContextDetailLevel.Editor;
							sharingMessageItem.SharingPermissions = SharingContextPermissions.Editor;
							break;
						case CalendarSharingDetailLevel.Delegate:
							if (calendarSharingRecipient.ADRecipient == null)
							{
								throw new NotSupportedException("Delegate permission is not supported for external user!");
							}
							sharingMessageItem.SharingDetail = SharingContextDetailLevel.Editor;
							sharingMessageItem.SharingPermissions = SharingContextPermissions.Editor;
							this.AddDelegateWithPermissions(calendarSharingRecipient.ADRecipient, calendarSharingRecipient.ViewPrivateAppointments);
							break;
						default:
							throw new NotSupportedException(string.Format("A calendar sharing invite with sharing level '{0}' is not supported!", calendarSharingRecipient.DetailLevelType));
						}
						this.TraceDebug("Sharing Detail level:{0}", new object[]
						{
							sharingMessageItem.SharingDetail
						});
						sharingMessageItem.Sender = new Participant(this.AccessingPrincipal);
						sharingMessageItem.From = sharingMessageItem.Sender;
						sharingMessageItem.Subject = this.Request.Subject;
						sharingMessageItem.Recipients.Add(new Participant(calendarSharingRecipient.EmailAddress.Name, calendarSharingRecipient.EmailAddress.EmailAddress, calendarSharingRecipient.EmailAddress.RoutingType));
						sharingMessageItem.SharingMessageType = SharingMessageType.Invitation;
						sharingMessageItem.SetSubmitFlags(SharingSubmitFlags.None);
						sharingMessageItem.FrontEndLocator = new FrontEndLocator();
						this.CopyBodyToItem(sharingMessageItem);
						this.TraceDebug("Send sharing message", new object[0]);
						sharingMessageItem.Send();
						SharingPermissionLogger.LogEntry(base.MailboxSession, string.Format("Sent calendar sharing invite type: {0}; user: {1}", calendarSharingRecipient.DetailLevelType, calendarSharingRecipient.EmailAddress.EmailAddress), this.loggingContextIdentifier);
						calendarShareInviteResponse.AddSucessResponse(calendarSharingRecipient.EmailAddress.EmailAddress);
						sendCalendarSharingInviteStatistic.IncreaseSucceededInvite(calendarSharingRecipient.DetailLevel);
					}
				}
				catch (Exception ex)
				{
					if (ex is OutOfMemoryException || ex is AccessViolationException || ex is StackOverflowException || ex is TypeInitializationException)
					{
						throw;
					}
					this.TraceError("Unable to send sharing invite from user {0} to {1} for Calendar {2}. Reason:{3}", new object[]
					{
						this.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress,
						calendarSharingRecipient.EmailAddress,
						this.Request.CalendarName,
						ex
					});
					SharingPermissionLogger.LogEntry(base.MailboxSession, string.Format("FAILED: Sending calendar sharing invite has failed!! Exception: {0}", ex), this.loggingContextIdentifier);
					calendarShareInviteResponse.AddFailureResponse(calendarSharingRecipient.EmailAddress.EmailAddress, ex.Message);
					sendCalendarSharingInviteStatistic.LogFailedInvite(ex, calendarSharingRecipient);
				}
			}
			string successString = sendCalendarSharingInviteStatistic.GetSuccessString();
			if (!string.IsNullOrEmpty(successString))
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, SendCalendarSharingInviteMetadata.Successes, successString);
			}
			string errorString = sendCalendarSharingInviteStatistic.GetErrorString();
			if (!string.IsNullOrEmpty(errorString))
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericError(RequestDetailsLogger.Current, "SCSIError", errorString);
			}
			return calendarShareInviteResponse;
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x000F1FFC File Offset: 0x000F01FC
		private SharingMessageItem CreateMessage(CalendarSharingRecipient recipient)
		{
			SharingProvider provider = this.SharingProviderLocater.GetProvider(recipient.SmtpAddress, recipient.ADRecipient, new FrontEndLocator());
			return SharingMessageItem.CreateWithSpecficProvider(base.MailboxSession, base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts), this.Request.CalendarStoreId, provider, true);
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x000F204C File Offset: 0x000F024C
		private void CopyBodyToItem(SharingMessageItem item)
		{
			if (this.Request.Body.BodyType == BodyType.HTML)
			{
				using (TextWriter textWriter = item.Body.OpenTextWriter(BodyFormat.TextHtml))
				{
					textWriter.Write(this.Request.Body.Value);
					return;
				}
			}
			using (TextWriter textWriter2 = item.Body.OpenTextWriter(BodyFormat.TextPlain))
			{
				textWriter2.Write(this.Request.Body.Value);
			}
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x000F20E8 File Offset: 0x000F02E8
		private void TraceError(string messageFormat, params object[] args)
		{
			ExTraceGlobals.GetCalendarSharingRecipientInfoCallTracer.TraceError((long)this.GetHashCode(), messageFormat, args);
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x000F20FD File Offset: 0x000F02FD
		private void TraceDebug(string messageFormat, params object[] args)
		{
			ExTraceGlobals.GetCalendarSharingRecipientInfoCallTracer.TraceDebug((long)this.GetHashCode(), messageFormat, args);
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x000F2114 File Offset: 0x000F0314
		private void AddDelegateWithPermissions(ADRecipient adRecipient, bool viewPrivateAppointments)
		{
			DelegateUserCollectionHandler delegateUserCollectionHandler = new DelegateUserCollectionHandler(base.MailboxSession, this.adRecipientSessionContext);
			if (delegateUserCollectionHandler.GetDelegateUser(adRecipient) == null)
			{
				string text = adRecipient.PrimarySmtpAddress.ToString();
				UserId userId = new UserId();
				userId.DisplayName = adRecipient.DisplayName;
				userId.PrimarySmtpAddress = text;
				int delegateUsersCount = delegateUserCollectionHandler.DelegateUsersCount;
				delegateUserCollectionHandler.AddDelegateWithCalendarEditorPermission(userId, viewPrivateAppointments);
				if (delegateUsersCount == 0)
				{
					delegateUserCollectionHandler.SetDelegateOptions(DeliverMeetingRequestsType.DelegatesAndSendInformationToMe);
				}
				delegateUserCollectionHandler.SaveDelegate(false);
				SharingPermissionLogger.LogEntry(base.MailboxSession, string.Format("Add Delegate user: {0}; ViewPrivateAppointment: {1}", text, viewPrivateAppointments), this.loggingContextIdentifier);
			}
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x000F21B4 File Offset: 0x000F03B4
		private void RevokeDelegatePermissionIfADelegateUser(ADRecipient adRecipient)
		{
			DelegateUserCollectionHandler delegateUserCollectionHandler = new DelegateUserCollectionHandler(base.MailboxSession, this.adRecipientSessionContext);
			DelegateUser delegateUser = delegateUserCollectionHandler.GetDelegateUser(adRecipient);
			if (delegateUser != null)
			{
				delegateUserCollectionHandler.RemoveDelegate(delegateUser);
				delegateUserCollectionHandler.SaveDelegate(false);
				SharingPermissionLogger.LogEntry(base.MailboxSession, string.Format("Remove Delegate user: {0}", adRecipient.PrimarySmtpAddress), this.loggingContextIdentifier);
			}
		}

		// Token: 0x0400283E RID: 10302
		private static readonly string SendCalendarSharingInviteActionName = typeof(SendCalendarSharingInvite).Name;

		// Token: 0x0400283F RID: 10303
		private readonly SharingProviderLocator SharingProviderLocater;

		// Token: 0x04002840 RID: 10304
		private readonly ADRecipientSessionContext adRecipientSessionContext;

		// Token: 0x04002841 RID: 10305
		private readonly string loggingContextIdentifier;
	}
}
