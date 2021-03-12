using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000189 RID: 393
	internal class QuarantinedMessageDetail : ConfigurablePropertyBag
	{
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00032581 File Offset: 0x00030781
		// (set) Token: 0x06000FE1 RID: 4065 RVA: 0x00032593 File Offset: 0x00030793
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[QuarantinedMessageDetailSchema.OrganizationalUnitRootProperty];
			}
			set
			{
				this[QuarantinedMessageDetailSchema.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x000325A6 File Offset: 0x000307A6
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x000325B8 File Offset: 0x000307B8
		public Guid ExMessageId
		{
			get
			{
				return (Guid)this[QuarantinedMessageDetailSchema.ExMessageIdProperty];
			}
			set
			{
				this[QuarantinedMessageDetailSchema.ExMessageIdProperty] = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x000325CB File Offset: 0x000307CB
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x000325DD File Offset: 0x000307DD
		public Guid EventId
		{
			get
			{
				return (Guid)this[MessageEventSchema.EventIdProperty];
			}
			set
			{
				this[MessageEventSchema.EventIdProperty] = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x000325F0 File Offset: 0x000307F0
		// (set) Token: 0x06000FE7 RID: 4071 RVA: 0x00032602 File Offset: 0x00030802
		public string ClientMessageId
		{
			get
			{
				return this[QuarantinedMessageDetailSchema.ClientMessageIdProperty] as string;
			}
			set
			{
				this[QuarantinedMessageDetailSchema.ClientMessageIdProperty] = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x00032610 File Offset: 0x00030810
		// (set) Token: 0x06000FE9 RID: 4073 RVA: 0x00032622 File Offset: 0x00030822
		public DateTime Received
		{
			get
			{
				return (DateTime)this[QuarantinedMessageDetailSchema.ReceivedProperty];
			}
			set
			{
				this[QuarantinedMessageDetailSchema.ReceivedProperty] = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00032635 File Offset: 0x00030835
		// (set) Token: 0x06000FEB RID: 4075 RVA: 0x00032647 File Offset: 0x00030847
		public string MailDirection
		{
			get
			{
				return this[QuarantinedMessageDetailSchema.MailDirectionProperty] as string;
			}
			set
			{
				this[QuarantinedMessageDetailSchema.MailDirectionProperty] = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x00032655 File Offset: 0x00030855
		// (set) Token: 0x06000FED RID: 4077 RVA: 0x00032667 File Offset: 0x00030867
		public int MessageSize
		{
			get
			{
				return (int)this[QuarantinedMessageDetailSchema.MessageSizeProperty];
			}
			set
			{
				this[QuarantinedMessageDetailSchema.MessageSizeProperty] = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x0003267A File Offset: 0x0003087A
		// (set) Token: 0x06000FEF RID: 4079 RVA: 0x0003268C File Offset: 0x0003088C
		public string MessageSubject
		{
			get
			{
				return this[QuarantinedMessageDetailSchema.MessageSubjectProperty] as string;
			}
			set
			{
				this[QuarantinedMessageDetailSchema.MessageSubjectProperty] = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x0003269A File Offset: 0x0003089A
		// (set) Token: 0x06000FF1 RID: 4081 RVA: 0x000326AC File Offset: 0x000308AC
		public string QuarantineType
		{
			get
			{
				return this[QuarantinedMessageDetailSchema.QuarantineTypeProperty] as string;
			}
			set
			{
				this[QuarantinedMessageDetailSchema.QuarantineTypeProperty] = value;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x000326BA File Offset: 0x000308BA
		// (set) Token: 0x06000FF3 RID: 4083 RVA: 0x000326CC File Offset: 0x000308CC
		public DateTime Expires
		{
			get
			{
				return (DateTime)this[QuarantinedMessageDetailSchema.ExpiresProperty];
			}
			set
			{
				this[QuarantinedMessageDetailSchema.ExpiresProperty] = value;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x000326DF File Offset: 0x000308DF
		// (set) Token: 0x06000FF5 RID: 4085 RVA: 0x000326F1 File Offset: 0x000308F1
		public string PartName
		{
			get
			{
				return this[QuarantinedMessageDetailSchema.PartNameProperty] as string;
			}
			set
			{
				this[QuarantinedMessageDetailSchema.PartNameProperty] = value;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x000326FF File Offset: 0x000308FF
		// (set) Token: 0x06000FF7 RID: 4087 RVA: 0x00032711 File Offset: 0x00030911
		public string MimeName
		{
			get
			{
				return this[QuarantinedMessageDetailSchema.MimeNameProperty] as string;
			}
			set
			{
				this[QuarantinedMessageDetailSchema.MimeNameProperty] = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0003271F File Offset: 0x0003091F
		// (set) Token: 0x06000FF9 RID: 4089 RVA: 0x00032731 File Offset: 0x00030931
		public string SenderAddress
		{
			get
			{
				return this[QuarantinedMessageDetailSchema.SenderAddressProperty] as string;
			}
			set
			{
				this[QuarantinedMessageDetailSchema.SenderAddressProperty] = value;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x0003273F File Offset: 0x0003093F
		// (set) Token: 0x06000FFB RID: 4091 RVA: 0x00032751 File Offset: 0x00030951
		public string RecipientAddress
		{
			get
			{
				return this[QuarantinedMessageDetailSchema.RecipientAddressProperty] as string;
			}
			set
			{
				this[QuarantinedMessageDetailSchema.RecipientAddressProperty] = value;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x0003275F File Offset: 0x0003095F
		// (set) Token: 0x06000FFD RID: 4093 RVA: 0x00032771 File Offset: 0x00030971
		public bool Quarantined
		{
			get
			{
				return (bool)this[QuarantinedMessageDetailSchema.QuarantinedProperty];
			}
			set
			{
				this[QuarantinedMessageDetailSchema.QuarantinedProperty] = value;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00032784 File Offset: 0x00030984
		// (set) Token: 0x06000FFF RID: 4095 RVA: 0x00032796 File Offset: 0x00030996
		public bool Notified
		{
			get
			{
				return (bool)this[QuarantinedMessageDetailSchema.NotifiedProperty];
			}
			set
			{
				this[QuarantinedMessageDetailSchema.NotifiedProperty] = value;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x000327A9 File Offset: 0x000309A9
		// (set) Token: 0x06001001 RID: 4097 RVA: 0x000327BB File Offset: 0x000309BB
		public bool Reported
		{
			get
			{
				return (bool)this[QuarantinedMessageDetailSchema.ReportedProperty];
			}
			set
			{
				this[QuarantinedMessageDetailSchema.ReportedProperty] = value;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x000327CE File Offset: 0x000309CE
		// (set) Token: 0x06001003 RID: 4099 RVA: 0x000327E0 File Offset: 0x000309E0
		public bool Released
		{
			get
			{
				return (bool)this[QuarantinedMessageDetailSchema.ReleasedProperty];
			}
			set
			{
				this[QuarantinedMessageDetailSchema.ReleasedProperty] = value;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x000327F3 File Offset: 0x000309F3
		public override ObjectId Identity
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x000327FA File Offset: 0x000309FA
		public override Type GetSchemaType()
		{
			return typeof(QuarantinedMessageDetailSchema);
		}
	}
}
