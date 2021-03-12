using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000129 RID: 297
	internal class ResourceBookingAssistantLogEntry : AssistantLogEntryBase
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0004E0B0 File Offset: 0x0004C2B0
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0004E0B8 File Offset: 0x0004C2B8
		[LogField("MG")]
		public Guid MailboxGuid { get; internal set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0004E0C1 File Offset: 0x0004C2C1
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x0004E0C9 File Offset: 0x0004C2C9
		[LogField("TG")]
		public Guid TenantGuid { get; internal set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0004E0D2 File Offset: 0x0004C2D2
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x0004E0DA File Offset: 0x0004C2DA
		[LogField("DG")]
		public Guid DatabaseGuid { get; internal set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0004E0E3 File Offset: 0x0004C2E3
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x0004E0EB File Offset: 0x0004C2EB
		[LogField("IAB")]
		public bool IsAutomatedBooking { get; internal set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0004E0F4 File Offset: 0x0004C2F4
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x0004E0FC File Offset: 0x0004C2FC
		[LogField("STPT")]
		public DateTime StartProcessingTime { get; internal set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0004E105 File Offset: 0x0004C305
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x0004E10D File Offset: 0x0004C30D
		[LogField("STPPT")]
		public DateTime StopProcessingTime { get; internal set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0004E116 File Offset: 0x0004C316
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x0004E11E File Offset: 0x0004C31E
		[LogField("OC")]
		public string ObjectClass { get; internal set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0004E127 File Offset: 0x0004C327
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x0004E12F File Offset: 0x0004C32F
		[LogField("ICFNA")]
		public bool IsCalendarFolderNotAvailable { get; internal set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0004E138 File Offset: 0x0004C338
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x0004E140 File Offset: 0x0004C340
		[LogField("IDMR")]
		public bool IsDelegatedMeetingRequest { get; internal set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0004E149 File Offset: 0x0004C349
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x0004E151 File Offset: 0x0004C351
		[LogField("MS")]
		public string MeetingSender { get; internal set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0004E15A File Offset: 0x0004C35A
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x0004E162 File Offset: 0x0004C362
		[LogField("MIMID")]
		public string MeetingInternetMessageId { get; internal set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0004E16B File Offset: 0x0004C36B
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x0004E173 File Offset: 0x0004C373
		[LogField("MMID")]
		public string MeetingMessageId { get; internal set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0004E17C File Offset: 0x0004C37C
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x0004E184 File Offset: 0x0004C384
		[LogField("IOR")]
		public bool IsOrganizer { get; internal set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0004E18D File Offset: 0x0004C38D
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x0004E195 File Offset: 0x0004C395
		[LogField("METF")]
		public string MapiEventFlag { get; internal set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0004E19E File Offset: 0x0004C39E
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x0004E1A6 File Offset: 0x0004C3A6
		[LogField("IMM")]
		public bool IsMeetingMessage { get; internal set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0004E1AF File Offset: 0x0004C3AF
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x0004E1B7 File Offset: 0x0004C3B7
		[LogField("IMMI")]
		public bool IsMeetingMessageInvalid { get; internal set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0004E1C0 File Offset: 0x0004C3C0
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x0004E1C8 File Offset: 0x0004C3C8
		[LogField("IRBRP")]
		public bool IsResourceBookingRequestProcessed { get; internal set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0004E1D1 File Offset: 0x0004C3D1
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x0004E1D9 File Offset: 0x0004C3D9
		[LogField("IMMP")]
		public bool IsMeetingMessageProcessed { get; internal set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0004E1E2 File Offset: 0x0004C3E2
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x0004E1EA File Offset: 0x0004C3EA
		[LogField("DCM")]
		public bool DeleteCanceledMeeting { get; internal set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0004E1F3 File Offset: 0x0004C3F3
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x0004E1FB File Offset: 0x0004C3FB
		[LogField("DNMM")]
		public bool DeleteNonMeetingMessage { get; internal set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0004E204 File Offset: 0x0004C404
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x0004E20C File Offset: 0x0004C40C
		[LogField("ER")]
		public string EvaluationResult { get; internal set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0004E215 File Offset: 0x0004C415
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x0004E21D File Offset: 0x0004C41D
		[LogField("BR")]
		public string BookingRole { get; internal set; }
	}
}
