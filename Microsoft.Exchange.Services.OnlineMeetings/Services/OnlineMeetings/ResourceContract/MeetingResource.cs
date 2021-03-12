using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200004E RID: 78
	[DataContract(Name = "meetingResource")]
	internal abstract class MeetingResource : Resource
	{
		// Token: 0x0600027A RID: 634 RVA: 0x00008F79 File Offset: 0x00007179
		protected MeetingResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00008F82 File Offset: 0x00007182
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00008F94 File Offset: 0x00007194
		[DataMember(Name = "entryExitAnnouncement", EmitDefaultValue = false)]
		public EntryExitAnnouncement? EntryExitAnnouncement
		{
			get
			{
				return new EntryExitAnnouncement?(base.GetValue<EntryExitAnnouncement>("entryExitAnnouncement"));
			}
			set
			{
				base.SetValue<EntryExitAnnouncement>("entryExitAnnouncement", value);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00008FA7 File Offset: 0x000071A7
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00008FB9 File Offset: 0x000071B9
		[DataMember(Name = "automaticLeaderAssignment", EmitDefaultValue = false)]
		public AutomaticLeaderAssignment? AutomaticLeaderAssignment
		{
			get
			{
				return new AutomaticLeaderAssignment?(base.GetValue<AutomaticLeaderAssignment>("automaticLeaderAssignment"));
			}
			set
			{
				base.SetValue<AutomaticLeaderAssignment>("automaticLeaderAssignment", value);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00008FCC File Offset: 0x000071CC
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00008FDE File Offset: 0x000071DE
		[DataMember(Name = "accessLevel", EmitDefaultValue = false)]
		public AccessLevel? AccessLevel
		{
			get
			{
				return new AccessLevel?(base.GetValue<AccessLevel>("accessLevel"));
			}
			set
			{
				base.SetValue<AccessLevel>("accessLevel", value);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00008FF1 File Offset: 0x000071F1
		// (set) Token: 0x06000282 RID: 642 RVA: 0x00008FFE File Offset: 0x000071FE
		[DataMember(Name = "onlineMeetingId", EmitDefaultValue = false)]
		public string OnlineMeetingId
		{
			get
			{
				return base.GetValue<string>("onlineMeetingId");
			}
			set
			{
				base.SetValue<string>("onlineMeetingId", value);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000900C File Offset: 0x0000720C
		// (set) Token: 0x06000284 RID: 644 RVA: 0x00009019 File Offset: 0x00007219
		[DataMember(Name = "onlineMeetingUri", EmitDefaultValue = false)]
		public string OnlineMeetingUri
		{
			get
			{
				return base.GetValue<string>("onlineMeetingUri");
			}
			set
			{
				base.SetValue<string>("onlineMeetingUri", value);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00009027 File Offset: 0x00007227
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00009034 File Offset: 0x00007234
		[DataMember(Name = "description", EmitDefaultValue = false)]
		public string Description
		{
			get
			{
				return base.GetValue<string>("description");
			}
			set
			{
				base.SetValue<string>("description", value);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00009042 File Offset: 0x00007242
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000904F File Offset: 0x0000724F
		[DataMember(Name = "expirationTime", EmitDefaultValue = false)]
		public DateTime? ExpirationTime
		{
			get
			{
				return base.GetValue<DateTime?>("expirationTime");
			}
			set
			{
				base.SetValue<DateTime?>("expirationTime", value);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00009062 File Offset: 0x00007262
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000906F File Offset: 0x0000726F
		[DataMember(Name = "extensions", EmitDefaultValue = false)]
		public ResourceCollection<OnlineMeetingExtensionResource> Extensions
		{
			get
			{
				return base.GetValue<ResourceCollection<OnlineMeetingExtensionResource>>("extensions");
			}
			set
			{
				base.SetValue<ResourceCollection<OnlineMeetingExtensionResource>>("extensions", value);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000907D File Offset: 0x0000727D
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000908A File Offset: 0x0000728A
		[DataMember(Name = "isActive", EmitDefaultValue = false)]
		public bool? IsActive
		{
			get
			{
				return base.GetValue<bool?>("isActive");
			}
			set
			{
				base.SetValue<bool?>("isActive", value);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000909D File Offset: 0x0000729D
		// (set) Token: 0x0600028E RID: 654 RVA: 0x000090AA File Offset: 0x000072AA
		[DataMember(Name = "phoneUserAdmission", EmitDefaultValue = false)]
		public PhoneUserAdmission? PhoneUserAdmission
		{
			get
			{
				return base.GetValue<PhoneUserAdmission?>("phoneUserAdmission");
			}
			set
			{
				base.SetValue<PhoneUserAdmission?>("phoneUserAdmission", value);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600028F RID: 655 RVA: 0x000090BD File Offset: 0x000072BD
		// (set) Token: 0x06000290 RID: 656 RVA: 0x000090CA File Offset: 0x000072CA
		[DataMember(Name = "lobbyBypassForPhoneUsers", EmitDefaultValue = false)]
		public LobbyBypassForPhoneUsers? LobbyBypassForPhoneUsers
		{
			get
			{
				return base.GetValue<LobbyBypassForPhoneUsers?>("lobbyBypassForPhoneUsers");
			}
			set
			{
				base.SetValue<LobbyBypassForPhoneUsers?>("lobbyBypassForPhoneUsers", value);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000090DD File Offset: 0x000072DD
		// (set) Token: 0x06000292 RID: 658 RVA: 0x000090EA File Offset: 0x000072EA
		[DataMember(Name = "organizerUri", EmitDefaultValue = false)]
		public string OrganizerUri
		{
			get
			{
				return base.GetValue<string>("organizerUri");
			}
			set
			{
				base.SetValue<string>("organizerUri", value);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000090F8 File Offset: 0x000072F8
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00009105 File Offset: 0x00007305
		[DataMember(Name = "leaders", EmitDefaultValue = false)]
		public string[] Leaders
		{
			get
			{
				return base.GetValue<string[]>("leaders");
			}
			set
			{
				base.SetValue<string[]>("leaders", value);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00009113 File Offset: 0x00007313
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00009120 File Offset: 0x00007320
		[DataMember(Name = "attendees", EmitDefaultValue = false)]
		public string[] Attendees
		{
			get
			{
				return base.GetValue<string[]>("attendees");
			}
			set
			{
				base.SetValue<string[]>("attendees", value);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000912E File Offset: 0x0000732E
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000913B File Offset: 0x0000733B
		[DataMember(Name = "conferenceId", EmitDefaultValue = false)]
		public string ConferenceId
		{
			get
			{
				return base.GetValue<string>("conferenceId");
			}
			set
			{
				base.SetValue<string>("conferenceId", value);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00009149 File Offset: 0x00007349
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000915B File Offset: 0x0000735B
		[DataMember(Name = "onlineMeetingRel", EmitDefaultValue = false)]
		public OnlineMeetingRel? OnlineMeetingRel
		{
			get
			{
				return new OnlineMeetingRel?(base.GetValue<OnlineMeetingRel>("onlineMeetingRel"));
			}
			set
			{
				base.SetValue<OnlineMeetingRel>("onlineMeetingRel", value);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000916E File Offset: 0x0000736E
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000917B File Offset: 0x0000737B
		[DataMember(Name = "subject", EmitDefaultValue = false)]
		public string Subject
		{
			get
			{
				return base.GetValue<string>("subject");
			}
			set
			{
				base.SetValue<string>("subject", value);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00009189 File Offset: 0x00007389
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00009196 File Offset: 0x00007396
		[DataMember(Name = "joinUrl", EmitDefaultValue = false)]
		public string JoinUrl
		{
			get
			{
				return base.GetValue<string>("joinUrl");
			}
			set
			{
				base.SetValue<string>("joinUrl", value);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600029F RID: 671 RVA: 0x000091A4 File Offset: 0x000073A4
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x000091AC File Offset: 0x000073AC
		[Ignore]
		public long? Version { get; set; }

		// Token: 0x04000196 RID: 406
		private const string EntryExitAnnouncementPropertyName = "entryExitAnnouncement";

		// Token: 0x04000197 RID: 407
		private const string AutomaticLeaderAssignmentPropertyName = "automaticLeaderAssignment";

		// Token: 0x04000198 RID: 408
		private const string AccessLevelPropertyName = "accessLevel";

		// Token: 0x04000199 RID: 409
		private const string OnlineMeetingIdPropertyName = "onlineMeetingId";

		// Token: 0x0400019A RID: 410
		private const string OnlineMeetingUriPropertyName = "onlineMeetingUri";

		// Token: 0x0400019B RID: 411
		private const string DescriptionPropertyName = "description";

		// Token: 0x0400019C RID: 412
		private const string ExpirationTimePropertyName = "expirationTime";

		// Token: 0x0400019D RID: 413
		private const string IsActivePropertyName = "isActive";

		// Token: 0x0400019E RID: 414
		private const string ExtensionsPropertyName = "extensions";

		// Token: 0x0400019F RID: 415
		private const string LobbyBypassForPhoneUsersPropertyName = "lobbyBypassForPhoneUsers";

		// Token: 0x040001A0 RID: 416
		private const string OrganizerUriPropertyName = "organizerUri";

		// Token: 0x040001A1 RID: 417
		private const string LeadersPropertyName = "leaders";

		// Token: 0x040001A2 RID: 418
		private const string AttendeesPropertyName = "attendees";

		// Token: 0x040001A3 RID: 419
		private const string ConferenceIdPropertyName = "conferenceId";

		// Token: 0x040001A4 RID: 420
		private const string OnlineMeetingRelPropertyName = "onlineMeetingRel";

		// Token: 0x040001A5 RID: 421
		private const string SubjectPropertyName = "subject";

		// Token: 0x040001A6 RID: 422
		private const string JoinUrlPropertyName = "joinUrl";
	}
}
