using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000038 RID: 56
	[DataContract]
	internal sealed class MailboxInformation
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00004583 File Offset: 0x00002783
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000458B File Offset: 0x0000278B
		[DataMember(IsRequired = true)]
		public Guid MailboxGuid { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00004594 File Offset: 0x00002794
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000459C File Offset: 0x0000279C
		[DataMember(IsRequired = true)]
		public ulong MailboxSize { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000273 RID: 627 RVA: 0x000045A5 File Offset: 0x000027A5
		// (set) Token: 0x06000274 RID: 628 RVA: 0x000045AD File Offset: 0x000027AD
		[DataMember(IsRequired = true)]
		public ulong MailboxItemCount { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000045B6 File Offset: 0x000027B6
		// (set) Token: 0x06000276 RID: 630 RVA: 0x000045BE File Offset: 0x000027BE
		[DataMember(IsRequired = true)]
		public Guid MdbGuid { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000045C7 File Offset: 0x000027C7
		// (set) Token: 0x06000278 RID: 632 RVA: 0x000045CF File Offset: 0x000027CF
		[DataMember(IsRequired = true)]
		public string MdbName { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000045D8 File Offset: 0x000027D8
		// (set) Token: 0x0600027A RID: 634 RVA: 0x000045E0 File Offset: 0x000027E0
		[DataMember]
		public string MdbLegDN { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600027B RID: 635 RVA: 0x000045E9 File Offset: 0x000027E9
		// (set) Token: 0x0600027C RID: 636 RVA: 0x000045F1 File Offset: 0x000027F1
		[DataMember(IsRequired = true)]
		public int ServerVersion { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000045FA File Offset: 0x000027FA
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00004602 File Offset: 0x00002802
		[DataMember]
		public string ServerMailboxRelease { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000460B File Offset: 0x0000280B
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00004613 File Offset: 0x00002813
		[DataMember]
		public Guid MailboxHomeMdbGuid { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000461C File Offset: 0x0000281C
		// (set) Token: 0x06000282 RID: 642 RVA: 0x00004624 File Offset: 0x00002824
		[DataMember]
		public int RulesSize { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000462D File Offset: 0x0000282D
		// (set) Token: 0x06000284 RID: 644 RVA: 0x00004635 File Offset: 0x00002835
		[DataMember]
		public int RecipientType { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000463E File Offset: 0x0000283E
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00004646 File Offset: 0x00002846
		[DataMember]
		public int RecipientTypeDetails { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000464F File Offset: 0x0000284F
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00004657 File Offset: 0x00002857
		[DataMember]
		public long RecipientTypeDetailsLong { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00004660 File Offset: 0x00002860
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00004668 File Offset: 0x00002868
		[DataMember]
		public int RecipientDisplayType { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00004671 File Offset: 0x00002871
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00004679 File Offset: 0x00002879
		[DataMember]
		public Guid ArchiveGuid { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00004682 File Offset: 0x00002882
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000468A File Offset: 0x0000288A
		[DataMember]
		public Guid[] AlternateMailboxes { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00004693 File Offset: 0x00002893
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000469B File Offset: 0x0000289B
		[DataMember]
		public string MailboxIdentity { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000046A4 File Offset: 0x000028A4
		// (set) Token: 0x06000292 RID: 658 RVA: 0x000046AC File Offset: 0x000028AC
		[DataMember]
		public bool? UseMdbQuotaDefaults { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000046B5 File Offset: 0x000028B5
		// (set) Token: 0x06000294 RID: 660 RVA: 0x000046BD File Offset: 0x000028BD
		[DataMember]
		public ulong? MailboxQuota { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000046C6 File Offset: 0x000028C6
		// (set) Token: 0x06000296 RID: 662 RVA: 0x000046CE File Offset: 0x000028CE
		[DataMember]
		public ulong? MailboxDumpsterQuota { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000297 RID: 663 RVA: 0x000046D7 File Offset: 0x000028D7
		// (set) Token: 0x06000298 RID: 664 RVA: 0x000046DF File Offset: 0x000028DF
		[DataMember]
		public ulong? MailboxArchiveQuota { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000299 RID: 665 RVA: 0x000046E8 File Offset: 0x000028E8
		// (set) Token: 0x0600029A RID: 666 RVA: 0x000046F0 File Offset: 0x000028F0
		[DataMember]
		public ulong? MdbQuota { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000046F9 File Offset: 0x000028F9
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00004701 File Offset: 0x00002901
		[DataMember]
		public ulong? MdbDumpsterQuota { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000470A File Offset: 0x0000290A
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00004712 File Offset: 0x00002912
		[DataMember]
		public string UserDataXML { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000471B File Offset: 0x0000291B
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x00004723 File Offset: 0x00002923
		[DataMember]
		public ulong RegularItemCount { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000472C File Offset: 0x0000292C
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00004734 File Offset: 0x00002934
		[DataMember]
		public ulong RegularDeletedItemCount { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000473D File Offset: 0x0000293D
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00004745 File Offset: 0x00002945
		[DataMember]
		public ulong AssociatedItemCount { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000474E File Offset: 0x0000294E
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00004756 File Offset: 0x00002956
		[DataMember]
		public ulong AssociatedDeletedItemCount { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000475F File Offset: 0x0000295F
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00004767 File Offset: 0x00002967
		[DataMember]
		public ulong RegularItemsSize { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00004770 File Offset: 0x00002970
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00004778 File Offset: 0x00002978
		[DataMember]
		public ulong RegularDeletedItemsSize { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00004781 File Offset: 0x00002981
		// (set) Token: 0x060002AC RID: 684 RVA: 0x00004789 File Offset: 0x00002989
		[DataMember]
		public ulong AssociatedItemsSize { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00004792 File Offset: 0x00002992
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000479A File Offset: 0x0000299A
		[DataMember]
		public ulong AssociatedDeletedItemsSize { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002AF RID: 687 RVA: 0x000047A3 File Offset: 0x000029A3
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x000047AB File Offset: 0x000029AB
		[DataMember]
		public MailboxServerInformation ServerInformation { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x000047B4 File Offset: 0x000029B4
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x000047BC File Offset: 0x000029BC
		[DataMember(IsRequired = false)]
		public string ProviderName { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000047C5 File Offset: 0x000029C5
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x000047CD File Offset: 0x000029CD
		[DataMember(IsRequired = false)]
		public int MailboxTableFlags { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000047D6 File Offset: 0x000029D6
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x000047DE File Offset: 0x000029DE
		[DataMember(IsRequired = false)]
		public int ContentAggregationFlags { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000047E7 File Offset: 0x000029E7
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x000047EF File Offset: 0x000029EF
		[DataMember(IsRequired = false)]
		public Guid[] ContainerMailboxGuids { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000047F8 File Offset: 0x000029F8
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00004800 File Offset: 0x00002A00
		[DataMember(IsRequired = false)]
		public float MrsVersion { get; set; }

		// Token: 0x060002BB RID: 699 RVA: 0x0000480C File Offset: 0x00002A0C
		public override string ToString()
		{
			string format = "mbxGuid={0}\nmbxName='{1}\nmbxHomeMdbGuid={19}\nmbxSize={2} (deleted={3})\nmbxItemCount={4}\nrecipientType={5} {6} {7}\narchiveGuid={8}\naltMailboxes={9}\nmbxQuota={10}\nmbxDumpsterQuota={11}\nmbxArchiveQuota={12}\nmdbGuid={13}\nmdbName='{14}'\nserverVersion={15}\nmdbQuota={16}\nmdbDumpsterQuota={17}\nrulesSize={18}\nserverInfo={20}\n";
			string text = (this.UseMdbQuotaDefaults == true) ? "(use MDB default)" : ((this.MailboxQuota == null) ? "(unlimited)" : new ByteQuantifiedSize(this.MailboxQuota.Value).ToString());
			string text2 = (this.UseMdbQuotaDefaults == true) ? "(use MDB default)" : ((this.MailboxDumpsterQuota == null) ? "(unlimited)" : new ByteQuantifiedSize(this.MailboxDumpsterQuota.Value).ToString());
			string text3 = (this.UseMdbQuotaDefaults == true) ? "(use MDB default)" : ((this.MailboxArchiveQuota == null) ? "(unlimited)" : new ByteQuantifiedSize(this.MailboxArchiveQuota.Value).ToString());
			return string.Format(format, new object[]
			{
				this.MailboxGuid,
				this.MailboxIdentity,
				new ByteQuantifiedSize(this.MailboxSize).ToString(),
				new ByteQuantifiedSize(this.RegularDeletedItemsSize).ToString(),
				this.MailboxItemCount,
				(RecipientType)this.RecipientType,
				(RecipientTypeDetails)this.RecipientTypeDetails,
				(RecipientDisplayType)this.RecipientDisplayType,
				this.ArchiveGuid.ToString(),
				(this.AlternateMailboxes != null && this.AlternateMailboxes.Length > 0) ? "(present)" : "(not present)",
				text,
				text2,
				text3,
				this.MdbGuid,
				this.MdbName,
				new ServerVersion(this.ServerVersion).ToString(),
				(this.MdbQuota == null) ? "(unlimited)" : new ByteQuantifiedSize(this.MdbQuota.Value).ToString(),
				(this.MdbDumpsterQuota == null) ? "(unlimited)" : new ByteQuantifiedSize(this.MdbDumpsterQuota.Value).ToString(),
				this.RulesSize,
				this.MailboxHomeMdbGuid,
				this.ServerInformation
			});
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00004B0C File Offset: 0x00002D0C
		public LocalizedString GetItemCountsAndSizesString()
		{
			return MrsStrings.ItemCountsAndSizes(this.RegularItemCount, new ByteQuantifiedSize(this.RegularItemsSize).ToString(), this.RegularDeletedItemCount, new ByteQuantifiedSize(this.RegularDeletedItemsSize).ToString(), this.AssociatedItemCount, new ByteQuantifiedSize(this.AssociatedItemsSize).ToString(), this.AssociatedDeletedItemCount, new ByteQuantifiedSize(this.AssociatedDeletedItemsSize).ToString());
		}
	}
}
