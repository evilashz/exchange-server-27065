using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000073 RID: 115
	public class FederatedDirectoryGroupType
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000EC65 File Offset: 0x0000CE65
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000EC6D File Offset: 0x0000CE6D
		public string Alias { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000EC76 File Offset: 0x0000CE76
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000EC7E File Offset: 0x0000CE7E
		public string CalendarUrl { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000EC87 File Offset: 0x0000CE87
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000EC8F File Offset: 0x0000CE8F
		public string DisplayName { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000EC98 File Offset: 0x0000CE98
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000ECA0 File Offset: 0x0000CEA0
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000ECA9 File Offset: 0x0000CEA9
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000ECB1 File Offset: 0x0000CEB1
		public string InboxUrl { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000ECBA File Offset: 0x0000CEBA
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000ECC2 File Offset: 0x0000CEC2
		public bool IsMember { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000ECCB File Offset: 0x0000CECB
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000ECD3 File Offset: 0x0000CED3
		public bool IsPinned { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
		public string JoinDate { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000ECED File Offset: 0x0000CEED
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000ECF5 File Offset: 0x0000CEF5
		public string LegacyDn { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000ECFE File Offset: 0x0000CEFE
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000ED06 File Offset: 0x0000CF06
		public string PeopleUrl { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000ED0F File Offset: 0x0000CF0F
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000ED17 File Offset: 0x0000CF17
		public string PhotoUrl { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000ED20 File Offset: 0x0000CF20
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000ED28 File Offset: 0x0000CF28
		public string SmtpAddress { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000ED31 File Offset: 0x0000CF31
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000ED39 File Offset: 0x0000CF39
		public FederatedDirectoryGroupTypeType GroupType { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000ED42 File Offset: 0x0000CF42
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000ED4A File Offset: 0x0000CF4A
		public ExDateTime JoinDateTime
		{
			get
			{
				return this.joinDateTime;
			}
			set
			{
				this.joinDateTime = value;
				this.JoinDate = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(value);
			}
		}

		// Token: 0x04000599 RID: 1433
		private ExDateTime joinDateTime;
	}
}
