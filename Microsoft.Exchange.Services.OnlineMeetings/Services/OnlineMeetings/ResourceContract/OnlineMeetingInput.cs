using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000067 RID: 103
	[DataContract(Name = "OnlineMeetingInput")]
	internal class OnlineMeetingInput : Resource
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x0000950B File Offset: 0x0000770B
		public OnlineMeetingInput() : base(string.Empty)
		{
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00009518 File Offset: 0x00007718
		public OnlineMeetingInput(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00009524 File Offset: 0x00007724
		public OnlineMeetingInput(OnlineMeetingInput input) : this()
		{
			this.AccessLevel = input.AccessLevel;
			this.EntryExitAnnouncement = input.EntryExitAnnouncement;
			this.Attendees = input.Attendees;
			this.AutomaticLeaderAssignment = input.AutomaticLeaderAssignment;
			this.Description = input.Description;
			this.ExpirationTime = input.ExpirationTime;
			this.Leaders = input.Leaders;
			this.PhoneUserAdmission = input.PhoneUserAdmission;
			this.LobbyBypassForPhoneUsers = input.LobbyBypassForPhoneUsers;
			this.Subject = input.Subject;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x000095AF File Offset: 0x000077AF
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x000095BC File Offset: 0x000077BC
		[DataMember(Name = "entryExitAnnouncement", EmitDefaultValue = false)]
		public EntryExitAnnouncement? EntryExitAnnouncement
		{
			get
			{
				return base.GetValue<EntryExitAnnouncement?>("entryExitAnnouncement");
			}
			set
			{
				base.SetValue<EntryExitAnnouncement?>("entryExitAnnouncement", value);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002EA RID: 746 RVA: 0x000095CF File Offset: 0x000077CF
		// (set) Token: 0x060002EB RID: 747 RVA: 0x000095DC File Offset: 0x000077DC
		[DataMember(Name = "automaticLeaderAssignment", EmitDefaultValue = false)]
		public AutomaticLeaderAssignment? AutomaticLeaderAssignment
		{
			get
			{
				return base.GetValue<AutomaticLeaderAssignment?>("automaticLeaderAssignment");
			}
			set
			{
				base.SetValue<AutomaticLeaderAssignment?>("automaticLeaderAssignment", value);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000095EF File Offset: 0x000077EF
		// (set) Token: 0x060002ED RID: 749 RVA: 0x000095FC File Offset: 0x000077FC
		[DataMember(Name = "accessLevel", EmitDefaultValue = false)]
		public AccessLevel? AccessLevel
		{
			get
			{
				return base.GetValue<AccessLevel?>("accessLevel");
			}
			set
			{
				base.SetValue<AccessLevel?>("accessLevel", value);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000960F File Offset: 0x0000780F
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000961C File Offset: 0x0000781C
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000962A File Offset: 0x0000782A
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x00009637 File Offset: 0x00007837
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

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000964A File Offset: 0x0000784A
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x00009657 File Offset: 0x00007857
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

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000966A File Offset: 0x0000786A
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00009677 File Offset: 0x00007877
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

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000968A File Offset: 0x0000788A
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x00009697 File Offset: 0x00007897
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

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x000096A5 File Offset: 0x000078A5
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x000096B2 File Offset: 0x000078B2
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

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002FA RID: 762 RVA: 0x000096C0 File Offset: 0x000078C0
		// (set) Token: 0x060002FB RID: 763 RVA: 0x000096CD File Offset: 0x000078CD
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

		// Token: 0x02000068 RID: 104
		internal static class PropertyNames
		{
			// Token: 0x040001DB RID: 475
			public const string AccessLevel = "accessLevel";

			// Token: 0x040001DC RID: 476
			public const string Attendees = "attendees";

			// Token: 0x040001DD RID: 477
			public const string AutomaticLeaderAssignment = "automaticLeaderAssignment";

			// Token: 0x040001DE RID: 478
			public const string Description = "description";

			// Token: 0x040001DF RID: 479
			public const string EntryExitAnnouncement = "entryExitAnnouncement";

			// Token: 0x040001E0 RID: 480
			public const string ExpirationTime = "expirationTime";

			// Token: 0x040001E1 RID: 481
			public const string Leaders = "leaders";

			// Token: 0x040001E2 RID: 482
			public const string LobbyBypassForPhoneUsers = "lobbyBypassForPhoneUsers";

			// Token: 0x040001E3 RID: 483
			public const string PhoneUserAdmission = "phoneUserAdmission";

			// Token: 0x040001E4 RID: 484
			public const string Subject = "subject";
		}
	}
}
