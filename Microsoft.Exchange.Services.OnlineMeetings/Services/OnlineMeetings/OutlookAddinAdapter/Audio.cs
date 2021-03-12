using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000C6 RID: 198
	[KnownType(typeof(Audio))]
	[XmlType("Audio")]
	[DataContract(Name = "Audio")]
	public class Audio
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0000BEE5 File Offset: 0x0000A0E5
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x0000BEED File Offset: 0x0000A0ED
		[XmlAttribute("Type")]
		[DataMember(Name = "Type", EmitDefaultValue = true)]
		public AudioType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
				this.AudioModalityEnabled = (this.type != AudioType.None);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000BF08 File Offset: 0x0000A108
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x0000BF10 File Offset: 0x0000A110
		[XmlAttribute("AudioModalityEnabled")]
		[DataMember(Name = "AudioModalityEnabled", EmitDefaultValue = true)]
		public bool AudioModalityEnabled { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000BF19 File Offset: 0x0000A119
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x0000BF21 File Offset: 0x0000A121
		[DataMember(Name = "CAA", EmitDefaultValue = false)]
		[XmlElement("CAA")]
		public CaaAudioType CaaAudio { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000BF2A File Offset: 0x0000A12A
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x0000BF32 File Offset: 0x0000A132
		[DataMember(Name = "ACP", EmitDefaultValue = false)]
		[XmlElement("ACP")]
		public AcpAudioType AcpAudio { get; set; }

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000BF3C File Offset: 0x0000A13C
		internal static Audio ConvertFrom(OnlineMeetingResult onlineMeetingResult)
		{
			Audio audio = new Audio();
			audio.AudioModalityEnabled = (onlineMeetingResult.MeetingPolicies.VoipAudio == Policy.Enabled);
			audio.Type = AudioType.CAA;
			audio.CaaAudio = new CaaAudioType();
			audio.CaaAudio.PstnId = onlineMeetingResult.OnlineMeeting.PstnMeetingId;
			if (onlineMeetingResult.DialIn.DialInRegions.Count > 0)
			{
				audio.CaaAudio.Region = new CaaRegion();
				audio.CaaAudio.Region.Name = onlineMeetingResult.DialIn.DialInRegions[0].Name;
			}
			audio.CaaAudio.BypassLobby = (onlineMeetingResult.OnlineMeeting.PstnUserLobbyBypass == LobbyBypass.Enabled);
			audio.CaaAudio.AnnouncementEnabled = (onlineMeetingResult.OnlineMeeting.AttendanceAnnouncementStatus == AttendanceAnnouncementsStatus.Enabled);
			return audio;
		}

		// Token: 0x04000323 RID: 803
		private AudioType type;
	}
}
