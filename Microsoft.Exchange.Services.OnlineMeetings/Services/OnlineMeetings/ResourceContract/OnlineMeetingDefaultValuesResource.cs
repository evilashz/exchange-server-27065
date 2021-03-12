using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000062 RID: 98
	[Get(typeof(OnlineMeetingDefaultValuesResource))]
	[DataContract(Name = "OnlineMeetingDefaultValuesResource")]
	[Parent("user")]
	internal class OnlineMeetingDefaultValuesResource : OnlineMeetingCapabilityResource
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00009340 File Offset: 0x00007540
		public OnlineMeetingDefaultValuesResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00009349 File Offset: 0x00007549
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00009356 File Offset: 0x00007556
		[DataMember(Name = "entryExitAnnouncement", EmitDefaultValue = false)]
		public EntryExitAnnouncement EntryExitAnnouncement
		{
			get
			{
				return base.GetValue<EntryExitAnnouncement>("entryExitAnnouncement");
			}
			set
			{
				base.SetValue<EntryExitAnnouncement>("entryExitAnnouncement", value);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00009369 File Offset: 0x00007569
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00009376 File Offset: 0x00007576
		[DataMember(Name = "automaticLeaderAssignment", EmitDefaultValue = false)]
		public AutomaticLeaderAssignment AutomaticLeaderAssignment
		{
			get
			{
				return base.GetValue<AutomaticLeaderAssignment>("automaticLeaderAssignment");
			}
			set
			{
				base.SetValue<AutomaticLeaderAssignment>("automaticLeaderAssignment", value);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00009389 File Offset: 0x00007589
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00009396 File Offset: 0x00007596
		[DataMember(Name = "accessLevel", EmitDefaultValue = false)]
		public AccessLevel AccessLevel
		{
			get
			{
				return base.GetValue<AccessLevel>("accessLevel");
			}
			set
			{
				base.SetValue<AccessLevel>("accessLevel", value);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002CB RID: 715 RVA: 0x000093A9 File Offset: 0x000075A9
		// (set) Token: 0x060002CC RID: 716 RVA: 0x000093B6 File Offset: 0x000075B6
		[DataMember(Name = "participantsWarningThreshold", EmitDefaultValue = false)]
		public int ParticipantsWarningThreshold
		{
			get
			{
				return base.GetValue<int>("participantsWarningThreshold");
			}
			set
			{
				base.SetValue<int>("participantsWarningThreshold", value);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002CD RID: 717 RVA: 0x000093C9 File Offset: 0x000075C9
		// (set) Token: 0x060002CE RID: 718 RVA: 0x000093D6 File Offset: 0x000075D6
		[DataMember(Name = "lobbyBypassForPhoneUsers", EmitDefaultValue = false)]
		public LobbyBypassForPhoneUsers LobbyBypassForPhoneUsers
		{
			get
			{
				return base.GetValue<LobbyBypassForPhoneUsers>("lobbyBypassForPhoneUsers");
			}
			set
			{
				base.SetValue<LobbyBypassForPhoneUsers>("lobbyBypassForPhoneUsers", value);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002CF RID: 719 RVA: 0x000093E9 File Offset: 0x000075E9
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x000093F6 File Offset: 0x000075F6
		[DataMember(Name = "defaultOnlineMeetingRel", EmitDefaultValue = false)]
		public OnlineMeetingRel DefaultOnlineMeetingRel
		{
			get
			{
				return base.GetValue<OnlineMeetingRel>("defaultOnlineMeetingRel");
			}
			set
			{
				base.SetValue<OnlineMeetingRel>("defaultOnlineMeetingRel", value);
			}
		}

		// Token: 0x040001CD RID: 461
		public const string Token = "onlineMeetingDefaultValues";

		// Token: 0x040001CE RID: 462
		private const string EntryExitAnnouncementPropertyName = "entryExitAnnouncement";

		// Token: 0x040001CF RID: 463
		private const string AutomaticLeaderAssignmentPropertyName = "automaticLeaderAssignment";

		// Token: 0x040001D0 RID: 464
		private const string AccessLevelPropertyName = "accessLevel";

		// Token: 0x040001D1 RID: 465
		private const string ParticipantsWarningThresholdPropertyName = "participantsWarningThreshold";

		// Token: 0x040001D2 RID: 466
		private const string LobbyBypassForPhoneUsersPropertyName = "lobbyBypassForPhoneUsers";

		// Token: 0x040001D3 RID: 467
		private const string DefaultOnlineMeetingRelPropertyName = "defaultOnlineMeetingRel";
	}
}
