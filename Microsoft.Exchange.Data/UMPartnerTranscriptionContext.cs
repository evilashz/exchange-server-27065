using System;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001D6 RID: 470
	internal class UMPartnerTranscriptionContext : UMPartnerContext
	{
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x00031B70 File Offset: 0x0002FD70
		// (set) Token: 0x0600105E RID: 4190 RVA: 0x00031B87 File Offset: 0x0002FD87
		public string CallId
		{
			get
			{
				return (string)base[UMPartnerTranscriptionContext.Schema.CallId];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.CallId] = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x00031B9A File Offset: 0x0002FD9A
		// (set) Token: 0x06001060 RID: 4192 RVA: 0x00031BB1 File Offset: 0x0002FDB1
		public string SessionId
		{
			get
			{
				return (string)base[UMPartnerTranscriptionContext.Schema.SessionId];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.SessionId] = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00031BC4 File Offset: 0x0002FDC4
		// (set) Token: 0x06001062 RID: 4194 RVA: 0x00031BDB File Offset: 0x0002FDDB
		public ExDateTime CreationTime
		{
			get
			{
				return (ExDateTime)base[UMPartnerTranscriptionContext.Schema.CreationTime];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.CreationTime] = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x00031BF3 File Offset: 0x0002FDF3
		// (set) Token: 0x06001064 RID: 4196 RVA: 0x00031C0A File Offset: 0x0002FE0A
		public SmtpAddress PartnerAddress
		{
			get
			{
				return (SmtpAddress)base[UMPartnerTranscriptionContext.Schema.PartnerAddress];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.PartnerAddress] = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00031C22 File Offset: 0x0002FE22
		// (set) Token: 0x06001066 RID: 4198 RVA: 0x00031C39 File Offset: 0x0002FE39
		public string PartnerTranscriptionAttachmentName
		{
			get
			{
				return (string)base[UMPartnerTranscriptionContext.Schema.PartnerTranscriptionAttachmentName];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.PartnerTranscriptionAttachmentName] = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00031C4C File Offset: 0x0002FE4C
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x00031C63 File Offset: 0x0002FE63
		public string PartnerAudioAttachmentName
		{
			get
			{
				return (string)base[UMPartnerTranscriptionContext.Schema.PartnerAudioAttachmentName];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.PartnerAudioAttachmentName] = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00031C76 File Offset: 0x0002FE76
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x00031C8D File Offset: 0x0002FE8D
		public string PcmAudioAttachmentName
		{
			get
			{
				return (string)base[UMPartnerTranscriptionContext.Schema.PcmAudioAttachmentName];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.PcmAudioAttachmentName] = value;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00031CA0 File Offset: 0x0002FEA0
		// (set) Token: 0x0600106C RID: 4204 RVA: 0x00031CB7 File Offset: 0x0002FEB7
		public string IpmAttachmentName
		{
			get
			{
				return (string)base[UMPartnerTranscriptionContext.Schema.IpmAttachmentName];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.IpmAttachmentName] = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x00031CCA File Offset: 0x0002FECA
		// (set) Token: 0x0600106E RID: 4206 RVA: 0x00031CE1 File Offset: 0x0002FEE1
		public int PartnerMaxDeliveryDelay
		{
			get
			{
				return (int)base[UMPartnerTranscriptionContext.Schema.PartnerMaxDeliveryDelay];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.PartnerMaxDeliveryDelay] = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x00031CF9 File Offset: 0x0002FEF9
		// (set) Token: 0x06001070 RID: 4208 RVA: 0x00031D10 File Offset: 0x0002FF10
		public int Duration
		{
			get
			{
				return (int)base[UMPartnerTranscriptionContext.Schema.Duration];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.Duration] = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x00031D28 File Offset: 0x0002FF28
		// (set) Token: 0x06001072 RID: 4210 RVA: 0x00031D3F File Offset: 0x0002FF3F
		public string AudioCodec
		{
			get
			{
				return (string)base[UMPartnerTranscriptionContext.Schema.AudioCodec];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.AudioCodec] = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x00031D52 File Offset: 0x0002FF52
		// (set) Token: 0x06001074 RID: 4212 RVA: 0x00031D69 File Offset: 0x0002FF69
		public string CallingParty
		{
			get
			{
				return (string)base[UMPartnerTranscriptionContext.Schema.CallingParty];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.CallingParty] = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x00031D7C File Offset: 0x0002FF7C
		// (set) Token: 0x06001076 RID: 4214 RVA: 0x00031D93 File Offset: 0x0002FF93
		public CultureInfo Culture
		{
			get
			{
				return (CultureInfo)base[UMPartnerTranscriptionContext.Schema.Culture];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.Culture] = value;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00031DA6 File Offset: 0x0002FFA6
		// (set) Token: 0x06001078 RID: 4216 RVA: 0x00031DBD File Offset: 0x0002FFBD
		public string Subject
		{
			get
			{
				return (string)base[UMPartnerTranscriptionContext.Schema.Subject];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.Subject] = value;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x00031DD0 File Offset: 0x0002FFD0
		// (set) Token: 0x0600107A RID: 4218 RVA: 0x00031DE7 File Offset: 0x0002FFE7
		public bool IsImportant
		{
			get
			{
				return (bool)base[UMPartnerTranscriptionContext.Schema.IsImportant];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.IsImportant] = value;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x00031DFF File Offset: 0x0002FFFF
		// (set) Token: 0x0600107C RID: 4220 RVA: 0x00031E16 File Offset: 0x00030016
		public bool IsCallAnsweringMessage
		{
			get
			{
				return (bool)base[UMPartnerTranscriptionContext.Schema.IsCallAnsweringMessage];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.IsCallAnsweringMessage] = value;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00031E2E File Offset: 0x0003002E
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x00031E45 File Offset: 0x00030045
		public Guid CallerGuid
		{
			get
			{
				return (Guid)base[UMPartnerTranscriptionContext.Schema.CallerGuid];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.CallerGuid] = value;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00031E5D File Offset: 0x0003005D
		// (set) Token: 0x06001080 RID: 4224 RVA: 0x00031E74 File Offset: 0x00030074
		public string CallerName
		{
			get
			{
				return (string)base[UMPartnerTranscriptionContext.Schema.CallerName];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.CallerName] = value;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00031E87 File Offset: 0x00030087
		// (set) Token: 0x06001082 RID: 4226 RVA: 0x00031E9E File Offset: 0x0003009E
		public string CallerIdDisplayName
		{
			get
			{
				return (string)base[UMPartnerTranscriptionContext.Schema.CallerIdDisplayName];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.CallerIdDisplayName] = value;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x00031EB1 File Offset: 0x000300B1
		// (set) Token: 0x06001084 RID: 4228 RVA: 0x00031EC8 File Offset: 0x000300C8
		public string UMDialPlanLanguage
		{
			get
			{
				return (string)base[UMPartnerTranscriptionContext.Schema.UMDialPlanLanguage];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.UMDialPlanLanguage] = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00031EDB File Offset: 0x000300DB
		// (set) Token: 0x06001086 RID: 4230 RVA: 0x00031EF2 File Offset: 0x000300F2
		public string CallerInformedOfAnalysis
		{
			get
			{
				return (string)base[UMPartnerTranscriptionContext.Schema.CallerInformedOfAnalysis];
			}
			set
			{
				base[UMPartnerTranscriptionContext.Schema.CallerInformedOfAnalysis] = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x00031F05 File Offset: 0x00030105
		protected override UMPartnerContext.UMPartnerContextSchema ContextSchema
		{
			get
			{
				return UMPartnerTranscriptionContext.Schema;
			}
		}

		// Token: 0x040009BC RID: 2492
		private static readonly UMPartnerTranscriptionContext.UMPartnerTranscriptionContextSchema Schema = new UMPartnerTranscriptionContext.UMPartnerTranscriptionContextSchema();

		// Token: 0x020001D7 RID: 471
		private class UMPartnerTranscriptionContextSchema : UMPartnerContext.UMPartnerContextSchema
		{
			// Token: 0x040009BD RID: 2493
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition CallId = new UMPartnerContext.UMPartnerContextPropertyDefinition("CallId", typeof(string), string.Empty);

			// Token: 0x040009BE RID: 2494
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition SessionId = new UMPartnerContext.UMPartnerContextPropertyDefinition("SessionId", typeof(string), string.Empty);

			// Token: 0x040009BF RID: 2495
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition CreationTime = new UMPartnerContext.UMPartnerContextPropertyDefinition("CreationTime", typeof(ExDateTime), ExDateTime.MinValue);

			// Token: 0x040009C0 RID: 2496
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition PartnerAddress = new UMPartnerContext.UMPartnerContextPropertyDefinition("PartnerAddress", typeof(SmtpAddress), SmtpAddress.Empty);

			// Token: 0x040009C1 RID: 2497
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition PartnerMaxDeliveryDelay = new UMPartnerContext.UMPartnerContextPropertyDefinition("PartnerMaxDeliveryDelay", typeof(int), 0);

			// Token: 0x040009C2 RID: 2498
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition PartnerTranscriptionAttachmentName = new UMPartnerContext.UMPartnerContextPropertyDefinition("PartnerTranscriptionAttachmentName", typeof(string), string.Empty);

			// Token: 0x040009C3 RID: 2499
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition PartnerAudioAttachmentName = new UMPartnerContext.UMPartnerContextPropertyDefinition("PartnerAudioAttachmentName", typeof(string), string.Empty);

			// Token: 0x040009C4 RID: 2500
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition PcmAudioAttachmentName = new UMPartnerContext.UMPartnerContextPropertyDefinition("PcmAudioAttachmentName", typeof(string), string.Empty);

			// Token: 0x040009C5 RID: 2501
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition IpmAttachmentName = new UMPartnerContext.UMPartnerContextPropertyDefinition("IpmAttachmentName", typeof(string), string.Empty);

			// Token: 0x040009C6 RID: 2502
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition Duration = new UMPartnerContext.UMPartnerContextPropertyDefinition("Duration", typeof(int), 0);

			// Token: 0x040009C7 RID: 2503
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition AudioCodec = new UMPartnerContext.UMPartnerContextPropertyDefinition("AudioCodec", typeof(string), string.Empty);

			// Token: 0x040009C8 RID: 2504
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition CallingParty = new UMPartnerContext.UMPartnerContextPropertyDefinition("CallingParty", typeof(string), string.Empty);

			// Token: 0x040009C9 RID: 2505
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition Culture = new UMPartnerContext.UMPartnerContextPropertyDefinition("Culture", typeof(CultureInfo), CultureInfo.InvariantCulture);

			// Token: 0x040009CA RID: 2506
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition Subject = new UMPartnerContext.UMPartnerContextPropertyDefinition("Subject", typeof(string), string.Empty);

			// Token: 0x040009CB RID: 2507
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition IsImportant = new UMPartnerContext.UMPartnerContextPropertyDefinition("IsImportant", typeof(bool), false);

			// Token: 0x040009CC RID: 2508
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition IsCallAnsweringMessage = new UMPartnerContext.UMPartnerContextPropertyDefinition("IsCallAnsweringMessage", typeof(bool), false);

			// Token: 0x040009CD RID: 2509
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition CallerGuid = new UMPartnerContext.UMPartnerContextPropertyDefinition("CallerGuid", typeof(Guid), Guid.Empty);

			// Token: 0x040009CE RID: 2510
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition CallerName = new UMPartnerContext.UMPartnerContextPropertyDefinition("CallerName", typeof(string), string.Empty);

			// Token: 0x040009CF RID: 2511
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition CallerIdDisplayName = new UMPartnerContext.UMPartnerContextPropertyDefinition("CallerIdDisplayName", typeof(string), string.Empty);

			// Token: 0x040009D0 RID: 2512
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition UMDialPlanLanguage = new UMPartnerContext.UMPartnerContextPropertyDefinition("UMDialPlanLanguage", typeof(string), string.Empty);

			// Token: 0x040009D1 RID: 2513
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition CallerInformedOfAnalysis = new UMPartnerContext.UMPartnerContextPropertyDefinition("CallerInformedOfAnalysis", typeof(string), string.Empty);
		}
	}
}
