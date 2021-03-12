using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000007 RID: 7
	[DataContract]
	internal sealed class DeliverySettings : ItemPropertiesBase
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002804 File Offset: 0x00000A04
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000280C File Offset: 0x00000A0C
		[DataMember]
		public int AutoReplyModeInt { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002815 File Offset: 0x00000A15
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000281D File Offset: 0x00000A1D
		public OlcAutoReplyMode AutoReplyMode
		{
			get
			{
				return (OlcAutoReplyMode)this.AutoReplyModeInt;
			}
			set
			{
				this.AutoReplyModeInt = (int)value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002826 File Offset: 0x00000A26
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000282E File Offset: 0x00000A2E
		[DataMember]
		public DateTime? AutoReplyStartTime { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002837 File Offset: 0x00000A37
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000283F File Offset: 0x00000A3F
		[DataMember]
		public DateTime? AutoReplyEndTime { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002848 File Offset: 0x00000A48
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002850 File Offset: 0x00000A50
		[DataMember]
		public string AutoReplyMessage { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002859 File Offset: 0x00000A59
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002861 File Offset: 0x00000A61
		[DataMember]
		public int AutoReplyMessageFormatInt { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000286A File Offset: 0x00000A6A
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002872 File Offset: 0x00000A72
		public OlcAutoReplyFormat AutoReplyMessageFormat
		{
			get
			{
				return (OlcAutoReplyFormat)this.AutoReplyMessageFormatInt;
			}
			set
			{
				this.AutoReplyMessageFormatInt = (int)value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000287B File Offset: 0x00000A7B
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002883 File Offset: 0x00000A83
		[DataMember]
		public bool BulkMailDeletion { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000288C File Offset: 0x00000A8C
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002894 File Offset: 0x00000A94
		[DataMember]
		public uint BulkMailProtectionLevelUInt { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000289D File Offset: 0x00000A9D
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000028A5 File Offset: 0x00000AA5
		[DataMember]
		public int ForwardingModeInt { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000028AE File Offset: 0x00000AAE
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000028B6 File Offset: 0x00000AB6
		public OlcForwardingMode ForwardingMode
		{
			get
			{
				return (OlcForwardingMode)this.ForwardingModeInt;
			}
			set
			{
				this.ForwardingModeInt = (int)value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000028BF File Offset: 0x00000ABF
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000028C7 File Offset: 0x00000AC7
		[DataMember]
		public string[] ForwardingList { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000028D0 File Offset: 0x00000AD0
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000028D8 File Offset: 0x00000AD8
		[DataMember]
		public DateTime? MailDeliveryBlackoutExpiration { get; set; }
	}
}
