using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000081 RID: 129
	[DataContract(Name = "applicationSettings")]
	internal class ApplicationSettings : Resource
	{
		// Token: 0x06000379 RID: 889 RVA: 0x00009F39 File Offset: 0x00008139
		public ApplicationSettings(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00009F42 File Offset: 0x00008142
		// (set) Token: 0x0600037B RID: 891 RVA: 0x00009F4F File Offset: 0x0000814F
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

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00009F5D File Offset: 0x0000815D
		// (set) Token: 0x0600037D RID: 893 RVA: 0x00009F6A File Offset: 0x0000816A
		[DataMember(Name = "endpointId", EmitDefaultValue = false)]
		public string EndpointId
		{
			get
			{
				return base.GetValue<string>("endpointId");
			}
			set
			{
				base.SetValue<string>("endpointId", value);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00009F78 File Offset: 0x00008178
		// (set) Token: 0x0600037F RID: 895 RVA: 0x00009F85 File Offset: 0x00008185
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

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00009F93 File Offset: 0x00008193
		// (set) Token: 0x06000381 RID: 897 RVA: 0x00009FA0 File Offset: 0x000081A0
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

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00009FB3 File Offset: 0x000081B3
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00009FBB File Offset: 0x000081BB
		public bool IsInternalApplication { get; set; }
	}
}
