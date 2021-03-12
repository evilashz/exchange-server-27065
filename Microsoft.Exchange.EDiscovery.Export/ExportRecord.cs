using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000053 RID: 83
	public class ExportRecord
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00017970 File Offset: 0x00015B70
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00017978 File Offset: 0x00015B78
		public string OriginalPath { get; internal set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00017981 File Offset: 0x00015B81
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x00017989 File Offset: 0x00015B89
		public string TargetPath { get; internal set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00017992 File Offset: 0x00015B92
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0001799A File Offset: 0x00015B9A
		public string Id { get; internal set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x000179A3 File Offset: 0x00015BA3
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x000179AB File Offset: 0x00015BAB
		public int DocumentId { get; internal set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x000179B4 File Offset: 0x00015BB4
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x000179BC File Offset: 0x00015BBC
		public string InternetMessageId { get; internal set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x000179C5 File Offset: 0x00015BC5
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x000179CD File Offset: 0x00015BCD
		public string PrimaryIdOfDuplicates { get; internal set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x000179D6 File Offset: 0x00015BD6
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x000179DE File Offset: 0x00015BDE
		public ExportFile ExportFile { get; internal set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x000179E7 File Offset: 0x00015BE7
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x000179EF File Offset: 0x00015BEF
		public ExportRecord Parent { get; internal set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x000179F8 File Offset: 0x00015BF8
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x00017A00 File Offset: 0x00015C00
		public string SourceId { get; internal set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00017A09 File Offset: 0x00015C09
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x00017A11 File Offset: 0x00015C11
		public string Title { get; internal set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00017A1A File Offset: 0x00015C1A
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x00017A22 File Offset: 0x00015C22
		public string Sender { get; internal set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00017A2B File Offset: 0x00015C2B
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x00017A33 File Offset: 0x00015C33
		public string SenderSmtpAddress { get; internal set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00017A3C File Offset: 0x00015C3C
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x00017A44 File Offset: 0x00015C44
		public uint Size { get; internal set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00017A4D File Offset: 0x00015C4D
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x00017A55 File Offset: 0x00015C55
		public DateTime SentTime { get; internal set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00017A5E File Offset: 0x00015C5E
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x00017A66 File Offset: 0x00015C66
		public DateTime ReceivedTime { get; internal set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00017A6F File Offset: 0x00015C6F
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00017A77 File Offset: 0x00015C77
		public string BodyPreview { get; internal set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00017A80 File Offset: 0x00015C80
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00017A88 File Offset: 0x00015C88
		public string Importance { get; internal set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00017A91 File Offset: 0x00015C91
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x00017A99 File Offset: 0x00015C99
		public bool IsRead { get; internal set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00017AA2 File Offset: 0x00015CA2
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x00017AAA File Offset: 0x00015CAA
		public bool HasAttachment { get; internal set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00017AB3 File Offset: 0x00015CB3
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x00017ABB File Offset: 0x00015CBB
		public string ToRecipients { get; internal set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00017AC4 File Offset: 0x00015CC4
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x00017ACC File Offset: 0x00015CCC
		public string CcRecipients { get; internal set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00017AD5 File Offset: 0x00015CD5
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x00017ADD File Offset: 0x00015CDD
		public string BccRecipients { get; internal set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00017AE6 File Offset: 0x00015CE6
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00017AEE File Offset: 0x00015CEE
		public string ToGroupExpansionRecipients { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00017AF7 File Offset: 0x00015CF7
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x00017AFF File Offset: 0x00015CFF
		public string CcGroupExpansionRecipients { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00017B08 File Offset: 0x00015D08
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x00017B10 File Offset: 0x00015D10
		public string BccGroupExpansionRecipients { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00017B19 File Offset: 0x00015D19
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x00017B21 File Offset: 0x00015D21
		public string DGGroupExpansionError { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00017B2A File Offset: 0x00015D2A
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x00017B32 File Offset: 0x00015D32
		public string DocumentType { get; internal set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00017B3B File Offset: 0x00015D3B
		public string MimeType
		{
			get
			{
				return "application/vnd.ms-outlook";
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00017B42 File Offset: 0x00015D42
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x00017B4A File Offset: 0x00015D4A
		public string RelationshipType { get; internal set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00017B53 File Offset: 0x00015D53
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x00017B5B File Offset: 0x00015D5B
		public bool IsUnsearchable { get; internal set; }

		// Token: 0x040001EB RID: 491
		public const string DocumentTypeFile = "File";

		// Token: 0x040001EC RID: 492
		public const string DocumentTypeMessage = "Message";

		// Token: 0x040001ED RID: 493
		public const string RelationshipTypeNone = "None";

		// Token: 0x040001EE RID: 494
		public const string RelationshipTypeContainer = "Container";
	}
}
