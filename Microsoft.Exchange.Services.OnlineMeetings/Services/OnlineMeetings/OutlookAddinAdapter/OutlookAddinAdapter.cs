using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000C0 RID: 192
	internal static class OutlookAddinAdapter
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x0000B60C File Offset: 0x0000980C
		internal static Capabilities GetUcCapabilities(OnlineMeetingResult onlineMeetingResult, CultureInfo userCulture)
		{
			return new Capabilities
			{
				CAAEnabled = (onlineMeetingResult.MeetingPolicies.AttendanceAnnouncementsStatus == AttendanceAnnouncementsStatus.Enabled),
				AnonymousAllowed = (onlineMeetingResult.DefaultValues.AccessLevel == AccessLevel.Everyone),
				PublicMeetingLimit = (onlineMeetingResult.OnlineMeeting.IsAssignedMeeting ? 1 : 0),
				PublicMeetingDefault = (onlineMeetingResult.DefaultValues.SchedulingTemplate == SchedulingTemplate.AdministratorSupplied),
				AutoPromoteAllowed = AutoPromote.ConvertFrom(onlineMeetingResult.OnlineMeeting.AutomaticLeaderAssignment).Value,
				DefaultAutoPromote = AutoPromote.ConvertFrom(onlineMeetingResult.DefaultValues.AutomaticLeaderAssignment).Value,
				BypassLobbyEnabled = (onlineMeetingResult.DefaultValues.PstnLobbyByPass == LobbyBypass.Enabled),
				ForgetPinUrl = onlineMeetingResult.DialIn.InternalDirectoryUri,
				LocalPhoneUrl = onlineMeetingResult.DialIn.InternalDirectoryUri,
				DefaultAnnouncementEnabled = (onlineMeetingResult.DefaultValues.AttendanceAnnouncementsStatus == AttendanceAnnouncementsStatus.Enabled),
				ACPMCUEnabled = onlineMeetingResult.DialIn.IsAudioConferenceProviderEnabled,
				Regions = Region.ConvertFrom(onlineMeetingResult.DialIn, userCulture),
				CustomInvite = CustomInvite.ConvertFrom(onlineMeetingResult.CustomizationValues)
			};
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000B728 File Offset: 0x00009928
		internal static Inband GetUCInband(OnlineMeetingResult onlineMeetingResult)
		{
			return new Inband
			{
				ACPs = AcpInformation.ConvertFrom(onlineMeetingResult.DialIn),
				MaxMeetingSize = onlineMeetingResult.MeetingPolicies.MeetingSize,
				AudioEnabled = (onlineMeetingResult.MeetingPolicies.VoipAudio == Policy.Enabled),
				EnableEnterpriseCustomizedHelp = !string.IsNullOrWhiteSpace(onlineMeetingResult.CustomizationValues.EnterpriseHelpUrl),
				CustomizedHelpUrl = onlineMeetingResult.CustomizationValues.EnterpriseHelpUrl
			};
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000B79C File Offset: 0x0000999C
		internal static MeetingSetting GetUCMeetingSetting(OnlineMeetingResult onlineMeetingResult)
		{
			return new MeetingSetting
			{
				IsPublic = onlineMeetingResult.OnlineMeeting.IsAssignedMeeting,
				ConferenceID = onlineMeetingResult.OnlineMeeting.Id,
				HttpJoinLink = onlineMeetingResult.OnlineMeeting.WebUrl,
				ConfJoinLink = onlineMeetingResult.OnlineMeeting.MeetingUri,
				Subject = onlineMeetingResult.OnlineMeeting.Subject,
				ExpiryDate = onlineMeetingResult.OnlineMeeting.ExpiryTime,
				AutoPromote = AutoPromote.ConvertFrom(onlineMeetingResult.OnlineMeeting.AutomaticLeaderAssignment),
				BodyLanguage = MeetingSetting.GetBodyLanguage(onlineMeetingResult.DialIn.DialInRegions),
				Participants = Participants.ConvertFrom(onlineMeetingResult.OnlineMeeting.Attendees, onlineMeetingResult.OnlineMeeting.Leaders),
				Permissions = Permissions.ConvertFrom(onlineMeetingResult.OnlineMeeting.Accesslevel),
				MeetingOwner = MeetingOwner.ConvertFrom(onlineMeetingResult.OnlineMeeting.OrganizerUri),
				Audio = Audio.ConvertFrom(onlineMeetingResult)
			};
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000B89C File Offset: 0x00009A9C
		internal static string GetConferenceTelUri(OnlineMeetingResult onlineMeetingResult)
		{
			if (onlineMeetingResult.DialIn.DialInRegions.Count == 0)
			{
				return string.Empty;
			}
			return string.Format("tel:{0},,{1}", onlineMeetingResult.DialIn.DialInRegions[0].Number, onlineMeetingResult.OnlineMeeting.PstnMeetingId);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000B8EC File Offset: 0x00009AEC
		internal static string Serialize(object ucObject)
		{
			if (ucObject == null)
			{
				throw new ArgumentNullException("ucObject");
			}
			Type type = ucObject.GetType();
			if (type != typeof(Capabilities) && type != typeof(Inband) && type != typeof(MeetingSetting))
			{
				throw new ArgumentException(string.Format("Not supported for serialization: {0}", type.Name));
			}
			string result;
			try
			{
				SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(type);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					safeXmlSerializer.Serialize(memoryStream, ucObject);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					using (StreamReader streamReader = new StreamReader(memoryStream))
					{
						string text = streamReader.ReadToEnd();
						text = text.Replace("\r\n", string.Empty);
						result = text;
					}
				}
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceError<string, string>(0, 0L, "[OutlookAddinAdapter.Serialize] An InvalidOperationException occurred while serializing object of type {0}: {1}", type.ToString(), (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
				throw;
			}
			return result;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000BA1C File Offset: 0x00009C1C
		internal static object Deserialize(string ucXml, Type targetType)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (targetType != typeof(Capabilities) && targetType != typeof(Inband) && targetType != typeof(MeetingSetting))
			{
				throw new ArgumentException(string.Format("Not supported for derialization: {0}", targetType.Name));
			}
			try
			{
				SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(targetType);
				using (StringReader stringReader = new StringReader(ucXml))
				{
					using (XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(stringReader))
					{
						return safeXmlSerializer.Deserialize(xmlTextReader);
					}
				}
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceError<string, string, string>(0, 0L, "[OutlookAddinAdapter.Deserialize] An xmlException occurred while deserializing object of type {0} from xml '{1}': {2}", targetType.ToString(), ucXml, (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
			}
			return null;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000BB20 File Offset: 0x00009D20
		internal static PropertyDefinitionStream GetUpdatedOutlookUserPropsPropDefStream(byte[] propDefStreamBlob)
		{
			PropertyDefinitionStream propertyDefinitionStream = new PropertyDefinitionStream(propDefStreamBlob);
			propertyDefinitionStream.AddFieldDefinition(OutlookAddinAdapter.CreateOnlineMeetingFieldDefinition(CalendarItemBaseSchema.ConferenceTelURI.Name));
			propertyDefinitionStream.AddFieldDefinition(OutlookAddinAdapter.CreateOnlineMeetingFieldDefinition(CalendarItemBaseSchema.OnlineMeetingConfLink.Name));
			propertyDefinitionStream.AddFieldDefinition(OutlookAddinAdapter.CreateOnlineMeetingFieldDefinition(CalendarItemBaseSchema.OnlineMeetingExternalLink.Name));
			propertyDefinitionStream.AddFieldDefinition(OutlookAddinAdapter.CreateOnlineMeetingFieldDefinition(CalendarItemBaseSchema.OnlineMeetingInternalLink.Name));
			propertyDefinitionStream.AddFieldDefinition(OutlookAddinAdapter.CreateOnlineMeetingFieldDefinition(CalendarItemBaseSchema.UCCapabilities.Name));
			propertyDefinitionStream.AddFieldDefinition(OutlookAddinAdapter.CreateOnlineMeetingFieldDefinition(CalendarItemBaseSchema.UCInband.Name));
			propertyDefinitionStream.AddFieldDefinition(OutlookAddinAdapter.CreateOnlineMeetingFieldDefinition(CalendarItemBaseSchema.UCMeetingSetting.Name));
			propertyDefinitionStream.AddFieldDefinition(OutlookAddinAdapter.CreateOnlineMeetingFieldDefinition(CalendarItemBaseSchema.UCOpenedConferenceID.Name));
			return propertyDefinitionStream;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		internal static FieldDefinitionStream CreateOnlineMeetingFieldDefinition(string fieldName)
		{
			return new FieldDefinitionStream(fieldName)
			{
				Flags = (FieldDefinitionStreamFlags.PDO_IS_CUSTOM | FieldDefinitionStreamFlags.PDO_PRINT_SAVEAS_DEF),
				Vt = VarEnum.VT_BSTR
			};
		}
	}
}
