using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x020000B2 RID: 178
	[Parent("user")]
	[Get(typeof(PoliciesResource))]
	[DataContract(Name = "Policies")]
	internal class PoliciesResource : Resource
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x0000A914 File Offset: 0x00008B14
		public PoliciesResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000A91D File Offset: 0x00008B1D
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0000A92A File Offset: 0x00008B2A
		[DataMember(Name = "SendFeedbackUrl", EmitDefaultValue = false)]
		public string SendFeedbackUrl
		{
			get
			{
				return base.GetValue<string>("SendFeedbackUrl");
			}
			set
			{
				base.SetValue<string>("SendFeedbackUrl", value);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000A938 File Offset: 0x00008B38
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0000A945 File Offset: 0x00008B45
		[DataMember(Name = "OnlineFeedbackUrl", EmitDefaultValue = false)]
		public string OnlineFeedbackUrl
		{
			get
			{
				return base.GetValue<string>("OnlineFeedbackUrl");
			}
			set
			{
				base.SetValue<string>("OnlineFeedbackUrl", value);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000A953 File Offset: 0x00008B53
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0000A960 File Offset: 0x00008B60
		[DataMember(Name = "EnableSQM", EmitDefaultValue = false)]
		public bool? EnableSQM
		{
			get
			{
				return base.GetValue<bool?>("EnableSQM");
			}
			set
			{
				base.SetValue<bool?>("EnableSQM", value);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000A973 File Offset: 0x00008B73
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000A980 File Offset: 0x00008B80
		[DataMember(Name = "EnableLogging", EmitDefaultValue = false)]
		public bool? EnableLogging
		{
			get
			{
				return base.GetValue<bool?>("EnableLogging");
			}
			set
			{
				base.SetValue<bool?>("EnableLogging", value);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000A993 File Offset: 0x00008B93
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x0000A9A0 File Offset: 0x00008BA0
		[DataMember(Name = "LoggingLevel", EmitDefaultValue = false)]
		public string LoggingLevel
		{
			get
			{
				return base.GetValue<string>("LoggingLevel");
			}
			set
			{
				base.SetValue<string>("LoggingLevel", value);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000A9AE File Offset: 0x00008BAE
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x0000A9BB File Offset: 0x00008BBB
		[DataMember(Name = "DisableEmoticons", EmitDefaultValue = false)]
		public bool? DisableEmoticons
		{
			get
			{
				return base.GetValue<bool?>("DisableEmoticons");
			}
			set
			{
				base.SetValue<bool?>("DisableEmoticons", value);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000A9CE File Offset: 0x00008BCE
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x0000A9DB File Offset: 0x00008BDB
		[DataMember(Name = "EnableMultiviewJoin", EmitDefaultValue = false)]
		public bool? EnableMultiviewJoin
		{
			get
			{
				return base.GetValue<bool?>("EnableMultiviewJoin");
			}
			set
			{
				base.SetValue<bool?>("EnableMultiviewJoin", value);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000A9EE File Offset: 0x00008BEE
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x0000A9FB File Offset: 0x00008BFB
		[DataMember(Name = "DisableHtmlIM", EmitDefaultValue = false)]
		public bool? DisableHtmlIM
		{
			get
			{
				return base.GetValue<bool?>("DisableHtmlIM");
			}
			set
			{
				base.SetValue<bool?>("DisableHtmlIM", value);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000AA0E File Offset: 0x00008C0E
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x0000AA1B File Offset: 0x00008C1B
		[DataMember(Name = "EnterpriseVoiceEnabled", EmitDefaultValue = false)]
		public bool? EnterpriseVoiceEnabled
		{
			get
			{
				return base.GetValue<bool?>("EnterpriseVoiceEnabled");
			}
			set
			{
				base.SetValue<bool?>("EnterpriseVoiceEnabled", value);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000AA2E File Offset: 0x00008C2E
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0000AA3B File Offset: 0x00008C3B
		[DataMember(Name = "ExchangeUMEnabled", EmitDefaultValue = false)]
		public bool? ExchangeUMEnabled
		{
			get
			{
				return base.GetValue<bool?>("ExchangeUMEnabled");
			}
			set
			{
				base.SetValue<bool?>("ExchangeUMEnabled", value);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000AA4E File Offset: 0x00008C4E
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0000AA5B File Offset: 0x00008C5B
		[DataMember(Name = "VoicemailUri", EmitDefaultValue = false)]
		public string VoicemailUri
		{
			get
			{
				return base.GetValue<string>("VoicemailUri");
			}
			set
			{
				base.SetValue<string>("VoicemailUri", value);
			}
		}

		// Token: 0x040002CD RID: 717
		public const string Token = "policies";
	}
}
