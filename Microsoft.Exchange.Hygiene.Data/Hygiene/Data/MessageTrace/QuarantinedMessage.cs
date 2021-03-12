using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000187 RID: 391
	[Serializable]
	internal class QuarantinedMessage : ConfigurablePropertyBag
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00032224 File Offset: 0x00030424
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x00032236 File Offset: 0x00030436
		public Guid ExMessageId
		{
			get
			{
				return (Guid)this[QuarantinedMessageSchema.ExMessageIdProperty];
			}
			set
			{
				this[QuarantinedMessageSchema.ExMessageIdProperty] = value;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00032249 File Offset: 0x00030449
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x0003225B File Offset: 0x0003045B
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

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x0003226E File Offset: 0x0003046E
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x00032280 File Offset: 0x00030480
		public string ClientMessageId
		{
			get
			{
				return (string)this[QuarantinedMessageSchema.ClientMessageIdProperty];
			}
			set
			{
				this[QuarantinedMessageSchema.ClientMessageIdProperty] = value;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0003228E File Offset: 0x0003048E
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x000322A0 File Offset: 0x000304A0
		public DateTime Received
		{
			get
			{
				return (DateTime)this[QuarantinedMessageSchema.ReceivedProperty];
			}
			set
			{
				this[QuarantinedMessageSchema.ReceivedProperty] = value;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x000322B3 File Offset: 0x000304B3
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x000322C5 File Offset: 0x000304C5
		public string SenderAddress
		{
			get
			{
				return (string)this[QuarantinedMessageSchema.SenderAddressProperty];
			}
			set
			{
				this[QuarantinedMessageSchema.SenderAddressProperty] = value;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x000322D3 File Offset: 0x000304D3
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x000322E5 File Offset: 0x000304E5
		public string MessageSubject
		{
			get
			{
				return (string)this[QuarantinedMessageSchema.MessageSubjectProperty];
			}
			set
			{
				this[QuarantinedMessageSchema.MessageSubjectProperty] = value;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x000322F3 File Offset: 0x000304F3
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x00032305 File Offset: 0x00030505
		public int MessageSize
		{
			get
			{
				return (int)this[QuarantinedMessageSchema.MessageSizeProperty];
			}
			set
			{
				this[QuarantinedMessageSchema.MessageSizeProperty] = value;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00032318 File Offset: 0x00030518
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x0003232A File Offset: 0x0003052A
		public string MailDirection
		{
			get
			{
				return (string)this[QuarantinedMessageSchema.MailDirectionProperty];
			}
			set
			{
				this[QuarantinedMessageSchema.MailDirectionProperty] = value;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00032338 File Offset: 0x00030538
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x0003234A File Offset: 0x0003054A
		public string QuarantineType
		{
			get
			{
				return (string)this[QuarantinedMessageSchema.QuarantineTypeProperty];
			}
			set
			{
				this[QuarantinedMessageSchema.QuarantineTypeProperty] = value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00032358 File Offset: 0x00030558
		// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x0003236A File Offset: 0x0003056A
		public DateTime Expires
		{
			get
			{
				return (DateTime)this[QuarantinedMessageSchema.ExpiresProperty];
			}
			set
			{
				this[QuarantinedMessageSchema.ExpiresProperty] = value;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0003237D File Offset: 0x0003057D
		// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x00032399 File Offset: 0x00030599
		public bool Notified
		{
			get
			{
				return (bool)(this[QuarantinedMessageSchema.NotifiedProperty] ?? false);
			}
			set
			{
				this[QuarantinedMessageSchema.NotifiedProperty] = value;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x000323AC File Offset: 0x000305AC
		// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x000323C8 File Offset: 0x000305C8
		public bool Quarantined
		{
			get
			{
				return (bool)(this[QuarantinedMessageSchema.QuarantinedProperty] ?? false);
			}
			set
			{
				this[QuarantinedMessageSchema.QuarantinedProperty] = value;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x000323DB File Offset: 0x000305DB
		// (set) Token: 0x06000FD8 RID: 4056 RVA: 0x000323F7 File Offset: 0x000305F7
		public bool Released
		{
			get
			{
				return (bool)(this[QuarantinedMessageSchema.ReleasedProperty] ?? false);
			}
			set
			{
				this[QuarantinedMessageSchema.ReleasedProperty] = value;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0003240A File Offset: 0x0003060A
		// (set) Token: 0x06000FDA RID: 4058 RVA: 0x00032426 File Offset: 0x00030626
		public bool Reported
		{
			get
			{
				return (bool)(this[QuarantinedMessageSchema.ReportedProperty] ?? false);
			}
			set
			{
				this[QuarantinedMessageSchema.ReportedProperty] = value;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00032439 File Offset: 0x00030639
		public override ObjectId Identity
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00032440 File Offset: 0x00030640
		public override Type GetSchemaType()
		{
			return typeof(QuarantinedMessageSchema);
		}
	}
}
