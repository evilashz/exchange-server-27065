using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005B3 RID: 1459
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ConversationNodeMetadata")]
	[Serializable]
	public class ConversationNodeMetadata
	{
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002BB1 RID: 11185 RVA: 0x000AFE0D File Offset: 0x000AE00D
		// (set) Token: 0x06002BB2 RID: 11186 RVA: 0x000AFE15 File Offset: 0x000AE015
		[DataMember(Name = "From", IsRequired = true, Order = 1)]
		public SingleRecipientType From { get; set; }

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x000AFE1E File Offset: 0x000AE01E
		// (set) Token: 0x06002BB4 RID: 11188 RVA: 0x000AFE26 File Offset: 0x000AE026
		[DataMember(Name = "Sender", IsRequired = true, Order = 2)]
		public SingleRecipientType Sender { get; set; }

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x000AFE2F File Offset: 0x000AE02F
		// (set) Token: 0x06002BB6 RID: 11190 RVA: 0x000AFE37 File Offset: 0x000AE037
		[DataMember(Name = "InternetMessageId", IsRequired = true, Order = 3)]
		public string InternetMessageId { get; set; }

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002BB7 RID: 11191 RVA: 0x000AFE40 File Offset: 0x000AE040
		// (set) Token: 0x06002BB8 RID: 11192 RVA: 0x000AFE48 File Offset: 0x000AE048
		[DataMember(Name = "SubjectPrefix", IsRequired = true, Order = 4)]
		public string SubjectPrefix { get; set; }

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002BB9 RID: 11193 RVA: 0x000AFE51 File Offset: 0x000AE051
		// (set) Token: 0x06002BBA RID: 11194 RVA: 0x000AFE59 File Offset: 0x000AE059
		[DataMember(Name = "Preview", IsRequired = true, Order = 5)]
		public string Preview { get; set; }

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002BBB RID: 11195 RVA: 0x000AFE62 File Offset: 0x000AE062
		// (set) Token: 0x06002BBC RID: 11196 RVA: 0x000AFE6A File Offset: 0x000AE06A
		[DataMember(Name = "References", IsRequired = true, Order = 6)]
		public string References { get; set; }

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002BBD RID: 11197 RVA: 0x000AFE73 File Offset: 0x000AE073
		// (set) Token: 0x06002BBE RID: 11198 RVA: 0x000AFE7B File Offset: 0x000AE07B
		[DataMember(Name = "AddedParticipants", IsRequired = true, Order = 7)]
		public SingleRecipientType[] AddedParticipants { get; set; }

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x000AFE84 File Offset: 0x000AE084
		// (set) Token: 0x06002BC0 RID: 11200 RVA: 0x000AFE8C File Offset: 0x000AE08C
		[DataMember(Name = "ReplyAllParticipants", IsRequired = true, Order = 8)]
		public SingleRecipientType[] ReplyAllParticipants { get; set; }

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002BC1 RID: 11201 RVA: 0x000AFE95 File Offset: 0x000AE095
		// (set) Token: 0x06002BC2 RID: 11202 RVA: 0x000AFE9D File Offset: 0x000AE09D
		[DateTimeString]
		[DataMember(Name = "ReceivedTime", IsRequired = true, Order = 9)]
		public string ReceivedTime { get; set; }

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x000AFEA6 File Offset: 0x000AE0A6
		// (set) Token: 0x06002BC4 RID: 11204 RVA: 0x000AFEAE File Offset: 0x000AE0AE
		[DataMember(Name = "InReplyTo", IsRequired = true, Order = 10)]
		public string InReplyTo { get; set; }

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x000AFEB7 File Offset: 0x000AE0B7
		// (set) Token: 0x06002BC6 RID: 11206 RVA: 0x000AFEBF File Offset: 0x000AE0BF
		[DataMember(Name = "ConversationIndexTrackingEx", IsRequired = true, Order = 11)]
		public string ConversationIndexTrackingEx { get; set; }

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x000AFEC8 File Offset: 0x000AE0C8
		// (set) Token: 0x06002BC8 RID: 11208 RVA: 0x000AFED0 File Offset: 0x000AE0D0
		[DataMember(Name = "Order", IsRequired = true, Order = 12)]
		public int Order { get; set; }

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x000AFED9 File Offset: 0x000AE0D9
		// (set) Token: 0x06002BCA RID: 11210 RVA: 0x000AFEE1 File Offset: 0x000AE0E1
		[DataMember(Name = "ItemClass", IsRequired = true, Order = 13)]
		public string ItemClass { get; set; }

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002BCB RID: 11211 RVA: 0x000AFEEA File Offset: 0x000AE0EA
		// (set) Token: 0x06002BCC RID: 11212 RVA: 0x000AFEF2 File Offset: 0x000AE0F2
		[DataMember(Name = "ThreadId", IsRequired = true, Order = 14)]
		public ItemId ThreadId { get; set; }

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x000AFEFB File Offset: 0x000AE0FB
		// (set) Token: 0x06002BCE RID: 11214 RVA: 0x000AFF03 File Offset: 0x000AE103
		[DataMember(Name = "Subject", IsRequired = true, Order = 15)]
		public string Subject { get; set; }
	}
}
