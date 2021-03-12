using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.OnlineMeetings;
using Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002FE RID: 766
	internal class CreateOnlineMeeting : CreateMeetingBase
	{
		// Token: 0x060019BB RID: 6587 RVA: 0x0005B554 File Offset: 0x00059754
		public CreateOnlineMeeting(CallContext callContext, string sipUri, ItemId itemId, bool isPrivate) : base(callContext, sipUri, isPrivate)
		{
			WcfServiceCommandBase.ThrowIfNull(itemId, "itemId", "CreateOnlineMeeting");
			this.ValidateItemId(itemId);
			this.itemId = itemId;
			OwsLogRegistry.Register(CreateOnlineMeeting.CreateOnlineMeetingActionName, typeof(CreateOnlineMeetingMetadata), new Type[0]);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0005B5A4 File Offset: 0x000597A4
		private void ValidateItemId(ItemId newItemId)
		{
			if (string.IsNullOrEmpty(newItemId.Id))
			{
				throw new OwaInvalidRequestException("itemId.Id should not be empty");
			}
			try
			{
				this.idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(newItemId);
			}
			catch (ObjectNotFoundException)
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)4005418156U);
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x0005B600 File Offset: 0x00059800
		private CalendarItem CalendarItem
		{
			get
			{
				if (this.calendarItem == null)
				{
					this.calendarItem = this.BindToItem(this.itemId);
				}
				return this.calendarItem;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x0005B622 File Offset: 0x00059822
		private MailboxSession MailboxSession
		{
			get
			{
				return (this.idAndSession.Session as MailboxSession) ?? base.MailboxIdentityMailboxSession;
			}
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0005B640 File Offset: 0x00059840
		protected override bool ShoudlMeetingBeCreated()
		{
			if (this.CalendarItem != null && !string.IsNullOrEmpty(this.CalendarItem.OnlineMeetingExternalLink))
			{
				base.LogAndTraceError(string.Format("CalendarItem with id '{0}' already has online meeting link '{1}' associated with it", this.itemId.Id, this.CalendarItem.OnlineMeetingExternalLink));
				return false;
			}
			return true;
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0005B690 File Offset: 0x00059890
		protected override void DiposeObjectsIfNeeded()
		{
			if (this.calendarItem != null)
			{
				this.calendarItem.Dispose();
			}
			base.DiposeObjectsIfNeeded();
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0005B6AC File Offset: 0x000598AC
		protected override OnlineMeetingType ProcessOnlineMeetingResult(UserContext userContext, OnlineMeetingResult result)
		{
			OnlineMeetingType result2;
			using (this.CalendarItem)
			{
				if (this.CalendarItem == null)
				{
					result2 = OnlineMeetingType.CreateFailedOnlineMeetingType("Unable to find existing item corresponding to id: " + this.itemId.Id);
				}
				else
				{
					this.CalendarItem.OpenAsReadWrite();
					this.CalendarItem.OnlineMeetingConfLink = result.OnlineMeeting.MeetingUri;
					this.CalendarItem.OnlineMeetingExternalLink = result.OnlineMeeting.WebUrl;
					this.CalendarItem.OnlineMeetingInternalLink = string.Empty;
					this.CalendarItem.UCOpenedConferenceID = Guid.NewGuid().ToString("B");
					this.CalendarItem.ConferenceTelURI = OutlookAddinAdapter.GetConferenceTelUri(result);
					Capabilities ucCapabilities = OutlookAddinAdapter.GetUcCapabilities(result, userContext.UserCulture);
					try
					{
						this.CalendarItem.UCCapabilities = OutlookAddinAdapter.Serialize(ucCapabilities);
					}
					catch (InvalidOperationException ex)
					{
						base.LogAndTraceError("An error occured while serializing UCCapabilities: " + UcwaConfigurationUtilities.BuildFailureLogString(ex));
						return OnlineMeetingType.CreateFailedOnlineMeetingType("An error occured while serializing UCCapabilities");
					}
					try
					{
						this.CalendarItem.UCInband = OutlookAddinAdapter.Serialize(OutlookAddinAdapter.GetUCInband(result));
					}
					catch (InvalidOperationException ex2)
					{
						base.LogAndTraceError("An error occured while serializing UCInband: " + UcwaConfigurationUtilities.BuildFailureLogString(ex2));
						return OnlineMeetingType.CreateFailedOnlineMeetingType("An error occured while serializing UCInband");
					}
					try
					{
						this.CalendarItem.UCMeetingSetting = OutlookAddinAdapter.Serialize(OutlookAddinAdapter.GetUCMeetingSetting(result));
					}
					catch (InvalidOperationException ex3)
					{
						base.LogAndTraceError("An error occured while serializing UCMeetingSetting: " + UcwaConfigurationUtilities.BuildFailureLogString(ex3));
						return OnlineMeetingType.CreateFailedOnlineMeetingType("An error occured while serializing UCMeetingSetting");
					}
					try
					{
						PropertyDefinitionStream updatedOutlookUserPropsPropDefStream = OutlookAddinAdapter.GetUpdatedOutlookUserPropsPropDefStream(this.CalendarItem.OutlookUserPropsPropDefStream);
						this.CalendarItem.OutlookUserPropsPropDefStream = updatedOutlookUserPropsPropDefStream.GetByteArray();
					}
					catch (EndOfStreamException ex4)
					{
						base.LogAndTraceError("[ProcessOnlineMeetingResult] A read error occurred when parsing OutlookUserPropsPropDefStream: " + UcwaConfigurationUtilities.BuildFailureLogString(ex4));
						return OnlineMeetingType.CreateFailedOnlineMeetingType("A read error occured when parsing OutlookUserPropsPropDefStream");
					}
					OnlineMeetingType onlineMeetingType = CreateOnlineMeeting.CreateOnlineMeetingTypeFromOnlineMeetingResult(result, ucCapabilities, base.CallContext.ProtocolLog);
					ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(this.CalendarItem.Id, new MailboxId(this.MailboxSession), null);
					onlineMeetingType.ItemId = new ItemId(concatenatedId.Id, concatenatedId.ChangeKey);
					this.CalendarItem.Save(SaveMode.NoConflictResolutionForceSave);
					result2 = onlineMeetingType;
				}
			}
			return result2;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0005B960 File Offset: 0x00059B60
		protected override OnlineMeetingSettings ConstructOnlineMeetingSettings()
		{
			return new OnlineMeetingSettings();
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0005B974 File Offset: 0x00059B74
		protected override void SetDefaultValuesForOptics()
		{
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.IsTaskCompleted, bool.FalseString);
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.IsUcwaSupported, bool.TrueString);
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.ItemId, this.itemId.Id);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0005B9DC File Offset: 0x00059BDC
		protected override void UpdateOpticsLog(OnlineMeetingResult createMeeting)
		{
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.UserGuid, ExtensibleLogger.FormatPIIValue(this.sipUri));
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.ConferenceId, createMeeting.OnlineMeeting.PstnMeetingId);
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.LeaderCount, createMeeting.LogEntry.MeetingSettings.Leaders.Count);
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.AttendeeCount, createMeeting.LogEntry.MeetingSettings.Attendees.Count);
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.ExpiryTime, createMeeting.LogEntry.MeetingSettings.ExpiryTime);
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.DefaultEntryExitAnnouncement, createMeeting.LogEntry.DefaultValuesResource.EntryExitAnnouncement.ToString());
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.AutomaticLeaderAssignment, createMeeting.LogEntry.DefaultValuesResource.AutomaticLeaderAssignment.ToString());
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.AccessLevel, createMeeting.LogEntry.DefaultValuesResource.AccessLevel.ToString());
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.ParticipantsWarningThreshold, createMeeting.LogEntry.DefaultValuesResource.ParticipantsWarningThreshold.ToString());
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.PolicyEntryExitAnnouncement, createMeeting.LogEntry.PoliciesResource.EntryExitAnnouncement.ToString());
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.PhoneUserAdmission, createMeeting.LogEntry.PoliciesResource.PhoneUserAdmission.ToString());
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.ExternalUserMeetingRecording, createMeeting.LogEntry.PoliciesResource.ExternalUserMeetingRecording.ToString());
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.MeetingRecording, createMeeting.LogEntry.PoliciesResource.MeetingRecording.ToString());
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.VoipAudio, createMeeting.LogEntry.PoliciesResource.VoipAudio.ToString());
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.MeetingSize, createMeeting.LogEntry.PoliciesResource.MeetingSize);
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.WorkerExceptions, createMeeting.LogEntry.BuildFailureString());
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0005BCD0 File Offset: 0x00059ED0
		private CalendarItem BindToItem(ItemId itemId)
		{
			StoreId id;
			try
			{
				id = StoreId.EwsIdToStoreObjectId(itemId.Id);
			}
			catch (InvalidIdMalformedException ex)
			{
				base.LogAndTraceError(string.Format("[GetCalendarItem] Invalid itemId '{0}' - exception: {1}", itemId.Id, UcwaConfigurationUtilities.BuildFailureLogString(ex)));
				return null;
			}
			CalendarItem result = null;
			try
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string>(0, 0L, "[CreateOnlineMeeting.GetCalendarItem] Trying to bind to item with provided ItemId '{0}'", itemId.Id);
				result = CalendarItem.Bind(this.MailboxSession, id);
			}
			catch (ArgumentException ex2)
			{
				base.LogAndTraceError(string.Format("[GetCalendarItem] Unable to bind to item with provided ItemId '{0}'; Exception: {1}", itemId.Id, UcwaConfigurationUtilities.BuildFailureLogString(ex2)));
			}
			catch (ObjectNotFoundException ex3)
			{
				base.LogAndTraceError(string.Format("[GetCalendarItem] Unable to find item with provided ItemId '{0}'; Exception: {1}", itemId.Id, UcwaConfigurationUtilities.BuildFailureLogString(ex3)));
			}
			return result;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0005BDA4 File Offset: 0x00059FA4
		private static OnlineMeetingAccessLevel ConvertAccessLevel(AccessLevel accessLevel)
		{
			switch (accessLevel)
			{
			case AccessLevel.SameEnterprise:
				return OnlineMeetingAccessLevel.Internal;
			case AccessLevel.Locked:
				return OnlineMeetingAccessLevel.Locked;
			case AccessLevel.Invited:
				return OnlineMeetingAccessLevel.Invited;
			case AccessLevel.Everyone:
				return OnlineMeetingAccessLevel.Everyone;
			default:
				throw new ArgumentException(string.Format("'{0}' is an invalid value for AccessLevel", accessLevel));
			}
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x0005BDEC File Offset: 0x00059FEC
		private static LobbyBypass ConvertLobbyBypass(LobbyBypass lobbyBypass)
		{
			switch (lobbyBypass)
			{
			case LobbyBypass.Disabled:
				return LobbyBypass.Disabled;
			case LobbyBypass.Enabled:
				return LobbyBypass.EnabledForGatewayParticipants;
			default:
				throw new ArgumentException(string.Format("'{0}' is an invalid value for LobbyBypass", lobbyBypass));
			}
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0005BE24 File Offset: 0x0005A024
		private static Presenters ConvertPresenters(AutomaticLeaderAssignment leaderAssignment)
		{
			switch (leaderAssignment)
			{
			case AutomaticLeaderAssignment.Disabled:
				return Presenters.Disabled;
			case AutomaticLeaderAssignment.SameEnterprise:
				return Presenters.Internal;
			case AutomaticLeaderAssignment.Everyone:
				return Presenters.Everyone;
			default:
				throw new ArgumentException(string.Format("'{0}' is an invalid value for AutomaticLeaderAssignment", leaderAssignment));
			}
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0005BE64 File Offset: 0x0005A064
		private static OnlineMeetingType CreateOnlineMeetingTypeFromOnlineMeetingResult(OnlineMeetingResult onlineMeetingResult, Capabilities capabilities, RequestDetailsLogger logger)
		{
			List<Exception> list = new List<Exception>();
			OnlineMeetingType onlineMeetingType = new OnlineMeetingType();
			onlineMeetingType.HelpUrl = onlineMeetingResult.CustomizationValues.InvitationHelpUrl;
			onlineMeetingType.LegalUrl = onlineMeetingResult.CustomizationValues.InvitationLegalUrl;
			onlineMeetingType.CustomFooterText = onlineMeetingResult.CustomizationValues.InvitationFooterText;
			onlineMeetingType.ExternalDirectoryUri = onlineMeetingResult.DialIn.ExternalDirectoryUri;
			onlineMeetingType.InternalDirectoryUri = onlineMeetingResult.DialIn.InternalDirectoryUri;
			onlineMeetingType.LogoUrl = onlineMeetingResult.CustomizationValues.InvitationLogoUrl;
			onlineMeetingType.WebUrl = onlineMeetingResult.OnlineMeeting.WebUrl;
			try
			{
				onlineMeetingType.AccessLevel = CreateOnlineMeeting.ConvertAccessLevel(onlineMeetingResult.OnlineMeeting.Accesslevel);
			}
			catch (ArgumentException item)
			{
				list.Add(item);
			}
			try
			{
				onlineMeetingType.LobbyBypass = CreateOnlineMeeting.ConvertLobbyBypass(onlineMeetingResult.OnlineMeeting.PstnUserLobbyBypass);
			}
			catch (ArgumentException item2)
			{
				list.Add(item2);
			}
			try
			{
				onlineMeetingType.Presenters = CreateOnlineMeeting.ConvertPresenters(onlineMeetingResult.OnlineMeeting.AutomaticLeaderAssignment);
			}
			catch (ArgumentException item3)
			{
				list.Add(item3);
			}
			if (capabilities == null)
			{
				return onlineMeetingType;
			}
			if (onlineMeetingResult.DialIn.IsAudioConferenceProviderEnabled)
			{
				onlineMeetingType.AcpInformation = new AcpInformationType();
				onlineMeetingType.AcpInformation.ParticipantPassCode = onlineMeetingResult.DialIn.ParticipantPassCode;
				onlineMeetingType.AcpInformation.TollNumber = onlineMeetingResult.DialIn.TollNumber;
				onlineMeetingType.AcpInformation.TollFreeNumbers = onlineMeetingResult.DialIn.TollFreeNumbers;
			}
			else
			{
				onlineMeetingType.ConferenceId = onlineMeetingResult.OnlineMeeting.PstnMeetingId;
				List<DialInNumberType> list2 = new List<DialInNumberType>();
				foreach (Region region in capabilities.Regions)
				{
					foreach (AccessNumber accessNumber in region.AccessNumbers)
					{
						DialInNumberType dialInNumberType = new DialInNumberType();
						dialInNumberType.RegionName = region.Name;
						dialInNumberType.Number = accessNumber.Number;
						try
						{
							dialInNumberType.Language = new CultureInfo(accessNumber.LanguageID).NativeName;
						}
						catch (CultureNotFoundException ex)
						{
							ExTraceGlobals.OnlineMeetingTracer.TraceError<int, string>(0, 0L, "CreateOnlineMeeting::CreateOnlineMeetingTypeFromOnlineMeetingResult. CultureNotFoundException occurred when creating CultureInfo from LanguageId '{0}'. Exception:'{1}'.  ", accessNumber.LanguageID, UcwaConfigurationUtilities.BuildFailureLogString(ex));
							list.Add(new OwaException(string.Format("Unable to create cultureInfo corresponding to lcid '{0}' for region '{1}' with number '{2}'", accessNumber.LanguageID, region.Name, accessNumber.Number), ex));
						}
						list2.Add(dialInNumberType);
					}
				}
				onlineMeetingType.Numbers = list2.ToArray();
			}
			if (list.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (Exception ex2 in list)
				{
					stringBuilder.AppendFormat("{0}::", UcwaConfigurationUtilities.BuildFailureLogString(ex2));
				}
				logger.Set(CreateOnlineMeetingMetadata.Exceptions, stringBuilder.ToString());
			}
			return onlineMeetingType;
		}

		// Token: 0x04000E34 RID: 3636
		private static readonly string CreateOnlineMeetingActionName = typeof(CreateOnlineMeeting).Name;

		// Token: 0x04000E35 RID: 3637
		private readonly ItemId itemId;

		// Token: 0x04000E36 RID: 3638
		private IdAndSession idAndSession;

		// Token: 0x04000E37 RID: 3639
		private CalendarItem calendarItem;
	}
}
