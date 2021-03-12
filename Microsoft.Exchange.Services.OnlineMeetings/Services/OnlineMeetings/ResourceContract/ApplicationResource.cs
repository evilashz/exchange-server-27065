using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200007F RID: 127
	[Link("events", true, Rel = "next")]
	[Link("policies", true)]
	[DataContract(Name = "application")]
	[Parent("applications")]
	[Get(typeof(ApplicationResource))]
	[Link("batching")]
	[Delete]
	internal class ApplicationResource : Resource
	{
		// Token: 0x06000370 RID: 880 RVA: 0x00009EBF File Offset: 0x000080BF
		public ApplicationResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00009EC8 File Offset: 0x000080C8
		// (set) Token: 0x06000372 RID: 882 RVA: 0x00009ED5 File Offset: 0x000080D5
		[DataMember(Name = "userAgent", EmitDefaultValue = false)]
		public string UserAgent
		{
			get
			{
				return base.GetValue<string>("userAgent");
			}
			set
			{
				base.SetValue<string>("userAgent", value);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00009EE3 File Offset: 0x000080E3
		// (set) Token: 0x06000374 RID: 884 RVA: 0x00009EF0 File Offset: 0x000080F0
		[DataMember(Name = "culture", EmitDefaultValue = false)]
		public string Culture
		{
			get
			{
				return base.GetValue<string>("culture");
			}
			set
			{
				base.SetValue<string>("culture", value);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00009EFE File Offset: 0x000080FE
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00009F0B File Offset: 0x0000810B
		[DataMember(Name = "type", EmitDefaultValue = false)]
		public ApplicationType ApplicationType
		{
			get
			{
				return base.GetValue<ApplicationType>("type");
			}
			set
			{
				base.SetValue<ApplicationType>("type", value);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00009F1E File Offset: 0x0000811E
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00009F2B File Offset: 0x0000812B
		[DataMember(Name = "onlineMeetings", EmitDefaultValue = false)]
		public MyMeetingsResource MyMeetings
		{
			get
			{
				return base.GetValue<MyMeetingsResource>("onlineMeetings");
			}
			set
			{
				base.SetValue<MyMeetingsResource>("onlineMeetings", value);
			}
		}

		// Token: 0x04000238 RID: 568
		public const string Token = "application";
	}
}
