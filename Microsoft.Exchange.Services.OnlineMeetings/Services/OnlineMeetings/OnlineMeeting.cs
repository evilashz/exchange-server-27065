using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x0200001A RID: 26
	internal class OnlineMeeting
	{
		// Token: 0x0600006F RID: 111 RVA: 0x000030C9 File Offset: 0x000012C9
		public OnlineMeeting(Collection<string> leaders, Collection<string> attendees)
		{
			this.leaders = leaders;
			this.attendees = attendees;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000030DF File Offset: 0x000012DF
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000030E7 File Offset: 0x000012E7
		public string Id { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000030F0 File Offset: 0x000012F0
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000030F8 File Offset: 0x000012F8
		public string MeetingUri { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003101 File Offset: 0x00001301
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003109 File Offset: 0x00001309
		public string WebUrl { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003112 File Offset: 0x00001312
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000311A File Offset: 0x0000131A
		public string PstnMeetingId { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003123 File Offset: 0x00001323
		// (set) Token: 0x06000079 RID: 121 RVA: 0x0000312B File Offset: 0x0000132B
		public LobbyBypass PstnUserLobbyBypass { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003134 File Offset: 0x00001334
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000313C File Offset: 0x0000133C
		public string OrganizerUri { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003145 File Offset: 0x00001345
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000314D File Offset: 0x0000134D
		public AttendanceAnnouncementsStatus AttendanceAnnouncementStatus { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003156 File Offset: 0x00001356
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000315E File Offset: 0x0000135E
		public AutomaticLeaderAssignment AutomaticLeaderAssignment { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003167 File Offset: 0x00001367
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000316F File Offset: 0x0000136F
		public string Subject { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003178 File Offset: 0x00001378
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003180 File Offset: 0x00001380
		public string Description { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003189 File Offset: 0x00001389
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003191 File Offset: 0x00001391
		public DateTime? ExpiryTime { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000319A File Offset: 0x0000139A
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000031A2 File Offset: 0x000013A2
		public bool? IsActive { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000031AB File Offset: 0x000013AB
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000031B3 File Offset: 0x000013B3
		public AccessLevel Accesslevel { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000031BC File Offset: 0x000013BC
		public IEnumerable<string> Leaders
		{
			get
			{
				return this.leaders;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000031C4 File Offset: 0x000013C4
		public IEnumerable<string> Attendees
		{
			get
			{
				return this.attendees;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000031CC File Offset: 0x000013CC
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000031D4 File Offset: 0x000013D4
		public bool IsAssignedMeeting { get; set; }

		// Token: 0x040000D0 RID: 208
		private readonly Collection<string> leaders;

		// Token: 0x040000D1 RID: 209
		private readonly Collection<string> attendees;
	}
}
