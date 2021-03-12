using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000F RID: 15
	[DataContract]
	internal sealed class UXSettings : ItemPropertiesBase
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00002E30 File Offset: 0x00001030
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00002E38 File Offset: 0x00001038
		[DataMember]
		public ushort MessagesPerPage { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00002E41 File Offset: 0x00001041
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00002E49 File Offset: 0x00001049
		[DataMember]
		public ushort Wrap { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00002E52 File Offset: 0x00001052
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00002E5A File Offset: 0x0000105A
		[DataMember]
		public byte HeadersByte { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00002E63 File Offset: 0x00001063
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00002E6B File Offset: 0x0000106B
		[DataMember]
		public byte ReplySeparatorByte { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00002E74 File Offset: 0x00001074
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00002E7C File Offset: 0x0000107C
		[DataMember]
		public bool HasReplyText { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002E85 File Offset: 0x00001085
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00002E8D File Offset: 0x0000108D
		[DataMember]
		public bool HasUserDefinedReplyTo { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002E96 File Offset: 0x00001096
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00002E9E File Offset: 0x0000109E
		[DataMember]
		public string ReplyAddress { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00002EA7 File Offset: 0x000010A7
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00002EAF File Offset: 0x000010AF
		[DataMember]
		public bool ConfirmPage { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00002EB8 File Offset: 0x000010B8
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00002EC0 File Offset: 0x000010C0
		[DataMember]
		public int InboxSortFlagsInt { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00002EC9 File Offset: 0x000010C9
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00002ED1 File Offset: 0x000010D1
		[DataMember]
		public bool MobileUser { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00002EDA File Offset: 0x000010DA
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00002EE2 File Offset: 0x000010E2
		[DataMember]
		public byte MobilePageAllByte { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00002EEB File Offset: 0x000010EB
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00002EF3 File Offset: 0x000010F3
		[DataMember]
		public byte DefaultViewTypeByte { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00002EFC File Offset: 0x000010FC
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00002F04 File Offset: 0x00001104
		[DataMember]
		public bool CalendarTentativeBookingEnabled { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00002F0D File Offset: 0x0000110D
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00002F15 File Offset: 0x00001115
		[DataMember]
		public string EmailComposeSignatureHtml { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00002F1E File Offset: 0x0000111E
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00002F26 File Offset: 0x00001126
		[DataMember]
		public CategorySettings[] CategorySettingsList { get; set; }
	}
}
