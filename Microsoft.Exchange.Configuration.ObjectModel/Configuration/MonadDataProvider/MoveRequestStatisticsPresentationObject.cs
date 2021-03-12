using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001C6 RID: 454
	public class MoveRequestStatisticsPresentationObject
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0003072E File Offset: 0x0002E92E
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x00030736 File Offset: 0x0002E936
		public string DisplayName { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0003073F File Offset: 0x0002E93F
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x00030747 File Offset: 0x0002E947
		public RequestStatus Status { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00030750 File Offset: 0x0002E950
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x00030758 File Offset: 0x0002E958
		public int PercentComplete { get; set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00030761 File Offset: 0x0002E961
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x00030769 File Offset: 0x0002E969
		public EnhancedTimeSpan? OverallDuration { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00030772 File Offset: 0x0002E972
		// (set) Token: 0x06000FDD RID: 4061 RVA: 0x0003077A File Offset: 0x0002E97A
		public ByteQuantifiedSize TotalMailboxSize { get; set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00030783 File Offset: 0x0002E983
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x0003078B File Offset: 0x0002E98B
		public int? BadItemsEncountered { get; set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00030794 File Offset: 0x0002E994
		// (set) Token: 0x06000FE1 RID: 4065 RVA: 0x0003079C File Offset: 0x0002E99C
		public DateTime? LastUpdateTimestamp { get; set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x000307A5 File Offset: 0x0002E9A5
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x000307AD File Offset: 0x0002E9AD
		public bool SuspendWhenReadyToComplete { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x000307B6 File Offset: 0x0002E9B6
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x000307BE File Offset: 0x0002E9BE
		public bool IsOffline { get; set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x000307C7 File Offset: 0x0002E9C7
		// (set) Token: 0x06000FE7 RID: 4071 RVA: 0x000307CF File Offset: 0x0002E9CF
		public string RemoteHostName { get; set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x000307D8 File Offset: 0x0002E9D8
		// (set) Token: 0x06000FE9 RID: 4073 RVA: 0x000307E0 File Offset: 0x0002E9E0
		public string MRSServerName { get; set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x000307E9 File Offset: 0x0002E9E9
		// (set) Token: 0x06000FEB RID: 4075 RVA: 0x000307F1 File Offset: 0x0002E9F1
		public ServerVersion SourceVersion { get; set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x000307FA File Offset: 0x0002E9FA
		// (set) Token: 0x06000FED RID: 4077 RVA: 0x00030802 File Offset: 0x0002EA02
		public ADObjectId SourceDatabase { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x0003080B File Offset: 0x0002EA0B
		// (set) Token: 0x06000FEF RID: 4079 RVA: 0x00030813 File Offset: 0x0002EA13
		public ServerVersion TargetVersion { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x0003081C File Offset: 0x0002EA1C
		// (set) Token: 0x06000FF1 RID: 4081 RVA: 0x00030824 File Offset: 0x0002EA24
		public ADObjectId TargetDatabase { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x0003082D File Offset: 0x0002EA2D
		// (set) Token: 0x06000FF3 RID: 4083 RVA: 0x00030835 File Offset: 0x0002EA35
		public DateTime? QueuedTimestamp { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0003083E File Offset: 0x0002EA3E
		// (set) Token: 0x06000FF5 RID: 4085 RVA: 0x00030846 File Offset: 0x0002EA46
		public EnhancedTimeSpan? TotalQueuedDuration { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x0003084F File Offset: 0x0002EA4F
		// (set) Token: 0x06000FF7 RID: 4087 RVA: 0x00030857 File Offset: 0x0002EA57
		public DateTime? StartTimestamp { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x00030860 File Offset: 0x0002EA60
		// (set) Token: 0x06000FF9 RID: 4089 RVA: 0x00030868 File Offset: 0x0002EA68
		public DateTime? CompletionTimestamp { get; set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x00030871 File Offset: 0x0002EA71
		// (set) Token: 0x06000FFB RID: 4091 RVA: 0x00030879 File Offset: 0x0002EA79
		public DateTime? SuspendedTimestamp { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x00030882 File Offset: 0x0002EA82
		// (set) Token: 0x06000FFD RID: 4093 RVA: 0x0003088A File Offset: 0x0002EA8A
		public LocalizedString Message { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00030893 File Offset: 0x0002EA93
		// (set) Token: 0x06000FFF RID: 4095 RVA: 0x0003089B File Offset: 0x0002EA9B
		public RequestFlags Flags { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x000308A4 File Offset: 0x0002EAA4
		// (set) Token: 0x06001001 RID: 4097 RVA: 0x000308AC File Offset: 0x0002EAAC
		public object Report { get; set; }
	}
}
