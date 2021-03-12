using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000010 RID: 16
	[DataContract]
	internal class OlcMessageProperties : ItemPropertiesBase
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00002F37 File Offset: 0x00001137
		private static ExDateTime ConvertToExDateTime(DateTime dateTime)
		{
			return new ExDateTime(ExTimeZone.UtcTimeZone, dateTime);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002F44 File Offset: 0x00001144
		public override void Apply(MailboxSession session, Item item)
		{
			PersistablePropertyBag propertyBag = item.PropertyBag;
			propertyBag[MessageItemSchema.IsRead] = this.IsRead;
			propertyBag[MessageItemSchema.WasEverRead] = this.IsRead;
			propertyBag[MessageItemSchema.IsDraft] = this.IsDraft;
			propertyBag[ItemSchema.ReceivedTime] = OlcMessageProperties.ConvertToExDateTime(this.ReceivedDate);
			propertyBag[StoreObjectSchema.LastModifiedTime] = OlcMessageProperties.ConvertToExDateTime(this.LastModifiedDate);
			IconIndex iconIndex = IconIndex.Default;
			OlcMessageType messageType = this.MessageType;
			switch (messageType)
			{
			case OlcMessageType.CALENDAR_REQUEST:
				iconIndex = IconIndex.AppointmentMeet;
				break;
			case OlcMessageType.CALENDAR_CANCEL:
				iconIndex = IconIndex.AppointmentMeetCancel;
				break;
			case OlcMessageType.CALENDAR_ACCEPTED:
				iconIndex = IconIndex.AppointmentMeetYes;
				break;
			case OlcMessageType.CALENDAR_TENTATIVE:
				iconIndex = IconIndex.AppointmentMeetMaybe;
				break;
			case OlcMessageType.CALENDAR_DECLINED:
				iconIndex = IconIndex.AppointmentMeetNo;
				break;
			default:
				if (messageType == OlcMessageType.SMS || messageType == OlcMessageType.MMS)
				{
					iconIndex = IconIndex.SmsDelivered;
				}
				break;
			}
			if (this.IsReplied && iconIndex == IconIndex.Default)
			{
				iconIndex = IconIndex.MailReplied;
			}
			if (this.IsForwarded && iconIndex == IconIndex.Default)
			{
				iconIndex = IconIndex.MailForwarded;
			}
			propertyBag[ItemSchema.IconIndex] = iconIndex;
			propertyBag[MessageItemSchema.MessageAnswered] = (this.IsReplied || this.IsForwarded);
			propertyBag[ItemSchema.Importance] = this.Importance;
			propertyBag[ItemSchema.Sensitivity] = this.Sensitivity;
			if (this.MapiFlagStatus == FlagStatus.Flagged)
			{
				propertyBag[ItemSchema.FlagStatus] = 2;
				propertyBag[ItemSchema.IsToDoItem] = true;
				propertyBag[ItemSchema.ItemColor] = 6;
				propertyBag[ItemSchema.PercentComplete] = 0.0;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000311A File Offset: 0x0000131A
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00003122 File Offset: 0x00001322
		[DataMember]
		public bool IsRead { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000312B File Offset: 0x0000132B
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00003133 File Offset: 0x00001333
		[DataMember]
		public bool IsSent { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000313C File Offset: 0x0000133C
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00003144 File Offset: 0x00001344
		[DataMember]
		public bool IsDraft { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000314D File Offset: 0x0000134D
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00003155 File Offset: 0x00001355
		[DataMember]
		public int OlcMessageTypeInt { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000315E File Offset: 0x0000135E
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00003166 File Offset: 0x00001366
		public OlcMessageType MessageType
		{
			get
			{
				return (OlcMessageType)this.OlcMessageTypeInt;
			}
			set
			{
				this.OlcMessageTypeInt = (int)value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000316F File Offset: 0x0000136F
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00003177 File Offset: 0x00001377
		[DataMember]
		public DateTime ReceivedDate { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003180 File Offset: 0x00001380
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00003188 File Offset: 0x00001388
		[DataMember]
		public DateTime LastModifiedDate { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00003191 File Offset: 0x00001391
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00003199 File Offset: 0x00001399
		[DataMember]
		public bool IsReplied { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000031A2 File Offset: 0x000013A2
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000031AA File Offset: 0x000013AA
		[DataMember]
		public bool IsForwarded { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000031B3 File Offset: 0x000013B3
		// (set) Token: 0x06000111 RID: 273 RVA: 0x000031BB File Offset: 0x000013BB
		[DataMember]
		public bool HasAttachments { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000031C4 File Offset: 0x000013C4
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000031CC File Offset: 0x000013CC
		[DataMember]
		public int? ImapUid { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000031D5 File Offset: 0x000013D5
		// (set) Token: 0x06000115 RID: 277 RVA: 0x000031DD File Offset: 0x000013DD
		[DataMember]
		public string LegacyPopId { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000031E6 File Offset: 0x000013E6
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000031EE File Offset: 0x000013EE
		[DataMember]
		public int MessageSize { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000031F7 File Offset: 0x000013F7
		// (set) Token: 0x06000119 RID: 281 RVA: 0x000031FF File Offset: 0x000013FF
		[DataMember]
		public bool IsConfirmedAsJunk { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00003208 File Offset: 0x00001408
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00003210 File Offset: 0x00001410
		[DataMember]
		public int SensitivityInt { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00003219 File Offset: 0x00001419
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00003221 File Offset: 0x00001421
		public Sensitivity Sensitivity
		{
			get
			{
				return (Sensitivity)this.SensitivityInt;
			}
			set
			{
				this.SensitivityInt = (int)value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000322A File Offset: 0x0000142A
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00003232 File Offset: 0x00001432
		[DataMember]
		public int ImportanceInt { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000323B File Offset: 0x0000143B
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00003243 File Offset: 0x00001443
		public Importance Importance
		{
			get
			{
				return (Importance)this.ImportanceInt;
			}
			set
			{
				this.ImportanceInt = (int)value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000324C File Offset: 0x0000144C
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00003254 File Offset: 0x00001454
		[DataMember]
		public int SenderIdStatusInt { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000325D File Offset: 0x0000145D
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00003265 File Offset: 0x00001465
		public SenderIdStatus SenderIdStatus
		{
			get
			{
				return (SenderIdStatus)this.SenderIdStatusInt;
			}
			set
			{
				this.SenderIdStatusInt = (int)value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000326E File Offset: 0x0000146E
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00003276 File Offset: 0x00001476
		[DataMember]
		public int? ItemColorInt { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00003280 File Offset: 0x00001480
		// (set) Token: 0x06000129 RID: 297 RVA: 0x000032B4 File Offset: 0x000014B4
		public ItemColor? ItemColor
		{
			get
			{
				int? itemColorInt = this.ItemColorInt;
				if (itemColorInt == null)
				{
					return null;
				}
				return new ItemColor?((ItemColor)itemColorInt.GetValueOrDefault());
			}
			set
			{
				ItemColor? itemColor = value;
				this.ItemColorInt = ((itemColor != null) ? new int?((int)itemColor.GetValueOrDefault()) : null);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000032E9 File Offset: 0x000014E9
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000032F1 File Offset: 0x000014F1
		[DataMember]
		public int? MapiFlagStatusInt { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000032FC File Offset: 0x000014FC
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00003330 File Offset: 0x00001530
		public FlagStatus? MapiFlagStatus
		{
			get
			{
				int? mapiFlagStatusInt = this.MapiFlagStatusInt;
				if (mapiFlagStatusInt == null)
				{
					return null;
				}
				return new FlagStatus?((FlagStatus)mapiFlagStatusInt.GetValueOrDefault());
			}
			set
			{
				FlagStatus? flagStatus = value;
				this.MapiFlagStatusInt = ((flagStatus != null) ? new int?((int)flagStatus.GetValueOrDefault()) : null);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00003365 File Offset: 0x00001565
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000336D File Offset: 0x0000156D
		[DataMember]
		public DateTime? FlagDueDate { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00003376 File Offset: 0x00001576
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0000337E File Offset: 0x0000157E
		[DataMember]
		public string FlagSubject { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00003387 File Offset: 0x00001587
		// (set) Token: 0x06000133 RID: 307 RVA: 0x0000338F File Offset: 0x0000158F
		[DataMember]
		public CategorySettings[] CategoryList { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00003398 File Offset: 0x00001598
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000033A0 File Offset: 0x000015A0
		[DataMember]
		public int OriginalFolderReasonInt { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000033A9 File Offset: 0x000015A9
		// (set) Token: 0x06000137 RID: 311 RVA: 0x000033B1 File Offset: 0x000015B1
		[DataMember]
		public int SenderClassInt { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000033BA File Offset: 0x000015BA
		// (set) Token: 0x06000139 RID: 313 RVA: 0x000033C2 File Offset: 0x000015C2
		[DataMember]
		public int AuthenticationTypeInt { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000033CB File Offset: 0x000015CB
		// (set) Token: 0x0600013B RID: 315 RVA: 0x000033D3 File Offset: 0x000015D3
		[DataMember]
		public int TrustedSourceInt { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000033DC File Offset: 0x000015DC
		// (set) Token: 0x0600013D RID: 317 RVA: 0x000033E4 File Offset: 0x000015E4
		[DataMember]
		public int SenderAuthResultInt { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000033ED File Offset: 0x000015ED
		// (set) Token: 0x0600013F RID: 319 RVA: 0x000033F5 File Offset: 0x000015F5
		[DataMember]
		public int PhishingInt { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000033FE File Offset: 0x000015FE
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00003406 File Offset: 0x00001606
		[DataMember]
		public int WarningInfoInt { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000340F File Offset: 0x0000160F
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00003417 File Offset: 0x00001617
		[DataMember]
		public bool SenderIsSafe { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00003420 File Offset: 0x00001620
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00003428 File Offset: 0x00001628
		[DataMember]
		public bool IsPraEmailPresent { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00003431 File Offset: 0x00001631
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00003439 File Offset: 0x00001639
		[DataMember]
		public string PraEmail { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00003442 File Offset: 0x00001642
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000344A File Offset: 0x0000164A
		[DataMember]
		public bool Unsubscribe { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00003453 File Offset: 0x00001653
		// (set) Token: 0x0600014B RID: 331 RVA: 0x0000345B File Offset: 0x0000165B
		[DataMember]
		public short ActionsTakenInt { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00003464 File Offset: 0x00001664
		// (set) Token: 0x0600014D RID: 333 RVA: 0x0000346C File Offset: 0x0000166C
		[DataMember]
		public OlcLegacyMessageProperties LegacyProperties { get; set; }
	}
}
