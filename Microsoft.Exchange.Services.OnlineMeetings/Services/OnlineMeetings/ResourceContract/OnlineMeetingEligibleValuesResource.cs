using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000063 RID: 99
	[Parent("user")]
	[DataContract(Name = "onlineMeetingEligibleValuesResource")]
	[Get(typeof(OnlineMeetingEligibleValuesResource))]
	internal class OnlineMeetingEligibleValuesResource : OnlineMeetingCapabilityResource
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x00009409 File Offset: 0x00007609
		public OnlineMeetingEligibleValuesResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00009412 File Offset: 0x00007612
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000941F File Offset: 0x0000761F
		[DataMember(Name = "entryExitAnnouncements", EmitDefaultValue = false)]
		public EntryExitAnnouncement[] EntryExitAnnouncements
		{
			get
			{
				return base.GetValue<EntryExitAnnouncement[]>("entryExitAnnouncements");
			}
			set
			{
				base.SetValue<EntryExitAnnouncement[]>("entryExitAnnouncements", value);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000942D File Offset: 0x0000762D
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000943A File Offset: 0x0000763A
		[DataMember(Name = "automaticLeaderAssignments", EmitDefaultValue = false)]
		public AutomaticLeaderAssignment[] AutomaticLeaderAssignments
		{
			get
			{
				return base.GetValue<AutomaticLeaderAssignment[]>("automaticLeaderAssignments");
			}
			set
			{
				base.SetValue<AutomaticLeaderAssignment[]>("automaticLeaderAssignments", value);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00009448 File Offset: 0x00007648
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00009455 File Offset: 0x00007655
		[DataMember(Name = "accessLevels", EmitDefaultValue = false)]
		public AccessLevel[] AccessLevels
		{
			get
			{
				return base.GetValue<AccessLevel[]>("accessLevels");
			}
			set
			{
				base.SetValue<AccessLevel[]>("accessLevels", value);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00009463 File Offset: 0x00007663
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00009470 File Offset: 0x00007670
		[DataMember(Name = "lobbyBypassForPhoneUsersSettings", EmitDefaultValue = false)]
		public LobbyBypassForPhoneUsers[] LobbyBypassForPhoneUsersSettings
		{
			get
			{
				return base.GetValue<LobbyBypassForPhoneUsers[]>("lobbyBypassForPhoneUsersSettings");
			}
			set
			{
				base.SetValue<LobbyBypassForPhoneUsers[]>("lobbyBypassForPhoneUsersSettings", value);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000947E File Offset: 0x0000767E
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000948B File Offset: 0x0000768B
		[DataMember(Name = "eligibleOnlineMeetingRels", EmitDefaultValue = false)]
		public OnlineMeetingRel[] EligibleOnlineMeetingRels
		{
			get
			{
				return base.GetValue<OnlineMeetingRel[]>("eligibleOnlineMeetingRels");
			}
			set
			{
				base.SetValue<OnlineMeetingRel[]>("eligibleOnlineMeetingRels", value);
			}
		}

		// Token: 0x040001D4 RID: 468
		public const string Token = "onlineMeetingEligibleValues";
	}
}
