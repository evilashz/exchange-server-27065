using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200096F RID: 2415
	internal sealed class SetCalendarSharingPermissions : CalendarActionBase<CalendarActionResponse>
	{
		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06004555 RID: 17749 RVA: 0x000F2EDB File Offset: 0x000F10DB
		// (set) Token: 0x06004556 RID: 17750 RVA: 0x000F2EE3 File Offset: 0x000F10E3
		private SetCalendarSharingPermissionsRequest Request { get; set; }

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06004557 RID: 17751 RVA: 0x000F2EEC File Offset: 0x000F10EC
		// (set) Token: 0x06004558 RID: 17752 RVA: 0x000F2EF4 File Offset: 0x000F10F4
		private StoreObjectId CalendarFolderId { get; set; }

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x000F2EFD File Offset: 0x000F10FD
		// (set) Token: 0x0600455A RID: 17754 RVA: 0x000F2F05 File Offset: 0x000F1105
		private ADObjectId AccessingPrincipalADObjectId { get; set; }

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x000F2F0E File Offset: 0x000F110E
		// (set) Token: 0x0600455C RID: 17756 RVA: 0x000F2F16 File Offset: 0x000F1116
		private ExchangePrincipal AccessingPrincipal { get; set; }

		// Token: 0x0600455D RID: 17757 RVA: 0x000F2F20 File Offset: 0x000F1120
		public SetCalendarSharingPermissions(MailboxSession session, SetCalendarSharingPermissionsRequest request, ADObjectId adObjectId, ADRecipientSessionContext adRecipientSessionContext, StoreObjectId calendarFolderId, ExchangePrincipal accessingPrincipal) : base(session)
		{
			this.Request = request;
			this.AccessingPrincipalADObjectId = adObjectId;
			this.adRecipientSessionContext = adRecipientSessionContext;
			this.CalendarFolderId = calendarFolderId;
			this.AccessingPrincipal = accessingPrincipal;
			this.loggingContextIdentifier = Guid.NewGuid().ToString();
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x000F2F74 File Offset: 0x000F1174
		public override CalendarActionResponse Execute()
		{
			CalendarActionResponse calendarActionResponse = new CalendarActionResponse();
			MailboxSession mailboxSession = base.MailboxSession;
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar);
			bool flag = defaultFolderId.Equals(this.CalendarFolderId);
			SharingPermissionLogger.LogEntry(base.MailboxSession, string.Format("Set calendar sharing permission; calendar type: {0}", flag ? "Primary" : "Seconday"), this.loggingContextIdentifier);
			if (flag)
			{
				CalendarActionError calendarActionError = this.RevokeDeletedDelegateUsersPermission();
				if (calendarActionError == CalendarActionError.CalendarActionDelegateManagementError)
				{
					SetCalendarSharingPermissions.TraceDebug(this.GetHashCode(), "Unexpected error while performing the requested operation on delegate users.", new object[0]);
					calendarActionResponse.ErrorCode = CalendarActionError.CalendarActionDelegateManagementError;
					calendarActionResponse.WasSuccessful = false;
					return calendarActionResponse;
				}
			}
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(mailboxSession, this.Request.CalendarStoreId))
			{
				CalendarFolderPermissionSet permissionSet = calendarFolder.GetPermissionSet();
				SharingPolicy sharingPolicy = DirectoryHelper.ReadSharingPolicy(mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, mailboxSession.MailboxOwner.MailboxInfo.IsArchive, mailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
				List<PermissionSecurityPrincipal> list = new List<PermissionSecurityPrincipal>();
				List<SetCalendarSharingPermissions.CalendarSharingInfo> list2 = new List<SetCalendarSharingPermissions.CalendarSharingInfo>();
				List<SetCalendarSharingPermissions.CalendarSharingInfo> list3 = null;
				DelegateUserCollectionHandler delegateUserCollectionHandler = null;
				bool flag2 = false;
				if (flag)
				{
					delegateUserCollectionHandler = new DelegateUserCollectionHandler(mailboxSession, this.adRecipientSessionContext);
					list3 = new List<SetCalendarSharingPermissions.CalendarSharingInfo>();
				}
				foreach (CalendarFolderPermission calendarFolderPermission in permissionSet)
				{
					PermissionSecurityPrincipal permissionSecurityPrincipal;
					if (this.ValidateFolderPermission(calendarFolderPermission, out permissionSecurityPrincipal))
					{
						if (CalendarSharingPermissionsUtils.ShouldSkipPermission(calendarFolderPermission, flag))
						{
							SetCalendarSharingPermissions.TraceDebug(this.GetHashCode(), string.Format("Skipping permission: {0}; Default (primary) calendar: {1}", calendarFolderPermission.PermissionLevel, flag), new object[0]);
						}
						else
						{
							CalendarSharingPermissionInfo calendarSharingPermissionInfo = this.Request.RecipientGivenIndexString(permissionSecurityPrincipal.IndexString);
							if (calendarSharingPermissionInfo == null)
							{
								list.Add(calendarFolderPermission.Principal);
								SetCalendarSharingPermissions.TraceDebug(this.GetHashCode(), string.Format("Client has removed the permissions for the user {0}", permissionSecurityPrincipal.Type.IsInternalUserPrincipal() ? permissionSecurityPrincipal.ADRecipient.PrimarySmtpAddress.ToString() : permissionSecurityPrincipal.ToString()), new object[0]);
							}
							else
							{
								CalendarSharingDetailLevel calendarSharingDetailLevel;
								Enum.TryParse<CalendarSharingDetailLevel>(calendarSharingPermissionInfo.CurrentDetailLevel, out calendarSharingDetailLevel);
								CalendarSharingDetailLevel calendarSharingDetailLevel2 = CalendarSharingPermissionsUtils.ConvertToCalendarSharingDetailLevelEnum(CalendarSharingPermissionsUtils.GetMaximumDetailLevel(sharingPolicy, calendarFolderPermission), flag);
								if (calendarSharingDetailLevel > calendarSharingDetailLevel2)
								{
									SharingPermissionLogger.LogEntry(base.MailboxSession, string.Format("Requested level of detail: {0}, which is greater than what is allowed by the policy. Reverting to maximum allowed {1}.", calendarSharingDetailLevel, calendarSharingDetailLevel2), this.loggingContextIdentifier);
									SetCalendarSharingPermissions.TraceDebug(this.GetHashCode(), "Caller asked for a level of detail of {0}, which is greater than what is allowed by the policy. Reverting to maximum allowed {1}.", new object[]
									{
										calendarSharingDetailLevel,
										calendarSharingDetailLevel2
									});
									calendarSharingDetailLevel = calendarSharingDetailLevel2;
								}
								CalendarSharingDetailLevel calendarSharingDetailLevel3;
								bool flag3;
								if (!CalendarSharingPermissionsUtils.GetSharingDetailLevelFromPermissionLevel(calendarFolderPermission, flag, delegateUserCollectionHandler, out calendarSharingDetailLevel3, out flag3))
								{
									SharingPermissionLogger.LogEntry(base.MailboxSession, string.Format("OWA does not manage this permission level: {0}. Principal: {1}", calendarFolderPermission.PermissionLevel, permissionSecurityPrincipal.Type.IsInternalUserPrincipal() ? permissionSecurityPrincipal.ADRecipient.PrimarySmtpAddress.ToString() : permissionSecurityPrincipal.ToString()), this.loggingContextIdentifier);
								}
								else
								{
									if (calendarSharingDetailLevel3 != calendarSharingDetailLevel)
									{
										list2.Add(new SetCalendarSharingPermissions.CalendarSharingInfo
										{
											CurrentDetailLevel = calendarSharingDetailLevel,
											ViewPrivateAppointments = calendarSharingPermissionInfo.ViewPrivateAppointments,
											EmailAddress = SetCalendarSharingPermissions.EmailAddressWrapperFromPrincipal(calendarFolderPermission.Principal)
										});
									}
									else if (flag && calendarSharingDetailLevel == CalendarSharingDetailLevel.Delegate && flag3 != calendarSharingPermissionInfo.ViewPrivateAppointments)
									{
										list3.Add(new SetCalendarSharingPermissions.CalendarSharingInfo
										{
											CurrentDetailLevel = calendarSharingDetailLevel,
											ViewPrivateAppointments = calendarSharingPermissionInfo.ViewPrivateAppointments,
											AdRecipient = calendarFolderPermission.Principal.ADRecipient
										});
									}
									if (!flag2)
									{
										flag2 = (calendarSharingDetailLevel == CalendarSharingDetailLevel.Delegate);
									}
								}
							}
						}
					}
				}
				foreach (PermissionSecurityPrincipal permissionSecurityPrincipal2 in list)
				{
					SharingPermissionLogger.LogEntry(base.MailboxSession, string.Format("Calendar sharing permission removed for: {0}", permissionSecurityPrincipal2.Type.IsInternalUserPrincipal() ? permissionSecurityPrincipal2.ADRecipient.PrimarySmtpAddress.ToString() : permissionSecurityPrincipal2.ToString()), this.loggingContextIdentifier);
					permissionSet.RemoveEntry(permissionSecurityPrincipal2);
				}
				this.SavePublishedCalendarOptions(mailboxSession, sharingPolicy);
				if (flag && list3.Count > 0)
				{
					this.ProcessViewPrivateAppointmentStatus(list3);
				}
				if (list2.Count > 0)
				{
					SharingPermissionLogger.LogEntry(base.MailboxSession, "Creating new calendar sharing invites for all updated permissions", this.loggingContextIdentifier);
					CalendarShareInviteResponse calendarShareInviteResponse = this.CreateNewPermissionsAndSendEmailNotification(list2);
					if (calendarShareInviteResponse.FailureResponses.Length > 0)
					{
						SetCalendarSharingPermissions.TraceDebug(this.GetHashCode(), "Unexpected error while setting permissions and sending calendar sharing invite.", new object[0]);
						calendarActionResponse.ErrorCode = CalendarActionError.CalendarActionSendSharingInviteError;
						calendarActionResponse.WasSuccessful = false;
						return calendarActionResponse;
					}
				}
				if (flag && flag2)
				{
					this.SetGlobalDelegateMeetingDelivery(this.Request.SelectedDeliveryOption);
				}
				FolderSaveResult folderSaveResult = calendarFolder.Save();
				if (folderSaveResult.OperationResult != OperationResult.Succeeded)
				{
					SetCalendarSharingPermissions.TraceDebug(this.GetHashCode(), "Folder save operation didn't succeed.", new object[0]);
					calendarActionResponse.ErrorCode = CalendarActionError.CalendarActionCannotSaveCalendar;
					calendarActionResponse.WasSuccessful = false;
					return calendarActionResponse;
				}
				SharingPermissionLogger.LogEntry(base.MailboxSession, "Calendar sharing permissions saved successfully", this.loggingContextIdentifier);
			}
			return calendarActionResponse;
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x000F34C8 File Offset: 0x000F16C8
		private bool ValidateFolderPermission(CalendarFolderPermission permission, out PermissionSecurityPrincipal principal)
		{
			principal = permission.Principal;
			if (principal == null || (principal.Type.IsInternalUserPrincipal() && principal.ADRecipient == null))
			{
				SharingPermissionLogger.LogEntry(base.MailboxSession, string.Format("PermissionSecurityPrincipal is null or ADRecipient is null for an internal user. Permission: {0}", permission), this.loggingContextIdentifier);
				return false;
			}
			return true;
		}

		// Token: 0x06004560 RID: 17760 RVA: 0x000F3518 File Offset: 0x000F1718
		private static EmailAddressWrapper EmailAddressWrapperFromPrincipal(PermissionSecurityPrincipal principal)
		{
			EmailAddressWrapper emailAddressWrapper = new EmailAddressWrapper();
			SmtpAddress smtpAddress;
			if (principal.Type.IsInternalUserPrincipal())
			{
				emailAddressWrapper.Name = principal.ADRecipient.DisplayName;
				smtpAddress = principal.ADRecipient.PrimarySmtpAddress;
			}
			else
			{
				emailAddressWrapper.Name = principal.ExternalUser.Name;
				smtpAddress = principal.ExternalUser.OriginalSmtpAddress;
			}
			emailAddressWrapper.EmailAddress = smtpAddress.ToString();
			emailAddressWrapper.RoutingType = "SMTP";
			return emailAddressWrapper;
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x000F3594 File Offset: 0x000F1794
		private CalendarActionError RevokeDeletedDelegateUsersPermission()
		{
			CalendarActionError result = CalendarActionError.None;
			new Dictionary<ADRecipient, CalendarSharingDetailLevel>();
			MailboxSession mailboxSession = base.MailboxSession;
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(mailboxSession, this.Request.CalendarStoreId))
			{
				CalendarFolderPermissionSet permissionSet = calendarFolder.GetPermissionSet();
				try
				{
					DelegateUserCollectionHandler delegateUserCollectionHandler = new DelegateUserCollectionHandler(mailboxSession, this.adRecipientSessionContext);
					bool flag = false;
					foreach (CalendarFolderPermission permission in permissionSet)
					{
						PermissionSecurityPrincipal permissionSecurityPrincipal;
						if (this.ValidateFolderPermission(permission, out permissionSecurityPrincipal) && permissionSecurityPrincipal.Type.IsInternalUserPrincipal() && this.Request.RecipientGivenIndexString(permissionSecurityPrincipal.IndexString) == null)
						{
							DelegateUser delegateUser = delegateUserCollectionHandler.GetDelegateUser(permissionSecurityPrincipal.ADRecipient);
							if (delegateUser != null)
							{
								delegateUserCollectionHandler.RemoveDelegate(delegateUser);
								flag = true;
								SharingPermissionLogger.LogEntry(base.MailboxSession, string.Format("Delegate permission removal is requested for {0}", permissionSecurityPrincipal.ADRecipient.PrimarySmtpAddress), this.loggingContextIdentifier);
							}
						}
					}
					if (flag)
					{
						delegateUserCollectionHandler.SaveDelegate(false);
						SharingPermissionLogger.LogEntry(base.MailboxSession, "All remove requested delegates have been removed successfully!", this.loggingContextIdentifier);
					}
				}
				catch (DelegateExceptionNotDelegate delegateExceptionNotDelegate)
				{
					SetCalendarSharingPermissions.TraceDebug(this.GetHashCode(), delegateExceptionNotDelegate.Message, new object[0]);
					result = CalendarActionError.CalendarActionDelegateManagementError;
				}
			}
			return result;
		}

		// Token: 0x06004562 RID: 17762 RVA: 0x000F3728 File Offset: 0x000F1928
		private void SavePublishedCalendarOptions(MailboxSession session, SharingPolicy sharingPolicy)
		{
			if (this.Request.PublishedCalendarPermissions != null)
			{
				PublishedCalendar publishedCalendar = null;
				try
				{
					if (PublishedCalendar.TryGetPublishedCalendar(session, this.Request.CalendarStoreId, new ObscureKind?(ObscureKind.Normal), out publishedCalendar))
					{
						DetailLevelEnumType adjustedDetailLevel;
						Enum.TryParse<DetailLevelEnumType>(this.Request.PublishedCalendarPermissions.CurrentDetailLevel, out adjustedDetailLevel);
						adjustedDetailLevel = CalendarSharingPermissionsUtils.GetAdjustedDetailLevel(sharingPolicy.GetAllowedForAnonymousCalendarSharing(), adjustedDetailLevel);
						if (publishedCalendar.DetailLevel != adjustedDetailLevel)
						{
							publishedCalendar.PublishedOptions.MailboxFolderId = new MailboxFolderId(this.AccessingPrincipalADObjectId, this.Request.CalendarStoreId, null);
							publishedCalendar.PublishedOptions.DetailLevel = adjustedDetailLevel;
							publishedCalendar.SavePublishedOptions();
						}
					}
					else
					{
						SetCalendarSharingPermissions.TraceDebug(this.GetHashCode(), "Calendar isn't published. Ignoring Everyone entry.", new object[0]);
					}
				}
				finally
				{
					if (publishedCalendar != null)
					{
						publishedCalendar.Dispose();
					}
				}
			}
		}

		// Token: 0x06004563 RID: 17763 RVA: 0x000F37F8 File Offset: 0x000F19F8
		private CalendarShareInviteResponse CreateNewPermissionsAndSendEmailNotification(List<SetCalendarSharingPermissions.CalendarSharingInfo> calendarSharingInfoCollection)
		{
			CalendarShareInviteResponse calendarShareInviteResponse = new CalendarShareInviteResponse();
			CalendarShareInviteRequest calendarShareInviteRequest = new CalendarShareInviteRequest();
			Microsoft.Exchange.Services.Core.Types.ItemId itemIdFromStoreId = IdConverter.GetItemIdFromStoreId(this.CalendarFolderId, new MailboxId(base.MailboxSession.MailboxGuid));
			calendarShareInviteRequest.CalendarId = new Microsoft.Exchange.Services.Wcf.Types.ItemId
			{
				Id = itemIdFromStoreId.Id,
				ChangeKey = itemIdFromStoreId.ChangeKey
			};
			calendarShareInviteRequest.SharingRecipients = new CalendarSharingRecipient[calendarSharingInfoCollection.Count];
			calendarShareInviteRequest.Body = new BodyContentType
			{
				BodyType = BodyType.HTML
			};
			calendarShareInviteRequest.Subject = ClientStrings.SharingInvitationUpdatedSubjectLine.ToString(base.MailboxSession.PreferedCulture);
			int num = 0;
			foreach (SetCalendarSharingPermissions.CalendarSharingInfo calendarSharingInfo in calendarSharingInfoCollection)
			{
				CalendarSharingRecipient calendarSharingRecipient = new CalendarSharingRecipient();
				calendarSharingRecipient.EmailAddress = calendarSharingInfo.EmailAddress;
				calendarSharingRecipient.DetailLevel = calendarSharingInfo.CurrentDetailLevel.ToString();
				calendarSharingRecipient.ViewPrivateAppointments = calendarSharingInfo.ViewPrivateAppointments;
				calendarShareInviteRequest.SharingRecipients[num] = calendarSharingRecipient;
				num++;
			}
			calendarShareInviteRequest.ValidateRequest(base.MailboxSession, this.adRecipientSessionContext);
			SendCalendarSharingInvite sendCalendarSharingInvite = new SendCalendarSharingInvite(base.MailboxSession, calendarShareInviteRequest, this.AccessingPrincipal, this.adRecipientSessionContext, this.loggingContextIdentifier);
			return sendCalendarSharingInvite.Execute();
		}

		// Token: 0x06004564 RID: 17764 RVA: 0x000F3960 File Offset: 0x000F1B60
		private void ProcessViewPrivateAppointmentStatus(List<SetCalendarSharingPermissions.CalendarSharingInfo> CalendarSharingInfoCollection)
		{
			DelegateUserCollectionHandler delegateUserCollectionHandler = new DelegateUserCollectionHandler(base.MailboxSession, this.adRecipientSessionContext);
			bool flag = false;
			foreach (SetCalendarSharingPermissions.CalendarSharingInfo calendarSharingInfo in CalendarSharingInfoCollection)
			{
				if (calendarSharingInfo.CurrentDetailLevel == CalendarSharingDetailLevel.Delegate)
				{
					ADRecipient adRecipient = calendarSharingInfo.AdRecipient;
					DelegateUser delegateUser = delegateUserCollectionHandler.GetDelegateUser(adRecipient);
					if (delegateUser != null)
					{
						bool viewPrivateAppointments = calendarSharingInfo.ViewPrivateAppointments;
						if (delegateUser.CanViewPrivateItems != viewPrivateAppointments)
						{
							delegateUser.CanViewPrivateItems = viewPrivateAppointments;
							flag = true;
							SharingPermissionLogger.LogEntry(base.MailboxSession, string.Format("Set view private appointment option: {0} for user: {1}", viewPrivateAppointments, adRecipient.PrimarySmtpAddress), this.loggingContextIdentifier);
						}
					}
				}
			}
			if (flag)
			{
				delegateUserCollectionHandler.SaveDelegate(false);
			}
		}

		// Token: 0x06004565 RID: 17765 RVA: 0x000F3A30 File Offset: 0x000F1C30
		private void SetGlobalDelegateMeetingDelivery(DeliverMeetingRequestsType delegateMeetingRequestDeliveryOption)
		{
			DelegateUserCollectionHandler delegateUserCollectionHandler = new DelegateUserCollectionHandler(base.MailboxSession, this.adRecipientSessionContext);
			DeliverMeetingRequestsType meetingRequestDeliveryOptionForDelegateUsers = delegateUserCollectionHandler.GetMeetingRequestDeliveryOptionForDelegateUsers();
			bool flag = meetingRequestDeliveryOptionForDelegateUsers != this.Request.SelectedDeliveryOption;
			if (flag)
			{
				delegateUserCollectionHandler.SetDelegateOptions(delegateMeetingRequestDeliveryOption);
				SharingPermissionLogger.LogEntry(base.MailboxSession, string.Format("Set view global delegate meeting request delivery to: {0}", delegateMeetingRequestDeliveryOption), this.loggingContextIdentifier);
				delegateUserCollectionHandler.SaveDelegate(false);
			}
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x000F3A9B File Offset: 0x000F1C9B
		internal static void TraceDebug(int hashCode, string messageFormat, params object[] args)
		{
			ExTraceGlobals.SetCalendarSharingPermissionsCallTracer.TraceDebug((long)hashCode, messageFormat, args);
		}

		// Token: 0x04002859 RID: 10329
		private readonly ADRecipientSessionContext adRecipientSessionContext;

		// Token: 0x0400285A RID: 10330
		private readonly string loggingContextIdentifier;

		// Token: 0x02000970 RID: 2416
		private class CalendarSharingInfo
		{
			// Token: 0x17000FA8 RID: 4008
			// (get) Token: 0x06004567 RID: 17767 RVA: 0x000F3AAB File Offset: 0x000F1CAB
			// (set) Token: 0x06004568 RID: 17768 RVA: 0x000F3AB3 File Offset: 0x000F1CB3
			public CalendarSharingDetailLevel CurrentDetailLevel { get; set; }

			// Token: 0x17000FA9 RID: 4009
			// (get) Token: 0x06004569 RID: 17769 RVA: 0x000F3ABC File Offset: 0x000F1CBC
			// (set) Token: 0x0600456A RID: 17770 RVA: 0x000F3AC4 File Offset: 0x000F1CC4
			public bool ViewPrivateAppointments { get; set; }

			// Token: 0x17000FAA RID: 4010
			// (get) Token: 0x0600456B RID: 17771 RVA: 0x000F3ACD File Offset: 0x000F1CCD
			// (set) Token: 0x0600456C RID: 17772 RVA: 0x000F3AD5 File Offset: 0x000F1CD5
			public EmailAddressWrapper EmailAddress { get; set; }

			// Token: 0x17000FAB RID: 4011
			// (get) Token: 0x0600456D RID: 17773 RVA: 0x000F3ADE File Offset: 0x000F1CDE
			// (set) Token: 0x0600456E RID: 17774 RVA: 0x000F3AE6 File Offset: 0x000F1CE6
			public ADRecipient AdRecipient { get; set; }
		}
	}
}
