using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200006C RID: 108
	[Parent("user")]
	[DataContract(Name = "OnlineMeetingPoliciesResource")]
	[Get(typeof(OnlineMeetingPoliciesResource))]
	internal class OnlineMeetingPoliciesResource : OnlineMeetingCapabilityResource
	{
		// Token: 0x0600030C RID: 780 RVA: 0x000097AF File Offset: 0x000079AF
		public OnlineMeetingPoliciesResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600030D RID: 781 RVA: 0x000097B8 File Offset: 0x000079B8
		// (set) Token: 0x0600030E RID: 782 RVA: 0x000097C5 File Offset: 0x000079C5
		[DataMember(Name = "entryExitAnnouncement", EmitDefaultValue = false)]
		public GenericPolicy EntryExitAnnouncement
		{
			get
			{
				return base.GetValue<GenericPolicy>("entryExitAnnouncement");
			}
			set
			{
				base.SetValue<GenericPolicy>("entryExitAnnouncement", value);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600030F RID: 783 RVA: 0x000097D8 File Offset: 0x000079D8
		// (set) Token: 0x06000310 RID: 784 RVA: 0x000097E5 File Offset: 0x000079E5
		[DataMember(Name = "phoneUserAdmission", EmitDefaultValue = false)]
		public GenericPolicy PhoneUserAdmission
		{
			get
			{
				return base.GetValue<GenericPolicy>("phoneUserAdmission");
			}
			set
			{
				base.SetValue<GenericPolicy>("phoneUserAdmission", value);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000311 RID: 785 RVA: 0x000097F8 File Offset: 0x000079F8
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00009805 File Offset: 0x00007A05
		[DataMember(Name = "externalUserMeetingRecording", EmitDefaultValue = false)]
		public GenericPolicy ExternalUserMeetingRecording
		{
			get
			{
				return base.GetValue<GenericPolicy>("externalUserMeetingRecording");
			}
			set
			{
				base.SetValue<GenericPolicy>("externalUserMeetingRecording", value);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00009818 File Offset: 0x00007A18
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00009825 File Offset: 0x00007A25
		[DataMember(Name = "meetingRecording", EmitDefaultValue = false)]
		public GenericPolicy MeetingRecording
		{
			get
			{
				return base.GetValue<GenericPolicy>("meetingRecording");
			}
			set
			{
				base.SetValue<GenericPolicy>("meetingRecording", value);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00009838 File Offset: 0x00007A38
		// (set) Token: 0x06000316 RID: 790 RVA: 0x00009845 File Offset: 0x00007A45
		[DataMember(Name = "voipAudio", EmitDefaultValue = false)]
		public GenericPolicy VoipAudio
		{
			get
			{
				return base.GetValue<GenericPolicy>("voipAudio");
			}
			set
			{
				base.SetValue<GenericPolicy>("voipAudio", value);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00009858 File Offset: 0x00007A58
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00009865 File Offset: 0x00007A65
		[DataMember(Name = "meetingSize", EmitDefaultValue = false)]
		public int MeetingSize
		{
			get
			{
				return base.GetValue<int>("meetingSize");
			}
			set
			{
				base.SetValue<int>("meetingSize", value);
			}
		}

		// Token: 0x040001F0 RID: 496
		public const string Token = "onlineMeetingPolicies";

		// Token: 0x040001F1 RID: 497
		private const string EntryExitAnnouncementPropertyName = "entryExitAnnouncement";

		// Token: 0x040001F2 RID: 498
		private const string PhoneUserAdmissionPropertyName = "phoneUserAdmission";

		// Token: 0x040001F3 RID: 499
		private const string ExternalUserMeetingRecordingPropertyName = "externalUserMeetingRecording";

		// Token: 0x040001F4 RID: 500
		private const string MeetingRecordingPropertyName = "meetingRecording";

		// Token: 0x040001F5 RID: 501
		private const string VoipAudioPropertyName = "voipAudio";

		// Token: 0x040001F6 RID: 502
		private const string MeetingSizePropertyName = "meetingSize";
	}
}
